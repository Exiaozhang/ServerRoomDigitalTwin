using System;
using ETModel;

namespace ETHotfix
{
    [MessageHandler(AppType.Gate)]
    public class C2G_SrdtPlayerInfoHandler: AMRpcHandler<C2G_SrdtPlayerInfo, G2C_PlayerInfo>
    {
        protected override async ETTask Run(Session session, C2G_SrdtPlayerInfo request, G2C_PlayerInfo response, Action reply)
        {
            response.Message = "王洪文";
        }
    }
}