﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.UserUseCases.AssignRole
{
    public class AssignRoleResponse
    {
        public bool IsSuccess { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }

}
