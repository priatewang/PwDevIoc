using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PwDevIoc
{
    public class AutoServiceAttribute : Attribute
    {
        /// <summary>
        /// 生命周期类型
        /// </summary>
        public LifeTimeType Mode { get; set; }

        /// <summary>
        /// 接口服务对应实现类
        /// </summary>
        public Type InterfaceType { get; set; }

        public AutoServiceAttribute()
        {


        }

        public AutoServiceAttribute(LifeTimeType mode = LifeTimeType.Transient)
        {
            Mode = mode;
        }

        public AutoServiceAttribute(Type type, LifeTimeType mode = LifeTimeType.Transient)
        {
            InterfaceType = type;
            Mode = mode;
        }
    }
}
