using AutoMapper;
using ClinicMedical.APIRest.Application.DTOs;
using ClinicMedical.APIRest.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicMedical.APIRest.Application.Mappings
{
    public class DomainToDTOMappingProfile : Profile
    {
        public DomainToDTOMappingProfile() 
        {
            CreateMap<PatientEntity, PatientDTO>().ReverseMap();
            CreateMap<EmployeeEntity, EmployeeDTO>().ReverseMap();
            CreateMap<DoctorEntity, DoctorDTO>().ReverseMap();
        }
    }
}
