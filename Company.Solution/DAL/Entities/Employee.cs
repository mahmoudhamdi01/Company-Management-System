using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Identity.Client.AuthScheme.PoP;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Employee
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = default!;
        public int Age { get; set; }
        public string Address { get; set; } = default!;
        public decimal Salary { get; set; }
        public string Email { get; set; } = default!;
        public string PhoneNumber { get; set; } = default!;
        public bool IsActive { get; set; }
        public DateTime HiringDate { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now;
        public string? ImageName { get; set; }
        [ForeignKey("department")]
        public int? DeptId { get; set; }
        public Department? department { get; set; }
    }
}
