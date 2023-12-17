using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using ToDoListBlazor.Interfaces;
using ToDoListBlazor.Models;

namespace ToDoListBlazor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProblemController: ControllerBase
    {
        private readonly IProblem _IProblem;
        public ProblemController(IProblem iProblem)
        {
            _IProblem = iProblem;
        }
        [HttpGet("Get")]
        public async Task<List<Problem>> Get()
        {
            Console.WriteLine("Get");
            //return null;
            return await Task.FromResult(_IProblem.GetProblemDetails());
        }
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Problem Problem = _IProblem.GetProblemData(id);
            if (Problem != null)
            {
                return Ok(Problem);
            }
            return NotFound();
        }
        [HttpPost]
        public void Post(Problem Problem)
        {
            _IProblem.AddProblem(Problem);
        }
        [HttpPut]
        public void Put(Problem Problem)
        {
            _IProblem.UpdateProblemDetails(Problem);
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _IProblem.DeleteProblem(id);
            return Ok();
        }
    }
}
