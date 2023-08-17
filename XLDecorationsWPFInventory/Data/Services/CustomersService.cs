using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

using XLDecorationsWPFInventory.Data.Models;

namespace XLDecorationsWPFInventory.Data.Services;

public class CustomersService : ICustomersService
{
	private readonly AppDbContext _context;

	ObservableCollection<CustomerEntity> customerEntities = new ObservableCollection<CustomerEntity>();


	public CustomersService()
	{
		_context = MainWindow._context;
	}

	public async Task<CustomerEntity> CreateCustomer(CustomerEntity entity)
	{


		await _context.Customers.AddAsync(entity);

		await _context.SaveChangesAsync();
		return entity;
	}

	public bool CustomerCheck(string customerName)
	{
		return _context.Customers.Where(item => item.CustomerName.ToLower() == customerName.ToLower()).Any();
	}

	public void DeleteCustomer(CustomerEntity customer)
	{

		var customerToRemove = _context.Customers.Where(item => item.Id == customer.Id).FirstOrDefault();
		if (customerToRemove is not null)
		{
			_context.Customers.Remove(customerToRemove);
			_context.SaveChanges();
		}


	}

	public ObservableCollection<CustomerEntity> GetCustomers()
	{
		_context.Customers.ForEachAsync(item =>
	   {
		   customerEntities.Add(item);
	   });


		return customerEntities;
	}
}
