namespace P3k_Bluetooth_GamePad_Over_LAN.ViewModels
{
   using P3k_Bluetooth_GamePad_Over_LAN.ViewModels.Templates;

   public class PrimaryActionButtonsViewModel(
      ButtonViewModel triangle,
      ButtonViewModel cross,
      ButtonViewModel square,
      ButtonViewModel circle)
   {
      public void UpdateButtonPress(float a, float b, float x, float y)
      {
         triangle.Value = y;
         cross.Value = a;
         square.Value = x;
         circle.Value = b;
      }
   }
}