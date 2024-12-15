using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using NetTopologySuite.Geometries;

namespace HACS.Models
{
    [Table("Resources")]
    public class Resource
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float Amount { get; set; }
        public string Unit { get; set; }
        public int OrganizationId { get; set; }
        public Point Location { get; set; }
        public Organization Organization { get; set; }
    }
}