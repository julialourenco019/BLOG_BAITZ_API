using Microsoft.AspNetCore.Mvc;
using BAITZ_BLOG_API.Auth;
using BAITZ_BLOG_API.Repository;
using BAITZ_BLOG_API.Services;
using BAITZ_BLOG_API.Domain;
using BAITZ_BLOG_API.Interfaces;

namespace BAITZ_BLOG_API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IClientService _clientService;

        public AuthController(IClientService clientService)
        {
           _clientService = clientService;
        }

        
        [HttpPost("login")]
        public IActionResult Login(string email, string password)
        {
            var client = _clientService.GetByEmail(email);

            if (client == null)
            {
                return BadRequest("Email not found.");
            }

            if (client.Password != password)
            {
                return BadRequest("Invalid password.");
            }

            var token = TokenAuth.GenerateToken(client);
            return Ok(new { message = "Login successful.", token });
        }

        // Register
        [HttpPost("register")]
        public IActionResult Register(string email, string password)
        {
            // Verifica se o email já está registrado
            var existingClient = _clientService.GetByEmail(email);
            if (existingClient != null)
            {
                return BadRequest("Email is already registered.");
            }

            // Cria um novo cliente
            var newClient = new Client
            {
                Email = email,
                Password = password // Recomenda-se usar hash de senha em produção
            };

            _clientService.Create(newClient);

            return Ok(new { message = "Registration successful." });
        }

        /*[HttpPost]
        public IActionResult Auth(string email, string password)
        {
           
            if ( email!= null && password == "fatec#Baitz123")
            {
                var token = TokenAuth.GenerateToken(new Domain.Client());
                return Ok(token);
            }

            return BadRequest(" email or password invalid");
        }*/


    }
}
