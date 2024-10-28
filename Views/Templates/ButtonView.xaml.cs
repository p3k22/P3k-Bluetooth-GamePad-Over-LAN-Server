namespace P3k_Bluetooth_GamePad_Over_LAN.Views
{
   using P3k_Bluetooth_GamePad_Over_LAN.ViewModels.Templates;

   /// <summary>
    /// Interaction logic for ButtonView.xaml
    /// </summary>
    public partial class ButtonView
   {
      public ButtonViewModel ViewModel { get; set; }

      public ButtonView(ButtonViewModel viewModel)
      {
         InitializeComponent();
         ViewModel = viewModel;
         DataContext = ViewModel;
      }
   }
}
