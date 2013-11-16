class ExchangeVariableValues
    {
        static void Main()
        {
            int x = 5;
            int y = 10;
            x = x + y;
            y = x - y;
            x = x - y;
            Console.WriteLine("x = {0}", x);
            Console.WriteLine("y = {0}", y);

            int a = 5;
            int b = 10;
            a = a ^ b;
            b = a ^ b;
            a = a ^ b;
            Console.WriteLine("a = {0}", a);
            Console.WriteLine("b = {0}", b);
        }
    }
