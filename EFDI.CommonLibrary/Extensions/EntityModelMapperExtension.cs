using AutoMapper;
using System;
using System.Collections.Generic;

namespace EFDI.CommonLibrary.Extensions
{
    public static class EntityModelMapperExtension
    {
        public static TDestination Convert<TSource, TDestination>(this TSource source, string[] ignoreMembers = null)
        {
            var config = new MapperConfiguration(cfg => {
                var t = cfg.CreateMap<TSource, TDestination>().MaxDepth(1);

                if (ignoreMembers != null && !ignoreMembers.Length.Equals(0))
                {
                    ignoreMembers.Each(ignoreMember => t.ForMember(ignoreMember, option => option.Ignore()));
                }
            });
            
            IMapper mapper = config.CreateMapper();
            var destination = Mapper.Map<TSource, TDestination>(source);

            return destination;

            //var mapper = Mapper.CreateMap<TSource, TDestination>().MaxDepth(1);
            //if (ignoreMembers != null && !ignoreMembers.Length.Equals(0))
            //    ignoreMembers.Each(ignoreMember => mapper.ForMember(ignoreMember, option => option.Ignore()));
            //var destination = Mapper.Map<TSource, TDestination>(source);
            //return destination;
        }

        public static TDestination Convert<TSource, TDestination>(this TSource source, Action<TSource, TDestination> customMapper, string[] ignoreMembers = null)
        {
            var output = Convert<TSource, TDestination>(source, ignoreMembers);
            if (customMapper != null)
                customMapper(source, output);
            return output;
        }

        public static void Each<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            foreach (var item in enumerable)
            {
                action(item);
            }
        }
    }
}
