using AutoMapper;
using Newsbook.Core.Interface.Servico;
using Newsbook.Core.Modelo;
using Newsbook.Core.WebApi.ResourceModel.Noticia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Newsbook.Core.WebApi.Controllers
{
    public class NoticiaDoFeedUrlController : ApiController
    {
        private readonly INoticiaDoFeedUrlServico _servico;

        public NoticiaDoFeedUrlController(INoticiaDoFeedUrlServico servico)
        {
            _servico = servico;
        }

        // GET: api/NoticiaDoFeedUrl
        [HttpGet]
        [Authorize]
        [Route("api/noticiadofeedurl/{id}")]
        public Task<HttpResponseMessage> Get(long id)
        {
            HttpResponseMessage response;

            try
            {  
                FeedUrl feed = new FeedUrl(){Id = id};
                var itens = _servico.Listar(feed).Select(x => x.Noticia).OrderByDescending(x => x.DataPublicacao).ToList();

                var itensResourceModel = Mapper.Map<List<Noticia>, List<GetNoticia>>(itens);
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

        [HttpGet]
        [Route("api/noticiadofeedurl/hoje/{id}")]
        [Authorize]
        public Task<HttpResponseMessage> GetOfToday(long id)
        {
            HttpResponseMessage response;

            try
            {
                var itens = _servico.Listar(DateTime.Now).OrderByDescending(x => x.Noticia.DataPublicacao).Select(x=> x.Noticia).ToList();

                var itensResourceModel = Mapper.Map<List<Noticia>, List<GetNoticia>>(itens);
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
    }
}
