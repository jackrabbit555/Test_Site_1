using Test_Site_1.Common.Dto;

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Test_Site_1.Application.Application.Services.Users.Queries.GetRoles
{
    public interface IGetRolesService
    {
        ResultDto<List<RolesDto>> Execute();
    }
}
