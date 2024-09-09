using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using API.Errors;
using INFRASTRUCTURE.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{

    public class BuggyController : BaseApiController
    {
     
        private readonly StoreContext _context;

        public BuggyController(StoreContext context)
        {
            _context = context;
         
        }

        [HttpGet("notFound")]
        public ActionResult GetnotFoundRequest()
        {
            var thing = _context.Products.Find(42);

            if(thing == null)
            {
                return NotFound(new ApiResponse(404));
            }

            return Ok();
        }

         [HttpGet("testauth")]
         [Authorize]
        public ActionResult<string> GetSecretText()
        {
           
            return "secret stuff";
        }


        [HttpGet("serverError")]
        public ActionResult GetServerError()
        {
            var thing = _context.Products.Find(42);

             var thing2 = thing.ToString();

            return Ok();
        }


        [HttpGet("badrequest")]
        public ActionResult GetBadRequest()
        {
            return BadRequest(new ApiResponse(400));
        }

        [HttpGet("badrequest/{Id}")]
        public ActionResult GetBadRequest(int Id)
        {
            return Ok();
        }

        
    }
}