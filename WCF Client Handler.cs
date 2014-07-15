    /// <summary>
    /// Executes a method on the specified WCF client.
    /// </summary>
    /// <typeparam name="T">The type of the WCF client.</typeparam>
    /// <typeparam name="TU">The return type of the method.</typeparam>
    /// <param name="client">The WCF client.</param>
    /// <param name="action">The method to execute.</param>
    /// <returns>A value from the executed method.</returns>
    /// <exception cref="CommunicationException">A WCF communication exception occurred.</exception>
    /// <exception cref="TimeoutException">A WCF timeout exception occurred.</exception>
    /// <exception cref="Exception">Another exception type occurred.</exception>
    public static TU Execute<T, TU>(this T client, Func<T, TU> action) where T : class, ICommunicationObject
    {
        if ((client == null) || (action == null))
        {
            return default(TU);
        }

        try
        {
            return action(client);
        }
        catch (CommunicationException)
        {
            client.Abort();
            throw;
        }
        catch (TimeoutException)
        {
            client.Abort();
            throw;
        }
        catch
        {
            if (client.State == CommunicationState.Faulted)
            {
                client.Abort();
            }

            throw;
        }
        finally
        {
            try
            {
                if (client.State != CommunicationState.Faulted)
                {
                    client.Close();
                }
            }
            catch
            {
                client.Abort();
            }
        }
    }
