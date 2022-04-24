using AutoMapper;

namespace BAL
{
    public class BaseBusiness
    {
        /// <summary>
        /// ObjectMapping function maps from entity object to response object
        /// </summary>
        /// <typeparam name="Source"></typeparam>
        /// <typeparam name="Destination"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        protected Destination ObjectMapping<Source, Destination>(Source source)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Source, Destination>();
            });
            
            var mapper = config.CreateMapper();

            var result = mapper.Map<Destination>(source);
            return result;
        }

        /// <summary>
        /// ListMapping function maps from entity list to response list
        /// </summary>
        /// <typeparam name="Source"></typeparam>
        /// <typeparam name="Destination"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        protected List<Destination> ListMapping<Source, Destination>(List<Source> source)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Source, Destination>();
            });
            
            var mapper = config.CreateMapper();

            var result = mapper.Map<List<Destination>>(source);
            return result;
        }
    }
}
