using Mamba.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace Mamba.Models
{
    public class Position : BaseEntity
    {
        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "only letters can be used")]
        public string Name { get; set; }

        //relational
        public List<Employee> Employees { get; set; }
    }
}
