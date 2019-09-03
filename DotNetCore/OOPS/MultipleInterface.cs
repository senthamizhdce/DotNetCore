using System;

namespace DotNetCore.OOPS
{
    // Driver Class 
    static public class MultipleInterface
    {
        // Main Method 
        static public void CallingMethod()
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
    interface Interface1
    {
        void Method();
    }

    interface Interface2
    {
        void Method();
    }

    class Class1 : Interface1, Interface2
    {
        void Interface1.Method()
        {
            Console.WriteLine("G1 : GeeksforGeeks");
        }

        void Interface2.Method()
        {
            Console.WriteLine("G2 : GeeksforGeeks");
        }

        //With or With-out public method

        // Defining method as public 
        public void Method()
        {
            Console.WriteLine("Geeks: GeeksforGeeks");
        }
    }
}
