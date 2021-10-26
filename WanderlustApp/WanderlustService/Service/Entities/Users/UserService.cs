using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using WanderlustPersistence.Entity;
using WanderlustInfrastructure.Query;
using WanderlustInfrastructure.Repository;
using WanderlustResource.Backend;
using WanderlustService.DataTransferObject.Filter;
using WanderlustService.Service.Common;
using WanderlustService.Service.PasswordManagement;
using WanderlustService.Service.QueryObject.Common;

namespace WanderlustService.Service.Entities.Users
{
    /// <summary>
    /// An implementation of the <see cref="IUserService"/>
    /// </summary>
    public class UserService : EntityServiceBase<User>, IUserService
    {
        /// <summary>
        /// A service class for password management
        /// </summary>
        private readonly IPasswordService passwordService;

        /// <summary>
        /// A query object used for filtering users
        /// </summary>
        private readonly QueryObjectBase<User, UserFilterDto, IQuery<User>> queryObject;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="repository">A repository for CRUD operations</param>
        /// <param name="passwordService">A class for password management</param>
        /// <param name="queryObject">Query object used for filtering entities</param>
        /// <param name="logger">Logger</param>
        public UserService(IRepository<User> repository, IPasswordService passwordService,
                           QueryObjectBase<User, UserFilterDto, IQuery<User>> queryObject, ILogger<UserService> logger)
            : base(repository, logger)
        {
            this.passwordService = passwordService;
            this.queryObject = queryObject;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public async Task<bool> AuthenticateAsync(string username, string password)
        {
            var storedUser = await FindByUsernameAsync(username);
            
            if (storedUser == null)
            {
                logger.LogWarning(string.Format(Warning.WLE001, username));
                return false;
            }
            
            bool result = passwordService.VerifyHashedPassword(storedUser.Password, storedUser.Salt, password);
            if (!result)
            {
                logger.LogWarning(string.Format(Warning.WLE002, username));
            }

            logger.LogInformation(string.Format(Info.ILE002, username));
            return result;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public async Task ChangePasswordAsync(string username, string oldPassword, string newPassword)
        {
            var storedUser = await FindByUsernameAsync(username);

            bool areCredentialsValid = await AuthenticateAsync(username, oldPassword); 
            if (!areCredentialsValid)
            {
                logger.LogWarning(string.Format(Warning.WLE003, username));
                throw new ArgumentException(Exceptions.WLE008);
            }

            var hashedNewPassword = passwordService.CreateHash(newPassword);
            storedUser.Password = hashedNewPassword.Item1;
            storedUser.Salt = hashedNewPassword.Item2;

            logger.LogInformation(string.Format(Info.ILE003, username));
            repository.Update(storedUser);
        }

        /// <summary>
        /// <inheritdoc>/>
        /// </summary>
        public async Task<User> FindByUsernameAsync(string username)
        {
            var filter = new UserFilterDto { Username = username };
            var storedUser = (await queryObject.ExecuteQueryAsync(filter)).Items.SingleOrDefault();
            return storedUser;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public async Task RegisterAsync(User user)
        {
            var storedUser = await FindByUsernameAsync(user.Username);
            if (storedUser != null)
            {
                throw new InvalidOperationException(string.Format(Exceptions.WLE011, user.Username));
            }

            var (hash, salt) = passwordService.CreateHash(user.Password);
            user.Password = hash;
            user.Salt = salt;

            logger.LogInformation(string.Format(Info.ILE004, user.Username));
            await CreateAsync(user);
        }
    }
}
