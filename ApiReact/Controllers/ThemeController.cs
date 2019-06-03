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
    public class ThemeController : BaseController
    {
        private readonly IServiceTheme _iServiceTheme;
        public ThemeController(IServiceTheme serviceTheme)
        {
            _iServiceTheme = serviceTheme;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                List<Theme> List = _iServiceTheme.FindById();
                return BuildJsonResponse(200, "Succès", List);
            }
            catch (Exception e)
            {
                return BuildJsonResponse(500, "Erreur serveur", null, e.Message);
            }
        }

        [HttpPost]
        [ValidateModel]
        public IActionResult Save([FromBody] Theme theme)
        {
            try
            {
                if (_iServiceTheme.Add(theme))
                {
                    return BuildJsonResponse(201, "Utilisateur enregistré avec succès", theme);
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
        public IActionResult Update([FromBody] Theme theme)
        {
            try
            {
                if (_iServiceTheme.Update(theme))
                {
                    return BuildJsonResponse(201, "Utilisateur enregistré avec succès", theme);
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
                if (_iServiceTheme.Delete(id))
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
                Theme theme = _iServiceTheme.Find(id);
                return BuildJsonResponse(200, "Succès", theme);
            }
            catch (Exception e)
            {
                return BuildJsonResponse(500, "Erreur serveur", null, e.Message);
            }
        }
    }
}