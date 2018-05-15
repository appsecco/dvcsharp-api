using System;
using System.ComponentModel.DataAnnotations;

namespace dvcsharp_core_api.Models
{
   public class Product
   {
      public int ID { get; set; }

      [StringLength(60)]
      [Required]
      public string name { get; set; }

      [Required]
      public string description { get; set; }

      [Required]
      public string skuId { get; set; }

      [Required]
      [Range(1, int.MaxValue, ErrorMessage = "Please enter a value greater than {1}")]
      public int unitPrice { get; set; }

      public string imageUrl;
      public string category;
   }
}