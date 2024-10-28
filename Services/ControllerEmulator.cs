namespace P3k_Bluetooth_GamePad_Over_LAN.Services
{
   using Nefarius.ViGEm.Client;
   using Nefarius.ViGEm.Client.Targets;
   using Nefarius.ViGEm.Client.Targets.Xbox360;

   // TODO -- Client app needs to send over device info. That way can tell if PlayStation or XBox controller
   public class ControllerEmulator
   {
      private readonly ViGEmClient _client;

      private readonly IXbox360Controller _controller;

      public ControllerEmulator()
      {
         _client = new ViGEmClient();
         _controller = _client.CreateXbox360Controller();
         _controller.Connect();
      }

      public void MapInputs(Dictionary<string, float> inputDictionary)
      {
         // Example mapping from dictionary to Xbox controller inputs
         foreach (var input in inputDictionary)
         {
            switch (input.Key)
            {
               // Standard buttons
               case "buttonA":
                  _controller.SetButtonState(Xbox360Button.A, input.Value > 0);
                  break;
               case "buttonB":
                  _controller.SetButtonState(Xbox360Button.B, input.Value > 0);
                  break;
               case "buttonX":
                  _controller.SetButtonState(Xbox360Button.X, input.Value > 0);
                  break;
               case "buttonY":
                  _controller.SetButtonState(Xbox360Button.Y, input.Value > 0);
                  break;
               case "l1":
                  _controller.SetButtonState(Xbox360Button.LeftShoulder, input.Value > 0);
                  break;
               case "r1":
                  _controller.SetButtonState(Xbox360Button.RightShoulder, input.Value > 0);
                  break;
               case "select":
                  _controller.SetButtonState(Xbox360Button.Back, input.Value > 0);
                  break;
               case "start":
                  _controller.SetButtonState(Xbox360Button.Start, input.Value > 0);
                  break;
               case "psButton":
                  _controller.SetButtonState(Xbox360Button.Guide, input.Value > 0);
                  break;

               // Triggers (as sliders)
               case "l2":
                  _controller.SetSliderValue(
                  Xbox360Slider.LeftTrigger,
                  (byte) (input.Value * 255)); // Assuming input is between 0.0 and 1.0
                  break;
               case "r2":
                  _controller.SetSliderValue(Xbox360Slider.RightTrigger, (byte) (input.Value * 255));
                  break;

               // D-Pad directions
               case "dpadX":
                  if (input.Value > 0)
                  {
                     _controller.SetButtonState(Xbox360Button.Right, true);
                     break;
                  }

                  if (input.Value < 0)
                  {
                     _controller.SetButtonState(Xbox360Button.Left, true);
                     break;
                  }

                  _controller.SetButtonState(Xbox360Button.Right, false);
                  _controller.SetButtonState(Xbox360Button.Left, false);
                  break;
               case "dpadY":
                  if (input.Value < 0)
                  {
                     _controller.SetButtonState(Xbox360Button.Up, true);
                     break;
                  }

                  if (input.Value > 0)
                  {
                     _controller.SetButtonState(Xbox360Button.Down, true);
                     break;
                  }

                  _controller.SetButtonState(Xbox360Button.Up, false);
                  _controller.SetButtonState(Xbox360Button.Down, false);
                  break;

               // Left analog stick
               case "leftStickX":
                  _controller.SetAxisValue(
                  Xbox360Axis.LeftThumbX,
                  (short) (input.Value * 32767)); // Assuming input is between -1.0 and 1.0
                  break;
               case "leftStickY":
                  _controller.SetAxisValue(Xbox360Axis.LeftThumbY, (short) (input.Value * 32767));
                  break;

               // Right analog stick
               case "rightStickX":
                  _controller.SetAxisValue(Xbox360Axis.RightThumbX, (short) (input.Value * 32767));
                  break;
               case "rightStickY":
                  _controller.SetAxisValue(Xbox360Axis.RightThumbY, (short) (input.Value * 32767));
                  break;

               // Clickable stick buttons
               case "leftStickButton":
                  _controller.SetButtonState(Xbox360Button.LeftThumb, input.Value > 0);
                  break;
               case "rightStickButton":
                  _controller.SetButtonState(Xbox360Button.RightThumb, input.Value > 0);
                  break;
            }
         }

         // Notify the controller of the state changes
         _controller.SubmitReport();
      }

      public void Disconnect()
      {
         _controller.Disconnect();
      }
   }
}
