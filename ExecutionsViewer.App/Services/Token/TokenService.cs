using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ExecutionsViewer.App.Database.Tables;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace ExecutionsViewer.App.Services.Token
{
    // public class TokenService : ITokenService
    // {
    //     readonly SymmetricSecurityKey key;
    //     readonly string issuer;
    //
    //     public TokenService(IConfiguration config)
    //     {
    //         this.key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));
    //         this.issuer = config["Jwt:Issuer"];
    //     }
    //
    //     public string CreateToken(User user)
    //     {
    //         var claims = new List<Claim>
    //         {
    //             new Claim(JwtRegisteredClaimNames.NameId, user.Id.ToString()),
    //             new Claim(JwtRegisteredClaimNames.UniqueName, user.Username)
    //         };
    //
    //         var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
    //
    //         var token = new JwtSecurityToken
    //         (
    //             issuer,
    //             issuer,
    //             claims,
    //             expires: DateTime.Now.AddMinutes(30),
    //             signingCredentials: credentials
    //         );
    //
    //         return new JwtSecurityTokenHandler().WriteToken(token);
    //     }
    //
    //     public int GetCurrentUserId()
    //     {
    //         // return Microsoft.AspNetCore.Mvc.User.Claims.
    //         return 0;
    //     }
    // }
}