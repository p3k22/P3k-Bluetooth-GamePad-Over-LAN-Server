namespace P3k_Bluetooth_GamePad_Over_LAN.Views;

using P3k_Bluetooth_GamePad_Over_LAN.ViewModels;

using System.Windows;
using System.Windows.Media;

partial class MainWindow
{
   private MainWindowViewModel _viewModel;

   public MainWindow(UIViews views, MainWindowViewModel viewModel)
   {
      InitializeComponent();

      JoystickElements.Content = views.AnalogueSticks;
      DPadElements.Content = views.DPad;
      PrimaryButtonsElements.Content = views.PrimaryButtons;
      SecondaryButtonsElements.Content = views.SecondaryButtons;
      Triggers.Content = views.Triggers;

      _viewModel = viewModel;
   }

   // Event handler for Start Listening button
   private void StartBroadcasting_Click(object sender, RoutedEventArgs e)
   {
      if (_viewModel is {IsBroadcasting: false, IsListening: false})
      {
         StartButton.Content = "Stop Receiving";
         _viewModel.StartBroadcasting();
         HackChangeButtonTextAsync();
      }
      else
      {
         ControllerState.Content = "Disconnected";
         ControllerState.Foreground = new SolidColorBrush() {Color = Colors.Red};
         StartButton.Content = "Start Receiving";
         _viewModel.StopBroadcasting();
         _viewModel.StopListening();
      }
   }

   private async void HackChangeButtonTextAsync()
   {
      ControllerState.Content = "Searching...";
      ControllerState.Foreground = new SolidColorBrush() {Color = Colors.Orange};

      while (!_viewModel.HasDisconnected)
      {
         await Task.Delay(100);
         if (_viewModel.ControllerEmulator != null)
         {
            ControllerState.Content = "Connected";
            ControllerState.Foreground = new SolidColorBrush() {Color = Colors.LimeGreen};
         }
      }

      ControllerState.Content = "Disconnected";
      ControllerState.Foreground = new SolidColorBrush() {Color = Colors.Red};
      StartButton.Content = "Start Receiving";
      _viewModel.HasDisconnected = false;
   }
}