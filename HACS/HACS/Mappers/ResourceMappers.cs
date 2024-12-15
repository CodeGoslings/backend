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
            OrganizationId = resource.OrganizationId
        };

        public static Resource ToResourceFromCreate(this CreateResourceDto resourceDto) => new Resource
        {
            Name = resourceDto.Name,
            Amount = resourceDto.Amount,
            Unit = resourceDto.Unit,
            OrganizationId = resourceDto.OrganizationId

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