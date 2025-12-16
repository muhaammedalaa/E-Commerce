using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core;
using Talabat.Core.Dtos.Basket;
using Talabat.Core.Dtos.Orders;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Orders;
using Talabat.Core.Repositories.Contarct;
using Talabat.Core.Service.Contract;
using Talabat.Core.Specifications;

namespace Talabat.Service.Services.Orders
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IBasketRepository _basketRepository;

        public OrderService(IUnitOfWork unitOfWork, IMapper mapper, IBasketRepository basketRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _basketRepository = basketRepository;
        }
        public async Task<OrderResult> CreateOrderAsync(OrderRequest orderRequest, string UserEmail)
        {
            //  BasketId 
            // AddressDto ShippingAddress 
            // DeliveryMethodId 
            var address = _mapper.Map<Address>(orderRequest.ShippingAddress);
            var basket = await _basketRepository.GetBasketAsync(orderRequest.BasketId);
            if (basket == null)
                throw new Exception("Basket Not Found");
            var orderItems = new List<OrderItem>();
            foreach (var item in basket.Items)
            {
                var spec = new ProductSpecifications(item.Id);
                var product = await _unitOfWork.Repository<Product, int>().GetAsyncSpec(spec);
                if (product == null)
                    throw new Exception($"Product with Id {item.Id} Not Found");
                var itemOrdered = new ProductInOrderItem(product.Id, product.Name, product.PictureUrl);
                var orderItem = new OrderItem(itemOrdered, product.Price, item.Quantity);
                orderItems.Add(orderItem);
            }
            var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod, int>().GetAsync(orderRequest.DeliveryMethodId);
            if (deliveryMethod == null)
                throw new Exception("Delivery Method Not Found");
            var subTotal = orderItems.Sum(item => item.Price * item.Quantity);
            var order = new Order(UserEmail, subTotal, orderItems, address, deliveryMethod, basket.PaymentIntentId);
            await _unitOfWork.Repository<Order, Guid>().AddAsync(order);
            await _unitOfWork.CompleteAsync();
            return _mapper.Map<OrderResult>(order);
        }

        public async Task<IEnumerable<DeliveryMethodResult>> GetDeliveryMethodsAsync()
        {
            var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod, int>().GetAllAsync();
            return _mapper.Map<IEnumerable<DeliveryMethodResult>>(deliveryMethod);
        }

        public async Task<IEnumerable<OrderResult>> GetOrderByEmailAsync(string userEmail)
        {
            var order = new OrderSpecifications(userEmail);
            return _mapper.Map<IEnumerable<OrderResult>>(await _unitOfWork.Repository<Order, Guid>().GetAllAsyncSpec(order));
        }

        public async Task<OrderResult> GetOrderByIdAsync(Guid id)
        {
            var order = new OrderSpecifications(id);
            return _mapper.Map<OrderResult>(await _unitOfWork.Repository<Order, Guid>().GetAllAsyncSpec(order));
        }
    }
}
