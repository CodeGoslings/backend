using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HACS.Dtos.Organization;
using HACS.Models;

namespace HACS.Mappers
{
    public static class OrganizationMappers
    {
        public static OrganizationDto ToOrganizationDto(this Organization organization)
        {
            return new OrganizationDto()
            {
                Id = organization.Id,
                Name = organization.Name,
                Email = organization.Email,
                Website = organization.Website,
                VatIN = organization.VatIN
            };
        }
        public static Organization ToOrganizationFromCreate(this CreateOrganizationDto organizationDto)
        {
            return new Organization()
            {
                Name = organizationDto.Name,
                Email = organizationDto.Email,
                Website = organizationDto.Website,
                VatIN = organizationDto.VatIN
            };
        }
        public static Organization ToOrganizationFromUpdate(this UpdateOrganizationDto organizationDto)
        {
            return new Organization()
            {
                Name = organizationDto.Name,
                Email = organizationDto.Email,
                Website = organizationDto.Website,
                VatIN = organizationDto.VatIN
            };
        }
    }
}