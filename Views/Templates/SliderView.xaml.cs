namespace P3k_Bluetooth_GamePad_Over_LAN.Views
{
   using P3k_Bluetooth_GamePad_Over_LAN.ViewModels;

   /// <summary>
   /// Interaction logic for SliderView.xaml
   /// </summary>
   public partial class SliderView
   {
      public SliderViewModel ViewModel { get; }

      public SliderView(SliderViewModel viewModel)
      {
          InitializeComponent();
          ViewModel = viewModel;
          DataContext = ViewModel;
      }
    }
}
