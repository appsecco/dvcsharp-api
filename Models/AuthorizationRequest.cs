using System;
using System.ComponentModel.DataAnnotations;

namespace dvcsharp_core_api.Models
{
   public class AuthorizationRequest
   {
      [EmailAddress]
      [Required]
      public string email { get; set; }

      [Required]
      public string password { get; set; }
   }
}