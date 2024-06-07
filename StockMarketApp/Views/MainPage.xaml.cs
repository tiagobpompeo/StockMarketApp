using StockMarketApp.Constants;
using StockMarketApp.Interface;
using StockMarketApp.Models;
using StockMarketApp.Repository;

namespace StockMarketApp;

public partial class MainPage : ContentPage
{
	int count = 0;

    public MainPage()
	{
		InitializeComponent();
    }

    private async void OnGetStockDataClicked(object sender, EventArgs e)
    {
        var symbol = StockSymbolEntry.Text;

        if (string.IsNullOrWhiteSpace(symbol))
        {
            await DisplayAlert("Error", "Please enter a valid stock symbol", "OK");
            return;
        }

        LoadingIndicator.IsRunning = true;
        LoadingIndicator.IsVisible = true;

        try
        {
            // Acessando a instância Singleton
            GenericRepository _genericRepository = GenericRepository.Instance;
            string apiQuery = $"{ApiConstants.global}{ApiConstants.globalQuote}&symbol={symbol}&apikey={ApiConstants.apiKey}";            
            var stockData = await _genericRepository.GetAsync<StockData>(apiQuery, string.Empty);

            if (symbol == "IBM")
            {
                UpdateUIOne(stockData);
            }

            if (symbol == "TSCO")
            {
                UpdateUITwo(stockData);
            }

        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Failed to get stock data: {ex.Message}", "OK");
        }
        finally
        {
            LoadingIndicator.IsRunning = false;
            LoadingIndicator.IsVisible = false;
        }

        _ = UpdateStockPricesAsync();
        FactoryMethodTest();
    }

    private void FactoryMethodTest()
    {
        FactoryStock fm = new FactoryStock();
        IStock stock  = fm.Choice_Stock("IBM");
        stock.Choiced();
    }
    public async Task UpdateStockPricesAsync()
    {
        while (true)
        {
            // Simula a chamada de uma API externa
            await Task.Delay(1000); // Simula o tempo de resposta da API
            var symbol = StockSymbolEntry.Text;

            if (string.IsNullOrWhiteSpace(symbol))
            {
                await DisplayAlert("Error", "Please enter a valid stock symbol", "OK");
                return;
            }

            LoadingIndicator.IsRunning = true;
            LoadingIndicator.IsVisible = true;

            try
            {
                GenericRepository _genericRepository = GenericRepository.Instance;
                string apiQuery = $"{ApiConstants.global}{ApiConstants.globalQuote}&symbol={symbol}&apikey={ApiConstants.apiKey}";
                var stockData = await _genericRepository.GetAsync<StockData>(apiQuery, string.Empty);

                if (symbol == "IBM")
                {
                    UpdateUIOne(stockData);
                }

                if (symbol == "TSCO")
                {
                    UpdateUITwo(stockData);
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Failed to get stock data: {ex.Message}", "OK");
            }
            finally
            {
                LoadingIndicator.IsRunning = false;
                LoadingIndicator.IsVisible = false;
            }
            await Task.Delay(TimeSpan.FromSeconds(10)); // Atualiza a cada 10 segundos
        }
    }



    private void UpdateUIOne(StockData stockData)
    {
        MainThread.BeginInvokeOnMainThread(() =>
        {
            StockNameLabel.Text = stockData.GlobalQuote._01symbol;
            StockPriceLabel.Text = stockData.GlobalQuote._05price.ToString();

            StockNameLabel2.Text = stockData.GlobalQuote._01symbol;
            StockPriceLabel2.Text = stockData.GlobalQuote._05price.ToString();

            StockNameLabel3.Text = stockData.GlobalQuote._01symbol;
            StockPriceLabel3.Text = stockData.GlobalQuote._05price.ToString();

            StockNameLabel4.Text = stockData.GlobalQuote._01symbol;
            StockPriceLabel4.Text = stockData.GlobalQuote._05price.ToString();

        });
    }

    private void UpdateUITwo(StockData stockData)
    {
        MainThread.BeginInvokeOnMainThread(() =>
        {
            StockNameLabel2.Text = stockData.GlobalQuote._01symbol;
            StockPriceLabel2.Text = stockData.GlobalQuote._05price.ToString();
        });
    }

}


