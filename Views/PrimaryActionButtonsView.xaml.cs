namespace P3k_Bluetooth_GamePad_Over_LAN.Views
{
   using P3k_Bluetooth_GamePad_Over_LAN.ViewModels;
   using P3k_Bluetooth_GamePad_Over_LAN.ViewModels.Templates;

   using System.Windows;
   using System.Windows.Media;

   /// <summary>
   /// Interaction logic for MainButtonsView.xaml
   /// </summary>
   public partial class PrimaryActionButtonsView
   {
      public PrimaryActionButtonsViewModel ViewModel { get; }

      public PrimaryActionButtonsView()
      {
            InitializeComponent();

            var triangle = new ButtonView(
            new ButtonViewModel("Triangle", 30, 30, Brushes.Coral, new CornerRadius(15), 8));
            Triangle.Content = triangle;

            var cross = new ButtonView(new ButtonViewModel("Cross", 30, 30, Brushes.Coral, new CornerRadius(15), 8));
            Cross.Content = cross;

            var square = new ButtonView(new ButtonViewModel("Square", 30, 30, Brushes.Coral, new CornerRadius(15), 8));
            Square.Content = square;

            var circle = new ButtonView(new ButtonViewModel("Circle", 30, 30, Brushes.Coral, new CornerRadius(15), 8));
            Circle.Content = circle;

            ViewModel = new PrimaryActionButtonsViewModel(
            triangle.ViewModel,
            cross.ViewModel,
            square.ViewModel,
            circle.ViewModel);
      }
   }
}
