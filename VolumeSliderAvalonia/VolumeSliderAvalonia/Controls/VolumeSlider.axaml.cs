using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using NAudio.CoreAudioApi;
using ReactiveUI;
using System;
using System.Reactive;
using System.Windows.Input;

namespace VolumeSliderAvalonia
{
    public class VolumeSlider : TemplatedControl
    {
        public static readonly StyledProperty<bool> IsSliderVisibleProperty =
                AvaloniaProperty.Register<VolumeSlider, bool>(nameof(IsSliderVisible));

        public bool IsSliderVisible
        {
            get => GetValue(IsSliderVisibleProperty);
            set => SetValue(IsSliderVisibleProperty, value);
        }

        public ReactiveCommand<Unit, Unit> CloseButtonCommand { get; }

        private void CloseButtonAction()
        {
            IsSliderVisible = false;
        }

        public ReactiveCommand<Unit, Unit> OpenSliderCommand { get; }

        private void RectangleTappedAction()
        {
            IsSliderVisible = true;
        }

        // Define a property for volume level
        public static readonly StyledProperty<double> VolumeLevelProperty =
            AvaloniaProperty.Register<VolumeSlider, double>(nameof(VolumeLevel), defaultValue: 0.5);

        public double VolumeLevel
        {
            get => GetValue(VolumeLevelProperty);
            set => SetValue(VolumeLevelProperty, value);
        }

        public VolumeSlider()
        {
            CloseButtonCommand = ReactiveCommand.Create(CloseButtonAction);
            OpenSliderCommand = ReactiveCommand.Create(RectangleTappedAction);

            // Get the initial system volume
            var initialVolume = GetSystemVolume();
            VolumeLevel = initialVolume;

            // Subscribe to changes in the VolumeLevel property
            this.WhenAnyValue(x => x.VolumeLevel)
                .Subscribe(newVolumeLevel =>
                {
                    // Set the system volume based on the new volume level
                    SetSystemVolume(newVolumeLevel);
                });
        }

        // Method to get system volume
        private double GetSystemVolume()
        {
            var enumerator = new MMDeviceEnumerator();
            var device = enumerator.GetDefaultAudioEndpoint(DataFlow.Render, Role.Multimedia);
            return device.AudioEndpointVolume.MasterVolumeLevelScalar;
        }

        // Method to set system volume
        private void SetSystemVolume(double volumeLevel)
        {
            var enumerator = new MMDeviceEnumerator();
            var device = enumerator.GetDefaultAudioEndpoint(DataFlow.Render, Role.Multimedia);
            device.AudioEndpointVolume.MasterVolumeLevelScalar = (float)volumeLevel;
        }
    }
}
