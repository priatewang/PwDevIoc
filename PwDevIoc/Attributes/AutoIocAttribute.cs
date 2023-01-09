using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PwDevIoc
{
    public class AutoIocAttribute : Attribute
    {
        /// <summary>
        /// 生命周期类型
        /// </summary>
        public LifeTimeType Mode { get; set; }

        /// <summary>
        /// 接口服务对应实现类
        /// </summary>
        public Type RelationClassType { get; set; }

        public AutoIocAttribute()
        {

        }

        public AutoIocAttribute(LifeTimeType mode = LifeTimeType.Normal)
        {
            Mode = mode;
        }

        public AutoIocAttribute(Type type, LifeTimeType mode = LifeTimeType.Normal)
        {
            RelationClassType = type;
            Mode = mode;
        }
    }
}
