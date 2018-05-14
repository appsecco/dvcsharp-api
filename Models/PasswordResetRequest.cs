using System;
using System.ComponentModel.DataAnnotations;

namespace dvcsharp_core_api.Models
{
   public class PasswordResetRequest
   {
      public int ID { get; set; }
      public string key { get; set; }
      public string email { get; set; }
      public string password { get; set; }
      public string passwordConfirmation { get; set; }
      public DateTime createdAt { get; set; }
      public DateTime updatedAt { get; set; }
   }
}