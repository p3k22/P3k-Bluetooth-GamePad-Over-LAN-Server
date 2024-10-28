namespace P3k_Bluetooth_GamePad_Over_LAN.Views;

public class UIViews(
   AnalogueSticksView analogueSticks,
   DPadView dPad,
   PrimaryActionButtonsView primaryButtons,
   SecondaryActionButtonsView secondaryButtons,
   TriggersView triggers)
{
   public AnalogueSticksView AnalogueSticks { get; set; } = analogueSticks;

   public DPadView DPad { get; set; } = dPad;

   public PrimaryActionButtonsView PrimaryButtons { get; set; } = primaryButtons;

   public SecondaryActionButtonsView SecondaryButtons { get; set; } = secondaryButtons;

   public TriggersView Triggers { get; set; } = triggers;
}