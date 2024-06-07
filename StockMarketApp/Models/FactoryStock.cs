using System;
using StockMarketApp.Interface;

namespace StockMarketApp.Models
{
	public class FactoryStock
	{





        public IStock Choice_Stock(string Symbol)
		{
            switch (Symbol)
			{
				case "TSLA": return new Tesla();

                case "APPLE": return new Apple();

                case "IBM": return new Ibm();

                case "Google": return new Google();

                default:return null;
					
			}
		}		
	}
}

