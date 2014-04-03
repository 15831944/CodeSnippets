public static string StringToBase64(string input)
{
    byte[] bytes = Encoding.UTF8.GetBytes(input);
    return Convert.ToBase64String(bytes);
}

public static string Base64ToString(string base64)
{
    byte[] data = Convert.FromBase64String(base64);
    return Encoding.UTF8.GetString(data);
}
