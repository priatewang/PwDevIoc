using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PwDevIoc.TestApp
{
 
    internal class TestClass : IService
    {
        public void Send(string msg)
        {
            Console.WriteLine("TestClass Send: " + msg);
        }

    }


    internal class MyClass : IService
    {
        public void Send(string msg)
        {
            Console.WriteLine("MyClass Send: " + msg);

        }
    }

    [AutoIoc(Mode = LifeTimeType.Singleton, RelationClassType = typeof(TestClass))]
    internal interface IService
    {
        void Send(string msg);
    }


    [AutoIoc]
    internal class User
    {
        public string Name { get; set; }
        IService _service { get; set; }

        public User(IService service)
        {
            _service=service;
        }

        public void ServiceSend(string msg)
        {
            _service.Send(msg);
        }


        public void UsePhoneSend(string name, string msg)
        {
            Console.WriteLine($"User UsePhoneSend to {name} : {msg}");

        }
    }
}
