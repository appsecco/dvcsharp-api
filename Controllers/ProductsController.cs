using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

      [HttpGet]
      public IEnumerable<Product> Get()
      {
         return _context.Products.ToList();
      }

      [HttpPost]
      public IActionResult Post([FromBody] Product product)
      {
         if(!ModelState.IsValid)
         {
            return BadRequest(ModelState);
         }

         var existingProduct = _context.Products.
            Where(b => (b.name == product.name) || (b.skuId == product.skuId)).
            FirstOrDefault();
         
         if(existingProduct != null) {
            ModelState.AddModelError("name", "Product name or skuId is already taken");
            return BadRequest(ModelState);
         }

         _context.Products.Add(product);
         _context.SaveChanges();

         return Ok(product);
      }

      [HttpGet("export")]
      public void Export()
      {
         XmlRootAttribute root = new XmlRootAttribute("Entities");
         XmlSerializer serializer = new XmlSerializer(typeof(Product[]), root);

         Response.ContentType = "application/xml";
         serializer.Serialize(HttpContext.Response.Body, _context.Products.ToArray());
      }

      [HttpGet("search")]
      public IActionResult Search(string keyword)
      {
         if (String.IsNullOrEmpty(keyword)) {
            return Ok("Cannot search without a keyword");
         }

         var query = $"SELECT * From Products WHERE name LIKE '%{keyword}%' OR description LIKE '%{keyword}%'";
         var products = _context.Products
            .FromSql(query)
            .ToList();

         return Ok(products);
      }

      [HttpPost("import")]
      public IActionResult Import()
      {
         XmlReader reader = XmlReader.Create(HttpContext.Request.Body);
         XmlRootAttribute root = new XmlRootAttribute("Entities");
         XmlSerializer serializer = new XmlSerializer(typeof(Product[]), root);

         var entities = (Product[]) serializer.Deserialize(reader);
         reader.Close();

         return Ok(entities);
      }
   }
}