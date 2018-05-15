using System;
using System.ComponentModel.DataAnnotations;

namespace dvcsharp_core_api.Models
{
   public class Product
   {
      int ID { get; set; }

      [StringLength(60)]
      [Required]
      public string name { get; set; }

      [Required]
      public string description { get; set; }

      [Required]
      public string skuId { get; set; }

      [Required]
      public int unitPrice { get; set; }

      public string imageUrl;
      public string category;
   }
}