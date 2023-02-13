using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PwDevIoc
{
    internal class ConstructorModel
    {
        public ConstructorModel() { }


        public ConstructorInfo Info { get; set; }

        public int Order { get; set; }

        internal ParameterInfo[] Parameters { get; set; }

        internal object[] values { get; set; }

    }
}
