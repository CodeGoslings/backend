using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HACS.Dtos.Resource
{
    public class UpdateResourceDto
    {
        public string Name { get; set; }
        public float Amount { get; set; }
        public string Unit { get; set; }
        public int OrganizationId { get; set; }
    }
}