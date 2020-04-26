using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Helpers;
using Api.Models.Users;
using Api.Resources;
using AutoMapper;
using BusinessLogic.Enums;
using BusinessLogic.Models;
using BusinessLogic.Services;
using BusinessLogic.Services.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService userService;
        private Helpers.IUrlHelper urlHelper;
        private IPropertyMappingService propertyMappingService;
        private readonly ISecurityService securityService;
        private readonly IMapper mapper;
        private readonly IStringLocalizer<UsersController> localizer;

        public UsersController(IStringLocalizer<UsersController> localizer,IUserService userService, Helpers.IUrlHelper urlHelper, IPropertyMappingService propertyMappingService, ISecurityService securityService, IMapper mapper)
        {
            this.userService = userService;
            this.urlHelper = urlHelper;
            this.propertyMappingService = propertyMappingService;
            this.securityService = securityService;
            this.mapper = mapper;
            this.localizer = localizer;
        }

        [HttpGet(Name = "GetUsers")]
        [Authorize(Roles = Role.Administrator)]
        public IActionResult Get([FromQuery] UserResourceParameters resourceParameters)
        {
            if (!propertyMappingService.ValidMappingExistsFor<User>(resourceParameters.OrderBy)) return BadRequest();

            var data = userService.GetAll(resourceParameters);

            var previousPageLink = data.HasPrevious ? urlHelper.CreateUserResourceUri("GetUsers", ResourceUriType.PreviousPage, resourceParameters) : null;

            var nextPageLink = data.HasNext ? urlHelper.CreateUserResourceUri("GetUsers", ResourceUriType.PreviousPage, resourceParameters) : null;
            var paginationMetadata = new
            {
                totalCount = data.TotalCount,
                pageSize = data.PageSize,
                currentPage = data.CurrentPage,
                totalPages = data.TotalPages,
                previousPageLink,
                nextPageLink
            };

            Response.Headers.Add("X-Pagination", Newtonsoft.Json.JsonConvert.SerializeObject(paginationMetadata));

            List<UserViewModel> resources = mapper.Map<List<UserViewModel>>(data);

            return Ok(resources);

        }

        [HttpGet("{id}", Name = "GetUser")]
        [Authorize(Roles = Role.Administrator)]
        public async Task<IActionResult> GetOne(int id)
        {
            User user = await userService.GetOneAsync(id);

            if (user == null) return NotFound();

            UserViewModel resource = mapper.Map<UserViewModel>(user);

            return Ok(resource);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator")]
        public IActionResult Delete(int id)
        {
            var data = userService.GetOne(id);

            if (data == null) return NotFound();

            userService.Delete(data);
            userService.Save();

            return NoContent();
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Administrator")]
        public IActionResult Update(int id, [FromBody] UpdateViewModel model)
        {
            var data = userService.GetOne(id);

            if (data == null) return BadRequest();

            User existingAnotherUser = userService.GetOne(model.Email);

            if (existingAnotherUser != null && existingAnotherUser.ID != id)
            {
                ModelState.AddModelError("Email", localizer[ResourceKeys.UserExists].Value);
                Response.StatusCode = 400;
            }
            if (!ModelState.IsValid) return new Helpers.UnprocessableEntityObjectResult(ModelState);

            User entity = mapper.Map(model, data);

            userService.Update(entity);

            if (!userService.Save()) throw new Exception("Updating application failed on save.");

            return NoContent();
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> CreateUser([FromBody] CreateViewModel model)
        {
            if (model == null) return BadRequest();

            if (model.Email != String.Empty && userService.GetOne(model.Email) != null)
            {
                ModelState.AddModelError("Email", localizer[ResourceKeys.UserExists].Value);
                Response.StatusCode = 400;
            }

            if (!ModelState.IsValid) return new Helpers.UnprocessableEntityObjectResult(ModelState);

            User entity = mapper.Map<User>(model);
            entity.Created = DateTime.Now;
            entity.Password = securityService.HashPassword(model.Password);

            var result = await userService.AddAsync(entity);

            if (!userService.Save()) throw new Exception("Creating new user failed on save.");

            UserViewModel resource = mapper.Map<UserViewModel>(entity);

            return CreatedAtRoute("GetUser", new { id = resource.ID }, resource);
        }

    }
}