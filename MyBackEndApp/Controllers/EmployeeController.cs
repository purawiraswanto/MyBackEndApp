using MyBackEndApp.Models;
using MyBackEndApp.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MyBackEndApp.Controllers
{
    public class EmployeeController : ApiController
    {

        //private List<Employee> lstEmployee = new List<Employee>
        private EmployeeDAL empDAL;
        public EmployeeController()
        {
            /*    new Employee{EmpId=1, EmpName="Pura", Designation="ERP Integration",
                    Department="IT", Qualification="ERP", BirthDate=new DateTime(1990, 12,12)},
                new Employee{EmpId=2, EmpName="Budi", Designation="Android",
                    Department="IT", Qualification="Android", BirthDate=new DateTime(1990, 01,12)},
                new Employee{EmpId=3, EmpName="Ami", Designation="Web Dev",
                     Department="IT", Qualification="ASP.Net", BirthDate=new DateTime(1990, 05,22)}
            };*/
            empDAL = new EmployeeDAL();
        }

        // GET: api/Employee
        public IEnumerable<Employee> Get()
        {
            //return new string[] { "value1", "value2" };
            //return lstEmployee;
            return empDAL.GetAllEmployee();
        }

        // GET: api/Employee/5
        public Employee Get(int id)
        {
            //return "value";
            //var result = lstEmployee.Where(e => e.EmpId == id).SingleOrDefault();
            //return result;
            //return new Employee();
            return empDAL.GetEmployeeByID(id);
        }

        // POST: api/Employee
        public IHttpActionResult Post(Employee emp)
        {
            try
            {
                empDAL.InsertEmployee(emp);
                return Ok("Data berhasil ditambahkan !");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        // PUT: api/Employee/5
        public IHttpActionResult Put(Employee emp)
        {
            try
            {
                empDAL.EditEmployee(emp);
                return Ok("Data berhasil diedit");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/Employee/5
        public IHttpActionResult Delete(int id)
        {
            try
            {
                empDAL.DeleteEmployee(id);
                return Ok("Data berhasil di delete");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
