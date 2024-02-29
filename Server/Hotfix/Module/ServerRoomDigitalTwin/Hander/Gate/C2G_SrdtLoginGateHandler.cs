using System;
using CommandLine;
using ETModel;

namespace ETHotfix
{
    [MessageHandler(AppType.Gate)]
    public class C2G_SrdtLoginGateHandler: AMRpcHandler<C2G_SrdtLoginGate, G2C_SrdtLoginGate>
    {
        protected override async ETTask Run(Session session, C2G_SrdtLoginGate request, G2C_SrdtLoginGate response, Action reply)
        {
            string account = Game.Scene.GetComponent<GateSessionKeyComponent>().Get(request.Key);
            if (account == null)
            {
                response.Error = ErrorCode.ERR_ConnectGateKeyError;
                response.Message = "Gate Key验证失败";
                reply();
                return;
            }

            Player player = ComponentFactory.Create<Player, String>(account);
            Game.Scene.GetComponent<PlayerComponent>().Add(player);

            session.AddComponent<SessionPlayerComponent>().Player = player;
            session.AddComponent<MailBoxComponent, String>(MailboxType.GateSession);
            
            response.PlayerId = player.Id;

            reply();
        }
    }
}