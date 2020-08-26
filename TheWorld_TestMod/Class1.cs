using System;
using TheWorld_CSharpize;
using TheWorld_CSharpize.api;
using TheWorld_TestMod;
using TheWorld_CSharpize.api.Entity;
using TheWorld_CSharpize.api.Registry;

namespace TheWorld_TestMod
{
    public class Class1 : MainClass
    {
        public Class1()
        {
            MOD_NAME = "testmod";
            MOD_DESC = "ehllo";
            MOD_AUTHOR = "NamuTre0e345";
        }

        public override void OnEnable()
        {
            Console.WriteLine("This is mod");
            return;
        }

        public override void OnInitMobs()
        {
            Registry.Register(new TestMob());
            Console.WriteLine("TestMob Gen: " + Registry.GetRegistryLength());
            return;
        }
    }

    public class TestMob : Mob
    {
        public TestMob()
        {
            setRegistryType(TheWorld_CSharpize.api.Registry.RegistryType.MOB);
            setRegistryName("test_mob");
            setMobName("TestMob");
            setMobDesc("난 테스트");
            Stat d = new Stat();
            //1층
            d.Exp = 1; d.MaxExp = 5;
            d.MaxHp = 30; d.Hp = d.MaxHp; d.HpGen = 0;//채력 
            d.MaxPp = 10; d.Pp = d.MaxPp; d.PpGen = 1;//마나 
            d.Atk = 3; d.Def = 0;//공격력, 방어력 
            d.Speed = 40; d.Power = 3;//속도, 힘
            setStat(d);
        }
    }
}