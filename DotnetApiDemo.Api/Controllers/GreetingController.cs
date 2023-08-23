using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DotnetApiDemo.Api.Controllers
{
    // baseUrl/api/Greetings
    [Route("api/greetings")]
    [ApiController]
    public class GreetingController : ControllerBase
    {
        [HttpGet]
        public string GetGreeting()
        {
            return "Hello";
        }

        [HttpPost]
        public string PostGreeting(string message)
        {
            return message;
        }

        [HttpPut]
        public string UpdateGreeting(string message)
        {
            return message;
        }


        ///api/greetings?id=1
        ///api/greetings/1
        [HttpDelete("{id}")]
        public string DeleteGreeting(int id)
        {
            return "greeting deleted";
        }
    }
}
