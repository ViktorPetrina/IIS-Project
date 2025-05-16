using System.Text;
using System.Xml;

namespace MobilePhoneSpecsApi.Utilities
{
    public class XmlValidation
    {
        public bool IsValid { get; private set; }
        public string ErrorMessages { get; private set; }

        public XmlValidation(bool isValid, string errorMsgs)
        {
            IsValid = isValid;
            ErrorMessages = errorMsgs;
        }
    }

    public static class XmlUtils
    {
        public static XmlValidation ValidateUsingXsd(string xml, string xsdPath)
        {
            bool isValid = true;
            StringBuilder errorMsgs = new StringBuilder();

            XmlReaderSettings settings = new XmlReaderSettings();
            settings.Schemas.Add(null, xsdPath);
            settings.ValidationType = ValidationType.Schema;

            settings.ValidationEventHandler += (sender, e) =>
            {
                isValid = false;
                Console.WriteLine($"Validation Error: {e.Message}");
                if (e.Exception != null)
                {
                    errorMsgs.Append($"Line: {e.Exception.LineNumber}, Position: {e.Exception.LinePosition}");
                }

            };

            using (StringReader stringReader = new StringReader(xml))
            using (XmlReader reader = XmlReader.Create(stringReader, settings))
            {
                try
                {
                    while (reader.Read()) { }
                }
                catch (XmlException ex)
                {
                    isValid = false;
                    errorMsgs.Append($"XML Exception: {ex.Message} at Line {ex.LineNumber}, Position {ex.LinePosition}");
                }

            }

            return new XmlValidation(isValid, errorMsgs.ToString());
        }

        public static XmlValidation ValidateUsingRng(string xml, string rngPath)
        {
            bool isValid = true;
            StringBuilder errorMsgs = new StringBuilder();

            var process = new System.Diagnostics.Process
            {
                StartInfo = new System.Diagnostics.ProcessStartInfo
                {
                    FileName = "java",
                    Arguments = $"-jar jing.jar {rngPath} {xmlPath}",
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false
                }
            };

            process.Start();
            string output = process.StandardOutput.ReadToEnd();
            string errors = process.StandardError.ReadToEnd();
            process.WaitForExit();

            if (!string.IsNullOrEmpty(errors))
            {
                isValid = false;
                errorMsgs.Append(errors);
            }

            return new XmlValidation(isValid, errorMsgs.ToString());
        }

        public static T DeserializeXml<T>(string xml)
        {
            using (StringReader stringReader = new StringReader(xml))
            {
                var serializer = new System.Xml.Serialization.XmlSerializer(typeof(T));

                if (serializer.Deserialize(stringReader) is T serialized)
                {
                    return serialized;
                }

                throw new Exception("The xml could not be serialized.");
            }
        }

    }
}
