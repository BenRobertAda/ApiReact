using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BLL;
using DAL.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace ApiReact.Controllers
{
    [Route("api/[controller]")]

    public class AuthController : BaseController
    {
        private readonly IServiceUser _iServiceUser;
        private readonly IConfiguration _iConfiguration;
        public AuthController(IServiceUser iserviceUser, IConfiguration configuration)
        {
            _iServiceUser = iserviceUser;
            _iConfiguration = configuration;
        }

        [HttpPost]
        public IActionResult LogIn([FromBody] ViewAuth viewAuth)
        {
            try
            {
                var user = _iServiceUser.LogIn(viewAuth.Login, viewAuth.Password);
                if (user == null)
                {
                    return BuildJsonResponse(404, "Utilisateur non trouvé", null, "Login ou password est incorrect");
                }

                var Claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),

                };

                var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_iConfiguration["JwtSecurityToken:Key"]));
                var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha512);
                var jwtSecurityToken = new JwtSecurityToken(issuer: _iConfiguration["JwtSecurityToken:Issuer"],
                    audience: _iConfiguration["JwtSecurityToken:Audience"],
                    claims: Claims,
                    expires: DateTime.UtcNow.AddMonths(1),
                    signingCredentials: signingCredentials);
                var data = new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                    expiration = jwtSecurityToken.ValidTo,
                    currentuser = user
                };

                return BuildJsonResponse(200, "Authentification avec succès", data);


            }
            catch (Exception e)
            {

                return BuildJsonResponse(500, "Erreur serveur", null, e.Message);
            }
        }
    }
}