using AutoMapper;
using Ejemplo0.Modelo;
using Ejemplo0.Modelo.Dto;
using Ejemplo0.Repositorio.IRepository;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace Ejemplo0.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ControladorGrado : ControllerBase
    {
        private readonly ILogger<ControladorGrado> _logger;
        private readonly IGrado _gradeRepo;
        private readonly IMapper _mapper;

        public ControladorGrado(ILogger<ControladorGrado> logger, IGrado gradeRepo, IMapper mapper)
        {
            _logger = logger;
            _gradeRepo = gradeRepo;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<GradosDto>>> GetGrade()
        {
            _logger.LogInformation("Obtener los Grados");

            var gradeList = await _gradeRepo.GetAll();

            return Ok(_mapper.Map<IEnumerable<GradosDto>>(gradeList));
        }

        [HttpGet("{id:int}", Name = "GetGrade")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<GradosDto>> GetGrade(int id)
        {
            if (id == 0)
            {
                _logger.LogError($"Error al traer Grado con Id {id}");
                return BadRequest();
            }
            var grade = await _gradeRepo.Get(s => s.IdGrados == id);

            if (grade == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<GradosDto>(grade));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<GradosDto>> AddGrade([FromBody] GradosCrearDto gradeCreateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (await _gradeRepo.Get(s => s.NombreGrado.ToLower() == gradeCreateDto.NombreGrado.ToLower()) != null)
            {
                ModelState.AddModelError("NombreExiste", "¡El Estudiante con ese Nombre ya existe!");
                return BadRequest(ModelState);
            }

            if (gradeCreateDto == null)
            {
                return BadRequest(gradeCreateDto);
            }

            Grado modelo = _mapper.Map<Grado>(gradeCreateDto);

            await _gradeRepo.Add(modelo);

            return CreatedAtRoute("GetGrade", new { id = modelo.IdGrados }, modelo);

        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteGrado(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var grade = await _gradeRepo.Get(s => s.IdGrados == id);

            if (grade == null)
            {
                return NotFound();
            }

            _gradeRepo.Remove(grade);

            return NoContent();
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateGrade(int id, [FromBody] GradosUpdateDto gradeUpdateDto)
        {
            if (gradeUpdateDto == null || id != gradeUpdateDto.IdGrados)
            {
                return BadRequest();
            }

            Grado modelo = _mapper.Map<Grado>(gradeUpdateDto);

            _gradeRepo.Update(modelo);

            return NoContent();
        }

        [HttpPatch("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdatePartialGrado(int id, JsonPatchDocument<GradosUpdateDto> patchDto)
        {
            if (patchDto == null || id == 0)
            {
                return BadRequest();
            }

            var grade = await _gradeRepo.Get(s => s.IdGrados == id, tracked: false);

            GradosUpdateDto gradeUpdateDto = _mapper.Map<GradosUpdateDto>(grade);

            if (grade == null) return BadRequest();

            patchDto.ApplyTo(gradeUpdateDto, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Grado modelo = _mapper.Map<Grado>(gradeUpdateDto);
            //Student modelo = new()
            //{
            //    StudentId = studentUpdateDto.StudentId,
            //    StudentName = studentUpdateDto.StudentName
            //};
            _gradeRepo.Update(modelo);

            return NoContent();
        }
    }
}
