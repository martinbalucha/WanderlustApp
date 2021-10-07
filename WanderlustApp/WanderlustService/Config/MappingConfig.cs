using AutoMapper;
using WanderlustPersistence.Entity;
using WanderlustService.DataTransferObject.Entities.Country;
using WanderlustService.DataTransferObject.Entities.User;

namespace WanderlustService.Config
{
    /// <summary>
    /// A configuration class for Automapper
    /// </summary>
    public static class MappingConfig
    {
        /// <summary>
        /// Configures object mapping
        /// </summary>
        /// <param name="config">Configuration expression</param>
        public static void ConfigureMapping(IMapperConfigurationExpression config)
        {
            config.CreateMap<CountryCreateDto, Country>();
            config.CreateMap<CountryUpdateDto, Country>();

            config.CreateMap<User, UserDto>();
            config.CreateMap<UserRegisterDto, User>();
            config.CreateMap<UserUpdateDto, User>();
        }
    }
}
