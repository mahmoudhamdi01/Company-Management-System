using DAL.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PL.ViewModels
{
    public class EmployeeViewModel
    {
            public int Id { get; set; }
            [Required(ErrorMessage = "Name is Required")]
            [MaxLength(50, ErrorMessage = "Max Length is 50 Chars")]
            [MinLength(5, ErrorMessage = "Min Length is 5 Chars")]
            public string Name { get; set; } = default!;
            [Range(18, 40)]
            public int Age { get; set; }
            public string Address { get; set; } = default!;
            [DataType(DataType.Currency)]
            public decimal Salary { get; set; }
            [EmailAddress]
            public string Email { get; set; } = default!;
            [Phone]
            public string PhoneNumber { get; set; } = default!;
            public bool IsActive { get; set; }
            public DateTime HiringDate { get; set; }
            public IFormFile? Image { get; set; }
            public string? ImageName { get; set; }
            [ForeignKey("department")]
            public int? DeptId { get; set; }
            public Department? department { get; set; }
    }
}
