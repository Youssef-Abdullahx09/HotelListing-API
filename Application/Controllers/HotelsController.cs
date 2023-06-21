using AutoMapper;
using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.Model;

namespace Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<HotelsController> _logger;

        public HotelsController(IUnitOfWork unitOfWork, IMapper mapper, ILogger<HotelsController> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetHotels() 
        {
            try
            {
                var hotels = await _unitOfWork.Hotels.GetAll(null, null, new List<string> { "Country" });
                var result = _mapper.Map<List<HotelDTO>>(hotels);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error Within {nameof(GetHotels)}");
                return StatusCode(500, $"{ex}Internal Server Error, Please Try Again Later");
            }
        }

        [HttpGet("{id:int}", Name = "GetHotel")]
        public async Task<IActionResult> GetHotel(int id)
        {
            try
            {
                var hotel = await _unitOfWork.Hotels.Get(entity => entity.Id == id, new List<string> { "Country" });
                var result = _mapper.Map<HotelDTO>(hotel);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error Within {nameof(GetHotel)}");
                return StatusCode(500, $"{ex}, Internal Server Error, Please Try Again Later");
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateHotel([FromBody] CreateHotelDTO obj)
        {
            if(!ModelState.IsValid)
            {
                _logger.LogWarning($"Invalid Post Attempt in {nameof(CreateHotel)}");
                return BadRequest(ModelState);
            }

            try
            {
                var hotel = _mapper.Map<Hotel>(obj);

                await _unitOfWork.Hotels.Insert(hotel);
                await _unitOfWork.Save();

                return CreatedAtRoute("GetHotel", new { id = hotel.Id }, hotel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something Went Wrong in {nameof(CreateHotel)}");
                return StatusCode(500, "Internal Server Error, Please Try Again Later");
            }
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateHotel(int id, [FromBody] CreateHotelDTO obj)
        {
            if(!ModelState.IsValid)
            {
                _logger.LogWarning($"Invalid Update attempt in {nameof(UpdateHotel)}");
                return BadRequest(ModelState);
            }
            try
            {
                var hotel = await _unitOfWork.Hotels.Get(entity => entity.Id == id);
                if(hotel == null)
                {
                    _logger.LogWarning($"Invalid Update attempt in {nameof(UpdateHotel)}");
                    return BadRequest("Submitted data is Invalid");
                }

                _mapper.Map(obj, hotel);

                _unitOfWork.Hotels.Update(hotel);
                await _unitOfWork.Save();
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something Went Wrong in {nameof(UpdateHotel)}");
                return StatusCode(500, "Internal Server Error, Please Try Again Later");
            }
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteHotel(int id)
        {
            if(id < 1)
            {
                _logger.LogWarning($"Invalid Update attempt in {nameof(DeleteHotel)}");
                return BadRequest();
            }
            try
            {
                var hotel = await _unitOfWork.Hotels.Get(entity => entity.Id == id);
                
                if (hotel == null)
                {
                    _logger.LogWarning($"Invalid Update attempt in {nameof(DeleteHotel)}");
                    return BadRequest();
                }

                _unitOfWork.Hotels.Delete(hotel);
                await _unitOfWork.Save();
                
                return NoContent();
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, $"Something Went Wrong in {nameof(DeleteHotel)}");
                return StatusCode(500, "Internal Server Error, Please Try Again Later");
            }
        }


    }
}
