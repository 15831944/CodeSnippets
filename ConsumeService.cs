// Consume service
// To accept untrusted https certificate
System.Net.ServicePointManager.ServerCertificateValidationCallback +=
    delegate(object sender, System.Security.Cryptography.X509Certificates.X509Certificate certificate,
                            System.Security.Cryptography.X509Certificates.X509Chain chain,
                            System.Net.Security.SslPolicyErrors sslPolicyErrors)
    {
        return true; // **** Always accept
    };

byte[] requestBody = Encoding.UTF8.GetBytes(json);
HttpWebRequest request = WebRequest.Create(@"https://localhost:43553/foo.svc/bar") as HttpWebRequest;
request.ContentType = "application/json; charset=UTF-8";
request.Accept = "application/json";
request.Method = "POST";
request.UserAgent = "foo";
request.ContentLength = requestBody.Length;

Stream requestStream = request.GetRequestStream();
requestStream.Write(requestBody, 0, requestBody.Length);
requestStream.Close();
var response = request.GetResponse();
using (var reader = new StreamReader(response.GetResponseStream()))
{
    string data = reader.ReadToEnd();
}
