using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiReact.Helpers;
using BLL;
using DAL.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiReact.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UserController : BaseController
    {
        private readonly IServiceUser _iServiceUser;
        public UserController(IServiceUser serviceUser)
        {
            _iServiceUser = serviceUser;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                List<User> List = _iServiceUser.FindById();
                return BuildJsonResponse(200, "Succès", List);
            }
            catch (Exception e)
            {
                return BuildJsonResponse(500, "Erreur serveur", null, e.Message);
            }
        }

        [HttpPost]
        [ValidateModel]
        public IActionResult Save([FromBody] User user)
        {
            try
            {
                if (_iServiceUser.Add(user))
                {
                    return BuildJsonResponse(201, "Utilisateur enregistré avec succès", user);
                }
                else
                {
                    return BuildJsonResponse(400, "Erreur d'enregistrement");
                }


            }
            catch (Exception e)
            {
                return BuildJsonResponse(500, "Erreur de serveur", null, e.Message);
            }
        }

        [HttpPut]
        [ValidateModel]
        public IActionResult Update([FromBody] User user)
        {
            try
            {
                if (_iServiceUser.Update(user))
                {
                    return BuildJsonResponse(201, "Utilisateur enregistré avec succès", user);
                }
                else
                {
                    return BuildJsonResponse(400, "Erreur d'enregistrement");
                }
            }
            catch (Exception e)
            {
                return BuildJsonResponse(500, "Erreur de serveur", null, e.Message);
            }
        }

        [HttpDelete("{id}")]
        //[HttpDelete("Delete/{id}")] // Permet de lever l'ambiguité de plusieurs méthodes (Delete ici)
        public IActionResult Delete(int id)
        {
            try
            {
                if (_iServiceUser.Delete(id))
                {

                    return BuildJsonResponse(201, "Utilisateur supprimé avec succès");
                }
                else
                {
                    return BuildJsonResponse(400, "Erreur de suppression");
                }
            }
            catch (Exception e)
            {
                return BuildJsonResponse(500, "Erreur de serveur", null, e.Message);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                User user = _iServiceUser.Find(id);
                return BuildJsonResponse(200, "Succès", user);
            }
            catch (Exception e)
            {
                return BuildJsonResponse(500, "Erreur serveur", null, e.Message);
            }
        }
    }
}