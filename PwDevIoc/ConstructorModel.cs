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
        internal ConstructorModel() { }

        /// <summary>
        /// 构造方法Info
        /// </summary>
        internal ConstructorInfo Info { get; set; }

        /// <summary>
        /// 排序标记（参数个数）
        /// </summary>
        internal int Order { get; set; }

        /// <summary>
        /// 参数集合
        /// </summary>
        internal ParameterInfo[] Parameters { get; set; }

    }
}
