using System;

namespace DotNetCore.CSharp.DesignPatterns.Creational
{
    public class SingletonRun
    {
        public void RunSingleton()
        {

        }
    }
    public class SingletonNotThreadSafe
    {
        private static SingletonNotThreadSafe instance = null;

        private SingletonNotThreadSafe()
        {
        }

        public static SingletonNotThreadSafe Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SingletonNotThreadSafe();
                }
                return instance;
            }
        }
    }

    // Bad code! Do not use!
    public sealed class SingletonDoubleCheckLocking
    {
        private static SingletonDoubleCheckLocking instance = null;
        private static readonly object padlock = new object();

        SingletonDoubleCheckLocking()
        {
        }

        public static SingletonDoubleCheckLocking Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (padlock)
                    {
                        if (instance == null)
                        {
                            instance = new SingletonDoubleCheckLocking();
                        }
                    }
                }
                return instance;
            }
        }
    }

    //Fourth version - not quite as lazy, but thread-safe without using locks
    public sealed class SingletonWithoutLock
    {
        private static readonly SingletonWithoutLock instance = new SingletonWithoutLock();

        // Explicit static constructor to tell C# compiler
        // not to mark type as beforefieldinit
        static SingletonWithoutLock()
        {
        }

        private SingletonWithoutLock()
        {
        }

        public static SingletonWithoutLock Instance
        {
            get
            {
                return instance;
            }
        }
    }

    //Fifth version - fully lazy instantiation
    public sealed class SingletonFullLazy
    {
        private SingletonFullLazy()
        {
        }

        public static SingletonFullLazy Instance { get { return Nested.instance; } }

        private class Nested
        {
            // Explicit static constructor to tell C# compiler
            // not to mark type as beforefieldinit
            static Nested()
            {
            }

            internal static readonly SingletonFullLazy instance = new SingletonFullLazy();
        }
    }

    //Sixth version - using .NET 4's Lazy<T> type
    public sealed class SingletonLazy
    {
        private static readonly Lazy<SingletonLazy>
            lazy = new Lazy<SingletonLazy>(() => new SingletonLazy());

        public static SingletonLazy Instance { get { return lazy.Value; } }

        private SingletonLazy()
        {
        }
    }
}
