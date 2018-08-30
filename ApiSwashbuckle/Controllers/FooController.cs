using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace ApiSwashbuckle.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FooController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<Foo>> Get()
        {
            return new[]
            {
                new Foo()
                {
                    Bar = "Bar", Baz = "Baz",

                },
                new Foo()
                {
                    Bar = "Bar", Baz = "Baz",
                },
            };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<Foo> Get([FromQuery] int id)
        {
            return new Foo()
            {
                Bar = "Bar",
                Baz = "Baz",
            };
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] Foo value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Foo value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
