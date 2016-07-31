using AutoMapper;
using Newsbook.Core.Interface.Servico;
using Newsbook.Core.Modelo;
using Newsbook.Core.WebApi.ResourceModel.FeedUrl;
using Newsbook.Core.WebApi.Validator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Newsbook.Core.WebApi.Controllers
{
    public class FeedUrlController : ApiController
    {

        private readonly IFeedUrlServico _servico;

        public FeedUrlController(IFeedUrlServico servico)
        {
            _servico = servico;
        }

        [HttpGet]
        [Route("api/feedurl")]
        [Authorize]
        public Task<HttpResponseMessage> Get()
        {
            HttpResponseMessage response;

            try
            {
                var itens = _servico.ListarAtivos();

                var itensResourceModel = Mapper.Map<List<FeedUrl>, IEnumerable<GetFeedUrl>>(itens);
                response = Request.CreateResponse(HttpStatusCode.OK, new
                {
                    d = itensResourceModel
                });
            }
            catch (Exception ex)
            {
                response = Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }

            var tsc = new TaskCompletionSource<HttpResponseMessage>();
            tsc.SetResult(response);
            return tsc.Task;
        }


        // GET: api/FeedUrl/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/FeedUrl
        [HttpPost]
        [ValidateModelStateFilter]
        [Route("api/feedurl")]
        public string Post(PostFeedUrl obj)
        {
            return "ok";
        }

        // PUT: api/FeedUrl/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/FeedUrl/5
        public void Delete(int id)
        {
        }
    }
}
