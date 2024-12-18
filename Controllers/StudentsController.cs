using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentPortal.Web.Data;
using StudentPortal.Web.Models;
using StudentPortal.Web.Models.Entities;

namespace StudentPortal.Web.Controllers
{
	[Authorize]
    public class StudentsController : Controller
    {
        private readonly ApplicationDbContext dbContext;
        public StudentsController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }


        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Add(AddStudentViewModel viewModel)
        {
            var student = new Student
            {
                //Id = viewModel.Id,
                FirstName = viewModel.FirstName,
                LastName = viewModel.LastName,
                MiddleName = viewModel.MiddleName,
                Course = viewModel.Course,
                Year = viewModel.Year
            };

            await dbContext.Students.AddAsync(student);

            await dbContext.SaveChangesAsync();

            TempData["SuccessMessage"] = "Student has been added successfully.";

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]

        public async Task<IActionResult> List()
        {
            var students = await dbContext.Students.ToListAsync();

            return View(students);
        }

        [HttpGet]

        public async Task<IActionResult> Edit(int id)
        {
            var student = await dbContext.Students.FindAsync(id);



            return View(student);
        }


        [HttpPost]

        public async Task<IActionResult> Edit(Student viewModel)
        {
            var student = await dbContext.Students.FindAsync(viewModel.Id);

            if (student is not null)
            {
                student.Id = viewModel.Id;
                student.FirstName = viewModel.FirstName;
                student.LastName = viewModel.LastName;
                student.MiddleName = viewModel.MiddleName;
                student.Course = viewModel.Course;
                student.Year = viewModel.Year;

                await dbContext.SaveChangesAsync();
            }
            TempData["SuccessMessage"] = "Student has been updated successfully.";
            return RedirectToAction("Index", "Home");
        }

		[HttpGet]
		public async Task<IActionResult> Delete(int Id)
        {
			var student = await dbContext.Students.FindAsync(Id);


			return View(student);
		}



        [HttpPost]
        public async Task<IActionResult> Delete(Student viewModel)
		{
			var student = await dbContext.Students
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == viewModel.Id);

			if (student is not null)
			{
				dbContext.Students.Remove(student);
				await dbContext.SaveChangesAsync();
			}
            TempData["SuccessMessage"] = "Student has been deleted successfully.";

            return RedirectToAction("Index", "Home");
		}

        [HttpGet]
        public IActionResult SubjectEnrollment()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetStudentDetails(int id)
        {
            var student = await dbContext.Students
                .FirstOrDefaultAsync(s => s.Id == id);

            if (student == null)
                return Json(new { success = false, message = "Student not found." });

            return Json(new
            {
                success = true,
                data = new
                {
                    name = $"{student.FirstName} {student.LastName}",
                    course = student.Course,
                    year = student.Year
                }
            });
        }

        [HttpGet]
        public async Task<IActionResult> GetSubjectSchedule(string edpCode)
        {
            var schedule = await dbContext.Schedules
                .Include(s => s.Subject)
                .FirstOrDefaultAsync(s => s.EdpCode == edpCode);

            if (schedule == null)
                return Json(new { success = false, message = "Schedule not found." });

            return Json(new { success = true, data = schedule });
        }

        [HttpPost]
        public async Task<IActionResult> EnrollSubject([FromBody] SubjectEnrollmentViewModel model)
        {
            if (model == null)
            {
                return Json(new { success = false, message = "Invalid data received." });
            }
            try
            {
                var student = await dbContext.Students.FindAsync(model.StudentId);
                if (student == null)
                {
                    return Json(new { success = false, message = "Student not found." });
                }

                foreach (var subject in model.EnrolledSubjects)
                {
                    var enrollment = new SubjectEnrollment
                    {
                        StudentId = model.StudentId,
                        EdpCode = subject.EdpCode,
                        EncodedDate = model.EnrollmentDate,
                        EncodedBy = model.EncodedBy
                    };

                    await dbContext.SubjectEnrollments.AddAsync(enrollment);
                }

                await dbContext.SaveChangesAsync();
                return Json(new { success = true, message = "Enrollment saved successfully." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Error saving enrollment: {ex.Message}" });
            }
        }

        [HttpGet]
        public async Task<IActionResult> EnrollmentForm(int studentId, DateTime enrollmentDate)
        {
            Console.WriteLine($"StudentId: {studentId}, EnrollmentDate: {enrollmentDate}");

            var student = await dbContext.Students.FindAsync(studentId);
            var enrollments = await dbContext.SubjectEnrollments
                .Include(se => se.Schedule)
                    .ThenInclude(s => s.Subject)
                .Where(se => se.StudentId == studentId &&
                            se.EnrollmentDate.Date == enrollmentDate.Date)
                .ToListAsync();

            if (student == null || !enrollments.Any())
            {
                return NotFound();
            }

            var viewModel = new SubjectEnrollmentViewModel
            {
                StudentId = student.Id,
                StudentName = $"{student.FirstName} {student.MiddleName} {student.LastName}",
                Course = student.Course,
                Year = student.Year,
                EnrollmentDate = enrollmentDate,
                EnrolledSubjects = enrollments.Select(e => new EnrolledSubjectViewModel
                {
                    EdpCode = e.EdpCode,
                    SubjectCode = e.Schedule.SubjCode,
                    Description = e.Schedule.Subject.SubjectDescription,
                    Units = e.Schedule.Subject.Units,
                    StartTime = e.Schedule.StartTime,
                    EndTime = e.Schedule.EndTime,
                    Room = e.Schedule.Room,
                    Section = e.Schedule.Section
                }).ToList(),
                TotalUnits = enrollments.Sum(e => e.Schedule.Subject.Units)
            };

         
            return View("EnrollmentForm", viewModel);
        }

    }
    }



    
    
