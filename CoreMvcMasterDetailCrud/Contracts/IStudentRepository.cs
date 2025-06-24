using CoreMvcMasterDetailCrud.Models;
namespace CoreMvcMasterDetailCrud.Contracts
{
    public interface IStudentRepository
    {
        IEnumerable<Student> GetStudents();
        Student GetStudent(int id);
        Student UpdateStudent(Student student);
        Student AddStudent(Student student);
        Student DeleteStudent(int id);
    }
}
