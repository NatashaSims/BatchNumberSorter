using AutoMapper;
using Media.Data.Infrastructure;
using Media.Data.Repositories;
using Media.Entities;
using Media.Web.Infrastructure.Core;
using Media.Web.Infrastructure.Extensions;
using Media.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace Media.Web.Controllers
{
    [RoutePrefix("api/batches")]
    public class BatchesController : ApiControllerBase
    {
        private readonly IEntityBaseRepository<Batch> _batchesRepository;
        public BatchesController(IEntityBaseRepository<Batch> batchesRepository, IEntityBaseRepository<Error> _errorsRepository, IUnitOfWork _unitOfWork)
            : base(_errorsRepository, _unitOfWork)
        {
            _batchesRepository = batchesRepository;
        }

        [AllowAnonymous]
        public HttpResponseMessage Get(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var batches = _batchesRepository.GetAll().Take(6).Where(batch => batch.Runs.Count > 0).ToList();
                IEnumerable<BatchViewModel> batchesVM = Mapper.Map<IEnumerable<Batch>, IEnumerable<BatchViewModel>>(batches);
                response = request.CreateResponse<IEnumerable<BatchViewModel>>(HttpStatusCode.OK, batchesVM);
                return response;
            });
        }


        [AllowAnonymous]
        [Route("{page:int=0}/{pageSize=3}/{filter?}")]
        public HttpResponseMessage Get(HttpRequestMessage request, int? page, int? pageSize, string filter = null)
        {
            int currentPage = page.Value; int currentPageSize = pageSize.Value;
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<Batch> batches = null;
                int totalBatches = new int();

                if (!string.IsNullOrEmpty(filter))
                {
                    batches = _batchesRepository.GetAll().OrderBy(m => m.ID).Where(m => m.BatchNumbers.ToLower().Contains(filter.ToLower().Trim())).ToList();
                }
                else
                {
                    batches = _batchesRepository.GetAll().ToList();
                }

                totalBatches = batches.Count(); batches = batches.Skip(currentPage * currentPageSize).Take(currentPageSize).ToList();
                IEnumerable<BatchViewModel> batchesVM = Mapper.Map<IEnumerable<Batch>, IEnumerable<BatchViewModel>>(batches);
                PaginationSet<BatchViewModel> pagedSet = new PaginationSet<BatchViewModel>()
                {
                    Page = currentPage,
                    TotalCount = totalBatches,
                    TotalPages = (int)Math.Ceiling((decimal)totalBatches / currentPageSize),
                    Items = batchesVM
                };
                response = request.CreateResponse<PaginationSet<BatchViewModel>>(HttpStatusCode.OK, pagedSet);
                return response;
            });
        }


        [AllowAnonymous]
        [Route("details/{id:int}")]
        public HttpResponseMessage Get(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var batch = _batchesRepository.GetSingle(id);
                BatchViewModel batchVM = Mapper.Map<Batch, BatchViewModel>(batch);
                response = request.CreateResponse<BatchViewModel>(HttpStatusCode.OK, batchVM);
                return response;
            });
        }


        [AllowAnonymous]
        [HttpPost]
        [Route("add")]
        public HttpResponseMessage Add(HttpRequestMessage request, BatchViewModel batch)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                Batch newBatch = new Batch();
                newBatch.UpdateBatch(batch);

                _batchesRepository.Add(newBatch);

                _unitOfWork.Commit();

                batch = Mapper.Map<Batch, BatchViewModel>(newBatch);
                response = request.CreateResponse<BatchViewModel>(HttpStatusCode.Created, batch);


                return response;
            });
        }
    }
}