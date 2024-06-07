

using System.Windows.Input;
using StockMarketApp.Constants;
using StockMarketApp.Repository;

namespace StockMarketApp.ViewModels
{
	public class MainPageViewModel:ViewModelBase
	{
        public ICommand OnGetStockDataCommand { get; set; }

        private string _stockSymbolEntry;
        public string StockSymbolEntry
        {
            get
            {
                return this._stockSymbolEntry;
            }
            set
            {
                this._stockSymbolEntry = value;
                OnPropertyChanged();
            }
        }


        private string _stockNameLabel;
        public string StockNameLabel
        {
            get
            {
                return this._stockNameLabel;
            }
            set
            {
                this._stockNameLabel = value;
                OnPropertyChanged();
            }
        }

        private string _stockPriceLabel;
        public string StockPriceLabel
        {
            get
            {
                return this._stockPriceLabel;
            }
            set
            {
                this._stockPriceLabel = value;
                OnPropertyChanged();
            }
        }


        public MainPageViewModel()
        {
            OnGetStockDataCommand = new Command(GetStockCommand);
        }

        private void GetStockCommand(object obj)
        {
            if (string.IsNullOrWhiteSpace(StockSymbolEntry))
            {
                return;
            }

            _ = CallServiceData();        
        }

        private async Task CallServiceData()
        {
            try
            {
                // Acessando a instância Singleton
                GenericRepository _genericRepository = GenericRepository.Instance;
                string apiQuery = $"{ApiConstants.global}{ApiConstants.globalQuote}&symbol={StockSymbolEntry}&apikey={ApiConstants.apiKey}";
                var stockData = await _genericRepository.GetAsync<StockData>(apiQuery, string.Empty);

                if (StockSymbolEntry == "IBM")
                {
                    UpdateUIOne(stockData);
                }

                if (StockSymbolEntry == "TSCO")
                {
                    UpdateUITwo(stockData);
                }

                _ = UpdateStockPricesAsync();

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error", $"Failed to get stock data: {ex.Message}", "OK");
            }
            finally
            {
                Console.WriteLine("");
            }
        }

        public async Task UpdateStockPricesAsync()
        {
            while (true)
            {
                // Simula a chamada de uma API externa
                await Task.Delay(1000); // Simula o tempo de resposta da API


                if (string.IsNullOrWhiteSpace(StockSymbolEntry))
                {                    
                    return;
                }

           
                try
                {
                    GenericRepository _genericRepository = GenericRepository.Instance;
                    string apiQuery = $"{ApiConstants.global}{ApiConstants.globalQuote}&symbol={StockSymbolEntry}&apikey={ApiConstants.apiKey}";
                    var stockData = await _genericRepository.GetAsync<StockData>(apiQuery, string.Empty);

                    if (StockSymbolEntry == "IBM")
                    {
                        UpdateUIOne(stockData);
                    }

                    if (StockSymbolEntry == "TSCO")
                    {
                        UpdateUITwo(stockData);
                    }
                }
                catch (Exception ex)
                {
                   
                }
                finally
                {
                  
                }
                await Task.Delay(TimeSpan.FromSeconds(10)); // Atualiza a cada 10 segundos
            }
        }

        private void UpdateUIOne(StockData stockData)
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                StockNameLabel = stockData.GlobalQuote._01symbol;
                StockPriceLabel = stockData.GlobalQuote._05price.ToString();

            });
        }

        private void UpdateUITwo(StockData stockData)
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                StockNameLabel = stockData.GlobalQuote._01symbol;
                StockPriceLabel = stockData.GlobalQuote._05price.ToString();
            });
        }
    }    
}

