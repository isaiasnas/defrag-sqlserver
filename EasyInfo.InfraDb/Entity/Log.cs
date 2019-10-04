using System;

namespace EasyInfo.InfraDb.Entity
{
    public class Log
    {
        public Log(string msg) => Msg = msg;
        public DateTime Data { get; set; } = DateTime.Now;
        public string Msg { get; set; }
    }
}