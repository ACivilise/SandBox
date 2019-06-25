using AutoMapper;

namespace SandBox.DataAccess.Mapper
{
    public static class GenericExtensions
    {
        public static TDestination MapAs<TDestination>(this object source)
        {
            return AutoMapper.Mapper.Map<TDestination>(source);
        }
        public static TDestination MapAs<TSource, TDestination>(this TSource source)
        {
            return AutoMapper.Mapper.Map<TDestination>(source);
        }
        /// <summary>
        /// Mapping d'une source de données vers un objet déjà existant.
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TDestination"></typeparam>
        /// <param name="source">Source des données.</param>
        /// <param name="destination">Objet à modifier.</param>
        /// <returns></returns>
        public static TDestination MapAs<TSource, TDestination>(this TSource source, TDestination destination)
        {
            return AutoMapper.Mapper.Map<TSource, TDestination>(source, destination);
        }
    }
}
