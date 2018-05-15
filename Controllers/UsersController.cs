using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
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

         HttpResponseMessage response = await client.GetAsync(url);
         response.EnsureSuccessStatusCode();
         string responseBody = await response.Content.ReadAsStringAsync();

         // TODO: Parse JSON and import users

         return Ok(responseBody);
      }
   }
}