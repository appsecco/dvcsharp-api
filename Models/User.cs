using System;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;

namespace dvcsharp_core_api.Models
{
   public class User
   {
      public const string RoleUser = "User";
      public const string RoleSupport = "Support";
      public const string RoleAdministrator = "Administrator";

      public int ID { get; set; }

      [Required]
      public string name { get; set; }
      [Required]
      public string email { get; set; }
      [Required]
      public string role { get; set; }
      [Required]
      //[System.Runtime.Serialization.IgnoreDataMember]
      public string password { get; set; }
      [Required]
      public DateTime createdAt { get; set; }
      [Required]
      public DateTime updatedAt { get; set; }

      public void updatePassword(string password)
      {
         this.password = getHashedPassword(password);
      }

      public string getHashedPassword(string password)
      {
         var md5 = MD5.Create();
         var hash = md5.ComputeHash(System.Text.Encoding.ASCII.GetBytes(password));

         return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
      }
   }
}