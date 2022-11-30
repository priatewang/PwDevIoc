using System;

namespace PwDevIoc.TestApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            ContainerBuilder builder = new ContainerBuilder();

            builder.RegisterType<User>();
            builder.RegisterType<IService, TestClass>(LifeTimeType.Singleton);
            //builder.RegisterType<IService, MyClass>("my");

            var container= builder.Build();

            var user = container.Get<User>();
            var service = container.Get<IService>();

            var service2 = container.Get<IService>();


            user.UsePhoneSend("ioc", "success");
            Console.WriteLine("------write Service------");
            service.Send("service success");

            Console.WriteLine("-----单例-----");




            Console.WriteLine(service.GetHashCode());
            Console.WriteLine(service2.GetHashCode());

            Console.ReadKey();
        }
    }
}
