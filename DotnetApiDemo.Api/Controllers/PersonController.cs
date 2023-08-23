using DotnetApiDemo.Data.Models;
using DotnetApiDemo.Data.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DotnetApiDemo.Api.Controllers
{
    // before: api/person
    //[Route("api/[controller]")]
    // now : api/people
    [Route("api/people")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IPersonRepository _personRepo;

        public PersonController(IPersonRepository personRepo)
        {
            _personRepo = personRepo;
        }
        [HttpGet]
        //api/people
        public async Task<IActionResult> GetPeople()
        {
            try
            {
                var people = await _personRepo.GetPeople();
                return Ok(people);
                // it will return 200 ok status code and People[] in response body
            }
            catch (Exception ex)
            {
                 // log exception here
                return StatusCode(StatusCodes.Status500InternalServerError,new ResponseModel { StatusCode=500,Message="Something went wrong!"});
            }
        }

        [HttpPost]
        // api/people (post)

        public async Task<IActionResult> CreatePerson([FromBody] Person person)
        {
           // check the validation
           if(!ModelState.IsValid)
            {
                // validation failed
                //return BadRequest(new ResponseModel { StatusCode = 400, Message = "Please pass all the required field and valid data" });
                // 422 
                return UnprocessableEntity(ModelState);
            }
            try
            {
                await _personRepo.AddPerson(person);
                return CreatedAtAction(nameof(CreatePerson), person);
            }
            catch (Exception ex)
            {
                // log exception here
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseModel { StatusCode = 500, Message = "Something went wrong!" });
            }

        }


        [HttpPut]
        public async Task<IActionResult> UpdatePerson(Person person)
        {
            // check the validation
            if (!ModelState.IsValid)
            {
                // validation failed
                return UnprocessableEntity(ModelState);
            }
            try
            {
                var existingPerson = await _personRepo.GetPersonById(person.Id);
                if (existingPerson == null)
                {
                    return NotFound(new ResponseModel { StatusCode = 404, Message = "No resource found" });
                }
                await _personRepo.UpdatePerson(person);
                return Ok(person);
            }
            catch (Exception ex)
            {
                // log exception here
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseModel { StatusCode = 500, Message = "Something went wrong!" });
            }

        }

        [HttpGet("{id}")]
        //api/people/{id}

        public async Task<IActionResult> GetPerson(int id)
        {
            try
            {
                var person = await _personRepo.GetPersonById(id);
                if (person == null)
                {
                    return NotFound(new ResponseModel { StatusCode = 404, Message = "No resource found" });
                }
                return Ok(person);
            }
            catch (Exception ex)
            {
                // log exception here
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseModel { StatusCode = 500, Message = "Something went wrong!" });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePerson(int id)
        {
            try
            {
                var person = await _personRepo.GetPersonById(id);
                if (person == null)
                {
                    return NotFound(new ResponseModel { StatusCode = 404, Message = "No resource found" });
                }
                await _personRepo.DeletePerson(id);
                return Ok(new ResponseModel { StatusCode=200,Message="Deleted successfully"});
            }
            catch (Exception ex)
            {
                // log exception here
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseModel { StatusCode = 500, Message = "Something went wrong!" });
            }
        }

        [HttpGet("another-get")]
        //api/people/another-get

        public async Task<IActionResult> AnotherGet()
        {
            return Ok("another get");
        }

        [HttpPost("another-post")]
        // api/people/another-post (post)

        public async Task<IActionResult> AnotherPost()
        {
            return Ok("posted successfully");
        }
    }
}
