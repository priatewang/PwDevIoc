﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PwDevIoc
{
    public class ServiceDescriptor
    {
        public string Id { get; set; }

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


        public ServiceDescriptor(Type type, LifeTimeType iocType = LifeTimeType.Normal)
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
        public ServiceDescriptor(Type source, Type target, LifeTimeType iocType = LifeTimeType.Normal, string tag = "")
        {
            Source = source;
            TargetService = target;
            ServiceType = iocType;
            Tag = tag;
            Id = source.Name + tag;

        }


        public object GetService()
        {
            switch (ServiceType)
            {
                case LifeTimeType.Normal:
                    return CreateInstance(TargetService);
                case LifeTimeType.Singleton:
                    if (Instance == null)
                        Instance = CreateInstance(TargetService);
                    return Instance;
                default:
                    return CreateInstance(TargetService);
            }
        }

        private object CreateInstance(Type type)
        {
            return System.Activator.CreateInstance(type);
        }

    }
}
