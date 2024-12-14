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
        public static Assignment ToAssignmentFromCreateDto(this CreateAssignmentRequestDto createAssignmentRequestDto)
        {
            return new Assignment
            {
                Description = createAssignmentRequestDto.Description,
                DueDate = createAssignmentRequestDto.DueDate,
                VolunteerId = createAssignmentRequestDto.VolunteerId
            };
        }


    }
}