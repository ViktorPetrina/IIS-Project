using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MobilePhoneSpecsApi.DTOs;
using MobilePhoneSpecsApi.Models;
using MobilePhoneSpecsApi.Repository;
using MobilePhoneSpecsApi.Utilities;

namespace MobilePhoneSpecsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpecificationsController : ControllerBase
    {
        private readonly IRepository<Specification> _repository;
        private readonly IMapper _mapper;

        public SpecificationsController(IRepository<Specification> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var specifications = await _repository.GetAllAsync();

            if (specifications.IsNullOrEmpty())
            {
                return NotFound();
            }

            return Ok(_mapper.Map<IEnumerable<SpecificationDto>>(specifications));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            try
            {
                var specification = await _repository.GetByIdAsync(id);
                return Ok(_mapper.Map<SpecificationDto>(specification));
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpPost]
        [Consumes("application/xml")]
        public async Task<IActionResult> Add(string validationType)
        {
            string xmlData;
            using (var reader = new StreamReader(Request.Body))
            {
                xmlData = await reader.ReadToEndAsync();
            }

            XmlValidationResult validationResult;
            switch (validationType)
            {
                case "xsd":
                    validationResult = XmlUtils.ValidateUsingXsd(xmlData);
                    break;

                case "rng":
                    validationResult = XmlUtils.ValidateUsingRng(xmlData);
                    break;

                default:
                    validationResult = new XmlValidationResult(false, "Invalid validation type.");
                    break;
            }

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.ErrorMessages);
            }

            var specificationDto = XmlUtils.DeserializeXml<SpecificationDto>(xmlData);
            var specification = _mapper.Map<Specification>(specificationDto);
            await _repository.AddAsync(specification);

            return Ok("Specification inserted.");
        }

        [HttpPut("{id}")]
        [Consumes("application/xml")]
        public async Task<IActionResult> Update(long id, string validationType)
        {
            if (!_repository.GetAllAsync().Result.Any(s => id.Equals(s.customId)))
            {
                return BadRequest("No matching ids.");
            }
                
            string xmlData;
            using (var reader = new StreamReader(Request.Body))
            {
                xmlData = await reader.ReadToEndAsync();
            }

            XmlValidationResult validation;
            switch (validationType)
            {
                case "xsd":
                    validation = XmlUtils.ValidateUsingXsd(xmlData);
                    break;

                case "rng":
                    validation = XmlUtils.ValidateUsingRng(xmlData);
                    break;

                default:
                    validation = new XmlValidationResult(false, "Invalid validation type.");
                    break;
            }

            if (!validation.IsValid)
            {
                return BadRequest(validation.ErrorMessages);
            }

            var specificationDto = XmlUtils.DeserializeXml<SpecificationDto>(xmlData);
            var specification = _mapper.Map<Specification>(specificationDto);
            await _repository.UpdateAsync(specification);

            return Ok("Specification updated.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            if (!_repository.GetAllAsync().Result.Any(ps => ps.customId.Equals(id)))
            {
                return NotFound("No object with provided id");
            }

            await _repository.DeleteAsync(id);
            return Ok("Object deleted.");
        }
    }
}
