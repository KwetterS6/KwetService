using System;
using System.Threading.Tasks;
using KwetService.Models;
using KwetService.Services;
using Microsoft.AspNetCore.Mvc;

namespace KwetService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class KwetController : ControllerBase
    {
        private readonly IKwetService _service;

        public KwetController(IKwetService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Insert([FromBody]NewKwetModel kwetModel)
        {
            Console.Out.WriteLine("message recieved");
            Console.Out.WriteLine(kwetModel);
            try
            {
                await _service.InsertKwet(kwetModel);
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

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            try
            {
                return Ok(await _service.GetByUserId(id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        
    }
}