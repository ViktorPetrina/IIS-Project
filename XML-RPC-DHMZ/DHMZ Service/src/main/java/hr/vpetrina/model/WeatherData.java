package hr.vpetrina.model;

import lombok.Getter;

import javax.xml.bind.annotation.*;

@XmlAccessorType(XmlAccessType.FIELD)
@Getter
public class WeatherData {

    @XmlElement(name = "Temp")
    public double temperature;

    @XmlElement(name = "Vlaga")
    public int humidity;

    @XmlElement(name = "Tlak")
    public String pressure;

    @XmlElement(name = "TlakTend")
    public String pressureTrend;

    @XmlElement(name = "VjetarSmjer")
    public String windDirection;

    @XmlElement(name = "VjetarBrzina")
    public double windSpeed;

    @XmlElement(name = "Vrijeme")
    public String weather;

    @XmlElement(name = "VrijemeZnak")
    public int weatherSymbol;
}

