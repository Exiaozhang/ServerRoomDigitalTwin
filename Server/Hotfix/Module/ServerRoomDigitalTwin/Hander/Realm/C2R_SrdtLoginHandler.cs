using System;
using System.Net;
using ETModel;

namespace ETHotfix
{
    [MessageHandler(AppType.Gate)]
    public class C2R_SrdtLoginHandler: AMRpcHandler<ETModel.C2R_SrdtLogin, ETModel.R2C_SrdtLogin>
    {
        protected override async ETTask Run(Session session, ETModel.C2R_SrdtLogin request, ETModel.R2C_SrdtLogin response, Action reply)
        {
            //这里是进行数据库验证，这里去掉了验证

            // 随机分配一个Gate
            StartConfig config = Game.Scene.GetComponent<RealmGateAddressComponent>().GetAddress();

            //读取内部服务器地址
            IPEndPoint innerAddress = config.GetComponent<InnerConfig>().IPEndPoint;

            //从地址缓存中取Session,如果没有则创建一个新的Session,并且保存到地址缓存中
            Session gateSession = Game.Scene.GetComponent<NetInnerComponent>().Get(innerAddress);

            // 向gate服务器请求一个key,客户端可以拿着这个key连接gate
            G2R_GetLoginKey g2RGetLoginKey = (G2R_GetLoginKey)await gateSession.Call(new R2G_GetLoginKey() { Account = request.Account });

            //上述过程已完成，将执行这句，获取外部服务器地址
            string outerAddress = config.GetComponent<OuterConfig>().Address2;

            //设置返回的信息
            response.Address = outerAddress;
            response.Key = g2RGetLoginKey.Key;
            //回复客户端的登录请求
            reply();
        }
    }
}