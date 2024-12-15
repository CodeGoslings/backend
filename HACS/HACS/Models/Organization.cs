using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HACS.Models
{
    [Table("Organizations")]
    public class Organization
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Website { get; set; } = string.Empty;
        public string VatIN { get; set; } = string.Empty;
        public List<VolunteerContract> VolunteerContracts { get; set; } = new List<VolunteerContract>();
        public List<Resource> Resources { get; set; } = new List<Resource>();
    }
}