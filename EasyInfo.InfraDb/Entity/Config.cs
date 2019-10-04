using System;
using System.Collections.Generic;

namespace EasyInfo.InfraDb.Entity
{
    public class Config
    {
        public string Connection { get; set; }
        public decimal Frag { get; set; } = 20;
        public string QueryIndexDefrag { get; set; }
        public int Intervalo { get; set; }
        public IList<Log> Logs { get; private set; } = new List<Log> { };

        public void Log(string msg)
        {
            try
            {
                Logs.Add(new Log(msg));
            }
            catch (Exception ex) { }
        }
    }
}