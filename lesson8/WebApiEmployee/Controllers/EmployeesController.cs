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

namespace WebApiEmployee.Controllers
{
    public class EmployeesController : ApiController
    {
        private mydatabaseEntities db = new mydatabaseEntities();

        static EmployeesController()
        {
            //Mapper.Initialize(
            //        cfg =>
            //        {
            //            cfg.CreateMap<Employee, Employee>().ForMember(e => e.Deparments, opt => opt.Ignore());
            //        }
            //    );
        }

        // GET: Employees
        [Route("employees")]
        public List<Employee> GetEmployee()
        {
            return Mapper.Map<List<Employee>,List<Employee>>( db.Employee.ToList());
        }

        //GET: EmployeeOfDepartments/5
        [Route("employeesofdep/{id}")]
        public List<Employee> GetEmployeeOfDepartments(int id)
        {
            return Mapper.Map<List<Employee>, List<Employee>>(db.Employee.Where(e => e.depId == id).ToList());
        }

        // GET: Employees/5
        [Route("employees/{id}")]
        [ResponseType(typeof(Employee))]
        public IHttpActionResult GetEmployee(int id)
        {
            Employee employee = db.Employee.Find(id);
            if (employee == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<Employee,Employee>(employee));
        }

        // PUT: putEmployees/5
        [Route("putemployee/{id}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutEmployee(int id, Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != employee.Id)
            {
                return BadRequest();
            }

            db.Entry(employee).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(id))
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

        // POST: addEmployees
        [Route("addemployee")]
        [ResponseType(typeof(Employee))]
        public HttpResponseMessage PostEmployee(Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
            }

            
            try
            {
                db.Employee.Add(employee);
                db.SaveChanges();
            }
            catch(Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest,e.Message);
            }

            return Request.CreateResponse(HttpStatusCode.Created, Mapper.Map<Employee, Employee>(employee));
            //return CreatedAtRoute("DefaultApi", new { id = employee.Id }, Mapper.Map<Employee, Employee>(employee));
        }

        // POST: editEmployees
        [Route("editemployee/{id}")]
        [ResponseType(typeof(Employee))]
        public HttpResponseMessage PostEditEmployee(int id, Employee employee)
        {
            Employee curEmployee = db.Employee.Find(id);
            if (curEmployee == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
            }
            try
            {
                curEmployee.name=employee.name;
                curEmployee.surname = employee.surname;
                curEmployee.position = employee.position;
                curEmployee.birthday = employee.birthday;
                curEmployee.depId = employee.depId;
                db.SaveChanges();
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, e.Message);
            }

            return Request.CreateResponse(HttpStatusCode.Created, Mapper.Map<Employee, Employee>(curEmployee));
        }

        // DELETE: deleteEmployees/5
        [Route("deleteemployee/{id}")]
        [ResponseType(typeof(Employee))]
        public IHttpActionResult DeleteEmployee(int id)
        {
            Employee employee = db.Employee.Find(id);
            if (employee == null)
            {
                return NotFound();
            }

            db.Employee.Remove(employee);
            db.SaveChanges();

            return Ok(Mapper.Map<Employee, Employee>(employee));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EmployeeExists(int id)
        {
            return db.Employee.Count(e => e.Id == id) > 0;
        }
    }
}