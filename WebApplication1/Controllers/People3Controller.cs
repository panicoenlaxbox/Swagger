using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class People3Controller : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public async Task<ActionResult<UsersQueryResponse>> Get()
        {
            return await Task.FromResult(new UsersQueryResponse(new PaginatedResult<UsersQueryResponse.UserDto>(1, 1, 1, new List<UsersQueryResponse.UserDto>())));
        }
    }
}
