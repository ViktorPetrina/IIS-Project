package hr.vpetrina;

import javax.xml.bind.annotation.XmlRootElement;

@XmlRootElement(name = "specification")
public class Specification {
    public PhoneDetailsDto phoneDetails;
    public GsmLaunchDetailsDto gsmLaunchDetails;
    public GsmBodyDetailsDto gsmBodyDetails;
    public GsmDisplayDetailsDto gsmDisplayDetails;
    public GsmMemoryDetailsDto gsmMemoryDetails;
    public GsmSoundDetailsDto gsmSoundDetails;
    public GsmBatteryDetailsDto gsmBatteryDetails;
}

class GsmBatteryDetailsDto {
    public String batteryCharging;
    public String batteryType;
}

class GsmBodyDetailsDto {
    public String bodyDimensions;
    public String bodyWeight;
    public String bodySim;
    public String bodyBuild;
    public String bodyOther1;
    public String bodyOther2;
    public String bodyOther3;
}

class GsmDisplayDetailsDto {
    public String displayType;
    public String displaySize;
    public String displayResolution;
    public String displayProtection;
    public String displayOther1;
}

class GsmLaunchDetailsDto {
    public String launchAnnounced;
    public String launchStatus;
}

class GsmMemoryDetailsDto {
    public String memoryCardSlot;
    public String memoryInternal;
    public String memoryOther1;
}

class GsmSoundDetailsDto {
    public String sound35MmJack;
    public String soundLoudspeaker;
    public String soundOther1;
    public String soundOther2;
}

class PhoneDetailsDto {
    public String yearValue;
    public String brandValue;
    public String modelValue;
}


