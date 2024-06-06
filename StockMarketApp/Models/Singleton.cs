using System;
namespace StockMarketApp.Models
{
    public sealed class Singleton
    {
        // Instância estática única da classe Singleton
        private static readonly Singleton instance = new Singleton();

        // Propriedade pública para acessar a instância única
        public static Singleton Instance
        {
            get
            {
                return instance;
            }
        }

        // Construtor privado para evitar a instanciação direta
        private Singleton()
        {
            // Inicialização da classe
        }

        // Método público que pode ser chamado na instância Singleton
        public void DoSomething()
        {
            Console.WriteLine("Singleton instance is doing something.");
        }
    }
}

