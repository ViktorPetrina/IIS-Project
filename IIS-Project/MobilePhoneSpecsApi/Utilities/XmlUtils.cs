using Microsoft.AspNetCore.Http;
using System.Diagnostics;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace MobilePhoneSpecsApi.Utilities
{
    public class XmlValidationResult
    {
        public bool IsValid { get; private set; }
        public string ErrorMessages { get; private set; }

        public XmlValidationResult(bool isValid, string errorMsgs)
        {
            IsValid = isValid;
            ErrorMessages = errorMsgs;
        }
    }
    public static class XmlUtils
    {
        private const string xsdPath = "ValidationFiles/specification.xsd";
        private const string rngPath = "ValidationFiles/specification.rng";
        private const string rngValidatorPath = @"RngValidator\RngValidatorService.jar";
        private const string javaPath = "java";

        public static XmlValidationResult ValidateUsingXsd(string xml)
        {
            bool isValid = true;
            StringBuilder errorMsgs = new StringBuilder();

            XmlReaderSettings settings = new XmlReaderSettings();
            settings.Schemas.Add(null, xsdPath);
            settings.ValidationType = ValidationType.Schema;
            settings.ValidationEventHandler += (sender, e) =>
            {
                isValid = false;
                errorMsgs.Append($"Validation Error: {e.Message}");
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
                    errorMsgs.Append($"Deserializing error: {ex.Message}");
                }
            }

            return new XmlValidationResult(isValid, errorMsgs.ToString());
        }

        public static XmlValidationResult ValidateUsingRng(string xmlContent)
        {
            bool isValid = true;
            StringBuilder errorMsgs = new StringBuilder();
            string tempFilePath = Path.GetTempFileName();

            try
            {
                File.WriteAllText(tempFilePath, xmlContent);

                var process = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = javaPath,
                        Arguments = $"-jar \"{rngValidatorPath}\" \"{rngPath}\" \"{tempFilePath}\"",
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        UseShellExecute = false,
                        CreateNoWindow = true
                    }
                };

                process.Start();

                string output = process.StandardOutput.ReadToEnd();
                string errors = process.StandardError.ReadToEnd();

                process.WaitForExit();

                if (process.ExitCode != 0)
                {
                    isValid = false;
                    errorMsgs.AppendLine(errors);
                }
            }
            catch (Exception ex)
            {
                isValid = false;
                errorMsgs.AppendLine($"Exception during validation: {ex.Message}");
            }
            finally
            {
                if (File.Exists(tempFilePath)) File.Delete(tempFilePath);
            }

            return new XmlValidationResult(isValid, errorMsgs.ToString());
        }


        public static T DeserializeXml<T>(string xml)
        {
            using (StringReader stringReader = new StringReader(xml))
            {
                var serializer = new XmlSerializer(typeof(T));

                if (serializer.Deserialize(stringReader) is T serialized)
                {
                    return serialized;
                }

                throw new Exception("The xml could not be serialized.");
            }
        }

    }
}
