using AutoMapper;
using Media.Entities;
using Media.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Media.Web.Infrastructure.Mappings
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public override string ProfileName
        {
            get
            {
                return "DomainToViewModelMappings";
            }
        }
        protected override void Configure()
        {
            CreateMap<Run, RunViewModel>()
                .ForMember(vm => vm.Batch, map => map.MapFrom(m => m.Batch.BatchNumbers))
                .ForMember(vm => vm.BatchId, map => map.MapFrom(m => m.Batch.ID));

            CreateMap<Batch, BatchViewModel>()
                .ForMember(vm => vm.NumberOfRuns, map => map.MapFrom(g => g.Runs.Count()));
        }
    }
}