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
        public Task<HttpResponseMessage> Get()
        {
            HttpResponseMessage response;

            try
            {
                var itens = _servico.Listar().OrderByDescending(x => x.DataPublicacao).ToList();

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
        [Route("api/noticia/hoje")]
        [Authorize]
        public Task<HttpResponseMessage> GetOfToday()
        {
            HttpResponseMessage response;

            try
            {
                var itens = _servico.Listar(DateTime.Now).OrderByDescending(x=> x.DataPublicacao).ToList();

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
