using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedService.MapService
{
    public class MapperService
    {
        public async Task<List<TDestination>> MapList<TSource, TDestination>(IEnumerable<TSource> sourceList)
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TSource, TDestination>();
            });

            var mapper = configuration.CreateMapper();

            return await Task.Run(() => mapper.Map<List<TDestination>>(sourceList));
        }
        public async Task<TDestination> MapSingle<TSource, TDestination>(TSource source)
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TSource, TDestination>();
            });

            var mapper = configuration.CreateMapper();

            return await Task.Run(() => mapper.Map<TDestination>(source));
        }

    }
}
