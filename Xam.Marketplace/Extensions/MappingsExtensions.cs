using System;
using Xam.Marketplace.Mappings;

namespace Xam.Marketplace.Extensions
{
    public static class MappingsExtensions
    {
        public static TDestination ProjectWith<TSource, TDestination>(
            this Mapping<TSource, TDestination> mapping, Func<TDestination,
                TDestination> projection, TSource source) => projection.Invoke(mapping.Project(source));
    }
}
