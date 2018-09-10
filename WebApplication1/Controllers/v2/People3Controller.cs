using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers.V2
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiVersion("2.0")]
    public class People3Controller : ControllerBase
    {
        // GET api/values
        /// <summary>
        /// Devuelve el ilustre nombre de Sergio
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<string> Get()
        {
            return await Task.FromResult("sergio");
        }
    }
}
