namespace ETModel
{
    /// <summary>
    /// 悬浮在Server物体上
    /// </summary>
    [Event(UIEventType.HoverOnServer)]
    public class HoverOnServer: AEvent<Server>
    {
        public override void Run(Server server)
        {
            UI ui = Game.Scene.GetComponent<UIComponent>().Get(DigitialTwinUIType.Tooltip);
            if (ui == null)
            {
                //弹出Tooltip在Server附近
                ui = TooltipFactory.Create(DigitialTwinUIType.Tooltip);
                Game.Scene.GetComponent<UIComponent>().Add(ui);
            }

            //下边应该是修改弹出Tooltip的信息
        }
    }
}