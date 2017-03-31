using System;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using Newtonsoft.Json;

namespace CargaCotacoes
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Obter configurações de acesso...");
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
                CloudConfigurationManager.GetSetting("StorageConnectionString"));

            Console.WriteLine(
                "Gerar a referência da tabela e criar no storage (caso a mesma não ainda exista)...");
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();
            CloudTable table = tableClient.GetTableReference("Cotacoes");
            table.CreateIfNotExists();

            Console.WriteLine("Incluindo cotações do Euro (EUR)...");
            Carga.IncluirCotacao(table, "EUR", "2017-03-20 16:59", 3.3016);
            Carga.IncluirCotacao(table, "EUR", "2017-03-21 16:59", 3.3417);

            Console.WriteLine("Listando cotações do Euro (EUR)...");
            Carga.ListarCotacoes<CotacaoEntity>(table, "EUR");

            Console.WriteLine("Incluindo cotações da Libra Esterlina (LIB)...");
            Carga.IncluirCotacao(table, "LIB", "2017-03-20 16:59", 3.7979);
            Carga.IncluirCotacao(table, "LIB", "2017-03-21 16:59", 3.8573);

            Console.WriteLine("Listando cotações da Libra Esterlina (LIB)...");
            Carga.ListarCotacoes<CotacaoEntity>(table, "LIB");

            Console.WriteLine("Incluindo cotações do Dólar (USD)...");
            TableBatchOperation batch = new TableBatchOperation();
            batch.Insert(new CotacaoDolarEntity("USD", "2017-03-20 16:59", 3.0717, 3.2300));
            batch.Insert(new CotacaoDolarEntity("USD", "2017-03-21 16:59", 3.0900, 3.2600));
            table.ExecuteBatch(batch);

            Console.WriteLine("Listando cotações do Dólar (USD)...");
            Carga.ListarCotacoes<CotacaoDolarEntity>(table, "USD");

            Console.WriteLine("Finalizado!");
            Console.ReadKey();
        }
    }
}