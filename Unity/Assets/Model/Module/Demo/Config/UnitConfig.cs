namespace ETModel
{
    [Config((int)(AppType.ClientH | AppType.ClientM | AppType.Gate | AppType.Map))]
    public partial class UnitConfigCategory: ACategory<UnitConfig>
    {
    }

    public class UnitConfig: IConfig
    {
        public string Name;
        public string Desc;
        public int Position;
        public int Height;
        public int Weight;
        public int Id { get; set; }
    }
}