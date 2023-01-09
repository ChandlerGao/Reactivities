using System.Security.Claims;
using System.Net.Mime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Security
{
     public class UserAccessor : IUserAccessor
     {
          private readonly IHttpContextAccessor _httpContextAccessor;
          public UserAccessor(IHttpContextAccessor httpContextAccessor)
          {
               this._httpContextAccessor = httpContextAccessor;
          }


          public string GetUserName()
          {
               return this._httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);
          }
     }
}