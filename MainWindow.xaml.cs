using NAudio.CoreAudioApi;
using NAudio.CoreAudioApi.Interfaces;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SoundwaveGenerator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IMMNotificationClient
    {
        WaveOutEvent waveOut;
        MMDeviceEnumerator enumerator;

        public MainWindow()
        {
            InitializeComponent();

            waveOut = new WaveOutEvent();

            enumerator = new MMDeviceEnumerator();
            enumerator.RegisterEndpointNotificationCallback(this);

            var signalGenerator = new SignalGenerator() { Frequency = 15 };
            waveOut.Init(signalGenerator);

            // Start minimized
            Hide();

            PlaySignalToSpeaker();
        }

        private void PlaySignalToSpeaker()
        {
            if (DefaultDeviceIsSpeaker())
            {
                waveOut.Play();
            }
            else
            {
                waveOut.Stop();
            }
        }

        public bool DefaultDeviceIsSpeaker()
        {
            var defaultAudioDevice = enumerator.GetDefaultAudioEndpoint(DataFlow.Render, Role.Console);
            return defaultAudioDevice.FriendlyName == "Speakers (NVIDIA High Definition Audio)";
        }

        public void OnDefaultDeviceChanged(DataFlow flow, Role role, string defaultDeviceId)
        {
            PlaySignalToSpeaker();
        }

        #region NotImplementedEventHandlers
        public void OnDeviceAdded(string pwstrDeviceId)
        {
            throw new NotImplementedException();
        }

        public void OnDeviceRemoved(string deviceId)
        {
            throw new NotImplementedException();
        }

        public void OnDeviceStateChanged(string deviceId, DeviceState newState)
        {
            throw new NotImplementedException();
        }

        public void OnPropertyValueChanged(string pwstrDeviceId, PropertyKey key)
        {
        }
        #endregion
    }
}
