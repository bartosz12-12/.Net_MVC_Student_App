using System.ComponentModel.DataAnnotations;

namespace StudentListApp.Models.Student
{
    public class AddStudent
    {
        [Required(ErrorMessage = "Name is required.")]
        [RegularExpression(@"^[\p{L}]+$", ErrorMessage = "Name can only contain letters.")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Name length must be between 1 and 50 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Last name is required.")]
        [RegularExpression(@"^[\p{L}]+$", ErrorMessage = "Last name can only contain letters.")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Last name length must be between 1 and 50 characters.")]
        public string LastName { get; set; }

        [RegularExpression(@"^\d{4,}$", ErrorMessage = "NrIndex must be a number with at least 4 digits.")]
        public int NrIndex { get; set; }
    }
}
