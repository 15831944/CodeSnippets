// Convert unicode escape sequences to unicode characters
public static string DecodeUnicodeEscapeText(string sequence)
{
    Regex regex = new Regex(@"[\\|%][uU]([0-9A-F]{4})", RegexOptions.IgnoreCase);
    return regex.Replace(sequence, match => ((char)int.Parse(match.Groups[1].Value,
      NumberStyles.HexNumber)).ToString());
}
