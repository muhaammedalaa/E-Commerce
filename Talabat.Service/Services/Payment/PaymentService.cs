using AutoMapper;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core;
using Talabat.Core.Dtos.Basket;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Orders;
using Talabat.Core.Repositories.Contarct;
using Talabat.Core.Service.Contract;
using Talabat.Core.Specifications;

namespace Talabat.Service.Services.Payment
{
    public class PaymentService : IPaymentService
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IConfiguration _configuration;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PaymentService(IBasketRepository basketRepository, IConfiguration configuration, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _basketRepository = basketRepository;
            _configuration = configuration;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<CustomerBasketDto> CreateOrUpdatePaymentIntentAsync(string basketId)
        {
            StripeConfiguration.ApiKey = _configuration.GetSection("StripeSettings:SecretKey").Value;
            var basket = await GetBasketAsync(basketId);
            await ValidateBasketAsync(basket);
            var total = CalculateTotal(basket);
            await CreationOrUpdatePaymentIntentAsync(basket, total);
            await _basketRepository.UpdateBasketAsync(basket);
            var basketDto = _mapper.Map<CustomerBasketDto>(basket);
            return basketDto;
        }
        private async Task<CustomerBasket> GetBasketAsync(string basketId)
        {
            var basket = await _basketRepository.GetBasketAsync(basketId);
            if (basket == null)
                throw new Exception("Basket not found");
            return basket;
        }
        private async Task ValidateBasketAsync(CustomerBasket basket)
        {
            foreach (var item in basket.Items)
            {
                var spec = new ProductSpecifications(item.Id);
                var product = await _unitOfWork.Repository<Talabat.Core.Entities.Product, int>().GetAsyncSpec(spec);
                if (product == null)
                    throw new Exception($"Product with Id {item.Id} Not Found");
                if (item.Price != product.Price)
                {
                    item.Price = product.Price;
                }

            }
            if (basket.DeliveryMethodId.HasValue)
            {
                var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod, int>().GetAsync(basket.DeliveryMethodId.Value);
                if (deliveryMethod == null)
                    throw new Exception($"Delivery Method with Id {basket.DeliveryMethodId.Value} Not Found");
                basket.ShippingPrice = deliveryMethod.Price;
            }

        }
        private long CalculateTotal(CustomerBasket basket)
        {
            decimal total = ((basket.Items.Sum(item => item.Quantity * item.Price) + (basket.ShippingPrice ?? 0)));
            total = Math.Round(total, 2, MidpointRounding.AwayFromZero);

            return (long)(total * 100);
        }
        private async Task CreationOrUpdatePaymentIntentAsync(CustomerBasket basket, long total)
        {
            var stripeService = new PaymentIntentService();
            if (string.IsNullOrEmpty(basket.PaymentIntentId))
            {
                var options = new PaymentIntentCreateOptions
                {
                    Amount = total,
                    Currency = "usd",
                    PaymentMethodTypes = new List<string> { "card" }
                };
                var intent = await stripeService.CreateAsync(options);
                basket.PaymentIntentId = intent.Id;
                basket.ClientSecret = intent.ClientSecret;
            }
            else
            {
                var options = new PaymentIntentUpdateOptions
                {
                    Amount = total
                };
                await stripeService.UpdateAsync(basket.PaymentIntentId, options);
            }
        }
    }
}
