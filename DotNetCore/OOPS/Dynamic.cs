using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCore.OOPS
{
    public class Dynamic
    {
        // Main Method 
        static public void DynamicMethod()
        {
            Interface1 obj = new Class1();
            obj.Method();

            Interface2 ob = new Class1();
            ob.Method();

            Class1 obj2 = new Class1();
            obj2.Method();

            dynamic myvalue = 10;
            Console.WriteLine("Get the actual type of val1: {0}",
                                  myvalue.GetType().ToString());
            myvalue = "10q";
            Console.WriteLine("Get the actual type of val1: {0}",
                                 myvalue.GetType().ToString());

            myvalue = 10d;
            Console.WriteLine("Get the actual type of val1: {0}",
                                 myvalue.GetType().ToString());
            myvalue = obj2;

            Console.WriteLine("Get the actual type of val1: {0}",
                     myvalue.GetType().ToString());

            myvalue.Method();

            Console.WriteLine("Get the actual type of val1: {0}",
                                 obj.GetType().ToString());

            Console.WriteLine("Get the actual type of val1: {0}",
                                 ob.GetType().ToString());

            Console.WriteLine("Get the actual type of val1: {0}",
                                 obj2.GetType().ToString());
        }
    }
}
