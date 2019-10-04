using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Dapper;
using EasyInfo.InfraDb.Entity;
using static System.Configuration.ConfigurationManager;

namespace EasyInfo.InfraDb
{
    public class Defrag
    {
        static string Ambiente { get; }
        static IDictionary<string, string> Map = new Dictionary<string, string>
        {
            ["prod"] = "server=;initial catalog=;user id=sa;password=;",
            ["homolog"] = "server=;initial catalog=;user id=sa;password=;",
        };

        static Defrag()
        {
            Ambiente = Map["homolog"];
#if !DEBUG
            Ambiente = Map["prod"];
#endif
            Config = SetConfig();
        }
        public static Config Config { get; }

        static Config SetConfig()
        {
            try
            {
                return new Config
                {
                    Connection = Ambiente,
                    Frag = Convert.ToDecimal(AppSettings["Frag"]),
                    Intervalo = Convert.ToInt32(AppSettings["Intervalo"]),
                    QueryIndexDefrag = AppSettings["QueryIndexDefrag"]
                };
            }
            catch (Exception ex)
            {
                return default(Config);
            }
        }

        public static void Execute()
        {
            try
            {
                if (Config == null) return;
                var indexs = Indexs();
                foreach (var index in indexs) index.Execute(Config);
            }
            catch (Exception ex)
            {
                Config.Log(ex.Message);
            }
        }

        static IEnumerable<IndexDb> Indexs()
        {
            try
            {
                IEnumerable<IndexDb> collection = null;
                using (var connection = new SqlConnection(Config.Connection))
                {
                    collection = connection.Query<IndexDb>(Config.QueryIndexDefrag, commandTimeout: 3);
                }

                return collection;
            }
            catch (Exception ex)
            {
                Config.Log(ex.Message);
                return default(IEnumerable<IndexDb>);
            }
        }
    }
}