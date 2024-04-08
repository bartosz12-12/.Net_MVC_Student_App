using StudentListApp.Models.Student;
using System.ComponentModel.DataAnnotations;

namespace StudentListApp.Models.Zajecia
{
    public class ZajeciaModel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Ects { get; set; }
        public ICollection<StudentModel> Students { get; set; } = new List<StudentModel>();
    }
}
