using Microsoft.AspNetCore.Mvc;
using V3.Data;
using V3.Models;
using Microsoft.EntityFrameworkCore;

namespace V3.Controllers
{
    [ApiController]
    public class HomeController : ControllerBase
    {
        //[Route("/orders")]
        //GET
        [HttpGet("/")]
       
        public IActionResult Get()=> Ok("/Swagger para APIs");
    }
}