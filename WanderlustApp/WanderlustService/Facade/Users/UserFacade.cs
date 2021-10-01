using AutoMapper;
using System;
using System.Threading.Tasks;
using WanderlustPersistence.Entity;
using WanderlustInfrastructure.UnitOfWork;
using WanderlustService.DataTransferObject.Entities.User;
using WanderlustService.Service.Entities.Users;

namespace WanderlustService.Facade.Users
{
    /// <summary>
    /// An implementation of <see cref="IUserFacade"/>
    /// </summary>
    public class UserFacade : FacadeBase, IUserFacade
    {
        /// <summary>
        /// A service class for manipulation of user entities
        /// </summary>
        private readonly IUserService userService;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="userService">A service class for manipulation of user entities</param>
        /// <param name="unitOfWorkContext">A context for creating units of work</param>
        /// <param name="mapper">Mapper</param>
        public UserFacade(IUserService userService, IUnitOfWorkContext unitOfWorkContext, IMapper mapper) : base(unitOfWorkContext, mapper)
        {
            this.userService = userService;
        }

        public async Task<bool> AuthentizeAsync(UserLoginDto userDto)
        {
            using (IUnitOfWork unitOfWork = unitOfWorkContext.Create())
            {
                bool result = await userService.AuthenticateAsync(userDto.Username, userDto.Password);
                await unitOfWork.CommitAsync();
                return result;
            }
        }

        public async Task ChangePasswordAsync(UserPasswordChangeDto userDto)
        {
            using (IUnitOfWork unitOfWork = unitOfWorkContext.Create())
            {
                await userService.ChangePasswordAsync(userDto.Username, userDto.OldPassword, userDto.NewPassword);
                await unitOfWork.CommitAsync();
            }
        }

        public async Task<UserDto> FindByUsernameAsync(string username)
        {
            using (IUnitOfWork unitOfWork = unitOfWorkContext.Create())
            {
                var user = await userService.FindByUsernameAsync(username);
                var userDto = mapper.Map<UserDto>(user);
                await unitOfWork.CommitAsync();
                return userDto;
            }
        }

        public async Task RegisterAsync(UserRegisterDto userDto)
        {
            User user = mapper.Map<User>(userDto);
            using (IUnitOfWork unitOfWork = unitOfWorkContext.Create())
            {
                await userService.RegisterAsync(user);
                await unitOfWork.CommitAsync();
            }
        }

        public Task UpdateAsync(UserUpdateDto userDto)
        {
            throw new NotImplementedException();
        }
    }
}
