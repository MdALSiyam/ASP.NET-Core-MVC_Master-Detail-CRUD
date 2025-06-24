using CoreMvcMasterDetailCrud.Models;
using CoreMvcMasterDetailCrud.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CoreMvcMasterDetailCrud.Controllers
{
    public class StudentsController : Controller
    {
        private readonly AppDbContext _db;
        private readonly IWebHostEnvironment _env;

        public StudentsController(AppDbContext db, IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }

        public IActionResult Index()
        {
            var students = _db.Students.Include(c=>c.Course).Include(m=>m.Moduless).ToList();
            return View(students);
        }
        public IActionResult Create()
        {
            StudentViewModel student = new StudentViewModel();
            student.Courses=_db.Courses.ToList();
            //student.Moduless.Add(new CourseModule { CourseModuleId = 1 });
            return View(student);
        }
        [HttpPost]
        public async Task <IActionResult> Create(StudentViewModel model)
               {
            Student student=new Student();
            string imageFileName = null;
            imageFileName = await GetImageFileName(model.ProfileFile);
            student.ImageUrl = imageFileName;
            student.StudentName= model.StudentName;
            student.Dob=model.Dob;
            student.Course=model.Course;
            student.MobileNo=model.MobileNo;
            student.CourseId=model.CourseId;
            student.IsEnrolled=model.IsEnrolled;
            student.Moduless = model.Moduless;
            _db.Students.Add(student);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        private async Task<string?> GetImageFileName(IFormFile profileFile)
        {
            string uniqueFileName = null;
            if (profileFile != null)
            {
                uniqueFileName=Guid.NewGuid().ToString() + Path.GetExtension(profileFile.FileName); ;
                var uploadFolder = Path.Combine(_env.WebRootPath, "images");
                var filePath=Path.Combine(uploadFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await profileFile.CopyToAsync(fileStream);
                }

               
            }
            return uniqueFileName;
        }
        [HttpPost]
        public async Task< ActionResult> Delete(int id)
        {
            var student =await _db.Students.Where(s=>s.StudentId==id).FirstOrDefaultAsync();

            if (student == null) 
            {
                return NotFound();
            }
            if (!string.IsNullOrEmpty(student.ImageUrl))
            {
                string imagePath = Path.Combine(_env.WebRootPath,"images",student.ImageUrl);
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }

            }
            _db.Students.Remove(student);
            await _db.SaveChangesAsync();

            
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Edit (int id)
        {
            var student = _db.Students.Include(c => c.Course).Include(c => c.Moduless).FirstOrDefault(s=>s.StudentId==id);
            if (student == null)                      
            {
                return NotFound();
            }
            var viewModel = new StudentViewModel
            {
                StudentId = student.StudentId,
                StudentName = student.StudentName,
                Dob = student.Dob,
                MobileNo = student.MobileNo,
                CourseId = student.CourseId,
                CourseName = student.Course.CourseName,
                IsEnrolled = student.IsEnrolled,
                ImageUrl = student.ImageUrl,
                Moduless=student.Moduless.ToList(),
                Courses=_db.Courses.ToList(),

            };
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(StudentViewModel model, string OldImageUrl)
        {
            var student = await _db.Students.Include(c => c.Course).Include(c => c.Moduless).FirstOrDefaultAsync(s => s.StudentId == model.StudentId);
            if (student == null)
            {
                return NotFound();
            }
            if (model.ProfileFile != null)
            {
                if (!string.IsNullOrEmpty(student.ImageUrl))
                {
                    var oldImagePath=Path.Combine(_env.WebRootPath,"images",student.ImageUrl);
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                    string imageFileName = null;
                    imageFileName = await GetImageFileName(model.ProfileFile);
                    student.ImageUrl = imageFileName;
                }
            }
            else 
            {
                student.ImageUrl = OldImageUrl;
            }

            student.StudentName = model.StudentName;
            student.Dob = model.Dob;
            student.CourseId = model.CourseId;
            student.IsEnrolled = model.IsEnrolled;
            student.MobileNo = model.MobileNo;
            student.ImageUrl = student.ImageUrl;
            var exitingModules=student.Moduless.ToList();
            var newmodules=model.Moduless.ToList();
            var existingModules = student.Moduless.ToList();
            foreach (var exModule in existingModules)
            {
                _db.CourseModules.Remove(exModule);
            }
            foreach (var item in newmodules)
            {
                _db.CourseModules.Add(new CourseModule
                {
                    ModuleName = item.ModuleName,
                    Duration = item.Duration,
                    StudentId = student.StudentId
                });
            }
            
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");



        }

    }
}
