using Media.Entities;
using Media.Web.Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using Media.Data.Infrastructure;
using Media.Data.Repositories;

namespace Media.Web.Controllers
{
    [RoutePrefix("api/directions")]
    public class DirectionsController : ApiControllerBase
    {
        public DirectionsController(IEntityBaseRepository<Error> errorsRepository, IUnitOfWork unitOfWork)
            : base(errorsRepository, unitOfWork) { }


        [AllowAnonymous]
        [Route("all")]
        public HttpResponseMessage Get(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                List<Direction> _runDirections = Enum.GetValues(typeof(Direction)).Cast<Direction>().ToList();

                response = request.CreateResponse<List<Direction>>(HttpStatusCode.OK, _runDirections);

                return response;
            });
        }
    }
}