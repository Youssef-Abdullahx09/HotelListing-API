using AutoMapper;
using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.Model;

namespace Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<CountriesController> _logger;

        public CountriesController(IUnitOfWork unitOfWork, IMapper mapper, ILogger<CountriesController> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetCountries([FromQuery] RequestParams requestParams)
        {
            //try
            //{
                var countries = await _unitOfWork.Countries.GetPagedList(requestParams);
                var result = _mapper.Map<IList<CountryDTO>>(countries);
                return Ok(result);
            //}
            //catch (Exception ex)
            //{
            //    _logger.LogError(ex, $"Something Went Wrong in the {nameof(GetCountries)}");
            //    return StatusCode(500, "Internal Server Error, Please Try Again Later");
            //}
        }

        [HttpGet("{id:int}", Name = "GetCountry")]
        public async Task<IActionResult> GetCountry(int id)
        {
            try
            {
                var country = await _unitOfWork.Countries.Get(entity => entity.Id == id, new List<string> { "Hotels" });
                var result = _mapper.Map<CountryDTO>(country);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something Went Wrong in the {nameof(GetCountry)}");
                return StatusCode(500, $"{ex},Internal Server Error, Please Try Again Later");
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateCountry([FromBody] CreateCountryDTO obj)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning($"Invalid Post attempt in {nameof(CreateCountry)}");
                return BadRequest(ModelState);
            }
            try
            {
                var country = _mapper.Map<Country>(obj);

                await _unitOfWork.Countries.Insert(country);
                await _unitOfWork.Save();

                return CreatedAtRoute("GetCountry", new { id = country.Id }, country);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex}, Something Went Wrong in the {nameof(CreateCountry)}");
                return StatusCode(500, "Internal Server Error, Please Try Again Later");
            }

        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateCountry(int id, [FromBody] CreateCountryDTO obj)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning($"Invalid Update attempt in {nameof(UpdateCountry)}");
                return BadRequest(ModelState);
            }

            try
            {
                var country = await _unitOfWork.Countries.Get(entity => entity.Id == id);
                if (country == null)
                {
                    _logger.LogWarning($"Invalid Update attempt in {nameof(UpdateCountry)}");
                    return BadRequest("Submitted data is Invalid");
                }
                _mapper.Map(obj, country);
                _unitOfWork.Countries.Update(country);
                await _unitOfWork.Save();
                return NoContent();
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, $"Something Went Wrong in {nameof(UpdateCountry)}");
                return StatusCode(500, "Internal Server Error, Please Try Again Later");
            }
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteCountry(int id)
        {
            if (id < 1)
            {
                _logger.LogWarning($"Invalid Update attempt in {nameof(DeleteCountry)}");
                return BadRequest();
            }

            try
            {
                var country = await _unitOfWork.Countries.Get(entity => entity.Id == id);

                if (country == null)
                {
                    _logger.LogWarning($"Invalid Update attempt in {nameof(DeleteCountry)}");
                    return BadRequest();
                }

                _unitOfWork.Countries.Delete(country);
                await _unitOfWork.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something Went Wrong in {nameof(DeleteCountry)}");
                return StatusCode(500, "Internal Server Error, Please Try Again Later");
            }

        }
    }
}
