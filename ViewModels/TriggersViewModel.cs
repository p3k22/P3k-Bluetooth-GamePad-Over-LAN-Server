namespace P3k_Bluetooth_GamePad_Over_LAN.ViewModels;

public class TriggersViewModel(SliderViewModel sliderViewModel)
{
   public void UpdateTriggerPress(float l2, float r2)
   {
      sliderViewModel.SliderValue = ((-l2) + r2) * 100;
   }
}