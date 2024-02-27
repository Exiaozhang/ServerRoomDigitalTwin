using ETModel;
namespace ETModel
{
	[Message(OuterOpcode.C2M_TestRequest)]
	public partial class C2M_TestRequest : IActorLocationRequest {}

	[Message(OuterOpcode.M2C_TestResponse)]
	public partial class M2C_TestResponse : IActorLocationResponse {}

	[Message(OuterOpcode.Actor_TransferRequest)]
	public partial class Actor_TransferRequest : IActorLocationRequest {}

	[Message(OuterOpcode.Actor_TransferResponse)]
	public partial class Actor_TransferResponse : IActorLocationResponse {}

	[Message(OuterOpcode.C2G_TestMessage)]
	public partial class C2G_TestMessage : IRequest {}

	[Message(OuterOpcode.G2C_TestMessage)]
	public partial class G2C_TestMessage : IResponse {}

	[Message(OuterOpcode.C2G_EnterMap)]
	public partial class C2G_EnterMap : IRequest {}

	[Message(OuterOpcode.G2C_EnterMap)]
	public partial class G2C_EnterMap : IResponse {}

// 自己的unit id
// 所有的unit
	[Message(OuterOpcode.UnitInfo)]
	public partial class UnitInfo {}

	[Message(OuterOpcode.M2C_CreateUnits)]
	public partial class M2C_CreateUnits : IActorMessage {}

	[Message(OuterOpcode.Frame_ClickMap)]
	public partial class Frame_ClickMap : IActorLocationMessage {}

	[Message(OuterOpcode.M2C_PathfindingResult)]
	public partial class M2C_PathfindingResult : IActorMessage {}

	[Message(OuterOpcode.C2R_Ping)]
	public partial class C2R_Ping : IRequest {}

	[Message(OuterOpcode.R2C_Ping)]
	public partial class R2C_Ping : IResponse {}

	[Message(OuterOpcode.G2C_Test)]
	public partial class G2C_Test : IMessage {}

	[Message(OuterOpcode.C2M_Reload)]
	public partial class C2M_Reload : IRequest {}

	[Message(OuterOpcode.M2C_Reload)]
	public partial class M2C_Reload : IResponse {}

	[Message(OuterOpcode.C2R_SrdtLogin)]
	public partial class C2R_SrdtLogin : IRequest {}

	[Message(OuterOpcode.R2C_SrdtLogin)]
	public partial class R2C_SrdtLogin : IResponse {}

//客户端登录到网关
	[Message(OuterOpcode.C2G_SrdtLoginGate)]
	public partial class C2G_SrdtLoginGate : IRequest {}

	[Message(OuterOpcode.G2C_SrdtLoginGate)]
	public partial class G2C_SrdtLoginGate : IResponse {}

	[Message(OuterOpcode.PlayerInfo)]
	public partial class PlayerInfo : IMessage {}

	[Message(OuterOpcode.C2G_SrdtPlayerInfo)]
	public partial class C2G_SrdtPlayerInfo : IRequest {}

	[Message(OuterOpcode.G2C_SrdtPlayerInfo)]
	public partial class G2C_SrdtPlayerInfo : IResponse {}

	[Message(OuterOpcode.C2G_SrdtEnterMap)]
	public partial class C2G_SrdtEnterMap : IRequest {}

	[Message(OuterOpcode.G2C_SrdtEnterMap)]
	public partial class G2C_SrdtEnterMap : IResponse {}

// 自己的unit id
// 所有的unit
}
namespace ETModel
{
	public static partial class OuterOpcode
	{
		 public const ushort C2M_TestRequest = 101;
		 public const ushort M2C_TestResponse = 102;
		 public const ushort Actor_TransferRequest = 103;
		 public const ushort Actor_TransferResponse = 104;
		 public const ushort C2G_TestMessage = 105;
		 public const ushort G2C_TestMessage = 106;
		 public const ushort C2G_EnterMap = 107;
		 public const ushort G2C_EnterMap = 108;
		 public const ushort UnitInfo = 109;
		 public const ushort M2C_CreateUnits = 110;
		 public const ushort Frame_ClickMap = 111;
		 public const ushort M2C_PathfindingResult = 112;
		 public const ushort C2R_Ping = 113;
		 public const ushort R2C_Ping = 114;
		 public const ushort G2C_Test = 115;
		 public const ushort C2M_Reload = 116;
		 public const ushort M2C_Reload = 117;
		 public const ushort C2R_SrdtLogin = 118;
		 public const ushort R2C_SrdtLogin = 119;
		 public const ushort C2G_SrdtLoginGate = 120;
		 public const ushort G2C_SrdtLoginGate = 121;
		 public const ushort PlayerInfo = 122;
		 public const ushort C2G_SrdtPlayerInfo = 123;
		 public const ushort G2C_SrdtPlayerInfo = 124;
		 public const ushort C2G_SrdtEnterMap = 125;
		 public const ushort G2C_SrdtEnterMap = 126;
	}
}
