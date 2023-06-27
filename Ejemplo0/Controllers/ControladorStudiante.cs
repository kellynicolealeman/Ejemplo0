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
    public class ControladorStudiante : ControllerBase
    {
        private readonly ILogger<ControladorStudiante> _logger;
        private readonly IStudiantes _studentRepo;
        private readonly IMapper _mapper;

        public ControladorStudiante(ILogger<ControladorStudiante> logger, IStudiantes studentRepo, IMapper mapper)
        {
            _logger = logger;
            _studentRepo = studentRepo;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<StudianteDto>>> GetStudents()
        {
            _logger.LogInformation("Obtener los Estudiantes");

            var studentList = await _studentRepo.GetAll();

            return Ok(_mapper.Map<IEnumerable<StudianteDto>>(studentList));
        }

        [HttpGet("{id:int}", Name = "GetStudent")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<StudianteDto>> GetStudent(int id)
        {
            if (id == 0)
            {
                _logger.LogError($"Error al traer Estudiante con Id {id}");
                return BadRequest();
            }
            var student = await _studentRepo.Get(s => s.IdStudiante == id);

            if (student == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<StudianteDto>(student));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<StudianteDto>> AddStudent([FromBody] StudianteCrearDto studentCreateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (await _studentRepo.Get(s => s.NombreStudiante.ToLower() == studentCreateDto.NombreStudiante.ToLower()) != null)
            {
                ModelState.AddModelError("NombreExiste", "¡El Estudiante con ese Nombre ya existe!");
                return BadRequest(ModelState);
            }

            if (studentCreateDto == null)
            {
                return BadRequest(studentCreateDto);
            }

            Studiante modelo = _mapper.Map<Studiante>(studentCreateDto);

            await _studentRepo.Add(modelo);

            return CreatedAtRoute("GetStudent", new { id = modelo.IdStudiante }, modelo);

        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var student = await _studentRepo.Get(s => s.IdStudiante == id);

            if (student == null)
            {
                return NotFound();
            }

            _studentRepo.Remove(student);

            return NoContent();
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateStudent(int id, [FromBody] StudianteUpdateDto studentUpdateDto)
        {
            if (studentUpdateDto == null || id != studentUpdateDto.IdStudiante)
            {
                return BadRequest();
            }

            Studiante modelo = _mapper.Map<Studiante>(studentUpdateDto);

            _studentRepo.Update(modelo);

            return NoContent();
        }

        [HttpPatch("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdatePartialStudent(int id, JsonPatchDocument<StudianteUpdateDto> patchDto)
        {
            if (patchDto == null || id == 0)
            {
                return BadRequest();
            }

            var student = await _studentRepo.Get(s => s.IdStudiante == id, tracked: false);

            StudianteUpdateDto studentUpdateDto = _mapper.Map<StudianteUpdateDto>(student);

            if (student == null) return BadRequest();

            patchDto.ApplyTo(studentUpdateDto, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Studiante modelo = _mapper.Map<Studiante>(studentUpdateDto);

            _studentRepo.Update(modelo);

            return NoContent();
        }
    }
}
