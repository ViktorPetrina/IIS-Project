package hr.vpetrina;

import javax.xml.bind.annotation.XmlElement;
import javax.xml.bind.annotation.XmlRootElement;
import java.util.List;

@XmlRootElement(name = "specifications")
public class SpecificationList {
    @XmlElement(name = "specification")
    public List<Specification> items;
}

