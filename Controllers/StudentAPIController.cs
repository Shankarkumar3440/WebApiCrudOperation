using CrudOperation.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CrudOperation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentAPIController : ControllerBase
    {
        private readonly MyDbContext context;

        public StudentAPIController(MyDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Students>>> GetStudents()
        {
            var data = await context.Students.ToListAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Students>> GetStudentById(int id)
        {
            var data = await context.Students.FindAsync(id);

             if(data == null)
            {
                return NotFound();
            }
            else
            {
                return data;

            }       
        }
        [HttpPost]
        public async Task<ActionResult<Students>> CreateStudentById(Students std)
        {
            var data = await context.Students.AddAsync(std);
            await context.SaveChangesAsync();
            return Ok(data);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Students>> UpdateStudentById(int id, Students std)
        {
            if (id != std.Id)
            {
                return BadRequest();
            }
            context.Entry(std).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return Ok(std);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<Students>> DeleteStudentsById(int id)
        {
            var data = await context.Students.FindAsync(id);
            if (data == null)
            {
                return NotFound();
            }
            context.Students.Remove(data);
            await context.SaveChangesAsync();
            return Ok();
        }
    }
}
