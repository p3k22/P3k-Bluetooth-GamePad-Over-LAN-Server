namespace P3k_Bluetooth_GamePad_Over_LAN.ViewModels
{
   using P3k_Bluetooth_GamePad_Over_LAN.ViewModels.Templates;

   public class SecondaryActionButtonsViewModel(
      ButtonViewModel select,
      ButtonViewModel start,
      ButtonViewModel l1,
      ButtonViewModel r1,
      ButtonViewModel l3,
      ButtonViewModel r3)
   {
      public void UpdateButtonPress(float selectV, float startV, float l1V, float r1V, float l3V, float r3V)
      {
         select.Value = selectV;
         start.Value = startV;
         l1.Value = l1V;
         r1.Value = r1V;
         l3.Value = l3V;
         r3.Value = r3V;
      }
   }
}
