using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HACS.Dtos.Assignment;
using HACS.Models;

namespace HACS.Mappers
{
    public static class AssignmentMappers
    {
        public static AssignmentDto ToAssignmentDto(this Assignment assignmentModel)
        {
            return new AssignmentDto
            {
                Id = assignmentModel.Id,
                Description = assignmentModel.Description,
                DueDate = assignmentModel.DueDate,
                VolunteerId = assignmentModel.VolunteerId
            };
        }
        public static Assignment ToAssignmentFromCreate(this CreateAssignmentDto createDto, int volunteerId)
        {
            return new Assignment
            {
                Description = createDto.Description,
                DueDate = createDto.DueDate,
                VolunteerId = volunteerId
            };
        }
        public static Assignment ToAssignmentFromUpdate(this UpdateAssignmentDto updateDto)
        {
            return new Assignment
            {
                Description = updateDto.Description,
                DueDate = updateDto.DueDate,
                VolunteerId = updateDto.VolunteerId
            };
        }


    }
}