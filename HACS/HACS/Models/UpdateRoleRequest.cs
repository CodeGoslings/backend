using System.ComponentModel.DataAnnotations;

namespace HACS.Models
{
    public class UpdateRoleRequest
    {
        [Required]
        [StringLength(50)]
        public string NewRoleName { get; set; }
    }
}
