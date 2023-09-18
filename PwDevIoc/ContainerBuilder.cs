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


        public void RegisterType<TService>(LifeTimeType type = LifeTimeType.Transient) where TService : class
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
        public void RegisterType<IService, TService>(string tag, LifeTimeType type = LifeTimeType.Transient) where TService : class
        {
            serviceDescriptors.Add(new ServiceDescriptor(typeof(IService), typeof(TService), type, tag));
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <typeparam name="IService">源类型（接口）</typeparam>
        /// <typeparam name="TService">目标类型（类）</typeparam>
        /// <param name="type"></param>
        public void RegisterType<IService, TService>(LifeTimeType type = LifeTimeType.Transient) where TService : class
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
                        if (attribute is AutoServiceAttribute)
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
                        AutoServiceAttribute attribute = (AutoServiceAttribute)Attribute.GetCustomAttribute(it, typeof(AutoServiceAttribute));
                        if (attribute.InterfaceType == null)
                        {
                            serviceDescriptors.Add(new ServiceDescriptor(it, attribute.Mode));
                        }
                        else
                        {
                            serviceDescriptors.Add(new ServiceDescriptor(attribute.InterfaceType, it,  attribute.Mode));

                        }


                    }

                }



            }
        }



        public IContainer Build()
        {
            foreach (var item in serviceDescriptors)
            {
                foreach (var it in item.Constructors)
                {
                    bool isDefault = item.Default == null;
                    foreach (var info in it.Parameters)
                    {
                        var descriptor = serviceDescriptors.First(x => x.Source == info.ParameterType);
                        if (descriptor == null)
                        {
                            isDefault = false;
                            continue;
                        }
                        item.DependServices.AddWithout(descriptor);
                    }
                    if (isDefault)
                    {
                        item.Default = it;
                    }
                }
            }

            return new Container(serviceDescriptors);

        }
    }
}
