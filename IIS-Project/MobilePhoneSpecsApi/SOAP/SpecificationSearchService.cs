using MobilePhoneSpecsApi.DTOs;
using MobilePhoneSpecsApi.Utilities;
using System.Xml;

namespace MobilePhoneSpecsApi.SOAP
{
    public class SpecificationSearchService : ISearchService
    {
        private const string XML_FILE_NAME = "specifications.xml";
        private const string DIRECTORY_NAME = "EntityFiles";
        private const string API_CALL = "http://localhost:5095/api/Specifications";

        public async Task<IEnumerable<SpecificationDto>> Search(string query)
        {
            await SaveEntitiesToXmlAsync();

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(Path.Combine(DIRECTORY_NAME, "data.xml"));

            string xpath = $"//specification[phoneDetails/modelValue[contains(., '{query}')]]";
            var matchedNodes = xmlDoc.SelectNodes(xpath);

            var result = new List<SpecificationDto>();

            if (matchedNodes == null)
            {
                return result;
            }

            foreach (XmlNode node in matchedNodes)
            {
                string nodeXml = node.OuterXml;
                var deserialized = XmlUtils.DeserializeXml<SpecificationDto>(nodeXml);
                if (deserialized != null)
                {
                    result.Add(deserialized);
                }
            }

            return result;
        }

        public async Task SaveEntitiesToXmlAsync()
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(API_CALL); 

            if (response.IsSuccessStatusCode)
            {
                string xmlContent = await response.Content.ReadAsStringAsync();

                string directoryPath = Path.Combine(AppContext.BaseDirectory, DIRECTORY_NAME);
                string xmlPath = Path.Combine(directoryPath, XML_FILE_NAME);

                await File.WriteAllTextAsync(xmlPath, xmlContent);
            }
        }
    }
}
