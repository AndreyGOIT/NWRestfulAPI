using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NWRestfulAPI.Models;

namespace NWRestfulAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        // Alustetaan tietokantayhteys
        NorthwindOriginalContext db = new NorthwindOriginalContext();

        // Hakee kaikki asiakkaat
        [HttpGet]
        public ActionResult GetAllCustomers()
        {
            try 
            {
                var asiakkaat = db.Customers.ToList();
                return Ok(asiakkaat );
            }
            catch (Exception ex)
            {
                return BadRequest("Tapahtui virhe. Lue lisää: " + ex.InnerException);
            }
        }

        // Hakee asiakkaan ID:n peristeella
        [HttpGet]
        [Route("{id}")]
        public ActionResult GetOneCustomerById(string id) 
        {
            try
            {
                var customer = db.Customers.Find(id);
                if (customer != null)
                {
                    return Ok(customer);
                }
                else
                {
                    return NotFound($"Asiakasta id:lla {id} ei loydy"); //string interpolation
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Tapahtui virhe. Lue lisää: " + ex.InnerException);
            }
        }

        // Lisää uuden asiakkaan
        [HttpPost]
        public ActionResult AddCustomer([FromBody] Customer cust)
        {
            try
            {
                db.Customers.Add(cust);
                db.SaveChanges();
                //return Created("Customer added: ", cust);
                return Ok("Added new customer" + cust.CompanyName);
            }
            catch (Exception ex)
            {
                return BadRequest("Tapahtui virhe. Lue lisää: " + ex.InnerException);
            }
            
        }

        // Hakee asiakkaan nimen osalla
        [HttpGet]
        [Route("company/{search}")]
        public ActionResult GetCustomerByName(string search)
        {
            var customers = db.Customers
                .Where(c => c.CompanyName.Contains(search))
                .ToList(); // <--- nimen osalla haku

            // var customers = db.Customers.Where(c => c.CompanyName.StartsWith(search)).ToList(); // <--- nimen alulla haku
            // var customers = db.Customers.Where(c => c.CompanyName == search)).ToList(); // <--- täydellinen mätsi

            if (customers.Count == 0)
            {
                return NoContent();
            }
            return Ok(customers);
        }

    }
}
