using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class People2Controller : ControllerBase
    {
        private static readonly List<PersonWrapper> People = new List<PersonWrapper>()
        {
            new PersonWrapper()
            {
                People = new List<Person<Address>>()
                {
                    new Person<Address>(2, "Jimena", "León", 8,
                        new List<Address>() { new Address() { PostalCode = "28054" } })
                }
            }
        };

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<PersonWrapper>> Get()
        {
            return People;
        }
    }
}
