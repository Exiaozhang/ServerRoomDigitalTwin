namespace ETModel
{
    /// <summary>
    /// 控制温度场的开关
    /// </summary>
    [Event(UIEventType.SiwtchHeatMap)]
    public class SwitchHeatMap: AEvent
    {
        public override void Run()
        {
            ServerRoom.Instance.SiwtchHeatMap();
        }
    }
}