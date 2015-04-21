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
    [EnableCors("*","*","*")]
    public class MensajesPublicosController : ApiController
    {
        private RedSocialEntities db = new RedSocialEntities();

        // GET: api/MensajesPublicos
        public IEnumerable<MensajePublico> GetMensajePublico()
        {
            return db.MensajePublico;
        }

        public IEnumerable<MensajePublico> GetMensajePublicoPorUsuario(int idUsuario)
        {
            return db.MensajePublico.Where(o=>o.idUsuario==idUsuario);
        }

        // GET: api/MensajesPublicos/5
        [ResponseType(typeof(MensajePublico))]
        public IHttpActionResult GetMensajePublico(int id)
        {
            MensajePublico mensajePublico = db.MensajePublico.Find(id);
            if (mensajePublico == null)
            {
                return NotFound();
            }

            return Ok(mensajePublico);
        }

        // PUT: api/MensajesPublicos/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutMensajePublico(int id, MensajePublico mensajePublico)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != mensajePublico.id)
            {
                return BadRequest();
            }

            db.Entry(mensajePublico).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MensajePublicoExists(id))
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

        // POST: api/MensajesPublicos
        [ResponseType(typeof(MensajePublico))]
        public IHttpActionResult PostMensajePublico(MensajePublico mensajePublico)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.MensajePublico.Add(mensajePublico);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = mensajePublico.id }, mensajePublico);
        }

        // DELETE: api/MensajesPublicos/5
        [ResponseType(typeof(MensajePublico))]
        public IHttpActionResult DeleteMensajePublico(int id)
        {
            MensajePublico mensajePublico = db.MensajePublico.Find(id);
            if (mensajePublico == null)
            {
                return NotFound();
            }

            db.MensajePublico.Remove(mensajePublico);
            db.SaveChanges();

            return Ok(mensajePublico);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MensajePublicoExists(int id)
        {
            return db.MensajePublico.Count(e => e.id == id) > 0;
        }
    }
}