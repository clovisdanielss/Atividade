using Atividade.Exceptions;
using Atividade.Models;
using Atividade.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Atividade.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SalesController : ControllerBase
    {
        [HttpGet("{id}")]
        public IActionResult Get([FromRoute] string id)
        {
            using (SalesRepository sales = new SalesRepository())
            {
                try
                {
                    var sale = sales.GetById(id);
                    return Ok(sale);
                }
                catch(Exception ex)
                {
                    return NotFound(ex.Message); 
                }
            }
        }

        [HttpPost("{id}/status")]
        public IActionResult UpdateStatus([FromRoute] string id, [FromBody] SaleStatus saleStatus)
        {
            using (SalesRepository sales = new SalesRepository())
            {
                try
                {
                    var sale = sales.GetById(id);
                    sales.ChangeStatus(sale, saleStatus);
                    return Ok(sale);
                }
                catch(Exception ex)
                {
                    if(ex is ItemNotFoundException)
                        return NotFound(ex.Message);
                    return BadRequest(ex.Message);
                }
                
            }
        }

        [HttpGet("{id}/status")]
        public IActionResult GetStatus([FromRoute] string id)
        {
            using (SalesRepository sales = new SalesRepository())
            {
                try
                {
                    var sale = sales.GetById(id);
                    return Ok(sale.Status);
                }
                catch (ItemNotFoundException ex)
                {
                    return NotFound(ex.Message);
                }
            }
        }

        [HttpGet("")]
        public IActionResult GetAll()
        {
            using (SalesRepository sales = new SalesRepository())
            {
                var allSales = sales.GetAll();
                return Ok(allSales);
            }
        }

        [HttpPost("")]
        public IActionResult Post([FromBody] Sale sale)
        {
            using (SalesRepository sales = new SalesRepository())
            {
                try
                {
                    sale = sales.Create(sale);
                    return new ObjectResult(sale){
                        StatusCode = 201
                    };
                }
                catch(Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] string id)
        {
            using (SalesRepository sales = new SalesRepository())
            {
                try
                {
                    var sale = sales.DeleteById(id);
                    return Ok(sale);
                }
                catch (Exception ex)
                {
                    return NotFound(ex.Message);
                }
            }
        }
    };


}