namespace ETModel
{
	[ObjectSystem]
	public class UnitGateComponentAwakeSystem : AwakeSystem<UnitGateComponent, long>
	{
		public override void Awake(UnitGateComponent self, long a)
		{
			self.Awake(a);
		}
	}

	/// <summary>
	/// 管理Gate服务器的ActorId
	/// </summary>
	public class UnitGateComponent : Component, ISerializeToEntity
	{
		public long GateSessionActorId;

		public bool IsDisconnect;

		public void Awake(long gateSessionId)
		{
			this.GateSessionActorId = gateSessionId;
		}
	}
}