using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentPortal.Web.Data;
using StudentPortal.Web.Models;
using StudentPortal.Web.Models.Entities;

namespace StudentPortal.Web.Controllers
{
    [Authorize]
    public class SubjectController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public SubjectController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        // GET: Subject/Add
        [HttpGet]
        public IActionResult Add()
        {
            var model = new SubjectViewModel();
            return View(model);
        }

        // POST: Subject/Add
        [HttpPost]
        public async Task<IActionResult> Add(SubjectViewModel viewModel)
        {
            // Validate the model state
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            // Create a new Subject entity from the view model
            var subject = new Subject
            {
                Subj_Code = viewModel.Subj_Code,
                SubjectDescription = viewModel.SubjectDescription,
                Units = viewModel.Units,
                Offering = viewModel.Offering,
                Category = viewModel.Category,
                Course_Code = viewModel.Course_Code,
                CurriculumYear = viewModel.CurriculumYear,
                SubjectRequisite = !string.IsNullOrEmpty(viewModel.SubjectRequisite) ? viewModel.SubjectRequisite : null,
                IsPreRequisite = viewModel.IsPreRequisite ?? false
            };

            // Add the new subject to the database
            await dbContext.Subjects.AddAsync(subject);
            await dbContext.SaveChangesAsync();

            TempData["SuccessMessage"] = "Subject has been added successfully.";
            return RedirectToAction("List");
        }

        // GET: Subject/List
        [HttpGet]
        public async Task<IActionResult> List()
        {
            var subjects = await dbContext.Subjects.ToListAsync();
            return View(subjects);
        }

        // GET: Subject/Edit/{id}
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var subject = await dbContext.Subjects
        .AsNoTracking()
        .FirstOrDefaultAsync(s => s.Subj_Code == id);

            if (subject == null)
            {
                return NotFound();
            }

            var viewModel = new SubjectViewModel
            {
                Subj_Code = subject.Subj_Code,
                SubjectDescription = subject.SubjectDescription,
                Units = subject.Units,
                Offering = subject.Offering,
                Category = subject.Category,
                Course_Code = subject.Course_Code,
                CurriculumYear = subject.CurriculumYear,
                SubjectRequisite = subject.SubjectRequisite,
                IsPreRequisite = subject.IsPreRequisite
            };

            return View(viewModel);
        }

        // GET: Subject/Search
        [HttpGet]
        public async Task<IActionResult> Search(string subjCode)
        {
            if (string.IsNullOrEmpty(subjCode))
            {
                return BadRequest("Subject code cannot be null or empty.");
            }

            var subjects = await dbContext.Subjects
                .Where(s => s.Subj_Code.Contains(subjCode))
                .ToListAsync();

            return Json(subjects);
        }

        // POST: Subject/Edit
        [HttpPost]
        public async Task<IActionResult> Edit(SubjectViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel); // Return the view model in case of validation errors
            }

            var subject = await dbContext.Subjects.FindAsync(viewModel.Id);
            if (subject == null)
            {
                return NotFound();
            }

            // Update the subject properties with the values from the view model
            subject.Subj_Code = viewModel.Subj_Code;
            subject.SubjectDescription = viewModel.SubjectDescription;
            subject.Units = viewModel.Units;
            subject.Offering = viewModel.Offering;
            subject.Category = viewModel.Category;
            subject.Course_Code = viewModel.Course_Code; // Ensure the property name matches the model
            subject.CurriculumYear = viewModel.CurriculumYear;

            // Update the requisite
            subject.SubjectRequisite = !string.IsNullOrEmpty(viewModel.SubjectRequisite) ? viewModel.SubjectRequisite : null;

            // Set IsPreRequisite based on the view model
            subject.IsPreRequisite = viewModel.IsPreRequisite;

            await dbContext.SaveChangesAsync();

            TempData["SuccessMessage"] = "Subject has been updated successfully.";
            return RedirectToAction("List"); // Redirect back to the list after saving
        }

        // GET: Subject/Delete/{id}
        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            var subject = await dbContext.Subjects.FirstOrDefaultAsync(s => s.Subj_Code == id);
            if (subject == null)
            {
                return NotFound();
            }
            return View(subject);
        }

        // POST: Subject/Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var subject = await dbContext.Subjects
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Subj_Code == id);

            if (subject != null)
            {
                dbContext.Subjects.Remove(subject);
                await dbContext.SaveChangesAsync();

                TempData["SuccessMessage"] = "Subject has been deleted successfully.";
            }

            return RedirectToAction("List");
        }
    }
}
