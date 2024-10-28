namespace P3k_Bluetooth_GamePad_Over_LAN.Services;

using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;

public static class UdpBroadcaster
{
   private const string BroadcastMessage = "DISCOVER_P3KBTPASSTHROUGH_CLIENT";

   private const int BroadcastPort = 8888;

   public static bool IsBroadcasting;

   public static string LastConnectionIP;

   private static UdpClient? broadcastClient; // Separate client for broadcasting

   private static CancellationTokenSource cancellationTokenSource;

   private static UdpClient? listenerClient; // Separate client for listening

   private static Timer? timer;

   public static void StartBroadcasting()
   {
      // Initialize the broadcasting client
      if (broadcastClient == null)
      {
         broadcastClient = new UdpClient {EnableBroadcast = true};
         IsBroadcasting = true;
         cancellationTokenSource = new CancellationTokenSource();
         LastConnectionIP = string.Empty;
      }
      else
      {
         return;
      }

      // Start broadcasting every 3 seconds
      timer = new Timer(Broadcast!, null, 0, 3000);

      // Start listening for replies on a different UdpClient instance
      ListenForReplies();
   }

   public static void StopBroadcasting()
   {
      Debug.WriteLine("Stopping UDP Broadcast");

      IsBroadcasting = false;
      timer?.Change(Timeout.Infinite, Timeout.Infinite);
      timer?.Dispose();

      CloseUdpClientsAsync();
   }

   private static string GetLocalIPAddress()
   {
      var host = Dns.GetHostEntry(Dns.GetHostName());
      foreach (var ip in host.AddressList)
      {
         if (ip.AddressFamily == AddressFamily.InterNetwork)
         {
            return ip.ToString();
         }
      }

      throw new Exception("Local IP Address Not Found!");
   }

   private static void Broadcast(object state)
   {
      try
      {
         var endPoint = new IPEndPoint(IPAddress.Broadcast, BroadcastPort);
         var messageBytes = Encoding.ASCII.GetBytes(BroadcastMessage);

         // Broadcast the message using the broadcasting client
         broadcastClient!.Send(messageBytes, messageBytes.Length, endPoint);
         Debug.WriteLine("Broadcast sent: " + BroadcastMessage);
      }
      catch (SocketException ex)
      {
         Debug.WriteLine($"SocketException occurred while broadcasting: {ex.Message}");
      }
      catch (Exception ex)
      {
         Debug.WriteLine($"Exception occurred while broadcasting: {ex.Message}");
      }
   }

   private static async void CloseUdpClientsAsync()
   {
      await cancellationTokenSource.CancelAsync();
      await Task.Delay(100);
      broadcastClient?.Close();
      listenerClient?.Close();
      broadcastClient = null;
      listenerClient = null;
   }

   private static async void ListenForReplies()
   {
      try
      {
         // Initialize the listener client to bind to the broadcast port
         listenerClient = new UdpClient(BroadcastPort); // Listening on the broadcast port

         while (IsBroadcasting)
         {
            var result = await listenerClient.ReceiveAsync(cancellationTokenSource.Token);
            var receivedMessage = Encoding.ASCII.GetString(result.Buffer);
            var senderAddress = result.RemoteEndPoint.Address.ToString();

            if (senderAddress != GetLocalIPAddress() && receivedMessage == "REPLY_FROM_SERVER")
            {
               Debug.WriteLine($"Received reply from {senderAddress}: Message = {receivedMessage}");
               LastConnectionIP = senderAddress;
            }
         }
      }
      catch (OperationCanceledException ex)
      {
         Debug.WriteLine($"BroadcastListener cancelled: {ex.Message}");
      }
      catch (SocketException ex)
      {
         Debug.WriteLine($"SocketException occurred while receiving: {ex.Message}");
      }
      catch (Exception ex)
      {
         Debug.WriteLine($"Exception occurred while receiving: {ex.Message}");
      }
   }
}