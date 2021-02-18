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
using WebServer.Models.DBModels;
using WebServer.Models.ResponseModels;

namespace WebServer.Controllers
{
    [Route("api/directs")]
    [ApiController]
    public class DirectController : ControllerBase
    {
        private IAuthenticationService _authService;
        private IDirectService _directService;

        public DirectController(IAuthenticationService authService, IDirectService directService)
        {
            _authService = authService;
            _directService = directService;
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetUserDirects()
        {
            try
            {
                var principal = HttpContext.User;
                string userId = _authService.GetPrincipalClaim(principal, ClaimTypes.NameIdentifier);
                Response<List<DirectModel>> response = _directService.GetUserDirects(int.Parse(userId));
                return StatusCode(response.Status, response.Data);
            }
            catch (HttpException exception)
            {
                return StatusCode(exception.Status, exception.Message);
            }
        }   

        [Authorize]
        [HttpGet("{directId}")]
        public IActionResult GetDirectMessages(int directId)
        {
            try
            {
                Response<List<DirectMessage>> response = _directService.GetDirectMessages(directId);
                return StatusCode(response.Status, response.Data);
            }
            catch (HttpException exception)
            {
                return StatusCode(exception.Status, exception.Message);
            }
        }
    }
}
