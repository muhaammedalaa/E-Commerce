using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Dtos.Orders;

namespace Talabat.Core.Service.Contract
{
    public interface IOrderService
    {
        //GetById ==> Take Guid Id ==> Return OrderResult
        Task<OrderResult> GetOrderByIdAsync(Guid id);
        //GetAllByEmail ==> Take String Email ==> Return IEnumerable<OrderResult>
        Task<IEnumerable<OrderResult>> GetOrderByEmailAsync(string userEmail);
        //CreateOrder ==> Take OrderRequest , String Email ==> Return OrderResult
        Task<OrderResult> CreateOrderAsync(OrderRequest orderRequest, string UserEmail);
        //GetDeliveryMethod ==> Return IEnumerable<DeliveryMethodResult>
        Task<IEnumerable<DeliveryMethodResult>> GetDeliveryMethodsAsync();
    }
}
