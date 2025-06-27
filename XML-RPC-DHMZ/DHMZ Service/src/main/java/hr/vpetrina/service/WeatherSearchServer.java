package hr.vpetrina.service;

import javax.xml.bind.JAXBException;
import java.io.IOException;
import java.util.List;

public class WeatherSearchServer {

    public List<Double> getTempByCityName(String name) throws JAXBException, IOException {
        var country = WeatherService.getCountry();

        return country.getCities()
                .stream()
                .filter(city -> city.getName().toLowerCase().contains(name.toLowerCase()))
                .map(city -> city.getWeatherData().getTemperature())
                .toList();
    }
}
