using Newtonsoft.Json;
using System;

namespace DotNetCore.OOPS
{
    public class Dynamic
    {
        static string data;

        public static void SetObject(object value)
        {
            data = JsonConvert.SerializeObject(value);
        }

        public static T GetObject<T>(string data)
        {
            //var value = session.GetString(key);
            return  JsonConvert.DeserializeObject<T>(data);
        }

        // Main Method 
        static public void DynamicMethod()
        {
            SetObject(new { Date = DateTime.Now });
            dynamic b = GetObject<dynamic>(data);

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
