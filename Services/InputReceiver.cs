namespace P3k_Bluetooth_GamePad_Over_LAN.Services
{
   using System.Diagnostics;
   using System.IO;
   using System.Net;
   using System.Net.Sockets;
   using System.Text;
   using System.Windows;

   public class InputReceiver
   {
      public bool ClientHasDisconnected { get; private set; }

      private readonly Action<Dictionary<string, float>> _updateLabelsCallback;

      private CancellationTokenSource _cts;

      private TcpListener _tcpListener;

      public InputReceiver(Action<Dictionary<string, float>> updateLabelsCallback)
      {
         _updateLabelsCallback = updateLabelsCallback;
      }

      public static IPAddress GetLocalIPAddress()
      {
         var host = Dns.GetHostEntry(Dns.GetHostName());

         foreach (var ip in host.AddressList)
         {
            if (ip.AddressFamily == AddressFamily.InterNetwork)
            {
               return ip;
            }
         }

         throw new Exception("No network adapters with an IPv4 address in the system!");
      }

      public void StartListening(IPAddress localIP, int port)
      {
         if (_tcpListener != null)
         {
            Debug.WriteLine("Server is already listening.");
            return;
         }

         try
         {
            _tcpListener = new TcpListener(localIP, port);
            _tcpListener.Start();
            _cts = new CancellationTokenSource();
            _ = Task.Run(() => ListenForClientsAsync(_cts.Token));
            Debug.WriteLine("Server started listening.");
         }
         catch (Exception ex)
         {
            Debug.WriteLine($"Exception in StartListening: {ex.Message}");
         }
      }

      public void StopListening()
      {
         if (_tcpListener == null)
         {
            Debug.WriteLine("Server is not listening.");
            return;
         }

         try
         {
            _cts?.Cancel();
            _tcpListener?.Stop();
            _tcpListener = null;
            _cts = null;
            Debug.WriteLine("Server stopped listening.");
         }
         catch (Exception ex)
         {
            Debug.WriteLine($"Exception in StopListening: {ex.Message}");
         }
      }

      private async Task ListenForClientsAsync(CancellationToken cancellationToken)
      {
         ClientHasDisconnected = false;

         try
         {
            while (!cancellationToken.IsCancellationRequested)
            {
               var client = await _tcpListener.AcceptTcpClientAsync();

               if (client != null)
               {
                  Debug.WriteLine("Client connected.");
                  _ = Task.Run(() => HandleClientAsync(client, cancellationToken), cancellationToken);
               }
            }
         }
         catch (ObjectDisposedException)
         {
            Debug.WriteLine("Listener has been stopped.");
         }
         catch (Exception ex)
         {
            Debug.WriteLine($"Exception in ListenForClientsAsync: {ex.Message}");
         }
         finally
         {
            ClientHasDisconnected = true;
            Debug.WriteLine("DISCONNECTED");
         }
      }

      private async Task HandleClientAsync(TcpClient client, CancellationToken cancellationToken)
      {
         try
         {
            using (client)
            {
               var stream = client.GetStream();
               var buffer = new byte[4096];
               var memoryStream = new MemoryStream();

               while (!cancellationToken.IsCancellationRequested)
               {
                  int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length, cancellationToken);
                  if (bytesRead == 0)
                  {
                     // Client disconnected
                     break;
                  }

                  memoryStream.Write(buffer, 0, bytesRead);

                  // Check for complete messages
                  while (TryGetCompleteMessage(memoryStream, out var message))
                  {
                     if (!string.IsNullOrWhiteSpace(message))
                     {
                        var extractedValues = ExtractControllerInput.GetFromJson(message);

                        // Ensure application is running before trying to update the UI
                        if (Application.Current?.Dispatcher != null && !cancellationToken.IsCancellationRequested)
                        {
                           await Application.Current.Dispatcher.InvokeAsync(
                           () => { _updateLabelsCallback(extractedValues); },
                           System.Windows.Threading.DispatcherPriority.Send);
                        }
                        else
                        {
                           Debug.WriteLine("Application is closing, skipping UI update.");
                           return;
                        }
                     }
                  }
               }
            }
         }
         catch (OperationCanceledException)
         {
            Debug.WriteLine("Operation canceled.");
         }
         catch (Exception ex)
         {
            Debug.WriteLine($"Exception in HandleClientAsync: {ex.Message}");
         }
         finally
         {
            ClientHasDisconnected = true;
            Debug.WriteLine("Client connection closed.");
         }
      }

      private bool TryGetCompleteMessage(MemoryStream memoryStream, out string message)
      {
         message = null;

         var buffer = memoryStream.GetBuffer();
         int length = (int) memoryStream.Length;

         for (int i = 0; i < length; i++)
         {
            if (buffer[i] == '\n')
            {
               message = Encoding.UTF8.GetString(buffer, 0, i);
               int remainingBytes = length - (i + 1);

               if (remainingBytes > 0)
               {
                  Array.Copy(buffer, i + 1, buffer, 0, remainingBytes);
                  memoryStream.Position = remainingBytes;
                  memoryStream.SetLength(remainingBytes);
               }
               else
               {
                  memoryStream.SetLength(0);
               }

               return true;
            }
         }

         return false;
      }
   }
}
