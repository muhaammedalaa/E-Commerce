using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Talabat.Core.Service.Contract;

namespace Talabat.APIs.Controllers
{
    [Authorize]
    public class PaymentsController : BaseApiController
    {
        private readonly IPaymentService _paymentService;

        public PaymentsController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }
        [HttpPost("{basketId}")]
        public async Task<ActionResult> CreateOrUpdatePaymentIntent(string basketId)
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);

            var basket = await _paymentService.CreateOrUpdatePaymentIntentAsync(basketId);
            if (basket == null)
                return BadRequest(new { error = "Problem with your basket" });
            return Ok(basket);
        }
    }
}
