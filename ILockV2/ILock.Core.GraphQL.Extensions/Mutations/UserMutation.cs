using ILock.Core.Data;
using ILock.Core.Data.Entities;
using ILock.Core.GraphQL.Extensions.Types;
using ILock.Core.Services.Abstractions;

namespace ILock.Core.GraphQL.Extensions.Mutations
{
    /// <summary>
    /// The event module mutations.
    /// </summary>
    [ExtendObjectType(nameof(Mutation))]
    public class UserMutation : ILockMutationType
    {
        /// <summary>
        /// Adds the user.
        /// </summary>
        /// <param name="addUserPayload">The add user payload.</param>
        /// <param name="context">The context.</param>
        /// <param name="cryptoService">The crypto service.</param>
        /// <returns>A Data.Entities.User.</returns>
        public Data.Entities.User AddUser(Data.Entities.User addUserPayload, [Service] AuthDBContext context, [Service] ICryptoService cryptoService)
        {
            User alreadyAvailableUser = context.Users.FirstOrDefault(u => u.Username.ToUpper() == addUserPayload.Username.ToUpper() || u.Email.ToUpper() == addUserPayload.Email.ToUpper());

            if (alreadyAvailableUser == null)
            {
                addUserPayload.Password = cryptoService.ComputePassword(addUserPayload.Email, addUserPayload.Password);
                context.Users.Add(addUserPayload);
                context.SaveChanges();
                return addUserPayload;
            }
            throw new InvalidOperationException("User already exists with these credentials");
        }

        /// <summary>
        /// Updates the User async.
        /// </summary>
        /// <param name="updateUserPayload">The update event payload.</param>
        /// <param name="userService">The User Service.</param>
        /// <returns>A Task.</returns>
        public async Task<Data.Entities.User> UpdateUserAsync(Data.Entities.User updateUserPayload, [Service] IUserService userService)
        {
            await userService.UpdateAsync(updateUserPayload);
            userService.Commit();

            return updateUserPayload;
        }
    }
}
