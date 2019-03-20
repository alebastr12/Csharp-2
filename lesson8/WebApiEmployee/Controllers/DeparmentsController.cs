using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WebApiEmployee;
using AutoMapper;
using System.Diagnostics;

namespace WebApiEmployee.Controllers
{
    public class DeparmentsController : ApiController
    {
        private mydatabaseEntities db = new mydatabaseEntities();

        // GET: Deparments
        [Route("departments")]
        public IList<Deparments> GetDeparments()
        {
            return Mapper.Map<List<Deparments>, List<Deparments>>(db.Deparments.ToList());
        }

        // GET: Deparments/5
        [Route("departments/{id}")]
        [ResponseType(typeof(Deparments))]
        public IHttpActionResult GetDeparments(int id)
        {

            Deparments deparments = db.Deparments.Find(id);
            if (deparments == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<Deparments,Deparments> (deparments));
        }

        // PUT: putDeparments/5
        [Route("putdepartments/{id}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutDeparments(int id, Deparments deparments)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != deparments.Id)
            {
                return BadRequest();
            }

            db.Entry(deparments).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DeparmentsExists(id))
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

        // POST: addDeparments
        [Route("adddepartments")]
        [ResponseType(typeof(Deparments))]
        public IHttpActionResult PostDeparments(Deparments deparments)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            
            try
            {
                db.Deparments.Add(deparments);
                db.SaveChanges();
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            } 
            return Created("Employees DB", Mapper.Map<Deparments, Deparments>(deparments));
        }

        // DELETE: deleteDeparments/5
        [Route("deletedepartments/{id}")]
        [ResponseType(typeof(Deparments))]
        public IHttpActionResult DeleteDeparments(int id)
        {
            Deparments deparments = db.Deparments.Find(id);
            if (deparments == null)
            {
                return NotFound();
            }

            db.Deparments.Remove(deparments);
            db.SaveChanges();

            return Ok(Mapper.Map<Deparments, Deparments>(deparments));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DeparmentsExists(int id)
        {
            return db.Deparments.Count(e => e.Id == id) > 0;
        }
    }
}