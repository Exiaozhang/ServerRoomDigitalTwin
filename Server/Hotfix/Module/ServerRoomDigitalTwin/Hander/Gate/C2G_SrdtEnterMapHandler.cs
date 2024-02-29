using System;
using System.Net;
using ETModel;

namespace ETHotfix
{
    [MessageHandler(AppType.Gate)]
    public class C2G_SrdtEnterMapHandler: AMRpcHandler<C2G_SrdtEnterMap, G2C_SrdtEnterMap>
    {
        protected override async ETTask Run(Session session, C2G_SrdtEnterMap request, G2C_SrdtEnterMap response, Action reply)
        {
            Player player = session.GetComponent<SessionPlayerComponent>().Player;
            IPEndPoint mapAddress = StartConfigComponent.Instance.MapConfigs[0].GetComponent<InnerConfig>().IPEndPoint;
            Session mapSession = Game.Scene.GetComponent<NetInnerComponent>().Get(mapAddress);
            

            M2G_SrdtCreateUnit createUnit =
                    (M2G_SrdtCreateUnit)await mapSession.Call(new G2M_SrdtCreateUnit() { PlayerId = player.Id, GateSessionId = session.InstanceId });

            player.UnitId = createUnit.UnitId;
            response.UnitId = createUnit.UnitId;

            reply();
        }
    }
}