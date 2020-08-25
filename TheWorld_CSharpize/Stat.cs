using System;
using System.Collections;
using System.Collections.Generic;
namespace TheWorld_CSharpize
{
    public class Stat
    {
        //스텟
        public float Lv, Exp, MaxExp, Hp, MaxHp, HpGen, Pp, MaxPp, PpGen, Atk, Def, Speed, Power;
        public string Name;

        // 좌표
        public int x, y;

        // 분배스탯
        public int STR;
        public int INT;
        public int SPD;
        public int REG;
        public int BOD;

        // 장비
        public int Hat, Body, Leggings, Shose, Wepon, Shiled = 19;

        // 아이템
        public Dictionary<int, int> Items = new Dictionary<int, int>();
    }
}