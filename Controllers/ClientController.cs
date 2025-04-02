using BAITZ_BLOG_API.Domain;
using BAITZ_BLOG_API.Interfaces;
using BAITZ_BLOG_API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BAITZ_BLOG_API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ClientController : ControllerBase
    {
      private IClientService _clientService;
        public ClientController( IClientService clientService)
        {
            _clientService = clientService;
        }
        [Authorize]
        [HttpGet]
        public IActionResult GetAllClient()
        {
            var client = _clientService.GetAll();
            return Ok(client);
        }
        [Authorize]
        [HttpGet]
        [Route("{id:int}")]
        public IActionResult GetClientById(int id) 
        {
            var client = _clientService.GetById(id);
            if(client == null)
            {
                return NotFound();
            }
            return Ok(client);
        }
        [Authorize]
        [HttpPost]
        public IActionResult PostClient(Client client)
        {
             _clientService.Create(client);
            return Ok(client);
          

        }
        [Authorize]
        [HttpPut]
        [Route("{id:int}")]
        public IActionResult PutClient(int id, Client client)
        {

            _clientService.Update(id, client);
            return Ok();

        }
        [Authorize]
        [HttpDelete]
        [Route("{id:int}")]
        public IActionResult DeleteClient(int id)
        {
            _clientService.Delete(id);
            return Ok();
        }

    }
}
