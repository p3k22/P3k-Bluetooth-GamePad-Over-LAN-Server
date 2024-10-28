namespace P3k_Bluetooth_GamePad_Over_LAN.Views;

using P3k_Bluetooth_GamePad_Over_LAN.ViewModels;
using P3k_Bluetooth_GamePad_Over_LAN.Views.Templates;

using System.Windows.Media;

public partial class DPadView
{
   public DPadView()
   {
      InitializeComponent();

      var dPad = new JoyStickView(new JoyStickViewModel("DPad", new SolidColorBrush {Color = Colors.LightSeaGreen}));
      ViewModel = dPad.ViewModel;
      DPad.Content = dPad;
   }

   public JoyStickViewModel ViewModel { get; }
}