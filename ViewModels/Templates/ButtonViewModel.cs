namespace P3k_Bluetooth_GamePad_Over_LAN.ViewModels.Templates;

using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Media;

public class ButtonViewModel : INotifyPropertyChanged
{
   private Brush _originalBackgroundColor;

   private Brush _backgroundColor = new BitmapCacheBrush();

   private CornerRadius _cornerRadius = new CornerRadius(0);

   private double _fontSize = 10;

   private double _height = 50;

   private double _width = 50;

   private float _value;

   private string _buttonLabel = "";

   // Constructor to initialize the button view model
   public ButtonViewModel(
      string label,
      double width,
      double height,
      Brush color,
      CornerRadius cornerRadius,
      double fontSize)
   {
      ButtonLabel = label;
      Width = width;
      Height = height;
      BackgroundColor = color;
      _originalBackgroundColor = color;
      CornerRadius = cornerRadius;
      FontSize = fontSize;
      Value = 0;
   }

   public event PropertyChangedEventHandler? PropertyChanged;

   // Background color of the button
   public Brush BackgroundColor
   {
      get => _backgroundColor;
      set
      {
         _backgroundColor = value;
         OnPropertyChanged();
      }
   }

   // Label text inside the button
   public string ButtonLabel
   {
      get => _buttonLabel;
      set
      {
         _buttonLabel = value;
         OnPropertyChanged();
      }
   }

   // Corner radius to control rounded corners
   public CornerRadius CornerRadius
   {
      get => _cornerRadius;
      set
      {
         _cornerRadius = value;
         OnPropertyChanged();
      }
   }

   // FOntSize
   public double FontSize
   {
      get => _fontSize;
      set
      {
         _fontSize = value;
         OnPropertyChanged();
      }
   }

   // Height of the button
   public double Height
   {
      get => _height;
      set
      {
         _height = value;
         OnPropertyChanged();
      }
   }

   public float Value
   {
      get => _value;
      set
      {
         _value = value;
         BackgroundColor = Value == 0 ? _originalBackgroundColor : new SolidColorBrush() {Color = Colors.Aquamarine};

         OnPropertyChanged();
      }
   }

   // Width of the button
   public double Width
   {
      get => _width;
      set
      {
         _width = value;
         OnPropertyChanged();
      }
   }

   private void OnPropertyChanged([CallerMemberName] string propertyName = null)
   {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
   }
}