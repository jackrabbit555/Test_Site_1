﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

using Test_Site_1.Application.Application.Services.Users.Queries.GetRoles;
using Test_Site_1.Application.Services.Users.Commands.EditUser;
using Test_Site_1.Application.Services.Users.Commands.RemoveUser;
using Test_Site_1.Application.Services.Users.Commands.RgegisterUser;
using Test_Site_1.Application.Services.Users.Commands.UserSatusChange;
using Test_Site_1.Application.Services.Users.Queries.GetUsers;

namespace Test_Site.Areas.Admin.Controllers
{
    
        [Area("Admin")]
        public class UsersController : Controller
        {
            private readonly IGetUsersService _getUsersService;
            private readonly IGetRolesService _getRolesService;
            private readonly IRegisterUserService _registerUserService;
            private readonly IRemoveUserService _removeUserService;
            private readonly IUserSatusChangeService _userSatusChangeService;
            private readonly IEditUserService _editUserService;
            public UsersController(IGetUsersService getUsersService
                , IGetRolesService getRolesService
                , IRegisterUserService registerUserService
                , IRemoveUserService removeUserService
                , IUserSatusChangeService userSatusChangeService
                , IEditUserService editUserService)
            {
                _getUsersService = getUsersService;
                _getRolesService = getRolesService;
                _registerUserService = registerUserService;
                _removeUserService = removeUserService;
                _userSatusChangeService = userSatusChangeService;
                _editUserService = editUserService;
            }


            public IActionResult Index(string serchkey, int page = 1)
            {
                return View(_getUsersService.Execute(new RequestGetUserDto
                {
                    Page = page,
                    SearchKey = serchkey,
                }));
            }





            [HttpGet]
            public IActionResult Create()
            {
                ViewBag.Roles = new SelectList(_getRolesService.Execute().Data, "Id", "Name");
                return View();
            }


            [HttpPost]
            public IActionResult Create(string Email, string FullName, long RoleId, string Password, string RePassword)
            {
                var result = _registerUserService.Execute(new RequestRegisterUserDto
                {
                    Email = Email,
                    FullName = FullName,
                    roles = new List<RolesInRegisterUserDto>()
                   {
                        new RolesInRegisterUserDto
                        {
                             Id= RoleId
                        }
                   },
                    Password = Password,
                    RePasword = RePassword,
                });
                return Json(result);
            }

            [HttpPost]
            public IActionResult Delete(long UserId)
            {
                return Json(_removeUserService.Execute(UserId));
            }

            [HttpPost]
            public IActionResult UserSatusChange(long UserId)
            {
                return Json(_userSatusChangeService.Execute(UserId));
            }

            [HttpPost]
            public IActionResult Edit(long UserId, string Fullname)
            {
                return Json(_editUserService.Execute(new RequestEdituserDto
                {
                    Fullname = Fullname,
                    UserId = UserId,
                }));
            }

        }
    }

