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
    public class MensajesPrivadosController : ApiController
    {
        private RedSocialEntities db = new RedSocialEntities();

        // GET: api/MensajesPrivados
        public IEnumerable<MensajePrivado> GetMensajePrivado()
        {
            return db.MensajePrivado;
        }
        public IEnumerable<MensajePrivado> GetRecibidos(int idUsuarioDestino)
        {
            return db.MensajePrivado.Where(o=>o.idDestino==idUsuarioDestino);
        }
        public IEnumerable<MensajePrivado> GetEnviados(int idUsuarioOrigen)
        {
            return db.MensajePrivado.Where(o=>o.idOrigen==idUsuarioOrigen);
        }


        // GET: api/MensajesPrivados/5
        [ResponseType(typeof(MensajePrivado))]
        public IHttpActionResult GetMensajePrivado(int id)
        {
            MensajePrivado mensajePrivado = db.MensajePrivado.Find(id);
            if (mensajePrivado == null)
            {
                return NotFound();
            }

            return Ok(mensajePrivado);
        }

        // PUT: api/MensajesPrivados/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutMensajePrivado(int id, MensajePrivado mensajePrivado)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != mensajePrivado.id)
            {
                return BadRequest();
            }

            db.Entry(mensajePrivado).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MensajePrivadoExists(id))
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

        // POST: api/MensajesPrivados
        [ResponseType(typeof(MensajePrivado))]
        public IHttpActionResult PostMensajePrivado(MensajePrivado mensajePrivado)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.MensajePrivado.Add(mensajePrivado);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = mensajePrivado.id }, mensajePrivado);
        }

        // DELETE: api/MensajesPrivados/5
        [ResponseType(typeof(MensajePrivado))]
        public IHttpActionResult DeleteMensajePrivado(int id)
        {
            MensajePrivado mensajePrivado = db.MensajePrivado.Find(id);
            if (mensajePrivado == null)
            {
                return NotFound();
            }

            db.MensajePrivado.Remove(mensajePrivado);
            db.SaveChanges();

            return Ok(mensajePrivado);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MensajePrivadoExists(int id)
        {
            return db.MensajePrivado.Count(e => e.id == id) > 0;
        }
    }
}