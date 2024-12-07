using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Store.Application.Interfaces.FacadPattern;
using Store.Application.Services.User.Commands.AddUserService;
using Store.Application.Services.User.Commands.EditUserSerive;
using Store.Application.Services.User.Queries.GetUsersService;
using Store.Common;
using System.Net.Mail;

namespace EndPoint.WebSite.Areas.Admin.Controllers
{
    //[Authorize(Roles = "Admin, Operator")]
    [Area("Admin")]
    public class UsersController : Controller
    {
        private readonly IUserFacadPattern _user;

        public UsersController(IUserFacadPattern user)
        {
            _user = user;
        }
        public IActionResult Index(string searchKey, int page)
        {
            return View(_user.GetUsers.Execute(new RequestGetUserDTO
            {
                Page = page,
                SearchKey = searchKey,
            }));
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Roles = new SelectList(_user.GetRoles.Execute().Data, "Id", "Name");
            return View();
        }

        [HttpPost]
        public IActionResult Create(string Email, string FullName, int RoleId, string Password, string RePassword)
        {
            //checking validation
            {
                var valid = IsValidRequest(FullName, "لطفا نام خود را وارد کنید!");
                if (!valid.isSuccess)
                    return Json(valid);

                valid = IsValidRequest(Email, "لطفا ایمیل خود را وارد کنید!");
                if (!valid.isSuccess)
                    return Json(valid);

                if (!EmailIsValid(Email))
                    valid = IsValidRequest(" ", "ایمیل وارد شد معتبر نمی باشد!");
                if (!valid.isSuccess)
                    return Json(valid);

                valid = IsValidRequest(Password, "لطفا رمزعبور خود را وارد کنید!");
                if (!valid.isSuccess)
                    return Json(valid);

                valid = IsValidRequest(RePassword, "لطفا رمزعبور خود را تکرار کنید!");
                if (!valid.isSuccess)
                    return Json(valid);

                if (Password != RePassword)
                {
                    valid = IsValidRequest(" ", "رمزعبور و تکرار آن مغایرت دارند!");
                    return Json(valid);
                }
            }

            var request = new RequestAddUserDTO() { Email = Email, FullName = FullName, Password = Password };
            List<RoleInAddUserDTO> roleInAddUserDTOs = new List<RoleInAddUserDTO>()
                {
                    new RoleInAddUserDTO()
                    {
                        Id = RoleId,
                    }
                };
            request.Roles = roleInAddUserDTOs;


            var data = _user.AddUser.Execute(request, null);
            return Json(data);
        }
        private ResultDTO<AddUserDTO> IsValidRequest(string s, string Massege)
        {
            if (string.IsNullOrWhiteSpace(s))
                return new Store.Common.ResultDTO<AddUserDTO>
                {
                    isSuccess = false,
                    Message = Massege,
                    Data = new AddUserDTO
                    {
                        Id = 0
                    }
                };

            return new Store.Common.ResultDTO<AddUserDTO>
            {
                isSuccess = true,
                Message = "عملیات با موفقیت انجام شد!",
                Data = new AddUserDTO
                {
                    Id = 0
                }
            };

        }
        private bool EmailIsValid(string emailaddress)
        {
            try
            {
                MailAddress m = new MailAddress(emailaddress);

                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }


        [HttpPost]
        public IActionResult Edit(int Id, string FullName)
        {
            return Json(_user.EditUser.Execute(new RequestEditUserDTO()
            {
                id = Id,
                fullName = FullName
            }));
        }

        [HttpPost]
        public IActionResult Delete(int Id)
        {
            return Json(_user.DeleteUser.Execute(Id));
        }

        [HttpPost]
        public IActionResult UserStatusChange(int Id)
        {
            return Json(_user.UserStatus.Execute(Id));
        }
    }
}
