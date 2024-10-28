namespace P3k_Bluetooth_GamePad_Over_LAN.Views
{
   using P3k_Bluetooth_GamePad_Over_LAN.ViewModels;
   using P3k_Bluetooth_GamePad_Over_LAN.ViewModels.Templates;

   using System.Windows;
   using System.Windows.Media;

   public partial class SecondaryActionButtonsView
   {
      public SecondaryActionButtonsViewModel ViewModel { get; }

      public SecondaryActionButtonsView()
      {
         InitializeComponent();

         var select = new ButtonView(new ButtonViewModel("Select", 50, 25, Brushes.Coral, new CornerRadius(10), 10));
         Select.Content = select;

         var start = new ButtonView(new ButtonViewModel("Start", 50, 25, Brushes.Coral, new CornerRadius(10), 10));
         Start.Content = start;

         var l1 = new ButtonView(new ButtonViewModel("L1", 50, 25, Brushes.Coral, new CornerRadius(10), 10));
         L1.Content = l1;

         var r1 = new ButtonView(new ButtonViewModel("R1", 50, 25, Brushes.Coral, new CornerRadius(10), 10));
         R1.Content = r1;

         var l3 = new ButtonView(new ButtonViewModel("L3", 50, 25, Brushes.Coral, new CornerRadius(10), 10));
         L3.Content = l3;

         var r3 = new ButtonView(new ButtonViewModel("R3", 50, 25, Brushes.Coral, new CornerRadius(10), 10));
         R3.Content = r3;

         ViewModel = new SecondaryActionButtonsViewModel(
         select.ViewModel,
         start.ViewModel,
         l1.ViewModel,
         r1.ViewModel,
         l3.ViewModel,
         r3.ViewModel);
      }
   }
}
