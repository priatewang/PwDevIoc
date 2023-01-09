using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PwDevIoc
{
    public class ContainerBuilder
    {
        public ContainerBuilder()
        {
            serviceDescriptors = new ServiceCollection();
        }

        IServiceCollection serviceDescriptors;


        public void RegisterType<TService>(LifeTimeType type = LifeTimeType.Normal) where TService : class
        {
            serviceDescriptors.Add(new ServiceDescriptor(typeof(TService), type));
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <typeparam name="IService">源类型（接口）</typeparam>
        /// <typeparam name="TService">目标类型（类）</typeparam>
        /// <param name="tag">标记</param>
        /// <param name="type"></param>
        public void RegisterType<IService, TService>(string tag, LifeTimeType type = LifeTimeType.Normal) where TService : class
        {
            serviceDescriptors.Add(new ServiceDescriptor(typeof(IService), typeof(TService), type, tag));
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <typeparam name="IService">源类型（接口）</typeparam>
        /// <typeparam name="TService">目标类型（类）</typeparam>
        /// <param name="type"></param>
        public void RegisterType<IService, TService>(LifeTimeType type = LifeTimeType.Normal) where TService : class
        {
            serviceDescriptors.Add(new ServiceDescriptor(typeof(IService), typeof(TService), type));
        }


        /// <summary>
        /// 自动加载程序集中需要注册的对象
        /// </summary>
        public void AutoRegisterIoc()
        {
            var assembies = AppDomain.CurrentDomain.GetAssemblies();

            foreach (var item in assembies)
            {
                ///跳过系统程序集
                if (item.FullName.Contains("System"))
                {
                    continue;
                }
                var types = item.GetTypes();

                Func<Attribute[], bool> IsHaveAutoIocAttribute = o =>
                {
                    foreach (Attribute attribute in o)
                    {
                        if (attribute is AutoIocAttribute)
                        {
                            return true;
                        }
                    }
                    return false;
                };

                foreach (var it in types)
                {
                    if (IsHaveAutoIocAttribute(Attribute.GetCustomAttributes(it, true)))
                    {
                        //处理ioc自动注册
                        AutoIocAttribute attribute = (AutoIocAttribute)Attribute.GetCustomAttribute(it, typeof(AutoIocAttribute));
                        if (attribute.RelationClassType == null)
                        {
                            serviceDescriptors.Add(new ServiceDescriptor(it, attribute.Mode));
                        }
                        else
                        {
                            serviceDescriptors.Add(new ServiceDescriptor(it, attribute.RelationClassType, attribute.Mode));
                          
                        }


                    }

                }



            }
        }



        public IContainer Build()
        {
            return new Container(serviceDescriptors);

        }
    }
}
