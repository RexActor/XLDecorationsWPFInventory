using Castle.Core.Resource;

using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

using XLDecorationsWPFInventory.Data.Models;
using XLDecorationsWPFInventory.UserControls;

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

		public async Task DeleteOrder(OrdersEntity order)
		{
			var orderToDelete = await _context.Orders.Where(item => item.Id == order.Id).FirstOrDefaultAsync();
			if (orderToDelete is not null)
			{
				_context.Orders.Remove(orderToDelete);
				await _context.SaveChangesAsync();
			}
		}

		public async Task DeleteOrderItem(OrderItemEntity orderItem)
		{
			var orderItemToDelete = await _context.OrderItems.Where(item => item.Id == orderItem.Id).FirstOrDefaultAsync();
			if (orderItemToDelete is not null)
			{
				_context.OrderItems.Remove(orderItemToDelete);
				await _context.SaveChangesAsync();
			}
		}

		public List<AllOrdersEntity> GetAllOrders()
		{
			var allOrders = new List<AllOrdersEntity>();

			var orders = _context.Orders.Include(item => item.Customer).ToList();

			orders.ForEach(order =>
			{


				_context.OrderItems.Include(item => item.Material).Where(item => item.OrderId == order.Id).ToList().ForEach(orderItem =>
				{
					allOrders.Add(new AllOrdersEntity
					{
						OrderId = order.Id,
						Orders = order,
						OrderName = order.OrderName,
						OrderQuantity = order.OrderQuantity,
						TotalOrderValue = order.TotalOrderValue,
						Customer = order.Customer,
						CustomerId = order.CustomerId,
						OrderItems = orderItem,

					});
				});

			});

			return allOrders;
		}

		public List<OrderItemEntity> GetOrderItems(OrdersEntity order)
		{
			var orderItems = _context.OrderItems.Include(item => item.Orders).Include(item => item.Material).Where(item => item.OrderId == order.Id).ToList();
			return orderItems;
		}

		public List<OrdersEntity> GetOrders(CustomerEntity customer)
		{
			var orders = _context.Orders.Include(item => item.Customer).Where(item => item.CustomerId == customer.Id).ToList();

			orders.ForEach(order =>
			{

				order.TotalOrderValue = GetOrderValue(order);
			});

			return orders;
		}

		public double GetOrderValue(OrdersEntity order)
		{
			double orderValue = _context.OrderItems.Include(item => item.Orders).Include(item => item.Material).AsEnumerable().Where(item => item.OrderId == order.Id).Select(item => item.TotalCost).Sum();
			return orderValue;
		}

		public async Task UpdateOrderTotalCost(OrdersEntity order, double cost)
		{
			var orderToUpdate = await _context.Orders.Where(item => item.Id == order.Id).FirstOrDefaultAsync();
			if (orderToUpdate is not null)
			{
				orderToUpdate.TotalOrderValue = cost;
				_context.Orders.Update(orderToUpdate);
				_context.SaveChanges();
			}

		}
	}
}
