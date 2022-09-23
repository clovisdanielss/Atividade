using Microsoft.AspNetCore.Mvc;

namespace Atividade.Controllers
{
    [ApiController]
    [Route("")]
    public class IndexController : ControllerBase
    {
        [HttpGet(Name = "Home")]
        public string Get()
        {
            return "API de Vendas, se quiser ver a documentação vá à rota /swagger/";
        }
    };


}