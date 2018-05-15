using System;
using System.ComponentModel.DataAnnotations;

namespace dvcsharp_core_api.Models
{
   public class UserUpdateRequest
   {
      [StringLength(60)]
      [Required]
      public string name { get; set; }

      [EmailAddress]
      [Required]
      public string email { get; set; }

      [Required]
      public string role { get; set; }

      [Required]
      public string password { get; set; }

      [Compare("password", ErrorMessage = "The passwords do not match.")]
      [Required]
      public string passwordConfirmation { get; set; }
   }
}