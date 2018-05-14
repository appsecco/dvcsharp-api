using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using dvcsharp_core_api.Models;
using dvcsharp_core_api.Data;

namespace dvcsharp_core_api
{
   [Route("api/[controller]")]
   public class TokensController : Controller
   {
      private readonly GenericDataContext _context;

      public TokensController(GenericDataContext context)
      {
         _context = context;
      }

      [Authorize]
      [HttpGet("tokenInfo")]
      public IActionResult TokenInfo()
      {
         if(HttpContext.User.HasClaim(c => c.Type == "name")) {
            var email = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "name").Value;
            var currentUser = _context.Users.
               Where(b => b.email == email).
               FirstOrDefault();

            if(currentUser == null)
               return NotFound();

            return Json(currentUser);
         }
         else {
            return BadRequest();
         }
      }
   }
}