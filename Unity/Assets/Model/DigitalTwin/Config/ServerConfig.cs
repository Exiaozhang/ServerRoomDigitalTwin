using System;
using System.ComponentModel;

namespace ETModel
{
    [Config((int)(AppType.ClientH | AppType.ClientM | AppType.Gate | AppType.Map))]
    public partial class ServerConfigCategory: ACategory<ServerConfig>
    {
    }

    public class ServerConfig: IConfig
    {
        public int Id { get; set; }
        public String Name;
        public Int32 Position;
        public Int32 RackId;
    }
}