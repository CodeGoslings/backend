using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HACS.Dtos.Resource;
using HACS.Models;

namespace HACS.Mappers
{
    public static class ResourceMappers
    {
        public static ResourceDto ToResourceDto(this Resource resource) => new ResourceDto
        {
            Id = resource.Id,
            Name = resource.Name,
            Amount = resource.Amount,
            Unit = resource.Unit,
            OrganizationId = resource.OrganizationId,
            Location = new PointDto { Longitude = resource.Location.Coordinate.X, Latitude = resource.Location.Coordinate.Y }
        };

        public static Resource ToResourceFromCreate(this CreateResourceDto resourceDto) => new Resource
        {
            Name = resourceDto.Name,
            Amount = resourceDto.Amount,
            Unit = resourceDto.Unit,
            OrganizationId = resourceDto.OrganizationId,
            Location = new NetTopologySuite.Geometries.Point(19.457216, 51.759445) { SRID = 4326 }

        };
        public static Resource ToResourceFromUpdate(this UpdateResourceDto resourceDto) => new Resource
        {
            Name = resourceDto.Name,
            Amount = resourceDto.Amount,
            Unit = resourceDto.Unit,
            OrganizationId = resourceDto.OrganizationId
        };
    }
}