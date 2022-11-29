using System;

namespace PwDevIoc.TestApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            IocContainer container = new IocContainer();

            container.Register<User>();
            container.Register<IService, TestClass>();
            container.Register<IService, MyClass>();



            var user = container.Get<User>();
            var service = container.Get<IService>();


            user.UsePhoneSend("ioc", "success");
            Console.WriteLine("------write Service------");
            service.Send("service success");

            Console.ReadKey();
        }
    }
}
