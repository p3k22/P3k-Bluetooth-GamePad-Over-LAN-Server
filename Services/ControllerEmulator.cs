namespace P3k_Bluetooth_GamePad_Over_LAN.Services
{
   using Nefarius.ViGEm.Client;
   using Nefarius.ViGEm.Client.Targets;
   using Nefarius.ViGEm.Client.Targets.DualShock4;
   using Nefarius.ViGEm.Client.Targets.Xbox360;

   // TODO -- Client app needs to send over device info. That way can tell if PlayStation or XBox controller
   public class ControllerEmulator
   {
      private readonly ViGEmClient _client;

      private readonly IXbox360Controller _xBoxController;

      private readonly IDualShock4Controller _psController;

      private bool _isPSPad;

      public ControllerEmulator(bool isPSPad)
      {
         _isPSPad = isPSPad;

         _client = new ViGEmClient();

         if (!isPSPad)
         {
            _xBoxController = _client.CreateXbox360Controller();
            _xBoxController.Connect();
         }
         else
         {
            _psController = _client.CreateDualShock4Controller();
            _psController.Connect();
         }
      }

      public void MapInputs(Dictionary<string, float> inputDictionary)
      {
         if (!_isPSPad)
         {
            GetXboxInput(inputDictionary);
         }
         else
         {
            GetPSInput(inputDictionary);
         }
      }

      private void GetPSInput(Dictionary<string, float> inputDictionary)
      {
         DualShock4DPadDirection dpadDirection = DualShock4DPadDirection.None; // Default D-pad direction

         // Example mapping from dictionary to DualShock4 controller inputs
         foreach (var input in inputDictionary)
         {
            switch (input.Key)
            {
               // Standard buttons
               case "buttonA":
                  _psController.SetButtonState(DualShock4Button.Cross, input.Value > 0);
                  break;
               case "buttonB":
                  _psController.SetButtonState(DualShock4Button.Circle, input.Value > 0);
                  break;
               case "buttonX":
                  _psController.SetButtonState(DualShock4Button.Square, input.Value > 0);
                  break;
               case "buttonY":
                  _psController.SetButtonState(DualShock4Button.Triangle, input.Value > 0);
                  break;
               case "l1":
                  _psController.SetButtonState(DualShock4Button.ShoulderLeft, input.Value > 0);
                  break;
               case "r1":
                  _psController.SetButtonState(DualShock4Button.ShoulderRight, input.Value > 0);
                  break;
               case "select":
                  _psController.SetButtonState(DualShock4Button.Share, input.Value > 0);
                  break;
               case "start":
                  _psController.SetButtonState(DualShock4Button.Options, input.Value > 0);
                  break;
               case "psButton":
                  _psController.SetButtonState(DualShock4SpecialButton.Ps, input.Value > 0);
                  break;

               // Triggers (scaled from -1.0 to 1.0)
               case "l2":
                  _psController.SetButtonState(DualShock4Button.TriggerLeft, input.Value > 0);
                  _psController.SetSliderValue(DualShock4Slider.LeftTrigger, (byte) ((input.Value) * 255));
                  break;
               case "r2":
                  _psController.SetButtonState(DualShock4Button.TriggerRight, input.Value > 0);
                  _psController.SetSliderValue(DualShock4Slider.RightTrigger, (byte) ((input.Value) * 255));
                  break;

               // D-Pad directions (supporting diagonals)
               case "dpadX":
                  if (input.Value > 0)
                     dpadDirection = dpadDirection == DualShock4DPadDirection.North ?
                                        DualShock4DPadDirection.Northeast :
                                        DualShock4DPadDirection.East;
                  else if (input.Value < 0)
                     dpadDirection = dpadDirection == DualShock4DPadDirection.North ?
                                        DualShock4DPadDirection.Northwest :
                                        DualShock4DPadDirection.West;
                  break;
               case "dpadY":
                  if (input.Value > 0)
                     dpadDirection = dpadDirection == DualShock4DPadDirection.East ?
                                        DualShock4DPadDirection.Southeast :
                                        DualShock4DPadDirection.South;
                  else if (input.Value < 0)
                     dpadDirection = dpadDirection == DualShock4DPadDirection.West ?
                                        DualShock4DPadDirection.Northwest :
                                        DualShock4DPadDirection.North;
                  break;

               // Left analog stick (scaling -1.0 to 1.0 to 0 to 255)
               case "leftStickX":
                  _psController.SetAxisValue(DualShock4Axis.LeftThumbX, (byte) ((input.Value + 1) * 127.5f));
                  break;
               case "leftStickY":
                  _psController.SetAxisValue(DualShock4Axis.LeftThumbY, (byte) -((input.Value - 1) * 127.5f));
                  break;

               // Right analog stick
               case "rightStickX":
                  _psController.SetAxisValue(DualShock4Axis.RightThumbX, (byte) ((input.Value + 1) * 127.5f));
                  break;
               case "rightStickY":
                  _psController.SetAxisValue(DualShock4Axis.RightThumbY, (byte) -((input.Value - 1) * 127.5f));
                  break;

               // Clickable stick buttons
               case "l3":
                  _psController.SetButtonState(DualShock4Button.ThumbLeft, input.Value > 0);
                  break;
               case "r3":
                  _psController.SetButtonState(DualShock4Button.ThumbRight, input.Value > 0);
                  break;
            }
         }

         // Set the D-pad direction after processing all inputs
         _psController.SetDPadDirection(dpadDirection);

         // Notify the controller of the state changes
         _psController.SubmitReport();
      }

      private void GetXboxInput(Dictionary<string, float> inputDictionary)
      {
         // Example mapping from dictionary to Xbox controller inputs
         foreach (var input in inputDictionary)
         {
            switch (input.Key)
            {
               // Standard buttons
               case "buttonA":
                  _xBoxController.SetButtonState(Xbox360Button.A, input.Value > 0);
                  break;
               case "buttonB":
                  _xBoxController.SetButtonState(Xbox360Button.B, input.Value > 0);
                  break;
               case "buttonX":
                  _xBoxController.SetButtonState(Xbox360Button.X, input.Value > 0);
                  break;
               case "buttonY":
                  _xBoxController.SetButtonState(Xbox360Button.Y, input.Value > 0);
                  break;
               case "l1":
                  _xBoxController.SetButtonState(Xbox360Button.LeftShoulder, input.Value > 0);
                  break;
               case "r1":
                  _xBoxController.SetButtonState(Xbox360Button.RightShoulder, input.Value > 0);
                  break;
               case "select":
                  _xBoxController.SetButtonState(Xbox360Button.Back, input.Value > 0);
                  break;
               case "start":
                  _xBoxController.SetButtonState(Xbox360Button.Start, input.Value > 0);
                  break;
               case "psButton":
                  _xBoxController.SetButtonState(Xbox360Button.Guide, input.Value > 0);
                  break;

               // Triggers (as sliders)
               case "l2":
                  _xBoxController.SetSliderValue(
                  Xbox360Slider.LeftTrigger,
                  (byte) (input.Value * 255)); // Assuming input is between 0.0 and 1.0
                  break;
               case "r2":
                  _xBoxController.SetSliderValue(Xbox360Slider.RightTrigger, (byte) (input.Value * 255));
                  break;

               // D-Pad directions
               case "dpadX":
                  if (input.Value > 0)
                  {
                     _xBoxController.SetButtonState(Xbox360Button.Right, true);
                     break;
                  }

                  if (input.Value < 0)
                  {
                     _xBoxController.SetButtonState(Xbox360Button.Left, true);
                     break;
                  }

                  _xBoxController.SetButtonState(Xbox360Button.Right, false);
                  _xBoxController.SetButtonState(Xbox360Button.Left, false);
                  break;
               case "dpadY":
                  if (input.Value < 0)
                  {
                     _xBoxController.SetButtonState(Xbox360Button.Up, true);
                     break;
                  }

                  if (input.Value > 0)
                  {
                     _xBoxController.SetButtonState(Xbox360Button.Down, true);
                     break;
                  }

                  _xBoxController.SetButtonState(Xbox360Button.Up, false);
                  _xBoxController.SetButtonState(Xbox360Button.Down, false);
                  break;

               // Left analog stick
               case "leftStickX":
                  _xBoxController.SetAxisValue(
                  Xbox360Axis.LeftThumbX,
                  (short) (input.Value * 32767)); // Assuming input is between -1.0 and 1.0
                  break;
               case "leftStickY":
                  _xBoxController.SetAxisValue(Xbox360Axis.LeftThumbY, (short) (input.Value * 32767));
                  break;

               // Right analog stick
               case "rightStickX":
                  _xBoxController.SetAxisValue(Xbox360Axis.RightThumbX, (short) (input.Value * 32767));
                  break;
               case "rightStickY":
                  _xBoxController.SetAxisValue(Xbox360Axis.RightThumbY, (short) (input.Value * 32767));
                  break;

               // Clickable stick buttons
               case "leftStickButton":
                  _xBoxController.SetButtonState(Xbox360Button.LeftThumb, input.Value > 0);
                  break;
               case "rightStickButton":
                  _xBoxController.SetButtonState(Xbox360Button.RightThumb, input.Value > 0);
                  break;
            }
         }

         // Notify the controller of the state changes
         _xBoxController.SubmitReport();
      }

      public void Disconnect()
      {
         _xBoxController?.Disconnect();
         _psController?.Disconnect();
      }
   }
}
