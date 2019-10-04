using System;
using System.Data.SqlClient;
using Dapper;

namespace EasyInfo.InfraDb.Entity
{
    public class IndexDb
    {
        public string Table { get; set; }
        public string IndexName { get; set; }
        public decimal Frag { get; set; }
        public string Query { get => $"ALTER INDEX [{IndexName}] ON [{Table}] REBUILD;"; }
        public void Execute(Config config)
        {
            try
            {
                using (var connection = new SqlConnection(config.Connection))
                {
                    connection.Execute(Query);
                    config.Log($"percentual fragmentado -> [{Frag}] comando executado -> {Query}");
                }
            }
            catch (Exception ex) { config.Log(ex.Message); }
        }
    }
}