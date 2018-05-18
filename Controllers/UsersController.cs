using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json.Linq;
using dvcsharp_core_api.Models;
using dvcsharp_core_api.Data;

namespace dvcsharp_core_api
{
   [Route("api/[controller]")]
   public class UsersController : Controller
   {
      private readonly GenericDataContext _context;

      public UsersController(GenericDataContext context)
      {
         _context = context;
      }

      [Authorize]
      [HttpGet]
      public IEnumerable<User> Get()
      {
         return _context.Users.ToList();
      }

      [Authorize]
      [HttpPut("{id}")]
      public IActionResult Put(int id, [FromBody] Models.UserUpdateRequest user)
      {
         if(!ModelState.IsValid) {
            return BadRequest(ModelState);
         }

         var existingUser = _context.Users.SingleOrDefault(m => m.ID == id);
         if(existingUser == null) {
            return NotFound();
         }

         existingUser.name = user.name;
         existingUser.email = user.email;
         existingUser.role = user.role;
         existingUser.updatePassword(user.password);

         _context.Users.Update(existingUser);
         _context.SaveChanges();

         return Ok(existingUser);
      }

      [Authorize]
      [HttpDelete("{id}")]
      public IActionResult Delete(int id)
      {
         User user = _context.Users.SingleOrDefault(m => m.ID == id);

         if(user == null) {
            return NotFound();
         }

         _context.Users.Remove(user);
         _context.SaveChanges();

         return Ok(user);
      }

      [Authorize]
      [HttpGet("import")]
      public async Task<IActionResult> Import()
      {
         HttpClient client = new HttpClient();
         var url = HttpContext.Request.Query["url"].ToString();

         string responseBody = null;
         string errorMsg = "Success";

         try {
            HttpResponseMessage response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            responseBody = await response.Content.ReadAsStringAsync();

            MockUserImport(responseBody);
         }
         catch(Exception e) {
            errorMsg = e.Message;
         }

         return Ok(new JObject(
            new JProperty("Error", errorMsg),
            new JProperty("Content", responseBody)
         ));
      }

      private void MockUserImport(string data)
      {
         // Mock
      }
   }
}