using System;

namespace ETModel
{
    [ObjectSystem]
    public class UserAwakeSystem: AwakeSystem<User, String>
    {
        public override void Awake(User self, String account)
        {
            self.Awake(account);
        }
    }

    public class User: Entity
    {
        //数据库中的账号信息
        public String Account { get; private set; }
        public long UnitId { get; set; }

        public void Awake(String account)
        {
            this.Account = account;
        }
    }
}