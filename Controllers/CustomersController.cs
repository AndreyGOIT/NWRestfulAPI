using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NWRestfulAPI.Models;

namespace NWRestfulAPI.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        // Alustetaan tietokantayhteys

        // Perinteinen malli
        //NorthwindContext db = new NorthwindContext();

        // Dependency Injection malli
        private NorthwindContext db;

        public CustomersController(NorthwindContext dbparametri) // konstruktorissa
        {
            db = dbparametri; // saadaan tietokantayhteys
        }

        // Hakee kaikki asiakkaat
        [HttpGet]
        public ActionResult GetAllCustomers()
        {
            try
            {
                var asiakkaat = db.Customers.ToList();
                return Ok(asiakkaat);
            }
            catch (Exception ex)
            {
                return BadRequest("Tapahtui virhe. Lue lisää: " + ex.InnerException);
            }
        }

        // Hakee asiakkaan ID:n perusteella
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
                return Created($"/api/customers/{cust.CustomerId}", cust); ;
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
            try
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
            catch (Exception ex)
            {
                return BadRequest("Tapahtui virhe. Lue lisää: " + ex.InnerException);
            }

        }

        // Päivittää asiakkaan tiedot ID:n perusteella
        [HttpPut]
        [Route("{id}")]
        public ActionResult UpdateCustomer(string id, [FromBody] Customer cust)
        {
            try
            {
                if (id != cust.CustomerId) // jos id ei täsmää
                {
                    return BadRequest("Id ei täsmää");
                }

                var asiakas = db.Customers.Find(id);

                if (asiakas != null) // jos id:lla löytyy asiakas
                {
                    // päivitetään tiedot
                    // asiakas.CompanyName = cust.CompanyName;
                    // asiakas.ContactName = cust.ContactName;
                    // asiakas.ContactTitle = cust.ContactTitle;
                    // asiakas.Address = cust.Address;
                    // asiakas.City = cust.City;
                    // asiakas.Region = cust.Region;
                    // asiakas.PostalCode = cust.PostalCode;
                    // asiakas.Country = cust.Country;
                    // asiakas.Phone = cust.Phone;
                    // asiakas.Fax = cust.Fax;
                    db.Entry(asiakas).CurrentValues.SetValues(cust);

                    db.SaveChanges();
                    return Ok($"Asiakas {asiakas.CompanyName} id:lla {id} päivitetty");
                }
                else
                {
                    return NotFound($"Asiakasta id:lla {id} ei loydy");
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Tapahtui virhe. Lue lisää: " + ex.InnerException);
            }
        }

        // Poistaa asiakkaan ID:n perusteella
        [HttpDelete]
        [Route("{id}")]
        public ActionResult DeleteCustomer(string id)
        {
            try
            {
                var asiakas = db.Customers.Find(id);
                if (asiakas != null) // jos id:lla löytyy asiakas
                {
                    db.Customers.Remove(asiakas);
                    db.SaveChanges();
                    return Ok($"Asiakas id:lla {id} poistettu");
                }
                else
                {
                    return NotFound($"Asiakasta id:lla {id} ei loydy");
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Tapahtui virhe. Lue lisää: " + ex.InnerException);
            }

        }

    }
}
