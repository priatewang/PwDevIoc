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


    internal interface IService
    {
        void Send(string msg);
    }



    internal class User
    {
        public string Name { get; set; }


        public void UsePhoneSend(string name, string msg)
        {
            Console.WriteLine($"User UsePhoneSend to {name} : {msg}");

        }
    }
}
