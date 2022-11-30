﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PwDevIoc
{
    public class Container : IContainer
    {
        private IServiceCollection _services;

        public Container(IServiceCollection serviceDescriptors)
        {
            _services = serviceDescriptors;
        }


        public TService Get<TService>(string tag = "")
        {
            return CastInstance<TService>(_services[typeof(TService).Name + tag].GetService());
        }

        public TService Resolve<TService>(string tag = "")
        {
            return Get<TService>();
        }

        private static TService CastInstance<TService>(object instance)
        {
            try
            {
                // Allow a cast from null object to null TService.
                return (TService)instance;
            }
            catch (InvalidCastException castException)
            {
                return default(TService);
            }
        }
    }
}
