using System.ComponentModel.DataAnnotations;

namespace HACS.Models
{
    public class CreateRoleRequest
    {
        [Required]
        [StringLength(50)]
        public string RoleName { get; set; }
    }
}
