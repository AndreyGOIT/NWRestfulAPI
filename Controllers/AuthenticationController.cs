using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NWRestfulAPI.Services.Interfaces;
using NWRestfulAPI.Models;

namespace NWRestfulAPI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticateService _authenticateService;

        public AuthenticationController(IAuthenticateService authenticateService)
        {
            _authenticateService = authenticateService;
        }

        // Tähän tulee Front endin kirjautumisyritys
        [HttpPost("login")]
        public ActionResult<LoggedUser> Post([FromBody] Credentials credentials)
        {
            var loggedUser = _authenticateService.Authenticate(credentials.Username, credentials.Password);

            if (loggedUser == null)
                return BadRequest(new { message = "Käyttäjätunus tai salasana on virheellinen" });

            return Ok(loggedUser); // Palautus front endiin (sis. vain loggedUser luokan mukaiset kentät)
        }
    }
}