namespace ETModel
{
    [Event(UIEventType.ServerRackSceneLobbyToLobby)]
    public class ServerRackSceneLobbyToLobby: AEvent
    {
        public override void Run()
        {
            //清理机房大厅中的UI
            UIComponent uiComponent = Game.Scene.GetComponent<UIComponent>();
            UI ui = uiComponent.Get(DigitialTwinUIType.ServerRackLobby);
            ui.Dispose();
            uiComponent.Remove(DigitialTwinUIType.ServerRackLobby);
        }
    }
}