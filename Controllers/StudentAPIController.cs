using ASPCoreWebAPICRUD.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ASPCoreWebAPICRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentAPIController : ControllerBase
    {
        private readonly MyDbContext context;

        public class ApiResponse
        {
            public bool Success { get; set; }
            public string Message { get; set; }
        }

        public StudentAPIController(MyDbContext context)
        {
            this.context = context;
        }

        [HttpGet("GetStudents")]

        public async Task<ActionResult<List<Student>>> GetStudents()
        {
            var data = await context.Students.ToListAsync();              //gets all student details to list
            return Ok(data);

        }

        [HttpGet("{id:int}")]

        public async Task<ActionResult<Student>> GetStudentById(int id)    // returns particular student by Id(parameter)
        {
            var student = await context.Students.FindAsync(id);

            if (student == null || id <= 0)
            {
                return NotFound();
            }
            return Ok(student);
        }


        [HttpPost("NewStudent")]

        public async Task<ActionResult<Student>> CreateStudent(Student std)    // creates new student class-student, obj-std
        {
            await context.Students.AddAsync(std);
            await context.SaveChangesAsync();

            return Ok(new ApiResponse
            {
                Success = true,
                Message = "Student Created Successfully"
            });
        }

        [HttpPut("UpdateStudent/{id:int}")]

        public async Task<ActionResult<Student>> UpdateStudent(int id, Student std)  //update stdnt by taking id parameter 
        {

            var existingStudent = await context.Students.FindAsync(id);

            if (existingStudent == null && id != existingStudent.Id)
            {
                return BadRequest($"The Entered Id {id} Is Not Available In The List");
            }
            
            existingStudent.Age = std.Age;
            existingStudent.StudentName = std.StudentName;
            existingStudent.StudentGender = std.StudentGender;
            existingStudent.FatherName= std.FatherName;
            existingStudent.Grade = std.Grade;

            context.Students.Update(existingStudent);

            await context.SaveChangesAsync();


            return Ok(new ApiResponse
            {
                Success = true, 
                Message = "Student updated successfully"
            });
        }



        [HttpDelete("DeleteStudent/{id:int}")]
        public async Task<ActionResult<Student>> DeleteStudent(int id)
        {
            // Find the student by ID
            var std = await context.Students.FindAsync(id);

            // If the student is not found or the ID is invalid, return a NotFound response with a message
            if (std == null || id <= 0)
            {
                return NotFound(new ApiResponse
                {
                    Success = false,
                    Message = "Student not found or invalid ID."
                });
            }

            // Remove the student from the context
            context.Students.Remove(std);
            await context.SaveChangesAsync();

            // Return a success response with a message
            return Ok(new ApiResponse
            {
                Success = true,
                Message = "Student deleted successfully."
            });
        }

    }
}
