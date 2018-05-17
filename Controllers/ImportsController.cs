using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using dvcsharp_core_api.Models;
using dvcsharp_core_api.Data;

namespace dvcsharp_core_api
{
   [Route("api/[controller]")]
   public class ImportsController : Controller
   {
      private readonly GenericDataContext _context;

      public ImportsController(GenericDataContext context)
      {
         _context = context;
      }

      [HttpPost]
      public IActionResult Post()
      {
         XmlDocument xmlDocument = new XmlDocument();
         xmlDocument.Load(HttpContext.Request.Body);

         var entities = new List<Object>();

         foreach(XmlElement xmlItem in xmlDocument.SelectNodes("Entities/Entity"))
         {
            string typeName = xmlItem.GetAttribute("Type");

            if(String.IsNullOrEmpty(typeName))
               continue;

            //Console.WriteLine("Trying to deserialize: " + typeName);
            //Console.WriteLine("Content: " + xmlItem.InnerXml);

            var xser = new XmlSerializer(Type.GetType(typeName));
            var reader = new XmlTextReader(new StringReader(xmlItem.InnerXml));

            entities.Add(xser.Deserialize(reader));
         }

         return Ok(entities);
      }
   }
}