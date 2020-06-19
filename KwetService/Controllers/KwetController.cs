using System;
using System.Threading.Tasks;
using KwetService.Models;
using KwetService.Services;
using Microsoft.AspNetCore.Authorization;
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
        public async Task<IActionResult> Insert([FromBody]NewKwetModel kwetModel, [FromHeader(Name = "Authorization")] string token)
        {
            Console.Out.WriteLine("message recieved");
            Console.Out.WriteLine(kwetModel);
            try
            {
                return Ok( await _service.InsertKwet(kwetModel, token));
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

        [HttpPost("placeLike")]
        public async Task<IActionResult> PlaceLike([FromBody]LikeModel model)
        {
            try
            {
                return Ok(await _service.LikeKwet(model));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("removeLike")]
        public async Task<IActionResult> RemoveLike([FromBody] LikeModel model)
        {
            try
            {
                return Ok(await _service.RemoveLike(model));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        
    }
}