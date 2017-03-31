using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Azure.Documents.Client;

namespace APICatalogo.Controllers
{
    [Route("api/[controller]")]
    public class CatalogoController : Controller
    {
        [HttpGet]
        public IActionResult GetCatalogo(
            [FromServices]IConfiguration config)
        {
            using (var client = new DocumentClient(
                            new Uri(config["DocumentDB:EndpointUri"]),
                                    config["DocumentDB:PrimaryKey"]))
            {
                FeedOptions queryOptions =
                    new FeedOptions { MaxItemCount = -1 };

                return new ObjectResult(
                    client.CreateDocumentQuery(
                        UriFactory.CreateDocumentCollectionUri(
                            "MVPCommunityConnection", "Catalogo"),
                            "SELECT c.id, c.Nome FROM Catalogo c", queryOptions)
                        .ToList()
                    );
            }
        }

        [HttpGet("item/{id}")]
        public IActionResult GetItem(
            [FromServices]IConfiguration config, string id)
        {
            object resultado = null;

            using (var client = new DocumentClient(
                            new Uri(config["DocumentDB:EndpointUri"]),
                                    config["DocumentDB:PrimaryKey"]))
            {
                FeedOptions queryOptions =
                    new FeedOptions { MaxItemCount = -1 };

                if (id.StartsWith("PROD"))
                {
                    resultado = client.CreateDocumentQuery<Produto>(
                        UriFactory.CreateDocumentCollectionUri(
                            "MVPCommunityConnection", "Catalogo"), queryOptions)
                        .Where(i => i.id == id).AsEnumerable().FirstOrDefault();
                }
                else if (id.StartsWith("SERV"))
                {
                    resultado = client.CreateDocumentQuery<Servico>(
                        UriFactory.CreateDocumentCollectionUri(
                            "MVPCommunityConnection", "Catalogo"), queryOptions)
                        .Where(i => i.id == id).AsEnumerable().FirstOrDefault();
                }
            }

            if (resultado != null)
                return new ObjectResult(resultado);
            else
                return NotFound();
        }
    }
}