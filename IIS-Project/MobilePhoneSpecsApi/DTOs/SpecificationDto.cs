using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace MobilePhoneSpecsApi.DTOs
{
    [XmlRoot("specification")]
    public class SpecificationDto
    {
        public PhoneDetailsDto phoneDetails { get; set; }
        public GsmLaunchDetailsDto gsmLaunchDetails { get; set; }
        public GsmBodyDetailsDto gsmBodyDetails { get; set; }
        public GsmDisplayDetailsDto gsmDisplayDetails { get; set; }
        public GsmMemoryDetailsDto gsmMemoryDetails { get; set; }
        public GsmSoundDetailsDto gsmSoundDetails { get; set; }
        public GsmBatteryDetailsDto gsmBatteryDetails { get; set; }
    }

    public class GsmBatteryDetailsDto
    {
        public string batteryCharging { get; set; }
        public string batteryType { get; set; }
    }

    public class GsmBodyDetailsDto
    {
        public string bodyDimensions { get; set; }
        public string bodyWeight { get; set; }
        public string bodySim { get; set; }
        public string bodyBuild { get; set; }
        public string bodyOther1 { get; set; }
        public string bodyOther2 { get; set; }
        public string bodyOther3 { get; set; }
    }

    public class GsmDisplayDetailsDto
    {
        public string displayType { get; set; }
        public string displaySize { get; set; }
        public string displayResolution { get; set; }
        public string displayProtection { get; set; }
        public string displayOther1 { get; set; }
    }

    public class GsmLaunchDetailsDto
    {
        public string launchAnnounced { get; set; }
        public string launchStatus { get; set; }
    }

    public class GsmMemoryDetailsDto
    {
        public string memoryCardSlot { get; set; }
        public string memoryInternal { get; set; }
        public string memoryOther1 { get; set; }
    }

    public class GsmSoundDetailsDto
    {
        public string sound35MmJack { get; set; }
        public string soundLoudspeaker { get; set; }
        public string soundOther1 { get; set; }
        public string soundOther2 { get; set; }
    }

    public class PhoneDetailsDto
    {
        public string yearValue { get; set; }
        public string brandValue { get; set; }
        public string modelValue { get; set; }
    }
}

