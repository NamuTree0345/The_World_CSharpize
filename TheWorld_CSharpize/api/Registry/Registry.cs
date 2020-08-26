using System;
using System.Collections.Generic;
using System.Text;

namespace TheWorld_CSharpize.api.Registry
{
    public class Registry
    {
        public static void Register(Registerable registerable)
        {
            Program.modRegistry.Add(registerable);
        }

        public static int GetRegistryLength()
        {
            return Program.modRegistry.Count;
        }
    }
}
