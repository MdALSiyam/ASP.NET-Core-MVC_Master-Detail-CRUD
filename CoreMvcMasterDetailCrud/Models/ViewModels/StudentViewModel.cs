using System.ComponentModel.DataAnnotations;

namespace CoreMvcMasterDetailCrud.Models.ViewModels
{
    public class StudentViewModel
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; } = null!;

        [Required ,Display(Name ="Date of Birth"),DataType(DataType.Date),
        DisplayFormat(ApplyFormatInEditMode =true,DataFormatString ="{0:yyyy-MM-dd}")]
        public DateTime Dob { get; set; }
        public string MobileNo { get; set; }
        public string ImageUrl { get; set; }
        public bool IsEnrolled { get; set; }
        public int CourseId { get; set; }
        public int CourseModuleId { get; set; }
        public string ModuleName { get; set; } = null!;
        public int Duration { get; set; }
        public virtual Course Course { get; set; }
        public string CourseName { get; set; }
        public virtual ICollection<CourseModule> Moduless { get; set; } = new List<CourseModule>();
        public IList<Student> student { get; set; }
        public IFormFile ProfileFile { get; set; }

        public List<Course> Courses { get; set; }

    }
}
