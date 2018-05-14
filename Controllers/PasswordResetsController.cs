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

      [HttpPut]
      public IActionResult Put([FromBody] PasswordResetRequest passwordResetRequest)
      {
         if(String.IsNullOrEmpty(passwordResetRequest.key)) {
            ModelState.AddModelError("key", "Key is required for password reset");
            return BadRequest(ModelState);
         }

         if(String.IsNullOrEmpty(passwordResetRequest.password) || 
            String.IsNullOrEmpty(passwordResetRequest.passwordConfirmation)) {
               ModelState.AddModelError("password", "Password is required");
               ModelState.AddModelError("passwordConfirmation", "Password confirmation is required");
               return BadRequest(ModelState);
         }

         if(passwordResetRequest.password != passwordResetRequest.passwordConfirmation) {
            ModelState.AddModelError("password", "Password must match password confirmation");
            return BadRequest(ModelState);
         }

         var resetRequest = _context.PasswordResetRequests.
            Where(b => b.key == passwordResetRequest.key).FirstOrDefault();

         if(resetRequest == null) {
            ModelState.AddModelError("key", "Key not found in system");
            return BadRequest(ModelState);
         }

         var existingUser = _context.Users.
            Where(b => b.email == resetRequest.email).
            FirstOrDefault();

         existingUser.updatePassword(passwordResetRequest.password);

         _context.Users.Update(existingUser);
         _context.SaveChanges();

         return Ok("Password updated successfully for userId: " + existingUser.ID.ToString());
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

         if(exitingUser == null) {
            ModelState.AddModelError("email", "Email address does not exist");
            return BadRequest(ModelState);
         }

         var md5 = MD5.Create();
         var hash = md5.ComputeHash(System.Text.Encoding.ASCII.GetBytes(passwordResetRequest.email));

         passwordResetRequest.key = BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
         passwordResetRequest.createdAt = passwordResetRequest.updatedAt = DateTime.Now;

         _context.PasswordResetRequests.Add(passwordResetRequest);
         _context.SaveChanges();

         return Ok("An email with password reset link has been sent.");
      }
   }
}