using System;

namespace ETModel
{
    [Config((int)(AppType.ClientH | AppType.ClientM | AppType.Gate | AppType.Map))]
    public partial class ServerRackConfigCategory: ACategory<ServerRackConfig>
    {
    }

    public class ServerRackConfig: IConfig
    {
        public String Name;

        public Int32 Position;
        public int Id { get; set; }
    }
}