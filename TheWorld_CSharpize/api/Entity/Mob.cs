using System;
using System.Collections.Generic;
using System.Text;
using TheWorld_CSharpize.api.Registry;

namespace TheWorld_CSharpize.api.Entity
{
    public abstract class Mob : Registerable
    {
        private string mobName;
        private string mobDesc;
        private Stat stat;
        public Mob()
        {
            setRegistryType(RegistryType.MOB);
            
        }
        protected void setMobName(string str)
        {
            mobName = str;
        }
        public string getMobName()
        {
            return mobName;
        }
        protected void setStat(Stat _stat)
        {
            stat = _stat;
        }
        protected void setMobDesc(string _mobDesc)
        {
            mobDesc = _mobDesc;
        }
        public string getMobDesc()
        {
            return mobDesc;
        }
        
        public Stat getStat()
        {
            return stat;
        }
    }
}
