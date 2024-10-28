namespace P3k_Bluetooth_GamePad_Over_LAN.Views;

using P3k_Bluetooth_GamePad_Over_LAN.ViewModels;
using P3k_Bluetooth_GamePad_Over_LAN.Views.Templates;

using System.Windows.Media;

public partial class AnalogueSticksView
{
   public JoyStickViewModel LeftStickViewModel { get; set; }

   public JoyStickViewModel RightStickViewModel { get; set; }

   public AnalogueSticksView()
   {
      InitializeComponent();

      var leftStick = new JoyStickView(
      new JoyStickViewModel("Left Stick", new SolidColorBrush {Color = Colors.DarkSeaGreen}));
      var rightStick = new JoyStickView(
      new JoyStickViewModel("Right Stick", new SolidColorBrush {Color = Colors.CornflowerBlue}));

      LeftStickViewModel = leftStick.ViewModel;
      LeftStick.Content = leftStick;

      RightStickViewModel = rightStick.ViewModel;
      RightStick.Content = rightStick;
   }
}