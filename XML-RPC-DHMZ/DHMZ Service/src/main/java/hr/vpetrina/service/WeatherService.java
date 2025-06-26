package hr.vpetrina.service;

import hr.vpetrina.model.Country;

import javax.xml.bind.JAXBContext;
import javax.xml.bind.JAXBException;
import javax.xml.bind.Unmarshaller;
import java.io.IOException;
import java.io.InputStream;
import java.net.*;

public class WeatherService {
    private WeatherService() {}

    public static final String URL = "https://vrijeme.hr/hrvatska_n.xml";

    public static Country getCountry() throws IOException, JAXBException {
        URL url = URI.create(URL).toURL();
        HttpURLConnection connection = (HttpURLConnection) url.openConnection();
        connection.setRequestMethod("GET");

        InputStream xml = connection.getInputStream();

        JAXBContext jaxbContext = JAXBContext.newInstance(Country.class);
        Unmarshaller unmarshaller = jaxbContext.createUnmarshaller();

        return (Country) unmarshaller.unmarshal(xml);
    }
}
