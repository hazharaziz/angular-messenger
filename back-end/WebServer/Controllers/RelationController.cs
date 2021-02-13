using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebServer.Interfaces;

namespace WebServer.Controllers
{
    [Route("api/relations")]
    [ApiController]
    public class RelationController : ControllerBase
    {
        private IRelationAPI _relations;

        public RelationController(IRelationAPI relations)
        {
            _relations = relations;
        }
    }
}
