using System;
using System.ComponentModel;
using System.Net;
using System.Net.Sockets;

namespace Miller.Avionics.Garmin.G5
{
    public class XPlaneDataListener : INotifyPropertyChanged
    {
        private const int UdpPort = 49000;

        public event PropertyChangedEventHandler PropertyChanged;

        public float Pitch_Degrees
        {
            get { return _pitch_degrees; }
            set
            {
                _pitch_degrees = value;
                NotifyPropertyChanged(nameof(Pitch_Degrees));
            }
        }

        public float Role_Degrees
        {
            get { return _role_degrees; }
            set
            {
                _role_degrees = value;
                NotifyPropertyChanged(nameof(Role_Degrees));
            }
        }

        public void StartListening()
        {
            _asyncResult = _udpClient.BeginReceive(Receive, null); 
        }

        public void StopListening()
        {
            _udpClient.Close();
        }

        private void Receive(IAsyncResult result)
        {
            var ipEndpoint = new IPEndPoint(IPAddress.Any, UdpPort);
            var bytes = _udpClient.EndReceive(result, ref ipEndpoint);

            if (bytes.Length >= 4 && bytes[0] == 68 && bytes[1] == 65 && bytes[2] == 84 && bytes[3] == 65) // DATA
            {
                if (bytes[5] == 17)
                {
                    var pitch_degrees = BitConverter.ToSingle(bytes, 9);
                    var role_degrees = BitConverter.ToSingle(bytes, 13);
                    var trueHeading_degrees = BitConverter.ToSingle(bytes, 17);
                    var magHeading_degrees = BitConverter.ToSingle(bytes, 21);

                    Pitch_Degrees = pitch_degrees;
                    Role_Degrees = role_degrees;
                }
            }

            _udpClient.BeginReceive(Receive, null);
        }

        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        private float _pitch_degrees;
        private float _role_degrees;
        private IAsyncResult _asyncResult;
        private readonly UdpClient _udpClient = new UdpClient(UdpPort);
    }
}
