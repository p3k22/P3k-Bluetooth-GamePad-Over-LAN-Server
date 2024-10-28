namespace P3k_Bluetooth_GamePad_Over_LAN.ViewModels
{
   public class UIViewModels(
      JoyStickViewModel leftStick,
      JoyStickViewModel rightStick,
      JoyStickViewModel dPad,
      PrimaryActionButtonsViewModel primaryActionButtons,
      SecondaryActionButtonsViewModel secondaryActionButtons,
      TriggersViewModel triggers)
   {
      public JoyStickViewModel LeftStick { get; set; } = leftStick;

      public JoyStickViewModel RightStick { get; set; } = rightStick;

      public JoyStickViewModel DPad { get; set; } = dPad;

      public PrimaryActionButtonsViewModel PrimaryActionButtons { get; set; } = primaryActionButtons;

      public SecondaryActionButtonsViewModel SecondaryActionButtons { get; set; } = secondaryActionButtons;

      public TriggersViewModel Triggers { get; set; } = triggers;
   }
}
