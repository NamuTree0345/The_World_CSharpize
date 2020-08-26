using System;
using System.Collections.Generic;
using System.Text;

namespace TheWorld_CSharpize.api
{
    public abstract class MainClass
    {
        public virtual void OnEnable() { }
        public virtual void OnDisable() { }
        public virtual void OnInitMobs() { }

        public string MOD_NAME { get; set; }
        public string MOD_DESC { get; set; }
        public string MOD_AUTHOR { get; set; }
    }
}
