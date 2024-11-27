using Application.DTOs;
using AutoMapper;
using Domain;

namespace Application.Mapper
{
    public class Map : Profile
    {
        public Map()
        {
            CreateMap<LoanApplicants, LoanApplicantDTO>();

            CreateMap<LoanApplicantDTO, LoanApplicants>();
        }
    }
}