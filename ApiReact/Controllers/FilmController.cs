using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiReact.Helpers;
using DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BLL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace ApiReact.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class FilmController : BaseController
    {
        private readonly IServiceFilm _iServiceFilm;
        public FilmController(IServiceFilm serviceFilm)
        {
            _iServiceFilm = serviceFilm;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                List<Film> List = _iServiceFilm.FindById();
                return BuildJsonResponse(200, "Succès", List);
            }
            catch (Exception e)
            {
                return BuildJsonResponse(500, "Erreur serveur", null, e.Message);
            }
        }

        [HttpPost]
        [ValidateModel]
        public IActionResult Save([FromBody] Film film)
        {
            try
            {
                if (_iServiceFilm.Add(film))
                {
                    return BuildJsonResponse(201, "Utilisateur enregistré avec succès", film);
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
        public IActionResult Update([FromBody] Film film)
        {
            try
            {
                if (_iServiceFilm.Update(film))
                {
                    return BuildJsonResponse(201, "Utilisateur enregistré avec succès", film);
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
                if (_iServiceFilm.Delete(id))
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
                Film film = _iServiceFilm.Find(id);
                return BuildJsonResponse(200, "Succès", film);
            }
            catch (Exception e)
            {
                return BuildJsonResponse(500, "Erreur serveur", null, e.Message);
            }
        }
    }
}