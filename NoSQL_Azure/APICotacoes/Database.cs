using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;

namespace APICotacoes
{
    public static class Database
    {
        public static List<T> ListarCotacoes<T>(IConfiguration config,
           string siglaMoeda) where T : CotacaoEntity, new()
        {
            // Acessar configurações
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
                config["StorageConnectionString"]);

            // Obter referência da tabela
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();
            CloudTable table = tableClient.GetTableReference("Cotacoes");

            TableQuery<T> query = new TableQuery<T>().Where(
                TableQuery.GenerateFilterCondition(
                    "PartitionKey", QueryComparisons.Equal, siglaMoeda));

            // Obtém os resultados em segmentos
            List<T> cotacoes = new List<T>();
            TableContinuationToken continuationToken = null;
            do
            {
                Task<TableQuerySegment<T>> task =
                    table.ExecuteQuerySegmentedAsync(
                        query, continuationToken);
                TableQuerySegment<T> querySegment = task.Result;
                cotacoes.AddRange(querySegment.ToList());
                continuationToken = querySegment.ContinuationToken;
            } while (continuationToken != null);

            return cotacoes;
        }
    }
}