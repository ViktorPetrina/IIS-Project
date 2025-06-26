package hr.vpetrina;

import org.apache.xmlrpc.XmlRpcException;
import org.apache.xmlrpc.client.XmlRpcClient;
import org.apache.xmlrpc.client.XmlRpcClientConfigImpl;

import java.net.MalformedURLException;
import java.net.URI;
import java.util.List;
import java.util.Map;

@SuppressWarnings("unchecked")
public class Main {
    public static void main(String[] args) throws XmlRpcException, MalformedURLException {
        XmlRpcClientConfigImpl config = new XmlRpcClientConfigImpl();
        config.setServerURL(URI.create("http://localhost:8080").toURL());
        XmlRpcClient client = new XmlRpcClient();
        client.setConfig(config);

        String cityName = "Za";
        Object[] params = new Object[] { cityName };

        List<Map<String, Object>> result = (List<Map<String, Object>>) client.execute(
                "WeatherSearchService.getTempByCityName",
                params
        );

        for (Map<String, Object> city : result) {
            System.out.println(city.get("cityName") + ": " + city.get("temperature") + "°C");
        }
    }
}