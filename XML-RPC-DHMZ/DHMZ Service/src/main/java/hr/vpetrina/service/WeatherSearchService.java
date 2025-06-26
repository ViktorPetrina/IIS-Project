package hr.vpetrina.service;

import hr.vpetrina.model.CityTempDto;

import javax.xml.bind.JAXBException;
import java.io.IOException;
import java.util.List;

public class WeatherSearchService {

    public List<CityTempDto> getTempByCityName(String name) throws JAXBException, IOException {
        var country = WeatherService.getCountry();

        return country.getCities()
                .stream()
                .filter(city -> city.getName().toLowerCase().contains(name.toLowerCase()))
                .map(city -> new CityTempDto(city.getName(), city.getWeatherData().getTemperature()))
                .toList();
    }
}
