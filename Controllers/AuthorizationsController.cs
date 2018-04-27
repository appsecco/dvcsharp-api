using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using dvcsharp_core_api.Models;
using dvcsharp_core_api.Data;

namespace dvcsharp_core_api
{
   [Route("api/[controller]")]
   public class AuthorizationsController : Controller
   {
      private readonly GenericDataContext _context;

      public AuthorizationsController(GenericDataContext context)
      {
         _context = context;
      }

      [HttpPost]
      public IActionResult Post([FromBody] AuthorizationRequest authorizationRequest)
      {
         if(!ModelState.IsValid)
         {
            return BadRequest(ModelState);
         }

         var response = dvcsharp_core_api.Models.User.
            authorizeCreateAccessToken(_context, authorizationRequest);
            
         if(response == null) {
            ModelState.AddModelError("password", "Failed to authenticate");
            return BadRequest(ModelState);
         }

         return Ok(response);
      }
   }
}