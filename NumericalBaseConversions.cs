    private static int ToBase(int number, int baseNum)
    {
        StringBuilder sb = new StringBuilder();
        while (number > 0)
        {
            int digit = number % baseNum;
            sb.Append(digit);
            number /= baseNum;
        }
        sb.Reverse();
        if (sb.Length == 0)
        {
            sb.Append(0);
        }

        return int.Parse(sb.ToString());
    }

    private static int FromBase(int number, int baseNum)
    {
        int power = 0;
        int sum = 0;
        while (number > 0)
        {
            int digit = number % 10;
            sum += (int)Math.Pow(baseNum, power) * digit;

            power++;
            number /= 10;
        }

        return sum;
    }