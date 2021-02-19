﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebServer.Exceptions;
using WebServer.Interfaces;
using WebServer.Models.DBModels;
using WebServer.Models.ResponseModels;

namespace WebServer.Controllers
{
    [Route("api/groups")]
    [ApiController]
    public class GroupController : ControllerBase
    {
        private IAuthenticationService _authService;
        private IGroupService _groupService;

        public GroupController(IAuthenticationService authService, IGroupService groupService)
        {
            _authService = authService;
            _groupService = groupService;
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetUserGroups()
        {
            try
            {
                var principal = HttpContext.User;
                int userId = int.Parse(_authService.GetPrincipalClaim(principal, ClaimTypes.NameIdentifier));
                Response<List<GroupModel>> response = _groupService.GetUserGroups(userId);
                return StatusCode(response.Status, response.Data);
            }
            catch (HttpException exception)
            {
                return StatusCode(exception.Status, exception.Message);
            }
            catch (Exception exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, exception.Message);
            }
        }

        [Authorize]
        [HttpGet("{groupId}")]
        public IActionResult GetGroupInfo(int groupId)
        {
            try
            {
                var principal = HttpContext.User;
                int userId = int.Parse(_authService.GetPrincipalClaim(principal, ClaimTypes.NameIdentifier));
                Response<Group> response = _groupService.GetGroupInfo(userId, groupId);
                return StatusCode(response.Status, response.Data);
            }
            catch (HttpException exception)
            {
                return StatusCode(exception.Status, exception.Message);
            }
            catch (Exception exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, exception.Message);
            }
        }

        [Authorize]
        [HttpPost]
        public IActionResult CreateGroup([FromBody] Group group)
        {
            try
            {
                var principal = HttpContext.User;
                int userId = int.Parse(_authService.GetPrincipalClaim(principal, ClaimTypes.NameIdentifier));
                Response<string> response = _groupService.CreateGroup(userId, group);
                return StatusCode(response.Status, response.Data);
            }
            catch (HttpException exception)
            {
                return StatusCode(exception.Status, exception.Message);
            }
            catch (Exception exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, exception.Message);
            }
        }


    }
}
