package hr.vpetrina;

import org.apache.xmlrpc.XmlRpcException;
import org.apache.xmlrpc.client.XmlRpcClient;
import org.apache.xmlrpc.client.XmlRpcClientConfigImpl;

import java.net.MalformedURLException;
import java.net.URI;
import java.util.Arrays;
import java.util.List;

@SuppressWarnings("unchecked")
public class Main {
    public static void main(String[] args) throws XmlRpcException, MalformedURLException {
        XmlRpcClientConfigImpl config = new XmlRpcClientConfigImpl();
        config.setServerURL(URI.create("http://localhost:8080").toURL());
        XmlRpcClient client = new XmlRpcClient();
        client.setConfig(config);

        String cityName = "";
        Object[] params = new Object[] { cityName };

        Object[] response = (Object[]) client.execute(
                "WeatherSearchServer.getTempByCityName",
                params
        );

        List<Double> result = Arrays.stream(response)
                .map(Double.class::cast)
                .toList();

        result.forEach(System.out::println);
    }
}