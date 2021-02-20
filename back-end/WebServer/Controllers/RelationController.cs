﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using WebServer.Exceptions;
using WebServer.Interfaces;
using WebServer.Messages;
using WebServer.Models.DBModels;
using WebServer.Models.ResponseModels;

namespace WebServer.Controllers
{
    [Route("api/relations")]
    [ApiController]
    public class RelationController : ControllerBase
    {
        private IAuthenticationService _authService;
        private IRelationService _relationService;

        public RelationController(IAuthenticationService authService, IRelationService relationService)
        {
            _authService = authService;
            _relationService = relationService;
        }

        [Authorize]
        [HttpGet("followers")]
        public IActionResult GetFollowers()
        {
            try
            {
                var principal = HttpContext.User;
                string userId = _authService.GetClaim(principal, ClaimTypes.NameIdentifier);
                Response<List<UserModel>> response = _relationService.GetFollowers(userId);
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
        [HttpGet("followings")]
        public IActionResult GetFollowings()
        {
            try
            {
                var principal = HttpContext.User;
                string userId = _authService.GetClaim(principal, ClaimTypes.NameIdentifier);
                Response<List<UserModel>> response = _relationService.GetFollowings(userId);
                return StatusCode(StatusCodes.Status200OK, response.Data);
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
        [HttpGet("requests")]
        public IActionResult GetFollowRequests()
        {
            try
            {
                var principal = HttpContext.User;
                string userId = _authService.GetClaim(principal, ClaimTypes.NameIdentifier);
                Response<List<UserModel>> response = _relationService.GetFollowRequests(userId);
                return StatusCode(StatusCodes.Status200OK, response.Data);
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
        [HttpPost("send-request/{targetId}")]
        public IActionResult SendFollowRequest(int targetId)
        {            
            try
            {
                int userId = int.Parse(_authService.GetClaim(HttpContext.User, ClaimTypes.NameIdentifier));
                Response<string> response = _relationService.SendFollowRequest(targetId, userId);
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
        [HttpPut("accept-request/{requestId}")]
        public IActionResult AcceptFollowRequest(int requestId)
        {
            try
            {
                int userId = int.Parse(_authService.GetClaim(HttpContext.User, ClaimTypes.NameIdentifier));
                Response<string> response = _relationService.AcceptFollowRequest(userId, requestId);
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
        [HttpDelete("reject-request/{requestId}")]
        public IActionResult RejectFollowRequest(int requestId)
        {
            try
            {
                int userId = int.Parse(_authService.GetClaim(HttpContext.User, ClaimTypes.NameIdentifier));
                Response<string> response = _relationService.RejectFollowRequest(userId, requestId);
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
        [HttpDelete("cancel-request/{requestId}")]
        public IActionResult CancelFollowRequest(int requestId)
        {
            try
            {
                int userId = int.Parse(_authService.GetClaim(HttpContext.User, ClaimTypes.NameIdentifier));
                Response<string> response = _relationService.CancelRequest(requestId, userId);
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
        [HttpDelete("unfollow/{id}")]
        public IActionResult Unfollow(int followingId)
        {
            try
            {
                int userId = int.Parse(_authService.GetClaim(HttpContext.User, ClaimTypes.NameIdentifier));
                Response<string> response = _relationService.DeleteRelation(followingId, userId);
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
        [HttpDelete("delete-follower/{followerId}")]
        public IActionResult DeleteFollower(int followerId)
        {
            try
            {
                int userId = int.Parse(_authService.GetClaim(HttpContext.User, ClaimTypes.NameIdentifier));
                Response<string> response = _relationService.DeleteRelation(userId, followerId);
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
