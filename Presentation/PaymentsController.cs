using Microsoft.AspNetCore.Mvc;
using Services.Abstraction;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation
{
    public class PaymentsController(IServiceManager serviceManager) :ApiController
    {
        [HttpPost("{basketId}")]
        public async Task<ActionResult<BasketDto>> CreateOrUpdatePaymentIntent(string basketId)
        {
            var result = await serviceManager.paymentService.CreateOrUpdatePaymentIntentAsync(basketId);
            return Ok(result);
        }

        [HttpPost("WebHook")]
        public async Task<IActionResult> WebHook()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
            var headers = Request.Headers["Stripe-Signature"];
            await serviceManager.paymentService.UpdateOrderPaymentStatus(json, headers!);
            return new EmptyResult();
        }
    }
}
