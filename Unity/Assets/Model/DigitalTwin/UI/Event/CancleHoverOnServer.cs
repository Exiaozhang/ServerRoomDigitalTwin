namespace ETModel
{
    [Event(UIEventType.CancaleHoverOnServer)]
    public class CancleHoverOnServer: AEvent
    {
        public override void Run()
        {
            UIComponent uiComponent = Game.Scene.GetComponent<UIComponent>();
            UI ui = uiComponent.Get(DigitialTwinUIType.Tooltip);
            if (ui != null)
            {
                uiComponent.Remove(DigitialTwinUIType.Tooltip);
                ui.Dispose();
            }
        }
    }
}