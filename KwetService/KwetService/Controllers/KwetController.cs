using System;
using System.Threading.Tasks;
using KwetService.Models;
using KwetService.Services;
using Microsoft.AspNetCore.Mvc;

namespace KwetService.Controllers
{
    [Route("[controller]")]
    public class KwetController : ControllerBase
    {
        private readonly IKwetService _service;

        public KwetController(IKwetService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Insert([FromBody] Kwet kwet)
        {
            try
            {
                await _service.InsertKwet(kwet);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                return Ok(await _service.Get());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        
    }
}