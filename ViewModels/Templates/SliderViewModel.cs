namespace P3k_Bluetooth_GamePad_Over_LAN.ViewModels;

using System.ComponentModel;
using System.Runtime.CompilerServices;

public class SliderViewModel : INotifyPropertyChanged
{
   private double _maximum;

   private double _minimum;

   private double _sliderValue;

   private string _label;

   // Constructor to initialize default slider values
   public SliderViewModel(string label)
   {
      Minimum = -100; // Minimum value, can be negative
      Maximum = 100; // Maximum value, can be positive
      SliderValue = 0; // Start in the center
      Label = label;
   }

   public event PropertyChangedEventHandler PropertyChanged;

   public string Label
   {
      get => _label;
      set
      {
         _label = value;
         OnPropertyChanged();
      }
   }

   // Maximum value of the slider (supports positive values)
   public double Maximum
   {
      get => _maximum;
      set
      {
         _maximum = value;
         OnPropertyChanged();
      }
   }

   // Minimum value of the slider (supports negative values)
   public double Minimum
   {
      get => _minimum;
      set
      {
         _minimum = value;
         OnPropertyChanged();
      }
   }

   // Slider value bound to the UI
   public double SliderValue
   {
      get => _sliderValue;
      set
      {
         _sliderValue = value;
         OnPropertyChanged();
      }
   }

   public void UpdateSlider(float value)
   {
      var val = (float) value * 100;
      val = Math.Clamp(val, -100, 100);
      SliderValue = val;
   }

   protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
   {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
   }
}