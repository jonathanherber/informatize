using Microsoft.AspNetCore.Mvc;
using V3.Data;
using V3.Models;
using Microsoft.EntityFrameworkCore;

namespace V3.Controllers
{
    [ApiController]
    
    public class ComprasController : ControllerBase
    {
        decimal valorfinal;

        //[Route("/orders")]
        //GET
        [HttpGet("/compras")]
        public IActionResult Get([FromServices] AppDataContext context)
        {
            var compras = context.Compras
            .Include(x=>x.Carrinho)
            .ToList();
            return Ok(compras);
        }

        //POST
        [HttpPost("/compras/{id_carrinho:int}")]
        public IActionResult Post (
            [FromRoute] int id_carrinho,
            [FromServices] AppDataContext context
            )
            {
                Compra compras = new Compra();
                var carrinhoValido = context.Carrinhos.FirstOrDefault(x=>x.Id==id_carrinho);
                if (carrinhoValido == null)
                    return NotFound("Carrinho não encontrado");
                compras.CarrinhoId = id_carrinho;
                var prec = (from car in context.Carrinhos
                    join prod in context.Produtos on car.ProdutoId equals
                    prod.Id
                    select prod.Preco).ToList();
                var qtd = (from car in context.Carrinhos
                    join prod in context.Produtos on car.ProdutoId equals
                    prod.Id
                    select car.Quantidade).ToList();
                foreach (var p in prec)
                {
                    foreach (var q in qtd)
                    {
                        decimal preco = p;
                        int quant = q;
                        valorfinal = preco * quant;
                        break;
                    }
                }
                compras.Valor = valorfinal;
                context.Compras.Add(compras);
                context.SaveChanges();
                return Created($"/compras/{compras.Id}",compras);
            }
            
        //GET BY ID
        [HttpGet("/compras/{id:int}")]
        public IActionResult GetById(
            [FromRoute] int id,
            [FromServices] AppDataContext context){ 

            var ord = context.Compras.FirstOrDefault(x=>x.Id == id);
            if (ord == null)
                return NotFound("Compra não encontrada");
            return Ok(ord);
        }

        //DELETE
        [HttpDelete("/compras/{id:int}")]
         public IActionResult Delete (
            [FromRoute] int id,
            [FromServices] AppDataContext context)
            {
                var model = context.Compras.FirstOrDefault(x=>x.Id==id);
                if (model == null)
                    return NotFound("Compra não encontrada");
                context.Compras.Remove(model);
                context.SaveChanges();
                return Ok(model);
            }
    }
}