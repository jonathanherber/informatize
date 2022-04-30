using Microsoft.AspNetCore.Mvc;
using V3.Data;
using V3.Models;

namespace V3.Controllers
{
    [ApiController]
    
    public class ProdutosController : ControllerBase
    {
        //GET
        [HttpGet("/produtos")]
        public IActionResult Get([FromServices] AppDataContext context)=> Ok(context.Produtos.ToList());
        
        //POST
        [HttpPost("/produtos")]
        public IActionResult Post (
            [FromBody] Produto produto,
            [FromServices] AppDataContext context)
            {
                context.Produtos.Add(produto);
                context.SaveChanges();

                return Created($"/produtos/{produto.Id}",produto);
            }
        //GET BY ID
        [HttpGet("/produtos/{id:int}")]
        public IActionResult GetById(
            [FromRoute] int id,
            [FromServices] AppDataContext context){ 

            var prod = context.Produtos.FirstOrDefault(x=>x.Id == id);
            if (prod == null)
                return NotFound();
            return Ok(prod);
        }   
        //PUT
        [HttpPut("/produtos/{id:int}")]
         public IActionResult Put (
            [FromRoute] int id,
            [FromBody] Produto produto,
            [FromServices] AppDataContext context)
            {
                var model = context.Produtos.FirstOrDefault(x=>x.Id==id);
                if (model == null){
                    return NotFound();
                }
                model.Nome = produto.Nome;
                model.Preco = produto.Preco;
                
                
                context.Produtos.Update(model);
                context.SaveChanges();
                return Ok(model);
            }
            //DELETE
        [HttpDelete("/produtos/{id:int}")]
         public IActionResult Delete (
            [FromRoute] int id,
            [FromServices] AppDataContext context)
            {
                var model = context.Produtos.FirstOrDefault(x=>x.Id==id);
                if (model == null)
                    return NotFound();
                context.Produtos.Remove(model);
                context.SaveChanges();
                return Ok(model);
            }
    }
}