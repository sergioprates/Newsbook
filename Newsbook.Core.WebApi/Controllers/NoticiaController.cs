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
    public class NoticiaController : ApiController
    {
        private readonly INoticiaServico _servico;

        public NoticiaController(INoticiaServico servico)
        {
            _servico = servico;
        }

        [HttpGet]
        [Route("api/noticia")]
        [Authorize]
        public Task<HttpResponseMessage> Get(int? limit = null, int? skip = null)
        {
            HttpResponseMessage response;

            try
            {
                List<Noticia> itens = null;

                if (limit != null && skip != null)
                {
                    itens = _servico.Listar((int)limit, (int)skip).OrderByDescending(x => x.DataPublicacao).ToList();
                }
                else if (limit == null && skip != null)
                {
                    throw new InvalidOperationException("Não é possível acessar utilizando apenas o parametro skip.");
                }
                else
                {
                    itens = _servico.Listar().OrderByDescending(x => x.DataPublicacao).ToList();
                }

                var itensResourceModel = Mapper.Map<List<Noticia>, List<GetNoticia>>(itens);
                response = Request.CreateResponse(HttpStatusCode.OK, new
                {
                    d = itensResourceModel
                });
            }
            catch (InvalidOperationException ex)
            {
                response = Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
            catch (Exception ex)
            {
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

            var tsc = new TaskCompletionSource<HttpResponseMessage>();
            tsc.SetResult(response);
            return tsc.Task;
        }

        [HttpGet]
        [Route("api/noticia/{feedUrlId}")]
        [Authorize]
        public Task<HttpResponseMessage> GetByFeedUrl(string feedUrlId)
        {
            HttpResponseMessage response;

            try
            {
                var itens = _servico.Listar(new FeedUrl() { _id = feedUrlId }).OrderByDescending(x => x.DataPublicacao).ToList();

                var itensResourceModel = Mapper.Map<List<Noticia>, List<GetNoticia>>(itens);
                response = Request.CreateResponse(HttpStatusCode.OK, new
                {
                    d = itensResourceModel
                });
            }
            catch (Exception ex)
            {
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

            var tsc = new TaskCompletionSource<HttpResponseMessage>();
            tsc.SetResult(response);
            return tsc.Task;
        }
    }
}
