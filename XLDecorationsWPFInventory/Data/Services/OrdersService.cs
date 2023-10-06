using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

using XLDecorationsWPFInventory.Data.Models;

namespace XLDecorationsWPFInventory.Data.Services
{
	public class OrdersService : IOrdersService
	{

		private readonly AppDbContext _context;
		public OrdersService()
		{
			_context = MainWindow._context;
		}


		public async Task<OrdersEntity> CreateOrder(OrdersEntity entity)
		{


			await _context.Orders.AddAsync(entity);
			await _context.SaveChangesAsync();

			return entity;

		}

		public async Task<OrderItemEntity> CreateOrderItem(OrderItemEntity entity)
		{


			await _context.OrderItems.AddAsync(entity);
			await _context.SaveChangesAsync();

			return entity;

		}

		public List<OrderItemEntity> GetOrderItems(OrdersEntity order)
		{
			var orderItems = _context.OrderItems.Include(item => item.Orders).Include(item => item.Material).Where(item => item.OrderId == order.Id).ToList();
			return orderItems;
		}

		public List<OrdersEntity> GetOrders(CustomerEntity customer)
		{
			var orders = _context.Orders.Include(item => item.Customer).Where(item => item.CustomerId == customer.Id).ToList();
			return orders;
		}
	}
}
