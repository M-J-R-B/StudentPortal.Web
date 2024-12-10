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
        public async Task<IActionResult> EnrollSubject(SubjectEnrollmentViewModel model)
        {
            // Enrollment logic here
            return Json(new { success = true });
        }
    }

}

    
    
