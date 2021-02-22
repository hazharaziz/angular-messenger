using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebServer.Exceptions;
using WebServer.Interfaces;
using WebServer.Messages;
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
                int userId = int.Parse(_authService.GetClaim(HttpContext.User, ClaimTypes.NameIdentifier));
                Response<List<GroupModel>> response = _groupService.GetUserGroups(userId);
                return StatusCode(response.Status, response.Data);
            }
            catch (HttpException exception)
            {
                return StatusCode(exception.Status, exception.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status400BadRequest, Alerts.SomethingWentWrong);
            }
        }

        [Authorize]
        [HttpGet("{groupId}")]
        public IActionResult GetGroupInfo(int groupId)
        {
            try
            {
                int userId = int.Parse(_authService.GetClaim(HttpContext.User, ClaimTypes.NameIdentifier));
                Response<GroupInfoModel> response = _groupService.GetGroupInfo(userId, groupId);
                return StatusCode(response.Status, response.Data);
            }
            catch (HttpException exception)
            {
                return StatusCode(exception.Status, exception.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status400BadRequest, Alerts.SomethingWentWrong);
            }
        }

        [Authorize]
        [HttpGet("{groupId}/friends")]
        public IActionResult GetAvailableFriends(int groupId)
        {
            try
            {
                int userId = int.Parse
                    (_authService.GetClaim(HttpContext.User, ClaimTypes.NameIdentifier));
                Response<List<UserModel>> response = 
                    _groupService.GetAvailableFriends(userId, groupId);
                return StatusCode(response.Status, response.Data);
            }
            catch (HttpException exception)
            {
                return StatusCode(exception.Status, exception.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status400BadRequest, Alerts.SomethingWentWrong);
            }
        }

        [Authorize]
        [HttpPost]
        public IActionResult CreateGroup([FromBody] Group group)
        {
            try
            {
                int userId = int.Parse(_authService.GetClaim(HttpContext.User, ClaimTypes.NameIdentifier));
                Response<string> response = _groupService.CreateGroup(userId, group);
                return StatusCode(response.Status, response.Data);
            }
            catch (HttpException exception)
            {
                return StatusCode(exception.Status, exception.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status400BadRequest, Alerts.SomethingWentWrong);
            }
        }

        [Authorize]
        [HttpPut("{groupId}")]
        public IActionResult EditGroup(int groupId, [FromBody] Group editedGroup)
        {
            try
            {
                int userId = int.Parse(_authService.GetClaim(HttpContext.User, ClaimTypes.NameIdentifier));
                Response<string> response = _groupService.EditGroup(userId, groupId, editedGroup);
                return StatusCode(response.Status, response.Data);
            }
            catch (HttpException exception)
            {
                return StatusCode(exception.Status, exception.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status400BadRequest, Alerts.SomethingWentWrong);
            }
        }

        [Authorize]
        [HttpDelete("{groupId}")]
        public IActionResult DeleteGroup(int groupId)
        {
            try
            {
                int userId = int.Parse(_authService.GetClaim(HttpContext.User, ClaimTypes.NameIdentifier));
                Response<string> response = _groupService.DeleteGroup(userId, groupId);
                return StatusCode(response.Status, response.Data);
            }
            catch (HttpException exception)
            {
                return StatusCode(exception.Status, exception.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status400BadRequest, Alerts.SomethingWentWrong);
            }
        }

        [Authorize]
        [HttpPost("{groupId}/add-member")]
        public IActionResult AddMemberToGroup(int groupId, [FromBody] List<int> memberIds)
        {
            try
            {
                int userId = int.Parse(_authService.GetClaim(HttpContext.User, ClaimTypes.NameIdentifier));
                Response<string> response = _groupService.AddMembersToGroup(userId, groupId, memberIds);
                return StatusCode(response.Status, response.Data);
            }
            catch (HttpException exception)
            {
                return StatusCode(exception.Status, exception.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status400BadRequest, Alerts.SomethingWentWrong);
            }
        }


        [Authorize]
        [HttpDelete("{groupId}/remove-member/{memberId}")]
        public IActionResult RemoveMemberFromGroup(int groupId, int memberId)
        {
            try
            {
                int userId = int.Parse(_authService.GetClaim(HttpContext.User, ClaimTypes.NameIdentifier));
                Response<string> response = _groupService.RemoveMemberFromGroup(userId, groupId, memberId);
                return StatusCode(response.Status, response.Data);
            }
            catch (HttpException exception)
            {
                return StatusCode(exception.Status, exception.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status400BadRequest, Alerts.SomethingWentWrong);
            }
        }

        [Authorize]
        [HttpGet("{groupId}/messages")]
        public IActionResult GetGroupMessages(int groupId)
        {
            try
            {
                int userId = int.Parse(_authService.GetClaim(HttpContext.User, ClaimTypes.NameIdentifier));
                Response<List<GroupMessage>> response = _groupService.GetGroupMessages(userId, groupId);
                return StatusCode(response.Status, response.Data);
            }
            catch (HttpException exception)
            {
                return StatusCode(exception.Status, exception.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status400BadRequest, Alerts.SomethingWentWrong);
            }
        }

        [Authorize]
        [HttpPost("{groupId}/messages")]
        public IActionResult SendGroupMessage(int groupId, [FromBody] GroupMessage groupMessage)
        {
            try
            {
                int userId = int.Parse(_authService.GetClaim(HttpContext.User, ClaimTypes.NameIdentifier));
                Response<string> response = _groupService.SendGroupMessage(userId, groupId, groupMessage);
                return StatusCode(response.Status, response.Data);
            }
            catch (HttpException exception)
            {
                return StatusCode(exception.Status, exception.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status400BadRequest, Alerts.SomethingWentWrong);
            }
        }

        [Authorize]
        [HttpPut("{groupId}/messages/{messageId}")]
        public IActionResult EditGroupMessage
            (int groupId, int messageId, [FromBody] GroupMessage groupMessage)
        {
            try
            {
                int userId = int.Parse
                    (_authService.GetClaim(HttpContext.User, ClaimTypes.NameIdentifier));
                Response<string> response = 
                    _groupService.EditGroupMessage(userId, messageId, groupMessage);
                return StatusCode(response.Status, response.Data);
            }
            catch (HttpException exception)
            {
                return StatusCode(exception.Status, exception.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status400BadRequest, Alerts.SomethingWentWrong);
            }
        }

        [Authorize]
        [HttpDelete("{groupId}/messages/{messageId}")]
        public IActionResult DeleteGroupMessage(int groupId, int messageId)
        {
            try
            {
                int userId = int.Parse(_authService.GetClaim(HttpContext.User, ClaimTypes.NameIdentifier));
                Response<string> response = _groupService.DeleteGroupMessage(userId, groupId, messageId);
                return StatusCode(response.Status, response.Data);
            }
            catch (HttpException exception)
            {
                return StatusCode(exception.Status, exception.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status400BadRequest, Alerts.SomethingWentWrong);
            }
        }

        [Authorize]
        [HttpDelete("{groupId}/messages")]
        public IActionResult ClearGroupHistory(int groupId)
        {
            try
            {
                int userId = int.Parse(_authService.GetClaim(HttpContext.User, ClaimTypes.NameIdentifier));
                Response<string> response = _groupService.ClearGroupHistory(userId, groupId);
                return StatusCode(response.Status, response.Data);
            }
            catch (HttpException exception)
            {
                return StatusCode(exception.Status, exception.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status400BadRequest, Alerts.SomethingWentWrong);
            }
        }

        [Authorize]
        [HttpDelete("{groupId}/leave")]
        public IActionResult LeaveGroup(int groupId)
        {
            try
            {
                int userId = int.Parse(_authService.GetClaim(HttpContext.User, ClaimTypes.NameIdentifier));
                Response<string> response = _groupService.LeaveGroup(userId, groupId);
                return StatusCode(response.Status, response.Data);
            }
            catch (HttpException exception)
            {
                return StatusCode(exception.Status, exception.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status400BadRequest, Alerts.SomethingWentWrong);
            }
        }

    }
}
