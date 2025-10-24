using Xunit.Abstractions;
using ZigBeeNet.Hardware.Ember;
using ZigBeeNet.Tranport.SerialPort;

namespace ZigBeeNet.Hardware.Sonoff.Ember.Test
{
    public class ZigBeeDongleSonoffTest
    {
        private readonly ITestOutputHelper _output;

        public ZigBeeDongleSonoffTest(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void GetVersionString()
        {
            ZigBeeSerialPort zigbeePort = new ZigBeeSerialPort("COM5");

            ZigBeeDongleEzsp dongle = new ZigBeeDongleEzsp(zigbeePort);

            _output.WriteLine("Version String: " + dongle.VersionString);   

            Assert.Equal("Unknown", dongle.VersionString);
        }
    }
}
