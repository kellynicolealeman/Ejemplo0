using AutoMapper;
using Ejemplo0.Modelo;
using Ejemplo0.Modelo.Dto;

namespace Ejemplo0
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<Studiante, StudianteDto>();
            CreateMap<StudianteDto, Studiante>();

            CreateMap<Studiante, StudianteCrearDto>();
            CreateMap<Studiante, StudianteUpdateDto>();

            CreateMap<Grado, GradosDto>();
            CreateMap<GradosDto, Grado>();

            CreateMap<Grado, GradosCrearDto>();
            CreateMap<Grado, GradosUpdateDto>();

        }
    }
}
