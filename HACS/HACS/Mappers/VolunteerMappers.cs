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
                Email = volunteerModel.Email,
                Assignments = volunteerModel.Assignments.Select(x => x.ToAssignmentDto()).ToList()
            };
        }

        public static Volunteer ToVolunteerFromCreate(this CreateVolunteerDto volunteerDto) => new Volunteer
        {
            FirstName = volunteerDto.FirstName,
            LastName = volunteerDto.LastName,
            Email = volunteerDto.Email
        };
        public static Volunteer ToVolunteerFromUpdate(this UpdateVolunteerDto volunteerDto) => new Volunteer
        {
            FirstName = volunteerDto.FirstName,
            LastName = volunteerDto.LastName,
            Email = volunteerDto.Email
        };
    }
}