if (Environment.CurrentDirectory
               .ToLower()
               .EndsWith("bin\\debug"))
{
    Console.SetIn(new StreamReader("input.txt"));
}
