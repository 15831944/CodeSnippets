/* Printing special characters on the console needs two steps:
 * Change the console propertiesto enable Unicode-friendly font
 * Enable Unicode for the Consoleby adjusting its output encoding
 * Prefer UTF8 (Unicode)
 */

using System.Text;

Console.OutputEncoding = Encoding.UTF8;
Console.WriteLine("Това е кирилица: ☺");

/*
 * The currency format and number formats are different in different countries
 * E.g. the decimal separator could be "." or ","
 * To ensure the decimal separator is "." use the following code:
 */

using System.Threading;
using System.Globalization;

Thread.CurrentThread.CurrentCulture =
  CultureInfo.InvariantCulture;
Console.WriteLine(3.54); // 3.54
decimal value = decimal.Parse("1.33");
