using ILock.Core.Data.Entities;

namespace ILock.Core.Services.Abstractions
{
    /// <summary>
    /// Role Service Interface
    /// </summary>
    public interface IRoleService : IBaseEntityService<Role>
    {
        /// <summary>
        /// Gets the role ids by names.
        /// </summary>
        /// <param name="roleNames">The role names.</param>
        /// <returns>A list of int.</returns>
        List<int> GetRoleIdsByNames(List<string> roleNames);

        /// <summary>
        /// Gets the role names by ids.
        /// </summary>
        /// <param name="modelAssociatedRoleIDs">The model associated role i ds.</param>
        /// <returns>A list of Roles.</returns>
        IEnumerable<Role> GetRoleNamesByIds(IEnumerable<int> roleIDs);
    }
}
