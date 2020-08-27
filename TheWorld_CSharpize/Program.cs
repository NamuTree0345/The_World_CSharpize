using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading;
using TheWorld_CSharpize.api;
using TheWorld_CSharpize.api.Registry;

namespace TheWorld_CSharpize
{
    class Program
    {
        static Stat p;

        // MOD
        public static List<Registerable> modRegistry = new List<Registerable>();
        public static List<MainClass> mods = new List<MainClass>();

        public static void Sleep(int time)
        {
            //Thread.Sleep(time);
        }

        static int[] Item = { 5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

        static int[] ItemPrize = { 10, 50, 1000, 10000, 100000, 50, 1000, 10000, 100000, 50, 50, 50, 70, 30, 100, 70, 20, 150, 0 };

        static string[] ItemName = {"귀환석\n기절시 마을로 귀환시켜줍니다.",
                                "채력 물약(소)\nHp를 50 회복합니다.",
                                "채력 물약(중)\nHp를 750 회복합니다.",
                                "채력 물약(대)\nHp를 5,000 회복합니다.",
                                "채력 물약(특대)\nHp를 30,000 회복합니다.",
                                "마나 물약(소)\nPp를 50 회복합니다.",
                                "마나 물약(중)\nPp를 750 회복합니다.",
                                "마나 물약(대)\nPp를 5,000 회복합니다.",
                                "마나 물약(특대)\nPp를 30,000 회복합니다.",
                                "초보자의 목검",
                                "초보자의 나무 활",
                                "참나무 지팡이",
                                "초보자의 방패",
                                "토끼 가죽 투구",
                                "토끼 가죽 갑옷",
                                "토끼 가죽 바지",
                                "토끼 가죽 신발",
                                "토끼 가죽 로브",
                                "없음",
                                };

        static Stat[] Mob = new Stat[21];

        static float Money = 100;//돈

        static void SetMobStatGrade(int Grade, int Exp, int Gold, int MaxHp, int HpGen, int MaxPp, int PpGen, int Atk, int Def, int Speed, int Power)
        {
            Mob[Grade].Exp = Exp; Mob[Grade].MaxExp = Gold;
            Mob[Grade].MaxHp = MaxHp; Mob[Grade].Hp = Mob[Grade].MaxHp; Mob[Grade].HpGen = HpGen;//채력 
            Mob[Grade].MaxPp = MaxPp; Mob[Grade].Pp = MaxPp; Mob[Grade].PpGen = PpGen;//마나 
            Mob[Grade].Atk = Atk; Mob[Grade].Def = Def;//공격력, 방어력 
            Mob[Grade].Speed = Speed; Mob[Grade].Power = Power;//속도, 힘
        }
        static void playerRegenTick()//회복 
        {

            p.Hp += p.HpGen;
            p.Pp += p.PpGen;
            if (p.Hp > p.MaxHp)
            {

                p.Hp = p.MaxHp;

            }
            if (p.Pp > p.MaxPp)
            {

                p.Pp = p.MaxPp;

            }

        }


        static void Line()
        {

            ColorString(8, "━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━");

        }

        static string[] MAPINF =
        {
            "1층부터 10층의 던전에는 약한 마물들이 살고 있다.\n그 중 1층은 마을과도 연결되어 있는 곳.\n저 멀리 슬라임이 보인다.",
    "저 멀리 워터 슬라임이 보인다.",
    "저 멀리 파이어 슬라임이 보인다.",
    "저 멀리 포이즌 슬라임이 보인다.",
    "저 멀리 일렉트릭 슬라임이 보인다.",
    "저 멀리 아이스 슬라임이 보인다.",
    "저 멀리 스톤 슬라임이 보인다.",
    "저 멀리 에너지 슬라임이 보인다.",
    "저 멀리 엘리트 슬라임이 보인다.",
    "보스가 있는 최상층이다.\n저 멀리 슬라임 킹이 보인다.",
    "2층부터 20층의 던전에는 초보자가 잡을 수 없는 마물들이 살고 있다.\n저 멀리 스켈레톤이 보인다.",
    "저 멀리 스켈레톤 병사가 보인다.",
    "저 멀리 올드 스켈레톤이 보인다.",
    "저 멀리 올드 스켈레톤 병사가 보인다.",
    "저 멀리 스켈레톤 아처가 보인다.",
    "저 멀리 언데드 스켈레톤이 보인다.",
    "저 멀리 스켈레톤 군주가 보인다.",
    "저 멀리 언데드 스켈레톤 군주가 보인다.",
    "저 멀리 The Skeleton이 보인다.",
    "보스가 있는 최상층이다.\n저 멀리 스켈레톤 킹이 보인다.",
    "최종 보스가 있는 곳이다.\n저 멀리 코끼리 똥구멍이 보인다."

        };
        static string[] MobName = 
        {
            "슬라임",
    "워터 슬라임",
    "파이어 슬라임",
    "포이즌 슬라임",
    "일렉트릭 슬라임",
    "아이스 슬라임",
    "스톤 슬라임",
    "에너지 슬라임",
    "엘리트 슬라임",
    "슬라임 킹",
    "스켈레톤",
    "스켈레톤 병사",
    "올드 스켈레톤",
    "고대 스켈레톤 병사",
    "스켈레톤 아처",
    "언데드 스켈레톤",
    "스켈레톤 군주",
    "언데드 스켈레톤 군주",
    "The Skeleton",
    "스켈레톤 킹",
    "[최종보스]코끼리 똥구멍",
        };

        static void Main(string[] args)
        {
            //Array.Resize(ref MAPINF, 9999999);
            //Array.Resize(ref MobName, 9999999);
            p = new Stat();

            /*-----변수 초기화-----*/

            //플레이어 스텟	
            p.Lv = 1; p.Exp = 1000000; p.MaxExp = 10;//레벨 
            p.MaxHp = 100; p.Hp = p.MaxHp; p.HpGen = 0;//채력 
            p.MaxPp = 50; p.Pp = p.MaxPp; p.PpGen = 1;//마나 
            p.Atk = 5; p.Def = 0;//공격력, 방어력 
            p.Speed = 100; p.Power = 10;//속도, 힘 

            //플래이어 분배스텟
            p.STR = 0; p.INT = 0; p.SPD = 0; p.REG = 0; p.BOD = 0;//분배스텟
            Console.WriteLine(modRegistry.Count);
            // ModInit
            foreach (string file in Directory.GetFiles(Environment.CurrentDirectory + "\\Mods"))
            {
                if(file.EndsWith(".dll"))
                {
                    Assembly assembly = Assembly.LoadFrom(file);
                    Type[] types = assembly.GetTypes();
                    foreach (Type item in types)
                    {
                        //Console.WriteLine(item.BaseType.ToString());

                        if (item.BaseType.ToString() == "TheWorld_CSharpize.api.MainClass")
                        {
                            MainClass mC = (MainClass)Activator.CreateInstance(item);
                            mods.Add(mC);
                            mC.OnEnable();
                            mC.OnInitMobs();
                        }
                    }
                    //MainClass modmain = (MainClass)modConfig.get;
                    //mods.Add(modConfig);
                    //modmain.OnEnable();
                    //modmain.OnInitMobs();
                }
            }
            Console.WriteLine(modRegistry.Count);
            foreach(Registerable reg in modRegistry)
            {
                if(reg.getRegistryType() == RegistryType.MOB)
                {
                    api.Entity.Mob mb = (api.Entity.Mob)reg;
                    
                    Array.Resize(ref MobName, MobName.Length + 1);
                    Array.Resize(ref MAPINF, MAPINF.Length + 1);
                    Array.Resize(ref Mob, Mob.Length + 1);
                    Mob[Mob.Length - 1] = mb.getStat();
                    MobName[MobName.Length - 1] = mb.getMobName();
                    MAPINF[MAPINF.Length - 1] = mb.getMobDesc();
                    Console.WriteLine(MAPINF.Length);
                }
            }

            City();
        }
        static void Shop()//상점 
        {



            playerRegenTick();

            string Select = "";

            Line();
            ColorString(3, "-잡화상점-");
            Line();
            ColorString(9, "-여러 가지 물품을 판매하고 있다.");

        F1:

            Line();
            ColorString(10, "[메뉴와 호환되는 숫자를 입력하세요.]");
            ColorString(7, "1:소모품");
            //ColorString(7,"2:장비\n");
            //ColorString(7,"3:판매\n");
            ColorString(7, "4:탈주");

            Select = Console.ReadLine();

            switch (Select)
            {

                case "1":
                    ShopExpend();
                    break;

                    //case 2:
                    break;

                    //case 3:
                    break;

                case "4":
                    City();
                    break;

                default:
                    goto F1;

            }


        }

        static void ShopExpend()//소모품 
        {



            string Select;

            Line();
            ColorString(3, "-소모품-");

        G1:

            Line();
            ColorString(10, "[메뉴와 호환되는 숫자를 입력하세요.]");
            ColorString(7, "1:물약");
            ColorString(7, "2:[10골드]귀환석");
            ColorString(7, "3:탈주");

            Select = Console.ReadLine();

            switch (Select)
            {

                case "1":
                    ShopPotion();
                    break;

                case "2":
                    BuyItem(0);
                    break;

                case "3":
                    City();
                    break;

                default:
                    goto G1;

            }

        }

        static void BuyItem(int ItemNum)//아이템 구매 
        {

            if (Money > ItemPrize[ItemNum])
            {

                Item[ItemNum] += 1;
                Money -= ItemPrize[ItemNum];

                Line();
                ColorString(6, "");
                Console.WriteLine("{0}을(를)구매하였습니다!", ItemName[ItemNum]);

            }
            else
            {

                Line();
                ColorString(4, "");
                Console.WriteLine("골드가 모자랍니다!");

            }

            Shop();

        }

        static void ShopPotion()//물약 
        {



            int Select = 0;

            Line();
            ColorString(3, "-물약-");

        H1:

            Line();
            ColorString(10, "[메뉴와 호환되는 숫자를 입력하세요.]");
            ColorString(7, "1:[50골드]채력 물약(소)[Hp 50 회복]");
            ColorString(7, "2:[1,000골드]채력 물약(중)[Hp 750 회복]");
            ColorString(7, "3:[10,000골드]채력 물약(대)[Hp 5,000 회복]");
            ColorString(7, "4:[100,000골드]채력 물약(특대)[Hp 30,000 회복]");
            ColorString(7, "5:[50골드]마나 물약(소)[Pp 50 회복]");
            ColorString(7, "6:[1,000골드]마나 물약(중)[Pp 750 회복]");
            ColorString(7, "7:[10,000골드]마나 물약(대)[Pp 5,000 회복]");
            ColorString(7, "8:[100,000골드]마나 물약(특대)[Pp 30,000 회복]");
            ColorString(7, "9:탈주");

            Select = int.Parse(Console.ReadLine());

            if (0 < Select)
            {

                if (Select < 9)
                {

                    BuyItem(Select);

                }
                else if (Select == 9)
                {

                    City();

                }

            }

            goto H1;

        }

        static void SetMobStat()
        {
            SetMobStatGrade(0, 100, 10, 30, 0, 0, 0, 3, 0, 40, 3);
            SetMobStatGrade(1, 200, 20, 40, 0, 0, 0, 4, 0, 100, 6);
            SetMobStatGrade(2, 400, 40, 60, 0, 0, 0, 6, 0, 100, 9);
            SetMobStatGrade(3, 700, 70, 90, 1, 0, 0, 9, 0, 100, 12);
            SetMobStatGrade(4, 1100, 120, 150, 1, 0, 0, 13, 1, 100, 15);
            SetMobStatGrade(5, 1600, 190, 230, 2, 0, 0, 18, 2, 100, 18);
            SetMobStatGrade(6, 2200, 280, 480, 4, 0, 0, 24, 3, 100, 21);
            SetMobStatGrade(7, 2900, 450, 750, 7, 0, 0, 31, 4, 100, 24);
            SetMobStatGrade(8, 3700, 600, 1000, 10, 0, 0, 39, 5, 100, 27);
            SetMobStatGrade(9, 15000, 2000, 5000, 50, 10, 0, 50, 10, 100, 50);
            SetMobStatGrade(10, 5000, 800, 2000, 20, 10, 1, 100, 20, 100, 60);
            SetMobStatGrade(11, 10000, 1700, 3000, 30, 20, 2, 200, 35, 100, 70);
            SetMobStatGrade(12, 15000, 2600, 5000, 50, 30, 3, 300, 70, 100, 80);
            SetMobStatGrade(13, 20000, 3500, 8500, 85, 40, 4, 450, 120, 100, 90);
            SetMobStatGrade(14, 25000, 4700, 10000, 200, 50, 5, 700, 200, 100, 100);
            SetMobStatGrade(15, 30000, 6000, 17500, 175, 60, 6, 1350, 300, 100, 110);
            SetMobStatGrade(16, 40000, 8000, 25000, 250, 70, 7, 1750, 450, 100, 120);
            SetMobStatGrade(17, 50000, 10500, 40000, 400, 80, 8, 2500, 1000, 100, 130);
            SetMobStatGrade(18, 70000, 14500, 100000, 1000, 90, 9, 4000, 1500, 100, 140);
            SetMobStatGrade(19, 200000, 25000, 250000, 2500, 200, 10, 10000, 3000, 100, 200);
            SetMobStatGrade(20, 2147483100, 2147483100, 2147483100, 2147483100, 2147483100, 2147483100, 2147483100, 2147483100, 2147483100, 2147483100);
        }

        static void City()//마을 
        {

            //몹 스텟 초기화 
            Console.WriteLine(Mob.Length);
            for(int i = 0; i < Mob.Length; i++)
            {
                Mob[i] = new Stat();
            }

            SetMobStat();

            //힐 

            p.Hp = p.MaxHp;

            p.Pp = p.MaxPp;

            playerRegenTick();

            string Select = "";



            Line();
            ColorString(8, "-던전의 마을-");

            Line();
            ColorString(15, "유명한 던전이 있는 마을이다.");

        B1://B1

            Line();
            ColorString(10, "[메뉴와 호환되는 숫자를 입력하세요.]");
            ColorString(7, "1:던전 입장");
            ColorString(7, "2:잡화상점 입장");
            ColorString(7, "3:스테이더스 확인");
            ColorString(7, "4:정비");
            ColorString(7, "5:모드(TML)");

            Select = Console.ReadLine();

            switch (Select)
            {

                case "1":
                    Dungeon(0);
                    break;

                case "2":
                    Shop();
                    break;

                case "3":
                    ShowStat();
                    goto B1;
                    break;

                case "4":
                    ShowArmor();
                    ShowInventory();
                    SelectItem();
                    goto B1;
                    break;

                case "5":
                    ShowMods();
                    break;

                default:
                    goto B1;//B1

            }


        }

        static void ShowMods()
        {
            Console.WriteLine("모드 리스트");
            Console.WriteLine("-----------");
            foreach (MainClass modConfig in mods)
            {
                Console.WriteLine(modConfig.MOD_NAME + " - " + modConfig.MOD_DESC + "(by " + modConfig.MOD_AUTHOR + ")");
            }
            if(mods.Count == 0)
            {
                Console.WriteLine("모드가 없습니다.");
            }
            Console.WriteLine("-----------");
            Console.WriteLine("아무키나 누를시 탈주합니다.");
             Console.ReadKey();
            Console.Clear();
            City();
        }

        static void SelectItem()
        {

            int Select = 0;

            int i = 0;

            Line();
            ColorString(10, "[메뉴와 호환되는 숫자를 입력하세요.]");

            while (i < 19)
            {

                if (Item[i] > 0)
                {

                    Line();

                    ColorString(15, "");

                    Console.WriteLine("{0}:{1}", i + 1, i, ItemName[i]);



                }
                i += 1;

            }

            Select = int.Parse(Console.ReadLine());

            Select -= 1;

            if (Item[Select] > -1)
            {

                Useitem(Select);

            }
            else
            {

                Line();
                ColorString(5, "아이템이 존재하지 않거나 갯수가 부족합니다!");

            }

        }

        static void Useitem(int Num)
        {

            if (Item[Num] > -1)
            {

                switch (Num)
                {

                    case 0:
                        Line();
                        ColorString(4, "마을로 귀환합니다.");
                        Sleep(800);
                        Item[0] -= 1;
                        City();
                        break;

                    case 1:
                        Line();
                        break;

                    default:
                        Line();
                        ColorString(5, "아이템이 존재하지 않거나 갯수가 부족합니다!");
                        SelectItem();
                        break;

                }

            }

        }

        static void ShowInventory()//인벤토리
        {



            Line();

            ColorString(1, "-인벤토리-");

        }

        static void ShowStat()//스텟창 
        {

            Line();
            ColorString(1, "-스테이더스-");
            Line();
            ColorString(15, "");
            Console.WriteLine("LV:{0} Exp:{1}/{2}", p.Lv, p.MaxExp, p.Exp);
            Console.WriteLine("Hp:{0}/{1} HpGen:{2}", p.MaxHp, p.Hp, p.HpGen);
            Console.WriteLine("Pp:{0}/{1} PpGen:{2}", p.MaxPp, p.Pp, p.PpGen);
            Console.WriteLine("Atk:{0} Def:{1}", p.Atk, p.Def);
            Console.WriteLine("Speed:{0} Power:{1}", p.Speed, p.Power);

        }

        static void ShowArmor()//장비창
        {

            Line();
            ColorString(1, "-장비-");

            Line();
            ColorString(15, "");
            Console.WriteLine("모자:{0}", ItemName[p.Hat - 1]);
            Console.WriteLine("갑옷:{0}", ItemName[p.Body - 1]);
            Console.WriteLine("바지:{0}", ItemName[p.Leggings - 1]);
            Console.WriteLine("신발:{0}", ItemName[p.Shose - 1]);
            Console.WriteLine("무기:{0}", ItemName[p.Wepon - 1]);
            Console.WriteLine("방패:{0}", ItemName[p.Shiled - 1]);

        }

        static void CheckMobDied(int Grade)//몹 사망여부 
        {

            if (Mob[Grade].Hp < 1)
            {

                Line();
                ColorString(1, "");
                Console.WriteLine("{0}이(가) 죽었습니다!", MobName[Grade]);
                Sleep(800);

                p.Exp += Mob[Grade].Exp;
                Line();
                ColorString(2, "");
                Console.WriteLine("경험치를 {0} 획득하였습니다!", Mob[Grade].Exp);
                Sleep(800);

                Money += Mob[Grade].MaxExp;
                Line();
                ColorString(6, "");
                Console.WriteLine("{0}골드를 획득하였습니다!", Mob[Grade].MaxExp);
                Sleep(800);

                CheckLvUp();

                Dungeon(Grade + 1);

            }

        }

        static void LevelUp()//레벨업 
        {

            Line();
            ColorString(6, "레벨이 올랐습니다!");

            p.Lv += 1;
            p.Exp -= p.MaxExp;
            p.Hp = p.MaxHp;
            p.Pp = p.MaxPp;
            p.MaxExp *= 1.1F;
            p.MaxHp *= 1.04F;
            p.MaxPp *= 1.041F;
            p.HpGen += p.Lv / 5;
            p.PpGen += p.Lv / 3;
            p.Atk += p.Lv / 10;
            p.Def += p.Lv / 35;
            p.Speed += p.Lv / 100;
            p.Power += p.Lv / 20;

            CheckLvUp();

        }

        static void CheckLvUp()//레벨업 확인 
        {

            if (p.Exp > p.MaxExp - 1)
            {

                LevelUp();

            }

        }

        static void Dungeon(int Grade)//던전 
        {

            Dungeon_Start(Grade);

            Dungeon_Main(Grade);

        }

        static void Dungeon_Start(int Grade)//던전 입장 
        {



            playerRegenTick();
            Line();
            ColorString(3, "-던전에 입장하였습니다!-");
            Sleep(2000);
            Line();
            ColorString(4, "-기절시 귀환석이 없으면 사망하니 주의해주세요.");
            Sleep(2000);

        }

        static void Dungeon_Main(int Grade)//던전 선택창 
        {

            playerRegenTick();

            string Select = "";

            Line();
            ColorString(3, "");
            Console.WriteLine("-던전 {0}층-", Grade + 1);

            Line();
            ColorString(9, "");
            Console.WriteLine("{0}", MAPINF[Grade]);

        A1://A1

            Line();
            ColorString(10, "[메뉴와 호환되는 숫자를 입력하세요.]");
            ColorString(7, "1:전투");
            ColorString(7, "2:스탯 확인");
            ColorString(7, "3:정비");
            //ColorString(7,"3:인벤토리\n");

            Select = Console.ReadLine();
            //scanf("%d", &Select);

            switch (Select)
            {

                case "1":
                    Dungeon_Fight(Grade);
                    break;

                case "2":
                    ShowStat();
                    Dungeon_Main(Grade);
                    break;

                case "3":
                    ShowArmor();
                    ShowInventory();
                    SelectItem();
                    break;

                //case 3:
                //break;

                default:
                    goto A1; //A1

            }

        }
        static int Dungeon_Fight(int Grade)//전투 
        {

            playerRegenTick();

            Line();
            ColorString(4, "");
            Console.WriteLine("[{0}이(가) 튀어나왔다!]", MobName[Grade]);
            Sleep(1000);

            string Select = "";

        E1:

            Line();
            ColorString(2, "[무엇을 하시겠습니까?]");

        C1:

            Line();
            ColorString(10, "[메뉴와 호환되는 숫자를 입력하세요.]");
            ColorString(7, "1:공격");
            //ColorString(7,"2:방어\n");
            ColorString(7,"3:아이템 사용");
            //ColorString(7,"4:스킬\n");

            Select = Console.ReadLine();

            switch (Select)
            {

                case "1":
                    Fight_Attek(Grade);
                    break;
                case "3":
                    ShowInventory();
                    SelectItem();
                    break;

                default:
                    goto C1;//C1
                    break;

            }

            CheckMobDied(Grade);

            MobReganTick(Grade);

            MobAttek(Grade);

            CheckPlayerDied();

            goto E1;

        }

        static void MobReganTick(int Grade)
        {
            Mob[Grade].Hp += Mob[Grade].HpGen;
            Mob[Grade].Pp += Mob[Grade].PpGen;

            if (Mob[Grade].Hp > Mob[Grade].MaxHp)
            {

                Mob[Grade].Hp = Mob[Grade].MaxHp;

            }
            if (Mob[Grade].Pp > Mob[Grade].MaxPp)
            {

                Mob[Grade].Pp = Mob[Grade].MaxPp;

            }
        }

        static void MobAttek(int Grade)//몹 공격 
        {

            Line();
            ColorString(5, "");
            Console.WriteLine("{0}의 공격!", MobName[Grade]);
            Sleep(800);

            if (Mob[Grade].Atk - p.Def < 0)
            {

                Line();
                ColorString(4, "");
                Console.WriteLine("0의 피해를 입었습니다!");
                Sleep(800);

            }
            else
            {

                p.Hp -= Mob[Grade].Atk - p.Def;

                Line();
                ColorString(4, "");
                Console.WriteLine("{0}의 피해를 입었습니다!", Mob[Grade].Atk - p.Def);
                Sleep(800);

            }

            if (p.Hp < 0)
            {

                p.Hp = 0;

            }

            Line();
            ColorString(2, "");
            Console.WriteLine("남은 채력 {0}/{1}", p.MaxHp, p.Hp);
            Sleep(800);


        }


        static void CheckPlayerDied()//플래이어 사망여부 
        {

            if (p.Hp < 1)
            {

                Line();
                ColorString(4, "기절하셨습니다.");
                Sleep(800);

                if (Item[0] > 0)
                {

                    Line();
                    ColorString(4, "귀환석이 남아 있어 사망하지 않고 마을로 귀환합니다.");
                    Sleep(800);
                    Line();
                    ColorString(4, "기절하여 경험치가 0으로 초기화되며, 요구되는 경험치량이 2배 증가합니다.");
                    Sleep(800);
                    p.Exp = 0;
                    p.MaxExp *= 2;
                    p.Hp = 1;
                    Item[0] -= 1;
                    City();

                }
                else
                {

                    Line();
                    ColorString(4, "귀환석이 남아 있지 않습니다.\n당신은 사망하였습니다.");
                    Sleep(800);
                    Line();
                    ColorString(4, "게임을 처음부터 다시 시작합니다.");
                    Sleep(800);
                    Main(new string[0]);

                }

            }

        }

        static void Fight_Attek(int Grade)//공격 
        {

            if (p.Atk - Mob[Grade].Def < 0)
            {

                Line();
                ColorString(15, "");
                Console.WriteLine("{0}에게 0의 피해를 입혔습니다!", arg0: Mob[Grade]);
                Sleep(800);

            }
            else
            {

                Line();
                ColorString(15, "");
                Console.WriteLine("{0}에게 {1}의 피해를 입혔습니다!", MobName[Grade], p.Atk - Mob[Grade].Def);
                Sleep(800);

                Mob[Grade].Hp -= p.Atk - Mob[Grade].Def;

            }

            if (Mob[Grade].Hp < 0)
            {

                Mob[Grade].Hp = 0;

            }

            Line();
            ColorString(15, "");
            Console.WriteLine("{0} 남은 채력:{1}/{2}", MobName[Grade], Mob[Grade].MaxHp, Mob[Grade].Hp);
            Sleep(800);

        }


        static int ColorString(int color, string String)//색깔 택스트 출력 
        {

            ConsoleColor clr = ConsoleColor.White;
            switch(color)
            {
                case 0:
                    clr = ConsoleColor.Black; break;
                case 1:
                    clr = ConsoleColor.Blue; break;
                case 2:
                    clr = ConsoleColor.Green; break;
                case 3:
                    clr = ConsoleColor.Cyan; break;
                case 4:
                    clr = ConsoleColor.Red; break;
                case 5:
                    clr = ConsoleColor.Magenta; break;
                case 6:
                    clr = ConsoleColor.Yellow; break;
                case 7:
                    clr = ConsoleColor.Gray; break;
                case 8:
                    clr = ConsoleColor.DarkGray; break;
                case 9:
                    clr = ConsoleColor.Blue; break;
                case 10:
                    clr = ConsoleColor.Green; break;
                case 12:
                    clr = ConsoleColor.Red; break;
                case 13:
                    clr = ConsoleColor.Magenta; break;
                case 14:
                    clr = ConsoleColor.Yellow; break;
                case 15:
                    clr = ConsoleColor.White; break;
            }

            Console.ForegroundColor = clr;
            Console.WriteLine(String);
            Console.ResetColor();

            return 0;

        }
    }
    
}


