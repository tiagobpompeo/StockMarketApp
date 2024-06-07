using StockMarketApp.Services;
using StockMarketApp.ViewModels;

namespace StockMarketApp;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();
        InitNavigation();
    }

    private void InitNavigation()
    {
        MainPage = new StockMarketApp.Views.MainPage();
    }
}

