using AutoMapper;
using Newsbook.Core.Interface.Servico;
using Newsbook.Core.Modelo;
using Newsbook.Core.WebApi.ResourceModel.FeedUrl;
using Newsbook.Core.WebApi.Validator;
using Newsbook.FeedParserUrl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Xml;

namespace Newsbook.Core.WebApi.Controllers
{
    public class FeedUrlController : ApiController
    {

        private readonly IFeedUrlServico _servico;
        private readonly INoticiaServico _noticiaServico;

        public FeedUrlController(IFeedUrlServico servico, INoticiaServico noticiaServico)
        {
            _servico = servico;
            _noticiaServico = noticiaServico;
        }

        [HttpGet]
        [Route("api/feedurl")]
       [Authorize]
        public Task<HttpResponseMessage> Get()
        {
            HttpResponseMessage response;

            try
            {
                var itens = _servico.Listar().OrderBy(x=> x.Titulo).ToList();

                var itensResourceModel = Mapper.Map<List<FeedUrl>, IEnumerable<GetFeedUrl>>(itens);
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

        [HttpPost]
        [Route("api/feedurl/criar")]
        //[Authorize]
        public Task<HttpResponseMessage> Criar(string feed)
        {
            HttpResponseMessage response;

            try
            {
                Feed item = FeedParser.Parse(feed);

                FeedUrl f = new FeedUrl() { Ativo = true, Titulo = item.Title, Url=feed };
                f = _servico.Inserir(f);

                for (int i = 0; i < item.Items.Count; i++)
                {
                    Noticia n = new Noticia();
                    n.Ativo = true;
                    n.Conteudo = item.Items[i].Content;
                    n.DataPublicacao = item.Items[i].PublishDate;
                    n.FeedUrl = f;
                    n.Link = item.Items[i].Link;
                    n.Titulo = item.Items[i].Title;
                    n.Categorias = item.Items[i].Categories.ToList();
                   n = _noticiaServico.Inserir(n);
                }

                

                response = Request.CreateResponse(HttpStatusCode.OK, "OK");
            }
            catch (XmlException erro)
            {
                response = Request.CreateResponse(HttpStatusCode.BadRequest, "RSS mal formado. " + erro.Message);
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
