using System;
using ETModel;
using UnityEngine;

namespace ETHotfix.Module.ServerRoomDigitalTwin.Hander.Map
{
    [MessageHandler(AppType.Map)]
    public class G2M_SrdtCreateUnitHandler: AMRpcHandler<G2M_SrdtCreateUnit, M2G_SrdtCreateUnit>
    {
        protected override async ETTask Run(Session session, G2M_SrdtCreateUnit request, M2G_SrdtCreateUnit response, Action reply)
        {
            //创建基本单位附加移动、路径组件
            Unit unit = ComponentFactory.CreateWithId<Unit>(IdGenerater.GenerateId());
            unit.AddComponent<MoveComponent>();
            unit.AddComponent<UnitPathComponent>();
            unit.Position = new Vector3(0, 0, -10);

            //添加信箱使其成为Actor模型
            await unit.AddComponent<MailBoxComponent>().AddLocation();
            unit.AddComponent<UnitGateComponent, long>(request.GateSessionId);
            
            Game.Scene.GetComponent<UnitComponent>().Add(unit);
            response.UnitId = unit.Id;
            
            M2C_CreateUnits createUnits = new M2C_CreateUnits();
            Unit[] units = Game.Scene.GetComponent<UnitComponent>().GetAll();
            foreach (Unit u in units)
            {
                UnitInfo unitInfo = new UnitInfo();
                unitInfo.X = u.Position.x;
                unitInfo.Y = u.Position.y;
                unitInfo.Z = u.Position.z;
                unitInfo.UnitId = u.Id;
                createUnits.Units.Add(unitInfo);
            }
            MessageHelper.Broadcast(createUnits);

            reply();
        }
    }
}