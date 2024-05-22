using System.Collections.Generic;

namespace Test_Site_1.Application.Services.Users.Queries.GetUsers
{
    public class ReslutGetUserDto
    {
        public List<GetUsersDto> Users { get; set; }
        public int Rows { get; set; }
    }
}
