using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MimicAPI.V2.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("2.0")]

    public class PalavrasController : ControllerBase
    {
        /// <summary>
        /// Operação que realiza o cadastro da palavra
        /// </summary>
        /// <param name="palavra">Um objeto palavra com o seu ID</param>
        /// <returns>Um objeto pa</returns>
        [HttpGet("", Name = "ObterTodas")]

        public string ObterTodas()
        {
            return "Versão 2.0";

        }
    }
}
