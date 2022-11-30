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



        public IContainer Build()
        {
            return new Container(serviceDescriptors);

        }
    }
}
