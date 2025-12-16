using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Orders;

namespace Talabat.Core.Specifications
{
    public class OrderSpecifications : BaseSpecifications<Order, Guid>
    {
        //Get by Id
        //Cretria ==> id == o.id
        //Includes ==> (DeliveryMethod , OrderItems)
        public OrderSpecifications(Guid id) : base(o => o.Id == id)
        {
            AddIncluds();
        }
        //Get All Orders By Email
        //Cretria ==> email == o.email
        //Includes ==> (DeliveryMethod , OrderItems)
        public OrderSpecifications(string userEmail) : base(o => o.UserEmail == userEmail)
        {
            AddIncluds();
            AddOrderBy(o => o.OrderDate);
        }
        private void AddIncluds()
        {
            Includes.Add(o => o.DeliveryMethod);
            Includes.Add(o => o.OrderItems);
            //Includes.Add(o => o.OrderDate);
        }
    }
}
