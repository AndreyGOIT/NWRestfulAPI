using Microsoft.AspNetCore.Mvc;
using NWRestfulAPI.Models;

namespace NWRestfulAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        // Dependency Injection malli
        private NorthwindContext db;

        public EmployeesController(NorthwindContext dbparametri) // konstruktorissa
        {
            db = dbparametri; // saadaan tietokantayhteys
        }

        [HttpGet]
        public ActionResult GetAllEmployees()
        {
            var employees = db.Employees.ToList();
            return Ok(employees);
        }

        [HttpGet("{id}")]
        public ActionResult GetEmployeeById(int id)
        {
            var employee = db.Employees.Find(id);
            if (employee == null) return NotFound();
            return Ok(employee);
        }

        [HttpGet("city/{city}")]
        public ActionResult GetEmployeesByCity(string city)
        {
            var employees = db.Employees.Where(e => e.City == city).ToList();
            return Ok(employees);
        }

        [HttpPost]
        public ActionResult AddEmployee([FromBody] Employee emp)
        {
            db.Employees.Add(emp);
            db.SaveChanges();
            return Ok(emp);
        }

        [HttpPut("{id}")]
        public ActionResult UpdateEmployee(int id, [FromBody] Employee updated)
        {
            var emp = db.Employees.Find(id);
            if (emp == null) return NotFound();

            emp.FirstName = updated.FirstName;
            emp.LastName = updated.LastName;
            emp.Title = updated.Title;
            emp.City = updated.City;
            emp.Country = updated.Country;

            db.SaveChanges();
            return Ok(emp);
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteEmployee(int id)
        {
            var emp = db.Employees.Find(id);
            if (emp == null) return NotFound();

            db.Employees.Remove(emp);
            db.SaveChanges();
            return Ok($"Employee {id} deleted");
        }
    }
}
