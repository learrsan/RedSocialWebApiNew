using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using RedSocialWebApi.Models;

namespace RedSocialWebApi.Controllers
{
     [EnableCors("*", "*", "*")]
    public class AmigosController : ApiController
    {
        private RedSocialEntities db = new RedSocialEntities();

        // GET: api/Amigos
        public IEnumerable<Amigos> GetAmigos()
        {
            return db.Amigos;
        }
        

        public IEnumerable<Usuario> GetAmigosPorUsuario(int idUsuario)
        {
            return db.Amigos.Where(o=>o.idUsuario==idUsuario).
                Select(o=>o.Usuario);
        }
        // GET: api/Amigos/5
        [ResponseType(typeof(Amigos))]
        public IHttpActionResult GetAmigos(int id)
        {
            Amigos amigos = db.Amigos.Find(id);
            if (amigos == null)
            {
                return NotFound();
            }

            return Ok(amigos);
        }

        // PUT: api/Amigos/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAmigos(int id, Amigos amigos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != amigos.idUsuario)
            {
                return BadRequest();
            }

            db.Entry(amigos).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AmigosExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Amigos
        //[ResponseType(typeof(Amigos))]
        //public IHttpActionResult PostAmigos(Amigos amigos)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    db.Amigos.Add(amigos);

        //    try
        //    {
        //        db.SaveChanges();
        //    }
        //    catch (DbUpdateException)
        //    {
        //        if (AmigosExists(amigos.idUsuario))
        //        {
        //            return Conflict();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return CreatedAtRoute("DefaultApi", new { id = amigos.idUsuario }, amigos);
        //}

        // POST: api/Amigos
        [ResponseType(typeof(Amigos))]
        public IHttpActionResult PostAmigos(NuevoAmigo amigo)
        {

            var a = db.Usuario.FirstOrDefault(o => o.login == amigo.Email);

            if (a == null)
                return BadRequest();

            var amigos=new Amigos()
            {
                aceptado = false,
                idUsuario = amigo.IdUsuario,
                idAmigo = a.id,
               
            };


            

            db.Amigos.Add(amigos);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (AmigosExists(amigos.idUsuario))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = amigos.idUsuario }, amigos);
        }
        // DELETE: api/Amigos/5
        [ResponseType(typeof(Amigos))]
        public IHttpActionResult DeleteAmigos(int id)
        {
            Amigos amigos = db.Amigos.Find(id);
            if (amigos == null)
            {
                return NotFound();
            }

            db.Amigos.Remove(amigos);
            db.SaveChanges();

            return Ok(amigos);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AmigosExists(int id)
        {
            return db.Amigos.Count(e => e.idUsuario == id) > 0;
        }
    }
}