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
   public class ProductsController : Controller
   {
      private readonly GenericDataContext _context;

      public ProductsController(GenericDataContext context)
      {
         _context = context;
      }

      [HttpPost]
      public IActionResult Post([FromBody] Product product)
      {
         if(!ModelState.IsValid)
         {
            return BadRequest(ModelState);
         }

         return Ok();
      }

      [HttpGet("export")]
      public IActionResult Export()
      {
         return NotFound();
      }

      [HttpPost("import")]
      public IActionResult Import()
      {
         return NotFound();
      }
   }
}