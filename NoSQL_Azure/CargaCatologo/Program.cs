using System;
using System.Configuration;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;

namespace CargaCatologo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Obter configurações de acesso...");
            DocumentClient client = new DocumentClient(
                new Uri(ConfigurationManager.AppSettings["EndpointUri"]),
                ConfigurationManager.AppSettings["PrimaryKey"]);

            Console.WriteLine("Criar banco de dados...");
            client.CreateDatabaseAsync(
                new Database { Id = "MVPCommunityConnection" }).Wait();

            Console.WriteLine("Criar coleção...");
            DocumentCollection collectionInfo = new DocumentCollection();
            collectionInfo.Id = "Catalogo";

            collectionInfo.IndexingPolicy =
                new IndexingPolicy(new RangeIndex(DataType.String) { Precision = -1 });

            client.CreateDocumentCollectionAsync(
                UriFactory.CreateDatabaseUri("MVPCommunityConnection"),
                collectionInfo,
                new RequestOptions { OfferThroughput = 400 }).Wait();

            Console.WriteLine("Incluir produtos...");
            Carga.InserirDadosProdutos(client);

            Console.WriteLine("Incluir serviços...");
            Carga.InserirDadosServicos(client);

            Console.WriteLine("Finalizado!");
            Console.ReadKey();
        }
    }
}