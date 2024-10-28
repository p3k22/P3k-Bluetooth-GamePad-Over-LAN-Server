namespace P3k_Bluetooth_GamePad_Over_LAN.ViewModels;

using P3k_Bluetooth_GamePad_Over_LAN.Services;

using System.ComponentModel;
using System.Runtime.CompilerServices;

public class MainWindowViewModel : INotifyPropertyChanged
{
   public bool HasDisconnected;

   public bool IsListening;

   private readonly InputReceiver _inputReceiver;

   private UIViewModels _uiViewModels;

   public MainWindowViewModel(UIViewModels uiViewModels)
   {
      _uiViewModels = uiViewModels;

      _inputReceiver = new InputReceiver(UpdateControllerUI);
   }

   public event PropertyChangedEventHandler PropertyChanged;

   public string ConnectedIP => UdpBroadcaster.LastConnectionIP;

   public bool IsBroadcasting => UdpBroadcaster.IsBroadcasting;

   public ControllerEmulator ControllerEmulator { get; set; }

   public void StartBroadcasting()
   {
      UdpBroadcaster.StartBroadcasting();
      _ = WaitForConnectionAsync();
   }

   public void StopBroadcasting()
   {
      UdpBroadcaster.StopBroadcasting();
   }

   public void StopListening()
   {
      IsListening = false;
      _inputReceiver.StopListening();
   }

   protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
   {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
   }

   private async Task WaitForConnectionAsync()
   {
      await Task.Delay(100);

      while (IsBroadcasting)
      {
         if (ConnectedIP == string.Empty)
         {
            await Task.Delay(100);
         }
         else
         {
            StopBroadcasting();
            StartListening();
            break;
         }
      }
   }

   private async Task WaitForDisconnectionAsync()
   {
      while (!_inputReceiver.ClientHasDisconnected)
      {
         await Task.Delay(100);
      }

      ControllerEmulator.Disconnect();
      ControllerEmulator = null!;
      HasDisconnected = true;
      StopBroadcasting();
      StopListening();
   }

   private void StartListening()
   {
      IsListening = true;
      var localIp = InputReceiver.GetLocalIPAddress();
      _inputReceiver.StartListening(localIp, 8080);
      _ = WaitForDisconnectionAsync();
   }

   // Updates the joystick view model based on extracted values from JSON
   private void UpdateControllerUI(Dictionary<string, float> extractedValues)
   {
      if (ControllerEmulator == null)
      {
         ControllerEmulator = new ControllerEmulator();
      }

      var lx = 0f;
      var ly = 0f;
      var rx = 0f;
      var ry = 0f;
      var dx = 0f;
      var dy = 0f;
      var a = 0f;
      var b = 0f;
      var x = 0f;
      var y = 0f;
      var select = 0f;
      var start = 0f;
      var l1 = 0f;
      var r1 = 0f;
      var l2 = 0f;
      var r2 = 0f;
      var l3 = 0f;
      var r3 = 0f;

      ControllerEmulator.MapInputs(extractedValues);

      extractedValues.TryGetValue("leftStickX", out lx);
      extractedValues.TryGetValue("leftStickY", out ly);

      extractedValues.TryGetValue("rightStickX", out rx);
      extractedValues.TryGetValue("rightStickY", out ry);

      extractedValues.TryGetValue("dpadX", out dx);
      extractedValues.TryGetValue("dpadY", out dy);

      extractedValues.TryGetValue("buttonA", out a);
      extractedValues.TryGetValue("buttonB", out b);
      extractedValues.TryGetValue("buttonX", out x);
      extractedValues.TryGetValue("buttonY", out y);

      extractedValues.TryGetValue("select", out select);
      extractedValues.TryGetValue("start", out start);
      extractedValues.TryGetValue("l1", out l1);
      extractedValues.TryGetValue("r1", out r1);
      extractedValues.TryGetValue("l2", out l2);
      extractedValues.TryGetValue("r2", out r2);
      extractedValues.TryGetValue("l3", out l3);
      extractedValues.TryGetValue("r3", out r3);

      _uiViewModels.LeftStick.UpdateAxis(lx, -ly);
      _uiViewModels.RightStick.UpdateAxis(rx, -ry);
      _uiViewModels.DPad.UpdateAxis(dx, dy);
      _uiViewModels.PrimaryActionButtons.UpdateButtonPress(a, b, x, y);
      _uiViewModels.SecondaryActionButtons.UpdateButtonPress(select, start, l1, r1, l3, r3);
      _uiViewModels.Triggers.UpdateTriggerPress(l2, r2);
   }
}