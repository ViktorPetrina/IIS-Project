package hr.vpetrina;

import org.apache.xmlrpc.server.PropertyHandlerMapping;
import org.apache.xmlrpc.server.XmlRpcServer;
import org.apache.xmlrpc.webserver.WebServer;

import java.io.IOException;

public class Main {
    public static void main(String[] args) throws IOException {
        WebServer webServer = new WebServer(8080);
        XmlRpcServer xmlRpcServer = webServer.getXmlRpcServer();
        PropertyHandlerMapping phm = new PropertyHandlerMapping();

        phm.addHandler("Calculator", Calculator.class);
        xmlRpcServer.setHandlerMapping(phm);

        webServer.start();
        System.out.println("XML-RPC Server started on port 8080");
    }
}