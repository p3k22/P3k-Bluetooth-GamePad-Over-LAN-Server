namespace P3k_Bluetooth_GamePad_Over_LAN.ViewModels
{
   using System.ComponentModel;
   using System.Runtime.CompilerServices;
   using System.Windows.Controls;
   using System.Windows.Media;
   using System.Windows.Shapes;

   public class JoyStickViewModel : INotifyPropertyChanged
   {
      public JoyStickViewModel(string label, SolidColorBrush innerCircleColour)
      {
         Label = label;
         InnerCircleColour = innerCircleColour;
      }

      private double _centerX;

      private double _centerY;

      private double _radius;

      private Ellipse? _innerCircle;

      private Ellipse? _outerCircle;

      private float _axisX;

      private float _axisY;

      private SolidColorBrush _innerCircleColour = new SolidColorBrush {Color = Colors.Gray};

      private string _label;

      public event PropertyChangedEventHandler? PropertyChanged;

      public float AxisX
      {
         get => _axisX;
         set
         {
            if (_axisX != value)
            {
               _axisX = value;
               OnPropertyChanged();
            }
         }
      }

      public float AxisY
      {
         get => _axisY;
         set
         {
            if (_axisY != value)
            {
               _axisY = value;
               OnPropertyChanged();
            }
         }
      }

      public SolidColorBrush InnerCircleColour
      {
         get => _innerCircleColour;
         set
         {
            if (_innerCircleColour != value)
            {
               _innerCircleColour = value;
               OnPropertyChanged();
            }
         }
      }

      public string Label
      {
         get => _label;
         set
         {
            if (_label != value)
            {
               _label = value;
               OnPropertyChanged();
            }
         }
      }

      public void DisplayCanvas(Canvas canvas)
      {
         var canvasWidth = canvas.ActualWidth;
         var canvasHeight = canvas.ActualHeight;

         _centerX = canvasWidth / 2;
         _centerY = canvasHeight / 2;

         var diameter = Math.Min(canvasWidth, canvasHeight) * 0.8;
         _radius = diameter / 2;

         _outerCircle = new Ellipse {Width = diameter, Height = diameter, Stroke = Brushes.Black, StrokeThickness = 2};

         Canvas.SetLeft(_outerCircle, _centerX - _radius);
         Canvas.SetTop(_outerCircle, _centerY - _radius);

         canvas.Children.Add(_outerCircle);

         var innerDiameter = diameter / 4;
         _innerCircle = new Ellipse {Width = innerDiameter, Height = innerDiameter, Fill = InnerCircleColour};

         Canvas.SetLeft(_innerCircle, _centerX - (innerDiameter / 2));
         Canvas.SetTop(_innerCircle, _centerY - (innerDiameter / 2));

         canvas.Children.Add(_innerCircle);
      }

      public void UpdateAxis(float axisX, float axisY)
      {
         if (_innerCircle == null)
         {
            AxisX = 0;
            AxisY = 0;
            return;
         }

         AxisX = axisX;
         AxisY = axisY;

         var maxDistance = _radius - (_innerCircle.Width / 2);
         var x = (_centerX + (AxisX * maxDistance)) - (_innerCircle.Width / 2);
         var y = (_centerY + (AxisY * maxDistance)) - (_innerCircle.Height / 2);

         Canvas.SetLeft(_innerCircle, x);
         Canvas.SetTop(_innerCircle, y);
      }

      protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
      {
         PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
      }
   }
}