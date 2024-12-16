using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HACS.Dtos.VolunteerContract;
using HACS.Models;

namespace HACS.Mappers
{
    public static class VolunteerContractMappers
    {
        public static VolunteerContractDto ToVolunteerContractDto(this VolunteerContract model)
        {
            return new VolunteerContractDto
            {
                Id = model.Id,
                OrganizationId = model.OrganizationId,
                VolunteerId = model.VolunteerId,
                From = model.From,
                To = model.To
            };
        }

        public static VolunteerContract ToVolunteerContractFromCreate(this CreateVolunteerContractDto dto) => new VolunteerContract
        {
            OrganizationId = dto.OrganizationId,
            VolunteerId = dto.VolunteerId,
            From = dto.From,
            To = dto.To
        };
        public static VolunteerContract ToVolunteerContractFromUpdate(this UpdateVolunteerContractDto dto) => new VolunteerContract
        {
            OrganizationId = dto.OrganizationId,
            VolunteerId = dto.VolunteerId,
            From = dto.From,
            To = dto.To
        };
    }
}