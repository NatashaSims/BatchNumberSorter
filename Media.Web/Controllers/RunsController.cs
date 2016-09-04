using AutoMapper;
using Media.Data.Infrastructure;
using Media.Data.Repositories;
using Media.Entities;
using Media.Web.Infrastructure.Core;
using Media.Web.Infrastructure.Extensions;
using Media.Web.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace Media.Web.Controllers
{
    [RoutePrefix("api/runs")]
    public class RunsController : ApiControllerBase
    {
        private readonly IEntityBaseRepository<Run> _runsRepository;

        public RunsController(IEntityBaseRepository<Run> runsRepository, IEntityBaseRepository<Error> _errorsRepository, IUnitOfWork _unitOfWork)
            : base(_errorsRepository, _unitOfWork)
        {
            _runsRepository = runsRepository;
        }

        [AllowAnonymous]
        public HttpResponseMessage Get(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null; var runs = _runsRepository.GetAll().OrderByDescending(m => m.TimeTaken).Take(6).ToList();
                IEnumerable<RunViewModel> runsVM = Mapper.Map<IEnumerable<Run>, IEnumerable<RunViewModel>>(runs);
                response = request.CreateResponse<IEnumerable<RunViewModel>>(HttpStatusCode.OK, runsVM);
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

                List<RunViewModel> _runHistory = GetBatchRunHistory(id);

                response = request.CreateResponse<List<RunViewModel>>(HttpStatusCode.OK, _runHistory);

                return response;
            });
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("createrun")]
        public HttpResponseMessage CreateRun(HttpRequestMessage request, RunViewModel run)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                run = CreateSequenceRun(run);

                Run newRun = new Run();
                newRun.UpdateRun(run);

                _runsRepository.Add(newRun);

                _unitOfWork.Commit();

                run = Mapper.Map<Run, RunViewModel>(newRun);
                response = request.CreateResponse<RunViewModel>(HttpStatusCode.Created, run);

                return response;
            });
        }

        private RunViewModel CreateSequenceRun(RunViewModel run)
        {
            int[] numbers = Array.ConvertAll(run.Batch.Split(','), element => int.Parse(element));

            Stopwatch watch = new Stopwatch();
            watch.Start();
            Quicksort(numbers, 0, numbers.Length - 1);
            watch.Stop();

            run.Sequence = (run.Direction == 0) ? string.Join(",", numbers.Select(x => x.ToString()).ToArray()) : string.Join(",", numbers.Reverse().Select(x => x.ToString()).ToArray());
            run.TimeTaken = Convert.ToDecimal(watch.Elapsed.Ticks);

            return run;
        }

        public void Quicksort(int[] arr, int begin, int end)
        {
            int pivot = arr[(begin + (end - begin) / 2)];
            int left = begin;
            int right = end;

            while (left <= right)
            {
                while (arr[left] < pivot)
                {
                    left++;
                }
                while (arr[right] > pivot)
                {
                    right--;
                }
                if (left <= right)
                {
                    Swap(arr, left, right);
                    left++;
                    right--;
                }
            }

            if (begin < right)
            {
                Quicksort(arr, begin, left - 1);
            }

            if (end > left)
            {
                Quicksort(arr, right + 1, end);
            }
        }

        public void Swap(int[] items, int x, int y)
        {
            int temp = items[x];
            items[x] = items[y];
            items[y] = temp;
        }

        private List<RunViewModel> GetBatchRunHistory(int batchId)
        {
            List<RunViewModel> _runHistory = new List<RunViewModel>();
            List<Run> runs = new List<Run>();

            runs = _runsRepository.GetAll().OrderBy(m => m.TimeTaken).Where(m => m.BatchId.Equals(batchId)).ToList();
            IEnumerable<RunViewModel> runsVM = Mapper.Map<IEnumerable<Run>, IEnumerable<RunViewModel>>(runs);
            _runHistory = runsVM.ToList();

            return _runHistory;
        }

    }
}