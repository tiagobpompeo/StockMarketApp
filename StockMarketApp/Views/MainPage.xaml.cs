using StockMarketApp.Constants;
using StockMarketApp.Interface;
using StockMarketApp.Models;
using StockMarketApp.Repository;
using StockMarketApp.ViewModels;

namespace StockMarketApp.Views;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
        BindingContext = new MainPageViewModel();
    }

    private void FactoryMethodTest()
    {
        FactoryStock fm = new FactoryStock();
        IStock stock = fm.Choice_Stock("IBM");
        stock.Choiced();
    } 
    
}


