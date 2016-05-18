using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DelegatesAndEvents
{
    /*
    Example 2
    
        This example demonstrates composing delegates.A useful property of delegate objects is that they can be composed using the "+" operator. 
        A composed delegate calls the two delegates it was composed from. Only delegates of the same type can be composed.
        The "-" operator can be used to remove a component delegate from a composed delegate.
    */
    // compose.cs
    using System;

    delegate void MyDelegate(string s);

    class MyClass
    {
        public static void Hello(string s)
        {
            Console.WriteLine("  Hello, {0}!", s);
        }

        public static void Goodbye(string s)
        {
            Console.WriteLine("  Goodbye, {0}!", s);
        }

        public static void Main()
        {
            MyDelegate a, b, c, d;

            // Create the delegate object a that references 
            // the method Hello:
            a = Hello;// new MyDelegate(Hello);

            // Create the delegate object b that references 
            // the method Goodbye:
            b = Goodbye;// new MyDelegate(Goodbye);

            // The two delegates, a and b, are composed to form c: 
            c = a + b;

            // Remove a from the composed delegate, leaving d, 
            // which calls only the method Goodbye:
            d = c - a;

            Console.WriteLine("Invoking delegate a:");
            a("A");
            Console.WriteLine("Invoking delegate b:");
            b("B");
            Console.WriteLine("Invoking delegate c:");
            c("C");
            Console.WriteLine("Invoking delegate d:");
            d("D");
            Console.WriteLine("Press any key to exit ....");
            Console.ReadKey();
        }

        
    }

}
