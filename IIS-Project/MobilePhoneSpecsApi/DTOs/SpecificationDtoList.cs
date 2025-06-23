using System.Xml.Serialization;

namespace MobilePhoneSpecsApi.DTOs
{
    [XmlRoot("specifications")]
    public class SpecificationDtoList
    {
        [XmlElement("specification")]
        public List<SpecificationDto> Items { get; set; }
    }
}
