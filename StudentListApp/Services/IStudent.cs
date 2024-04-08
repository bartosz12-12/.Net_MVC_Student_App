using StudentListApp.Models.Student;

namespace StudentListApp.Services
{
    public interface IStudent
    {
        Task<List<StudentModel>> GetAll();
        Task AddStudent(StudentModel addStudent);
        Task EditStudent(StudentModel student, StudentModel studentModel);
        Task DeleteStudent(StudentModel student);
    }
}
