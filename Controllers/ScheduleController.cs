using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StudentPortal.Web.Data;
using StudentPortal.Web.Models;
using StudentPortal.Web.Models.Entities;

namespace StudentPortal.Web.Controllers
{
    [Authorize]
    public class ScheduleController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public ScheduleController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        // GET: Schedule/Add
        public async Task<IActionResult> Add()
        {
            var viewModel = new AddScheduleViewModel
            {
                Subjects = await dbContext.Subjects
                    .Select(s => new SelectListItem
                    {
                        Value = s.Subj_Code, // Assuming Subj_Code is the identifier
                        Text = s.SubjectDescription // Display text for the dropdown
                    })
                    .ToListAsync()
            };

            return View(viewModel);
        }

        // POST: Schedule/Add
        [HttpPost]
        public async Task<IActionResult> Add(AddScheduleViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Add logic to save the schedule to the database
                var newSchedule = new Schedule
                {
                    EdpCode = model.EdpCode,
                    SubjCode = model.SubjCode,
                    StartTime = model.StartTime,
                    EndTime = model.EndTime,
                    Room = model.Room,
                    Section = model.Section,
                    SchoolYear = model.SchoolYear
                };

                dbContext.Schedules.Add(newSchedule);
                await dbContext.SaveChangesAsync();

                return RedirectToAction("List");
            }

            // Repopulate subjects in case of a validation failure
            model.Subjects = await dbContext.Subjects
                .Select(s => new SelectListItem
                {
                    Value = s.Subj_Code,
                    Text = s.SubjectDescription
                })
                .ToListAsync();

            return View(model);
        }

        // GET: Schedule/List
        [HttpGet]
        public async Task<IActionResult> List()
        {
            var schedules = await dbContext.Schedules.ToListAsync();
            return View(schedules);
        }

        // GET: Schedule/Edit/{edpCode}
        [HttpGet]
        public async Task<IActionResult> Edit(string edpCode)
        {
            var schedule = await dbContext.Schedules.FindAsync(edpCode);
            if (schedule == null)
            {
                return NotFound(); // This returns a 404 if the schedule is not found
            }

            var viewModel = new AddScheduleViewModel
            {
                EdpCode = schedule.EdpCode,
                SubjCode = schedule.SubjCode,
                StartTime = schedule.StartTime,
                EndTime = schedule.EndTime,
                Room = schedule.Room,
                Section = schedule.Section,
                SchoolYear = schedule.SchoolYear,
                Subjects = await dbContext.Subjects
                    .Select(s => new SelectListItem
                    {
                        Value = s.Subj_Code,
                        Text = s.SubjectDescription
                    })
                    .ToListAsync()
            };

            return View(viewModel);
        }

        // POST: Schedule/Edit
        [HttpPost]
        public async Task<IActionResult> Edit(AddScheduleViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                // Repopulate subjects in case of a validation failure
                viewModel.Subjects = await dbContext.Subjects
                    .Select(s => new SelectListItem
                    {
                        Value = s.Subj_Code,
                        Text = s.SubjectDescription
                    })
                    .ToListAsync();

                return View(viewModel);
            }

            var schedule = await dbContext.Schedules.FindAsync(viewModel.EdpCode);
            if (schedule == null)
            {
                return NotFound();
            }

            // Update the schedule details
            schedule.SubjCode = viewModel.SubjCode;
            schedule.StartTime = viewModel.StartTime;
            schedule.EndTime = viewModel.EndTime;
            schedule.Room = viewModel.Room;
            schedule.Section = viewModel.Section;
            schedule.SchoolYear = viewModel.SchoolYear;

            await dbContext.SaveChangesAsync();

            TempData["SuccessMessage"] = "Schedule has been updated successfully.";
            return RedirectToAction("List");
        }

        // GET: Schedule/Delete/{edpCode}
        [HttpGet]
        public async Task<IActionResult> Delete(string edpCode)
        {
            var schedule = await dbContext.Schedules.FirstOrDefaultAsync(s => s.EdpCode == edpCode);
            if (schedule == null)
            {
                return NotFound();
            }

            return View(schedule);
        }

        // POST: Schedule/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string edpCode)
        {
            var schedule = await dbContext.Schedules.FirstOrDefaultAsync(s => s.EdpCode == edpCode);
            if (schedule != null)
            {
                dbContext.Schedules.Remove(schedule);
                await dbContext.SaveChangesAsync();

                TempData["SuccessMessage"] = "Schedule has been deleted successfully.";
            }

            return RedirectToAction("List");
        }
    }
}
