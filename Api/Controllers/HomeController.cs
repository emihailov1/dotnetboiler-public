using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic.Enums;
using BusinessLogic.Helpers;
using BusinessLogic.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{

    [Route("api/home")]
    public class HomeController : Controller
    {
        private readonly IUserService userService;

        public HomeController(IUserService userService)
        {
            this.userService = userService;
        }

        [Authorize]
        public async Task<string> Index()
        {
            int userId = int.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "user_id").Value);
            var user = await userService.GetOneAsync(userId);
            return $"Hello {user.Email}.";
        }

        [Route("admin")]
        [Authorize(Roles = Role.Administrator)]
        public async Task<string> Admin()
        {
            int userId = int.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "user_id").Value);
            var user = await userService.GetOneAsync(userId);
            return $"Hello {user.Email}. Your Role is:  {user.Role}";
        }
    }
}