using ETModel;

namespace ETHotfix
{
    public static class SRDTLoginHelper
    {
        public static async ETVoid OnLoginAsync()
        {
            // 创建一个ETModel层的Session
            ETModel.Session realmSession =
                    ETModel.Game.Scene.GetComponent<NetOuterComponent>().Create(GlobalConfigComponent.Instance.GlobalProto.Address);

            Session hotfixRealmSession = ComponentFactory.Create<Session, ETModel.Session>(realmSession);

            R2C_Login r2CLogin = (R2C_Login)await hotfixRealmSession.Call(new C2R_SrdtLogin() { Account = "123", Password = "111111" });

            hotfixRealmSession.Dispose();

            UnityEngine.MonoBehaviour.print($"{r2CLogin.Address}|{r2CLogin.Key}");
        }
    }
}