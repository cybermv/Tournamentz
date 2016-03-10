namespace Tournamentz.TestConsole
{
    using DAL;
    using System;

    internal class Program
    {
        private static void Main(string[] args)
        {
            int two = Dummy.GetNumberTwo();
            Console.WriteLine("Hello Tournamentz! This is number two: {0}", two);
            Console.Read();
        }
    }
}