using System;
using StockMarketApp.Interface;

namespace StockMarketApp.Models
{
    public class Ibm : IStock
    {
        public void Choiced()
        {
            Console.WriteLine("Escolhido IBM");
        }
    }
}

