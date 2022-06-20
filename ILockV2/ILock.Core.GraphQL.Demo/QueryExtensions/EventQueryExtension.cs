using ILock.Core.Data.Constants;
using ILock.Core.GraphQL.Demo.Constants;
using ILock.Core.GraphQL.Demo.Queries;
using ILock.Core.GraphQL.Extensions;
using ILock.Core.GraphQL.Extensions.Queries;

namespace ILock.Core.GraphQL.Demo.QueryExtensions
{
    /// <summary>
    /// The event query extension.
    /// </summary>
    [TypePermissionName(nameof(EntityName.Event), nameof(AccessLevel.Full), nameof(AccessLevel.Read))]
    public class EventQueryExtension : ObjectTypeExtension<Query>
    {
        private readonly string permissionName;
        private readonly List<string> accessLevels;

        /// <summary>
        /// Initializes a new instance of the <see cref="EventQueryExtension"/> class.
        /// </summary>
        public EventQueryExtension()
        {
            var pName = this.GetType().GetCustomAttributes(false).OfType<TypePermissionNameAttribute>().FirstOrDefault();
            if (pName != null)
            {
                this.permissionName = nameof(EntityName.Event);
                this.accessLevels = new List<string>
                {
                    nameof(AccessLevel.Full), nameof(AccessLevel.Read)
                };
            }
        }

        /// <summary>
        /// Configures the.
        /// </summary>
        /// <param name="descriptor">The descriptor.</param>
        protected override void Configure(IObjectTypeDescriptor<Query> descriptor)
        {
            descriptor.Field(nameof(EventQueryResolver.GetEvents))
                .Argument("countryId", a => a.Type<NonNullType<IntType>>())
                .Argument("scenarioId", a => a.Type<NonNullType<IntType>>())
                .Argument("retailerId", a => a.Type<NonNullType<IntType>>())
                .ResolveWith<EventQueryResolver>(EventQueryResolver => EventQueryResolver.GetEvents(default, default, default, default)).ConfigureAuthorization(this.permissionName, this.accessLevels.ToArray());

            descriptor.Field(nameof(EventQueryResolver.GetEventById))
            .ResolveWith<EventQueryResolver>(EventQueryResolver => EventQueryResolver.GetEventById(default, default)).ConfigureAuthorization(this.permissionName, this.accessLevels.ToArray());
        }
    }
}
