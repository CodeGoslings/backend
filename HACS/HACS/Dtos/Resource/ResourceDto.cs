using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HACS.Dtos.Resource
{
    public class ResourceDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float Amount { get; set; }
        public string Unit { get; set; }
        public int OrganizationId { get; set; }
        public PointDto Location { get; set; }
    }
    public class PointDto
    {
        public double Longitude { get; set; }
        public double Latitude { get; set; }
    }
}