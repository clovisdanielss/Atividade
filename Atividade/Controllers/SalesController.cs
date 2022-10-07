using Atividade.Messages;
using Atividade.Models;
using Atividade.Repository;
using Atividade.Services;
using Microsoft.AspNetCore.Mvc;

namespace Atividade.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SalesController : ControllerBase
    {
        private SalesRepository Repository;
        private SalesService Service;
        public SalesController(SalesRepository salesRepository, SalesService salesService)
        {
            Repository = salesRepository;
            Service = salesService;

        }
        [HttpGet("{id}")]
        public IActionResult Get([FromRoute] string id)
        {
            var sale = Repository.GetById(id);
            if (sale != null)
            {
                return Ok(sale);
            }
            else
            {
                return NotFound(SalesControllerMessages.ItemNotFound(id));
            }

        }

        [HttpPost("{id}/status")]
        public IActionResult UpdateStatus([FromRoute] string id, [FromBody] SaleStatus saleStatus)
        {
            var sale = Repository.GetById(id);
            if (sale == null)
            {
                return NotFound(SalesControllerMessages.ItemNotFound(id));
            }
            var statusChanged = Service.ChangeStatus(sale, saleStatus);
            var statusUpdated = false;
            if (statusChanged)
            {
                statusUpdated = Repository.Update(sale);
            }
            if (statusChanged && statusUpdated)
            {
                return Ok(sale);
            }
            else
            {
                return BadRequest(SalesControllerMessages.ErrorWhenUpdating(sale.Status, saleStatus));
            }
        }

        [HttpGet("{id}/status")]
        public IActionResult GetStatus([FromRoute] string id)
        {
            var sale = Repository.GetById(id);
            if (sale != null)
            {
                return Ok(sale.Status);
            }
            else
            {
                return NotFound(SalesControllerMessages.ItemNotFound(id));
            }
        }

        [HttpGet("")]
        public IActionResult GetAll()
        {
            var allSales = Repository.GetAll();
            return Ok(allSales);
        }

        [HttpPost("")]
        public IActionResult Post([FromBody] Sale sale)
        {
            var createdSale = Repository.Create(sale);
            if (createdSale != null)
            {
                return new ObjectResult(createdSale)
                {
                    StatusCode = 201
                };
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] string id)
        {
            var sale = Repository.DeleteById(id);
            if (sale != null)
            {
                return Ok(sale);
            }
            return NotFound();
        }
    };


}