using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PwDevIoc
{
    /// <summary>
    /// 服务描述类
    /// </summary>
    public class ServiceDescriptor
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 输入的标识
        /// </summary>
        public string Tag { get; set; }

        /// <summary>
        /// 源类型（接口）
        /// </summary>
        public Type Source { get; set; }

        /// <summary>
        /// 目标类
        /// </summary>
        public Type TargetService { get; set; }

        /// <summary>
        /// 生命周期类型
        /// </summary>
        public LifeTimeType ServiceType { get; set; }

        /// <summary>
        /// 单例模式的唯一对象
        /// </summary>
        public object Instance { get; set; }

        /// <summary>
        /// 默认使用的构造方法
        /// </summary>
        internal ConstructorModel Default = null;

        /// <summary>
        /// 构造方法属性集合
        /// </summary>
        internal List<ConstructorModel> Constructors { get; set; }

        /// <summary>
        /// 依赖或者需要注入的服务对象
        /// </summary>
        internal List<ServiceDescriptor> DependServices { get; set; }


        public ServiceDescriptor(Type type, LifeTimeType iocType = LifeTimeType.Transient)
          : this(type, type, iocType)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="target"></param>
        /// <param name="id"></param>
        /// <param name="iocType"></param>
        public ServiceDescriptor(Type source, Type target, LifeTimeType iocType = LifeTimeType.Transient, string tag = "")
        {
            Source = source;
            TargetService = target;
            ServiceType = iocType;
            Tag = tag;
            Id = source.Name + tag;

            InitConstructorModels();
        }

        /// <summary>
        /// 获取服务实例
        /// </summary>
        /// <returns></returns>
        public object GetService()
        {
            switch (ServiceType)
            {
                case LifeTimeType.Transient:
                    return CreateInstance(TargetService);
                case LifeTimeType.Singleton:
                    if (Instance == null)
                        Instance = CreateInstance(TargetService);
                    return Instance;
                default:
                    return CreateInstance(TargetService);
            }
        }




        /// <summary>
        /// 创建实例
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private object CreateInstance(Type type)
        {
            if (Default == null)
                return System.Activator.CreateInstance(type);
            List<object> paramters = new List<object>();
            foreach (var item in Default.Parameters)
            {
                var service = DependServices.Find(w => w.Source == item.ParameterType);
                if (service == null)
                    return System.Activator.CreateInstance(type);
                paramters.Add(service.GetService());
            }
            return Default.Info.Invoke(paramters.ToArray());
        }

        /// <summary>
        /// 构造方法信息初始化
        /// </summary>
        private void InitConstructorModels()
        {
            Constructors = new List<ConstructorModel>();
            DependServices = new List<ServiceDescriptor>();
            var infos = TargetService.GetConstructors();
            foreach (var info in infos)
            {
                var paramters = info.GetParameters();
                ConstructorModel model = new ConstructorModel()
                {
                    Info = info,
                    Order = paramters.Length,
                    Parameters = paramters,
                };
                Constructors.Add(model);
            }
            //根据参数多少进行降序排序，尽可能多的注入已经注册的接口和服务
            Constructors.Sort((x, y) => -x.Order.CompareTo(y.Order));
        }


    }
}
