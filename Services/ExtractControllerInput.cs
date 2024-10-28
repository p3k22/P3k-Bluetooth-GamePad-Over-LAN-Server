namespace P3k_Bluetooth_GamePad_Over_LAN.Services;

using Newtonsoft.Json.Linq;

public static class ExtractControllerInput
{
   public static Dictionary<string, float> GetFromJson(string jsonString)
   {
      var extractedValues = new Dictionary<string, float>();
      try
      {
         var jsonObject = JObject.Parse(jsonString);

         if (jsonObject.TryGetValue("leftStickX", out var lxToken))
         {
            extractedValues["leftStickX"] = lxToken.Value<float>();
         }
         else
         {
            extractedValues["leftStickX"] = 0;
         }

         if (jsonObject.TryGetValue("leftStickY", out var lyToken))
         {
            extractedValues["leftStickY"] = -lyToken.Value<float>();
         }
         else
         {
            extractedValues["leftStickY"] = 0;
         }

         if (jsonObject.TryGetValue("rightStickX", out var rxToken))
         {
            extractedValues["rightStickX"] = rxToken.Value<float>();
         }
         else
         {
            extractedValues["rightStickX"] = 0;
         }

         if (jsonObject.TryGetValue("rightStickY", out var ryToken))
         {
            extractedValues["rightStickY"] = -ryToken.Value<float>();
         }
         else
         {
            extractedValues["rightStickY"] = 0;
         }

         if (jsonObject.TryGetValue("dpadX", out var dxToken))
         {
            extractedValues["dpadX"] = dxToken.Value<float>();
         }
         else
         {
            extractedValues["dpadX"] = 0;
         }

         if (jsonObject.TryGetValue("dpadY", out var dyToken))
         {
            extractedValues["dpadY"] = dyToken.Value<float>();
         }
         else
         {
            extractedValues["dpadY"] = 0;
         }

         if (jsonObject.TryGetValue("start", out var start))
         {
            extractedValues["start"] = start.Value<float>();
         }
         else
         {
            extractedValues["start"] = 0;
         }

         if (jsonObject.TryGetValue("select", out var select))
         {
            extractedValues["select"] = select.Value<float>();
         }
         else
         {
            extractedValues["select"] = 0;
         }

         if (jsonObject.TryGetValue("l1", out var l1))
         {
            extractedValues["l1"] = l1.Value<float>();
         }
         else
         {
            extractedValues["l1"] = 0;
         }

         if (jsonObject.TryGetValue("l2", out var l2))
         {
            extractedValues["l2"] = l2.Value<float>();
         }
         else
         {
            extractedValues["l2"] = 0;
         }

         if (jsonObject.TryGetValue("leftStickButton", out var leftStickButton))
         {
            extractedValues["l3"] = leftStickButton.Value<float>();
         }
         else
         {
            extractedValues["l3"] = 0;
         }

         if (jsonObject.TryGetValue("r1", out var r1))
         {
            extractedValues["r1"] = r1.Value<float>();
         }
         else
         {
            extractedValues["r1"] = 0;
         }

         if (jsonObject.TryGetValue("r2", out var r2))
         {
            extractedValues["r2"] = r2.Value<float>();
         }
         else
         {
            extractedValues["r2"] = 0;
         }

         if (jsonObject.TryGetValue("rightStickButton", out var rightStickButton))
         {
            extractedValues["r3"] = rightStickButton.Value<float>();
         }
         else
         {
            extractedValues["r3"] = 0;
         }

         if (jsonObject.TryGetValue("buttonX", out var buttonX))
         {
            extractedValues["buttonX"] = buttonX.Value<float>();
         }
         else
         {
            extractedValues["buttonX"] = 0;
         }

         if (jsonObject.TryGetValue("buttonB", out var buttonB))
         {
            extractedValues["buttonB"] = buttonB.Value<float>();
         }
         else
         {
            extractedValues["buttonB"] = 0;
         }

         if (jsonObject.TryGetValue("buttonA", out var buttonA))
         {
            extractedValues["buttonA"] = buttonA.Value<float>();
         }
         else
         {
            extractedValues["buttonA"] = 0;
         }

         if (jsonObject.TryGetValue("buttonY", out var buttonY))
         {
            extractedValues["buttonY"] = buttonY.Value<float>();
         }
         else
         {
            extractedValues["buttonY"] = 0;
         }

         return extractedValues;
      }
      catch (Exception ex)
      {
         Console.WriteLine($"Error extracting JSON values: {ex.Message}");
         return extractedValues;
      }
   }
}