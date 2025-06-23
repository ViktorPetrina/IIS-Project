using MobilePhoneSpecsApi.DTOs;
using MobilePhoneSpecsApi.Utilities;
using System.Linq;
using System.Net.Http.Headers;
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
            xmlDoc.Load(GetFilePath());
            
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
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/xml"));
            HttpResponseMessage response = await client.GetAsync(API_CALL); 

            if (response.IsSuccessStatusCode)
            {
                string xmlContent = await response.Content.ReadAsStringAsync();
                await File.WriteAllTextAsync(GetFilePath(), xmlContent);
            }
        }

        private string GetFilePath() => Path.Combine(Path.GetFullPath(DIRECTORY_NAME), XML_FILE_NAME);
    }
}
