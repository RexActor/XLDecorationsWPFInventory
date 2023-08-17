using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XLDecorationsWPFInventory.Data.Models;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace XLDecorationsWPFInventory.Data.Services;

public interface ICustomersService
{
	public Task<CustomerEntity> CreateCustomer(CustomerEntity entity);
	public ObservableCollection<CustomerEntity> GetCustomers();
	public bool CustomerCheck(string customerName);
	public void DeleteCustomer(CustomerEntity customer);

}
