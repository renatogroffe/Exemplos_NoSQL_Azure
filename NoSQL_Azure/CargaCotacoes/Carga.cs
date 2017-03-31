using System;
using Microsoft.WindowsAzure.Storage.Table;
using Newtonsoft.Json;

namespace CargaCotacoes
{
    public static class Carga
    {
        public static void IncluirCotacao(CloudTable table,
            string siglaMoeda, string dataCotacao, double valorComercial)
        {
            CotacaoEntity cotacao =
                new CotacaoEntity(siglaMoeda, dataCotacao, valorComercial);

            TableOperation insertOperation = TableOperation.Insert(cotacao);
            table.Execute(insertOperation);
        }

        public static void ListarCotacoes<T>(
            CloudTable table, string siglaMoeda)
            where T : CotacaoEntity, new()
        {
            TableQuery<T> query = new TableQuery<T>()
                .Where(TableQuery.GenerateFilterCondition(
                    "PartitionKey", QueryComparisons.Equal, siglaMoeda));

            foreach (T entity in table.ExecuteQuery(query))
            {
                Console.Write(Environment.NewLine);
                Console.WriteLine(JsonConvert.SerializeObject(entity));
            }

            Console.Write(Environment.NewLine);
        }
    }
}