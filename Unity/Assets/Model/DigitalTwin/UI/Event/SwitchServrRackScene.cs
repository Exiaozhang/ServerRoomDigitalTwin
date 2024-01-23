namespace ETModel
{
    [Event(UIEventType.SwitchServrRackScene)]
    public class SwitchServrRackScene: AEvent
    {
        public override void Run( )
        {
            // 加载场景资源
            ETModel.Game.Scene.GetComponent<ResourcesComponent>().LoadBundle("serverrackscene.unity3d");

            using (SceneChangeComponent sceneChangeComponent = ETModel.Game.Scene.AddComponent<SceneChangeComponent>())
            {
                sceneChangeComponent.ChangeSceneAsync(SceneType.ServerRackScene);
            }
        }
    }
}