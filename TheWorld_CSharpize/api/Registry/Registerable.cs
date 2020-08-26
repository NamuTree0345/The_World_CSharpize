using System;
using System.Collections.Generic;
using System.Text;

namespace TheWorld_CSharpize.api.Registry
{
    public class Registerable
    {
        private string registryName;
        private RegistryType registryType;

        public RegistryType getRegistryType()
        {
            return registryType;
        }

        public void setRegistryName(string _n)
        {
            this.registryName = _n;
        }
        public void setRegistryType(RegistryType _n)
        {
            this.registryType = _n;
        }
    }
}
