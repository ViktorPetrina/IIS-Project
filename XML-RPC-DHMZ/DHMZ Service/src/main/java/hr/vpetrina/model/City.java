package hr.vpetrina.model;

import lombok.Getter;

import javax.xml.bind.annotation.*;

@XmlRootElement(name = "Grad")
@XmlAccessorType(XmlAccessType.FIELD)
@Getter
public class City {

    @XmlAttribute(name = "autom")
    public int automatic;

    @XmlElement(name = "GradIme")
    public String name;

    @XmlElement(name = "Lat")
    public double latitude;

    @XmlElement(name = "Lon")
    public double longitude;

    @XmlElement(name = "Podatci")
    public WeatherData weatherData;
}

