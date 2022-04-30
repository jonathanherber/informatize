using Microsoft.AspNetCore.Mvc;
using V3.Data;
using V3.Models;
using Microsoft.EntityFrameworkCore;

namespace V3.Controllers
{
    [ApiController]
    public class CarrinhoController : ControllerBase
    {

        //GET
        [HttpGet("/carrinho")]
        public IActionResult Get([FromServices] AppDataContext context)=> Ok(context.Carrinhos.
                Include(x=>x.Cliente).
                Include(x=>x.Produto).
                ToList());
        

        //POST
        [HttpPost("/carrinho")]
        public IActionResult Post (
            [FromBody] Carrinho carrinho,
            [FromServices] AppDataContext context)
            {
                var clienteValido = context.Clientes.FirstOrDefault(x=>x.Id==carrinho.ClienteId);
                    if (clienteValido != null)
                    {
                        var produtoValido = context.Produtos.FirstOrDefault(x=>x.Id==carrinho.ProdutoId);
                            if (produtoValido != null)
                                {
                                    if(carrinho.Quantidade>0){
                                        context.Carrinhos.Add(carrinho);
                                        context.SaveChanges();
                                        return Created($"/carrinho/{carrinho.Id}",carrinho);
                                    }
                                    else
                                    {
                                        return BadRequest("Quantidade precisa ser maior que zero");
                                    }

                                }
                            else
                            {
                                return NotFound("Produto não encontrado");
                            }
                
                    }
                    else{
                        return NotFound("Cliente não encontrado");
                    }

                
                
            }

        //GET BY ID
        [HttpGet("/carrinho/{id:int}")]
        public IActionResult GetById(
            [FromRoute] int id,
            [FromServices] AppDataContext context){ 
            var car = context.Carrinhos.FirstOrDefault(x=>x.Id == id);
            if (car == null)
                return NotFound("Carrinho não encontrado");
            return Ok(car);
        }  

        //DELETE
        [HttpDelete("/carrinho/{id:int}")]
         public IActionResult Delete (
            [FromRoute] int id,
            [FromServices] AppDataContext context)
            {
                var model = context.Carrinhos.FirstOrDefault(x=>x.Id==id);
                if (model == null)
                    return NotFound("Carrinho não encontrado");
                context.Carrinhos.Remove(model);
                context.SaveChanges();
                return Ok("Excluido com sucesso");
            }
        //PUT
        [HttpPut("/carrinho/{id:int}")]
         public IActionResult Put (
            [FromRoute] int id,
            [FromBody] Carrinho carrinho,
            [FromServices] AppDataContext context)
            {
                var model = context.Carrinhos.FirstOrDefault(x=>x.Id==id);
                var clienteValido = context.Clientes.FirstOrDefault(x=>x.Id==carrinho.ClienteId);
                    if (clienteValido != null)
                    {
                        var produtoValido = context.Produtos.FirstOrDefault(x=>x.Id==carrinho.ProdutoId);
                            if (produtoValido != null)
                                {
                                    if(carrinho.Quantidade>0){
                                        if (model != null)
                                        {
                                            model.ClienteId = carrinho.ClienteId;
                                            model.Quantidade = carrinho.Quantidade;
                                            model.ProdutoId = carrinho.ProdutoId;

                                            context.Carrinhos.Update(model);
                                            context.SaveChanges();
                                            return Ok("Atualizado com sucesso");
                                        }else
                                        {
                                            return NotFound("Carrinho não encontrado");
                                        }
                                        
                                    }
                                    else
                                    {
                                        return BadRequest("Quantidade precisa ser maior que zero");
                                    }

                                }
                            else
                            {
                                return NotFound("Produto não encotrado");
                            }
                
                    }
                    else{
                        return NotFound("Cliente não encontrado");
                    }

                
            }

    }
}