using System;

namespace OtherTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var type = typeof(MyClass);
            var custs = type.GetConstructors();
            foreach (var item in custs)
            {
                Console.WriteLine(item);
                foreach (var it in item.GetParameters())
                {
                    Console.WriteLine(it.Name + "  " + it.ParameterType);
                }
            }
        }
    }



    class MyClass
    {
        public MyClass()
        {

        }


        public MyClass(int id, string name)
        {
            ID = id;
            Name = name;
        }
        public MyClass(int id)
        {
            ID = id;
        }

        public int ID { get; set; }

        public string Name { get; set; }

    }
}
