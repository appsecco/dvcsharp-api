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
   public class RegistrationsController : Controller
   {
      private readonly GenericDataContext _context;

      public RegistrationsController(GenericDataContext context)
      {
         _context = context;
      }

      [HttpPost]
      public IActionResult Post([FromBody] RegistrationRequest registrationRequest)
      {
         if(!ModelState.IsValid)
         {
            return BadRequest(ModelState);
         }

         var existingUser = _context.Users.
            Where(b => b.email == registrationRequest.email).
            FirstOrDefault();

         if(existingUser != null) {
            ModelState.AddModelError("email", "Email address is already taken");
            return BadRequest(ModelState);
         }

         var user = new Models.User();
         user.name = registrationRequest.name;
         user.email = registrationRequest.email;
         user.role = Models.User.RoleUser;
         user.createdAt = user.updatedAt = DateTime.Now;
         user.updatePassword(registrationRequest.password);

         _context.Users.Add(user);
         _context.SaveChanges();

         return Ok(user);
      }
   }
}