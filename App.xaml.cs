namespace P3k_Bluetooth_GamePad_Over_LAN
{
   using P3k_Bluetooth_GamePad_Over_LAN.ViewModels;
   using P3k_Bluetooth_GamePad_Over_LAN.Views;

   using System.Windows;

   /// <summary>
   /// Interaction logic for App.xaml
   /// </summary>
   public partial class App
   {
      protected override void OnStartup(StartupEventArgs e)
      {
         base.OnStartup(e);

         var uiViews = new UIViews(
         new AnalogueSticksView(),
         new DPadView(),
         new PrimaryActionButtonsView(),
         new SecondaryActionButtonsView(),
         new TriggersView());

         var uiViewModels = new UIViewModels(
         uiViews.AnalogueSticks.LeftStickViewModel,
         uiViews.AnalogueSticks.RightStickViewModel,
         uiViews.DPad.ViewModel,
         uiViews.PrimaryButtons.ViewModel,
         uiViews.SecondaryButtons.ViewModel,
         uiViews.Triggers.ViewModel);

         var mainWindow = new MainWindow(uiViews, new MainWindowViewModel(uiViewModels));
         mainWindow.Show();
      }
   }
}
