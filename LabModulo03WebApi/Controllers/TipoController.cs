﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using LabModulo03WebApi.Extensions;
using LabModulo03WebApi.Models;

namespace LabModulo03WebApi.Controllers
{
    [EnableCors(origins:"*", headers:"*", methods:"*")]
    [LogFiltro]
    [Authorize]
    public class TipoController : ApiController
    {
        public Concesionario20Entities db= new Concesionario20Entities();


       [AllowAnonymous]
        public List<Tipo> GetTipos()
        {
            return db.Tipo.ToList();
        }

        [ResponseType(typeof(Tipo))]
        [Authorize(Roles = "Admin")]
        public IHttpActionResult GetTipos(int id)
        {
            //var a=db.Tipo.Find(id);

            //return Ok(a);
            Tipo tipo = db.Tipo.Find(id);
            if (tipo==null)
            {
                return NotFound();
            }
            return Ok(tipo);
        }

        public List<Tipo> GetTiposPorNombre(String nombre)
        {
            return db.Tipo.Where(o => o.NombreTipo.Contains(nombre)).ToList();
            

        }

        public IHttpActionResult PutTipo(int id, Tipo tipo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id !=tipo.IdTipo)
            {
                return BadRequest();
            }

            db.Entry(tipo).State=EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (Exception)
            {
                Console.WriteLine("Error al Guardar");
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        [ResponseType(typeof(Tipo))]
        public IHttpActionResult PostTipo(Tipo tipo)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Tipo.Add(tipo);
            db.SaveChanges();
            return CreatedAtRoute("DefaultApi", new {id = tipo.IdTipo}, tipo);
        }
        [ResponseType(typeof(Tipo))]
        public IHttpActionResult DeleteTipo(int id)
        {

            Tipo tipoBorrar = db.Tipo.Find(id);

            if (tipoBorrar==null)
            {
                return NotFound();
            }

            db.Tipo.Remove(tipoBorrar);

            try
            {
                db.SaveChanges();
            }
            catch (Exception)
            {
                Console.WriteLine("Error al Borrar");
            }

            return Ok(tipoBorrar);
        }


    }
}
