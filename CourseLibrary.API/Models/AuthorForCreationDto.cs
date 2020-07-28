using System;
using System.Collections.Generic;

namespace CourseLibrary.API.Models
{
    public class AuthorForCreationDto : BaseAuthorDto
    {

        public ICollection<CourseForCreationDto> Courses { get; set; }
          = new List<CourseForCreationDto>();

    }
}
