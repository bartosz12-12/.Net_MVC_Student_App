using Microsoft.EntityFrameworkCore;
using StudentListApp.Data;
using StudentListApp.Models.Student;

namespace StudentListApp.Services
{
    public class StudentService : IStudent
    {
        private DataContext _dataContext;

        public StudentService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<List<StudentModel>> GetAll()
        {
            var students = await _dataContext.Student.ToListAsync();
            return students;
        }

        public async Task AddStudent(StudentModel addStudent)
        {
            await _dataContext.Student.AddAsync(addStudent);
            await _dataContext.SaveChangesAsync();
        }


        public async Task EditStudent(StudentModel student, StudentModel studentModel)
        {
            if (student.Name != studentModel.Name || student.LastName != studentModel.LastName || student.NrIndex != studentModel.NrIndex)
            {
                student.Name = studentModel.Name;
                student.LastName = studentModel.LastName;
                student.NrIndex = studentModel.NrIndex;
                await _dataContext.SaveChangesAsync();
            }
        }

        public async Task DeleteStudent(StudentModel student)
        {
            _dataContext.Student.Remove(student);
            await _dataContext.SaveChangesAsync(); 
        }
    }
}
