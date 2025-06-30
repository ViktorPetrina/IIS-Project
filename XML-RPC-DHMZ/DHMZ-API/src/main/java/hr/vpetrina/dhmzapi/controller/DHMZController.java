package hr.vpetrina.dhmzapi.controller;

import org.apache.xmlrpc.XmlRpcException;
import org.apache.xmlrpc.client.XmlRpcClient;
import org.apache.xmlrpc.client.XmlRpcClientConfigImpl;
import org.springframework.web.bind.annotation.*;

import java.net.MalformedURLException;
import java.net.URI;
import java.util.Arrays;
import java.util.List;

@RestController
@RequestMapping("dhmz/search")
@CrossOrigin(origins = "http://127.0.0.1:5500")
public class DHMZController {

    @GetMapping("/query/{query}")
    public List<Double> filterByName(@PathVariable String query) throws MalformedURLException, XmlRpcException {
        XmlRpcClientConfigImpl config = new XmlRpcClientConfigImpl();
        config.setServerURL(URI.create("http://localhost:8081").toURL());
        XmlRpcClient client = new XmlRpcClient();
        client.setConfig(config);

        Object[] params = new Object[] { query };

        Object[] response = (Object[]) client.execute(
                "WeatherSearchServer.getTempByCityName",
                params
        );

        return Arrays.stream(response)
                .map(Double.class::cast)
                .toList();
    }

}
