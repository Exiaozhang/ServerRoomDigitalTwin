using UnityEngine;

namespace ETModel
{
    public static class SRDTLoginHelper
    {
        public static async ETVoid OnLoginAsync()
        {
            // 创建一个ETModel层的Session
            ETModel.Session realmSession =
                    ETModel.Game.Scene.GetComponent<NetOuterComponent>().Create(GlobalConfigComponent.Instance.GlobalProto.Address);

            R2C_SrdtLogin r2CLogin = (R2C_SrdtLogin)await realmSession.Call(new C2R_SrdtLogin() { Account = "123", Password = "111111" });

            //创建Gate的Session
            Session gateSession = Game.Scene.GetComponent<NetOuterComponent>().Create(r2CLogin.Address);
            Game.Scene.AddComponent<SessionComponent>().Session = gateSession;

            G2C_SrdtLoginGate g2CSrdtLoginGate =
                    (G2C_SrdtLoginGate)await SessionComponent.Instance.Session.Call(new C2G_SrdtLoginGate() { Key = r2CLogin.Key });

            Log.Info("登陆gate成功!");

            Player player = ComponentFactory.CreateWithId<Player>(g2CSrdtLoginGate.PlayerId);
            PlayerComponent playerComponent = Game.Scene.GetComponent<PlayerComponent>();
            playerComponent.MyPlayer = player;

            //这里做一段处理登录的问题
            // Game.EventSystem.Run(EventType.AddServer);

            G2C_SrdtPlayerInfo g2CSrdtPlayerInfo = (G2C_SrdtPlayerInfo)await SessionComponent.Instance.Session.Call(new C2G_SrdtPlayerInfo());
            Debug.Log("登录用户信息:" + g2CSrdtLoginGate.Message);

            //登录到Map服
            G2C_SrdtEnterMap g2CEnterMap = (G2C_SrdtEnterMap)await ETModel.SessionComponent.Instance.Session.Call(new C2G_SrdtEnterMap());
            
        }
    }
}