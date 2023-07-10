using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CrudAPI.Models;

namespace CrudAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CoursesController : ControllerBase
    {
        private readonly CourseapiContext _context;
        private readonly ILogger<CoursesController> _logger;

        public CoursesController(CourseapiContext dBContext, ILogger<CoursesController> logger)
        {
            _context = dBContext;
            _logger = logger;
        }

        // GET: api/Courses
        [HttpGet("Student/{studentId}/courses", Name = "GetGoals")]
        public IActionResult GetCourses(int studentId)
        {
            try
            {
                var student = _context.Students.FindAsync(studentId);

                if (student == null)
                {
                    return NotFound();
                }

                var courses = this._context.Courses.Where(c => c.Student.StudentId == studentId)
                        .ToList();
                _logger.LogInformation("The course list was successfully retrieved!");
                // _logger.LogInformation(courses);
                return Ok(courses);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving the list of students.");
                return StatusCode(500, "An error occurred while processing the request");
                throw;
            }
        }
        ///// GET: api/Courses
        // [HttpGet]
        // public ActionResult<IEnumerable<Course>> GetCourses()
        // {
        //     // var courses = this._context.Courses.ToList();
        //     // return Ok(courses);
        //     return _context.Courses.ToList();
        // }

        // GET: api/Courses/5

        [HttpGet("Student/{studentId}/course/{id}", Name = "GetGoal")]
        public ActionResult<Course> GetCourse(int studentId, int id)
        {
            var student = _context.Students.FindAsync(studentId);

            if (student == null)
            {
                return NotFound();
            }

            var course = _context.Courses
                        .Where(c => c.Student.StudentId == studentId && c.Id == id)
                        .FirstOrDefault();

            if (course == null)
            {
                return NotFound();
            }

            return course;
        }

        /// <summary>
        /// Creates a Course.
        /// </summary>
        /// <param name="item"></param>
        /// <returns>A newly created Course</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Todo
        ///     {
        ///        "id": 1,
        ///        "name": "Item #1",
        ///        "isComplete": true
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        // POST: api/Courses
        [HttpPost]
        public ActionResult<Course> CreateCourse(Course course)
        {
            if (course.Description == course.Name)
            {
                ModelState.AddModelError("Description", "Description: " + course.Description + " should be different from Name: " + course.Name + ".");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Courses.Add(course);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetCourse), new { id = course.Id }, course);
        }

        // PUT: api/Courses/5
        [HttpPut("{id}")]
        public IActionResult UpdateCourse(int id, Course course)
        {
            if (id != course.Id)
            {
                return BadRequest();
            }

            _context.Entry(course).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CourseExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }


        // DELETE: api/Courses/5
        [HttpDelete("{id}")]
        public IActionResult DeleteCourse(int id)
        {
            var course = _context.Courses.Find(id);
            if (course == null)
            {
                return NotFound();
            }

            _context.Courses.Remove(course);
            _context.SaveChanges();

            return NoContent();
        }

        private bool CourseExists(int id)
        {
            return _context.Courses.Any(e => e.Id == id);
        }

        // public IActionResult CreatCourseForStudent(int studentId, Course course)
        // {
        //     if (course.Description == course.Name)
        //     {
        //         ModelState.AddModelError("Description", "Description: " + course.Description + " should be different from Name: " + course.Name + ".");
        //     }

        //     if (!ModelState.IsValid)
        //     {
        //         return BadRequest(ModelState);
        //     }

        //     var student = _context.Students.Find(studentId);

        //     if (student == null)
        //     {
        //         return NotFound();
        //     }

        //     var maxCourseId = _context.Students.Select(a => a.Courses).Max(g => g.Id);

        //     var finalCourse = new Course()
        //     {
        //         Id = ++maxCourseId,
        //         Name = course.Name,
        //         Description = course.Description
        //     };

        //     _context.Courses.Add(finalCourse);

        //     return CreatedAtRoute("GetCourse", new { studentId, id = finalCourse.Id}, finalCourse);
        // }
    }
}
