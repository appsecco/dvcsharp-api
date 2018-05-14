using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using dvcsharp_core_api.Models;
using dvcsharp_core_api.Data;

namespace dvcsharp_core_api
{
   [Route("api/[controller]")]
   public class PasswordResetsController : Controller
   {
      private readonly GenericDataContext _context;

      public PasswordResetsController(GenericDataContext context)
      {
         _context = context;
      }

      [HttpPost]
      public IActionResult Post([FromBody] PasswordResetRequest passwordResetRequest)
      {
         if(!ModelState.IsValid)
         {
            return BadRequest(ModelState);
         }

         var exitingUser = _context.Users.
            Where(b => b.email == passwordResetRequest.email).
            FirstOrDefault();

         if(exitingUser != null) {
            ModelState.AddModelError("email", "Email address does not exist");
            return BadRequest(ModelState);
         }

         var md5 = MD5.Create();
         var hash = md5.ComputeHash(System.Text.Encoding.ASCII.GetBytes(passwordResetRequest.email));

         passwordResetRequest.key = BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
         _context.PasswordResetRequests.Add(passwordResetRequest);
         _context.SaveChanges();

         return Ok();
      }
   }
}