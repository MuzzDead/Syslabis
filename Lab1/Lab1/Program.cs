namespace laboratory
{
    class ThirdTask
    {
        public int CalculateA(int n, int m)
        {
            if (n == 0)
            {
                return m + 1;
            }
            else if (n > 0 && m == 0)
            {
                return CalculateA(n - 1, 1);
            }
            else if (n > 0 && m > 0)
            {
                return CalculateA(n - 1, CalculateA(n, m - 1));
            }
            else
            {
                throw new ArgumentException("n і m повинні бути невід'ємними числами.");
            }

        }
    }

    class SecondTask
    {
        public int sum(int n)
        {
            int result = 0;
            for (int i = 0; i < n; i++) {
                result += Convert.ToInt32(Math.Pow(i, 3)) + 3 * i * n * n + n;
            }
            return result;
        }
    }
    class FirstTask
    {
        public double Function(double x, double z)
        {
            double result;
            result = x + 2 * x + 3;
            result = result / (z - 2);
            result = result + Math.Atan(z);
            return result;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            FirstTask func = new FirstTask();

            double x, z, y;

            Console.WriteLine("Enter x and z:");
            x = Convert.ToDouble(Console.ReadLine());
            z = Convert.ToDouble(Console.ReadLine());

            y = func.Function(x, z);

            Console.WriteLine(y);

            SecondTask func2 = new SecondTask();

            int n;

            Console.WriteLine("Enter n:");
            n = Convert.ToInt32(Console.ReadLine());

            y = func2.sum(n);
            Console.WriteLine(y);

            ThirdTask task = new ThirdTask();

            int m;

            Console.WriteLine("Enter n and m:");
            n = Convert.ToInt32(Console.ReadLine());
            m = Convert.ToInt32(Console.ReadLine());

            int result = task.CalculateA(n, m);
            Console.WriteLine("A({0}, {1}) = {2}", n, m, result);
        }
    }
}
    
   