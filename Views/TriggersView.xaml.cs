namespace P3k_Bluetooth_GamePad_Over_LAN.Views;

using P3k_Bluetooth_GamePad_Over_LAN.ViewModels;

public partial class TriggersView
{
   public TriggersView()
   {
      InitializeComponent();

      var l2r2 = new SliderView(new SliderViewModel("Triggers (L2 / R2)"));
      ViewModel = new TriggersViewModel(l2r2.ViewModel);
      Trigger.Content = l2r2;
   }

   public TriggersViewModel ViewModel { get; }
}