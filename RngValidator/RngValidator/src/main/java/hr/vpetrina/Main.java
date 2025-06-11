package hr.vpetrina;

import java.io.File;
import javax.xml.XMLConstants;
import javax.xml.transform.stream.StreamSource;
import javax.xml.validation.Schema;
import javax.xml.validation.SchemaFactory;
import javax.xml.validation.Validator;
import org.xml.sax.SAXException;
import java.io.IOException;

public class Main {
    public static void main(String[] args) {
        if (args.length != 2) {
            System.err.println("Usage: java -jar RelaxNgValidator.jar schema.rng xml.xml");
            System.exit(2);
        }

        File schemaFile = new File(args[0]);
        File xmlFile = new File(args[1]);

        try {
            System.setProperty(
                    SchemaFactory.class.getName() + ":" + XMLConstants.RELAXNG_NS_URI,
                    "com.thaiopensource.relaxng.jaxp.XMLSyntaxSchemaFactory"
            );

            SchemaFactory factory = SchemaFactory.newInstance(XMLConstants.RELAXNG_NS_URI);
            Schema schema = factory.newSchema(schemaFile);
            Validator validator = schema.newValidator();

            validator.validate(new StreamSource(xmlFile));
            System.out.println("VALID");
            System.exit(0);

        } catch (SAXException e) {
            System.err.println("INVALID: " + e.getMessage());
            System.exit(1);
        } catch (IOException e) {
            System.err.println("IO ERROR: " + e.getMessage());
            System.exit(3);
        }
    }
}

