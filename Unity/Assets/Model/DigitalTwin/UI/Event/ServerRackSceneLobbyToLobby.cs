namespace ETModel
{
    [Event(UIEventType.ServerRackSceneLobbyToLobby)]
    public class ServerRackSceneLobbyToLobby: AEvent
    {
        public override void Run()
        {
            //清理机房大厅中的UI

            //切换到Lobby场景
            Game.EventSystem.Run(UIEventType.SwitchToMainScne);
        }
    }
}