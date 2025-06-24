using CoreMvcMasterDetailCrud.Contracts;
using CoreMvcMasterDetailCrud.Models;

namespace CoreMvcMasterDetailCrud.Services
{
    public class StudentRepository : IStudentRepository
    {
        private readonly AppDbContext _db;

        public StudentRepository(AppDbContext db)
        {
            _db = db;
        }

        public Student AddStudent(Student student)
        {
            throw new NotImplementedException();
        }

        public Student DeleteStudent(int id)
        {
            throw new NotImplementedException();
        }

        public Student GetStudent(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Student> GetStudents()
        {
            throw new NotImplementedException();
        }

        public Student UpdateStudent(Student student)
        {
            throw new NotImplementedException();
        }
    }
}
