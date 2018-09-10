using System.Collections.Generic;
using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Examples;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        private static readonly List<Person<Address>> People = new List<Person<Address>>()
        {
            new Person<Address>(1, "Sergio", "León", 42, new List<Address>() { new Address() { PostalCode = "28108"}}),
            new Person<Address>(2, "Jimena", "León", 8, new List<Address>() { new Address() { PostalCode = "28054"}})
        };
        // GET api/values
        [HttpGet]
        [SwaggerRequestExample(typeof(IEnumerable<Person<Address>>), typeof(MyExample))]
        [SwaggerResponse((int)HttpStatusCode.OK, null, typeof(IEnumerable<Person<Address>>))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, null, typeof(HttpError))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, null, typeof(string))]
        public ActionResult<IEnumerable<Person<Address>>> Get()
        {
            return People;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<Person<Address>> Get(int id)
        {
            return People.Single(p => p.Id == id);
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] Person<Address> value)
        {
            People.Add(value);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Person<Address> value)
        {
            var person = People.Single(p => p.Id == id);
            person.FirstName = value.FirstName;
            person.LastName = value.LastName;
            person.Age = value.Age;
            person.Data = value.Data;
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            People.Remove(new Person<Address>() { Id = id });
        }
    }

    public class MyExample : IExamplesProvider
    {
        public object GetExamples()
        {
            var data = new List<Person<Address>>()
            {
                new Person<Address>(1, "Sergio", "León", 42, new List<Address>() {new Address() {PostalCode = "28108"}})
            };
            return data;
        }
    }
}
