using ILock.Core.Data.Constants;
using ILock.Core.Data.Models;
using ILock.Core.GraphQL.Extensions.Constants;
using ILock.Core.GraphQL.Extensions.Mutations;
using ILock.Core.GraphQL.Extensions.Types;

namespace ILock.Core.GraphQL.Extensions.MutationExtensions
{
    /// <summary>
    /// The feature mutation extension.
    /// </summary>
    public class FeatureMutationExtension : ObjectTypeExtension<Mutation>, ILockAuthMutationType
    {
        private readonly string permissionName;
        private readonly List<string> accessLevels;

        /// <summary>
        /// Initializes a new instance of the <see cref="FeatureMutationExtension"/> class.
        /// </summary>
        public FeatureMutationExtension()
        {
            this.permissionName = nameof(EntityName.Feature);
            this.accessLevels = new List<string>
                {
                    nameof(AccessLevel.Full), nameof(AccessLevel.Write)
                };
        }

        /// <summary>
        /// Configures the mutation descriptor.
        /// </summary>
        /// <param name="descriptor">The descriptor.</param>
        protected override void Configure(IObjectTypeDescriptor<Mutation> descriptor)
        {
            descriptor.Field(nameof(FeatureMutation.AssignFeaturesWithExternalId))
                 .Argument("featureAssignmentPayload", a => a.Type<ILock.Core.GraphQL.Extensions.Arguments.FeatureAssignmentArgument>())
                .ResolveWith<FeatureMutation>(_ => _.AssignFeaturesWithExternalId(default, default, default)).ConfigureAuthorization(this.permissionName, this.accessLevels.ToArray());
        }
    }
}
