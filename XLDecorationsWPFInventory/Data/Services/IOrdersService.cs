using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using XLDecorationsWPFInventory.Data.Models;

namespace XLDecorationsWPFInventory.Data.Services
{
	public interface IOrdersService
	{

		public Task<OrdersEntity> CreateOrder(OrdersEntity entity);
		public Task<OrderItemEntity> CreateOrderItem(OrderItemEntity entity);
		public List<OrdersEntity> GetOrders(CustomerEntity customer);
		public List<AllOrdersEntity> GetAllOrders();
		public List<OrderItemEntity> GetOrderItems(OrdersEntity order);
		public Task DeleteOrder(OrdersEntity order);
		public Task DeleteOrderItem(OrderItemEntity orderItem);
		public double GetOrderValue(OrdersEntity order);
		public Task UpdateOrderTotalCost(OrdersEntity order,double cost);

	}
}
