using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
	public class Department
	{
        public int Id { get; set; }
        [Required(ErrorMessage ="Name is Required")]
        [MaxLength(50)]
        public string Name { get; set; } = default!;
        [Required(ErrorMessage ="Code is Required")]
        public string Code { get; set; } = default!;
		public DateTime DateOfCreation { get; set; }
        public ICollection<Employee> employees { get; set; } = new HashSet<Employee>();
    }
}
