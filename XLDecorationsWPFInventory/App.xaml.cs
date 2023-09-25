using Castle.DynamicProxy;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

using XLDecorationsWPFInventory.Data;
using XLDecorationsWPFInventory.Data.Services;

namespace XLDecorationsWPFInventory;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
	public IServiceProvider ServiceProvider { get; private set; }

	public IConfiguration Configuration { get; private set; }

	protected override void OnStartup(StartupEventArgs e)
	{
		var builder = new ConfigurationBuilder()
		 .SetBasePath(Directory.GetCurrentDirectory())
		 .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

		Configuration = builder.Build();

		var serviceCollection = new ServiceCollection();
		ConfigureServices(serviceCollection);

		ServiceProvider = serviceCollection.BuildServiceProvider();

		var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
		mainWindow.Show();
	}

	private void ConfigureServices(IServiceCollection services)
	{
		// ...
		services.AddScoped<ICustomersService, CustomersService>();
		services.AddScoped<IMaterialService, MaterialService>();
		services.AddScoped<IOrdersService, OrdersService>();
		services.AddTransient(typeof(MainWindow));
	}


}
