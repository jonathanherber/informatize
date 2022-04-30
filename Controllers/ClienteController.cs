using Microsoft.AspNetCore.Mvc;
using V3.Data;
using V3.Models;
using Microsoft.EntityFrameworkCore;

namespace Comercio.Controllers
{
    [ApiController]
    public class ClientesController : ControllerBase
    {
        //GET
        [HttpGet("/clientes")]
        public IActionResult Get([FromServices] AppDataContext context){
            return Ok(context.Clientes.ToList());
        }
         
        
        //POST
        [HttpPost("/clientes")]
        public IActionResult Post (
            [FromBody] Cliente cliente,
            [FromServices] AppDataContext context)
            {
                context.Clientes.Add(cliente);
                context.SaveChanges();

                return Created($"/clientes/{cliente.Id}",cliente);
            }
        //GET BY ID
        [HttpGet("/clientes/{id:int}")]
        public IActionResult GetById(
            [FromRoute] int id,
            [FromServices] AppDataContext context){ 

            var clien = context.Clientes.FirstOrDefault(x=>x.Id == id);
            if (clien == null)
                return NotFound();
            return Ok(clien);
        }   
        //PUT
        [HttpPut("/clientes/{id:int}")]
         public IActionResult Put (
            [FromRoute] int id,
            [FromBody] Cliente cliente,
            [FromServices] AppDataContext context)
            {
                var model = context.Clientes.FirstOrDefault(x=>x.Id==id);
                if (model == null){
                    return NotFound();
                }
                model.Nome = cliente.Nome;
                model.Telefone = cliente.Telefone;
                context.Clientes.Update(model);
                context.SaveChanges();
                return Ok(model);
            }
            //DELETE
        [HttpDelete("/clientes/{id:int}")]
         public IActionResult Delete (
            [FromRoute] int id,
            [FromServices] AppDataContext context)
            {
                var model = context.Clientes.FirstOrDefault(x=>x.Id==id);
                if (model == null)
                    return NotFound();
                context.Clientes.Remove(model);
                context.SaveChanges();
                return Ok(model);
            }
    }
}