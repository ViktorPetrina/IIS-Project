package hr.vpetrina.model;

import lombok.Getter;

import javax.xml.bind.annotation.*;
import java.util.List;

@XmlRootElement(name = "Hrvatska")
@XmlAccessorType(XmlAccessType.FIELD)
@Getter
public class Country {
    @XmlElement(name = "Grad")
    public List<City> cities;
}


