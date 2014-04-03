private Stream CreateResponseStream(string content)
{
    MemoryStream responseStream = new MemoryStream();
    StreamWriter writer = new StreamWriter(responseStream, Encoding.UTF8);
    writer.Write(content);
    writer.Flush();
    responseStream.Position = 0;
    //WebOperationContext.Current.OutgoingResponse.ContentType = "application/json";
    return responseStream;
}
