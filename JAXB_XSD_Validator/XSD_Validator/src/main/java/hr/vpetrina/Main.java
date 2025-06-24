package hr.vpetrina;

import java.io.File;
import javax.xml.XMLConstants;
import javax.xml.bind.JAXBContext;
import javax.xml.bind.Unmarshaller;
import javax.xml.bind.JAXBException;
import javax.xml.validation.Schema;
import javax.xml.validation.SchemaFactory;

import org.xml.sax.SAXException;

public class Main {
    public static void main(String[] args) {
        System.setProperty("com.sun.xml.bind.v2.bytecode.ClassTailor.noOptimize", "true");
        if (args.length != 2) {
            System.exit(2);
        }

        File schemaFile = new File(args[0]);
        File xmlFile = new File(args[1]);

        try {
            JAXBContext context = JAXBContext.newInstance(SpecificationList.class);
            Unmarshaller unmarshaller = context.createUnmarshaller();

            SchemaFactory schemaFactory = SchemaFactory.newInstance(XMLConstants.W3C_XML_SCHEMA_NS_URI);
            Schema schema = schemaFactory.newSchema(schemaFile);
            unmarshaller.setSchema(schema);
            unmarshaller.unmarshal(xmlFile);

            System.out.println("VALID");
            System.exit(0);

        } catch (SAXException | JAXBException e) {
            System.err.println("INVALID: " + e.getMessage());
            System.exit(1);
        }
    }
}
