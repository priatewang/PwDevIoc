using System;
using System.Collections.Generic;

namespace PwDevIoc
{
    public class IocContainer
    {
        private static Dictionary<Type, Type> relations = new Dictionary<Type, Type>();


        public IocContainer()
        {

        }

        /// <summary>
        /// 注册单个类型
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        public void Register<TService>()
        {
            relations[typeof(TService)] = typeof(TService);
        }
        /// <summary>
        /// 注册接口和类型的关系
        /// </summary>
        /// <typeparam name="ITService"></typeparam>
        /// <typeparam name="TService"></typeparam>
        public void Register<ITService, TService>()
        {
            relations[typeof(ITService)] = typeof(TService);
        }

        /// <summary>
        /// 获取类型的实体对象
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <returns></returns>
        public TService Get<TService>()
        {
            if (relations.TryGetValue(typeof(TService), out var service))
            {
                return (TService)CreateInstance(service);
            }
            return default(TService);
        }

        /// <summary>
        /// 根据类型Type创建对象
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private object CreateInstance(Type type)
        {
            return System.Activator.CreateInstance(type);
        }

    }
}
