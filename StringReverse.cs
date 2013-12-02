    public static void Reverse(this StringBuilder text)
    {
        if (text.Length > 1)
        {
            int pivotPos = text.Length / 2;
            for (int i = 0; i < pivotPos; i++)
            {
                int iRight = text.Length - (i + 1);
                char rightChar = text[i];
                char leftChar = text[iRight];
                text[i] = leftChar;
                text[iRight] = rightChar;
            }
        }
    }

    public static string Reverse(this string text)
    {
        StringBuilder temp = new StringBuilder(text);
        temp.Reverse();
        return temp.ToString();
    }