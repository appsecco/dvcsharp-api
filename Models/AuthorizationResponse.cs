using System;
using System.ComponentModel.DataAnnotations;

namespace dvcsharp_core_api.Models
{
   public class AuthorizationResponse
   {
      public string role;
      public string accessToken;
   }
}