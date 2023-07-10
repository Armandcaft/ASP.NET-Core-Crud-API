using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CrudAPI.Models;

namespace CrudAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentsController : ControllerBase
    {
        private readonly CourseapiContext _context;
        private readonly ILogger<StudentsController> _logger;

        public StudentsController(CourseapiContext context, ILogger<StudentsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/Students
        [HttpGet]
        public ActionResult<IEnumerable<Student>> GetStudents()
        {
            try
            {
                var students = _context.Students.AsQueryable();
                // Apply any filtering, sorting, or paging logic here using LINQ methods
                students = students.OrderBy(s => s.Name);
                var studentList = students.ToList();
                _logger.LogInformation("The students list was successfully retrieved!");
                return Ok(studentList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving the list of students.");
                return StatusCode(500, "An error occurred while processing the request");
                throw;
            }
        }

        // GET: api/Students/5
        /// <summary>
        /// Get a student by ID.
        /// </summary>
        /// <param name="id">The ID of the student.</param>
        /// <returns>The student details.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Student))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces("application/json")]
        public ActionResult<Student> GetStudentById(int id)
        {
            try
            {
                var student = _context.Students.FindAsync(id);

                if (student == null)
                {
                    return NotFound();
                }

                _logger.LogInformation("The student with ID: " + id + " was successfully retrieved!");
                return Ok(student);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving the student of ID: " + id + ".");
                return StatusCode(500, "An error occurred while processing the request");
                throw;
            }

        }

        // POST: api/Students
        [HttpPost]
        public ActionResult<Student> CreateStudent(Student newStudent)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("An error occured: " + ModelState + ".");
                    return BadRequest(ModelState);
                }

                _context.Students.Add(newStudent); //  _context.Entry(newStudent).State = EntityState.Added;
                _context.SaveChanges();

                _logger.LogInformation("A students was added (created) successfully! With id: " + newStudent.StudentId + ".");
                return CreatedAtAction(nameof(GetStudentById), new { id = newStudent.StudentId }, newStudent);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating the new student.");
                return StatusCode(500, "An error occurred while processing the request");
                throw;
            }
            
        }

        // PUT: api/Students/5
        [HttpPut("{id}")]
        public IActionResult UpdateStudent(int id, Student student)
        {
            if (id != student.StudentId)
            {
                _logger.LogError("The provided student ID:" + id + " does not match the ID in the request body." + student.StudentId + ".");
                return BadRequest("The provided student ID does not match the ID in the request body.");
            }

            var existingStudent = _context.Students.Find(id);
            if (existingStudent == null)
            {
                _logger.LogError("Student with ID:" + id + " not found!");
                return NotFound("Student not found.");
            }

            existingStudent.Name = student.Name;
            existingStudent.Age = student.Age;

            _context.Entry(existingStudent).State = EntityState.Modified;
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!StudentExists(id))
                {
                    _logger.LogError("Student with ID:" + id + "not found!");
                    return NotFound("Student not found.");
                }
                else
                {
                    _logger.LogError(ex, "An error occured... Update unsuccessful.");
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/Students/5
        [HttpDelete("{id}")]
        public IActionResult DeleteStudent(int id)
        {
            try
            {
                var studentToDelete = _context.Students.Find(id);
                if (studentToDelete == null)
                {
                    return NotFound("Student not found!");
                }

                _context.Students.Remove(studentToDelete);  // _context.Entry(studentToDelete).State = EntityState.Deleted;
                _context.SaveChanges();

                _logger.LogError("Student with ID: " + id + " was successfully deleted!");
                return NoContent();
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting the student with ID: {id}.");
                return StatusCode(500, "An error occurred while processing the request");
                throw;
            }
            
        }

        private bool StudentExists(int id)
        {
            return _context.Students.Any(e => e.StudentId == id);
        }
    }
}
