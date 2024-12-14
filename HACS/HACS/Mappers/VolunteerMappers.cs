using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HACS.Dtos.Volunteer;
using HACS.Models;

namespace HACS.Mappers
{
    public static class VolunteerMappers
    {
        public static VolunteerDto ToVolunteerDto(this Volunteer volunteerModel)
        {
            return new VolunteerDto
            {
                Id = volunteerModel.Id,
                FirstName = volunteerModel.FirstName,
                LastName = volunteerModel.LastName,
                Email = volunteerModel.Email
            };
        }
    }
}