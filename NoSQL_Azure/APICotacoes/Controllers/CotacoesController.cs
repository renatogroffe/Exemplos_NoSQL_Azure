using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace APICotacoes.Controllers
{
    [Route("api/[controller]")]
    public class CotacoesController : Controller
    {
        [HttpGet("{siglaMoeda}")]
        public IActionResult Get(
            [FromServices]IConfiguration config,
            string siglaMoeda)
        {
            object resultado = null;
            if (siglaMoeda == "EUR" || siglaMoeda == "LIB")
                resultado = Database.ListarCotacoes<CotacaoEntity>(config, siglaMoeda);
            else if (siglaMoeda == "USD")
                resultado = Database.ListarCotacoes<CotacaoDolarEntity>(config, "USD");

            if (resultado != null)
                return new ObjectResult(resultado);
            else
                return NotFound();
        }
    }
}