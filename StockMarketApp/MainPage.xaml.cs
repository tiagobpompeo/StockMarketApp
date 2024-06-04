using StockMarketApp.Constants;
using StockMarketApp.Repository;

namespace StockMarketApp;

public partial class MainPage : ContentPage
{
	int count = 0;

    public GenericRepository _genericRepository { get; set; }

    public MainPage()
	{
		InitializeComponent();
        _genericRepository = new GenericRepository();
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
            string apiQuery = $"{ApiConstants.global}{ApiConstants.globalQuote}&symbol={symbol}&apikey={ApiConstants.apiKey}";            
            var stockData = await _genericRepository.GetAsync<StockData>(apiQuery, string.Empty);
            UpdateUI(stockData);
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
    }

    private void UpdateUI(StockData stockData)
    {
        StockNameLabel.Text = stockData.GlobalQuote._01symbol;
        StockPriceLabel.Text = stockData.GlobalQuote._05price.ToString();
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
                string apiQuery = $"{ApiConstants.global}{ApiConstants.globalQuote}&symbol={symbol}&apikey={ApiConstants.apiKey}";
                var stockData = await _genericRepository.GetAsync<StockData>(apiQuery, string.Empty);
                UpdateUI(stockData);
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

    private void UpdateUiWithNewPrices(StockData stockData)
    {
        StockNameLabel.Text = stockData.GlobalQuote._01symbol;
        StockPriceLabel.Text = stockData.GlobalQuote._05price.ToString();
    }

}


