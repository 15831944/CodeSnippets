// Remove empty spaces and new lines in json
string jsonFixed = Regex.Replace(inputJson, "(\"(?:[^\"\\\\]|\\\\.)*\")|\\s+", "$1");
