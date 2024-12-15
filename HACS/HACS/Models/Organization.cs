using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HACS.Models
{
    public class Organization
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Website { get; set; } = string.Empty;
        public string VatIN { get; set; } = string.Empty;
    }
}