using ILock.Core.GraphQL.Demo.Data;
using ILock.Core.GraphQL.Extensions.Queries;
using System.Linq.Expressions;
using System.Reflection;

namespace ILock.Core.GraphQL.Demo.Queries
{
    public class BaseQueryResolver
    {
        private readonly List<(string Name, System.Reflection.ParameterInfo[] Parameters)> fields;

        public BaseQueryResolver()
        {
            this.fields =
            this.GetType().GetMethods().Where(t => t.IsPublic)
                .Select(t =>
                    (t.GetCustomAttributes(true).OfType<GraphQLNameAttribute>().FirstOrDefault()?.Name ?? t.Name,
                     t.GetParameters())).ToList();
        }
    }
    /// <summary>
    /// The queries related to event module.
    /// </summary>
    public class EventQueryResolver
    {
        /// <summary>
        /// Gets the events.
        /// </summary>
        /// <param name="appDbContext">The app db context.</param>
        /// <returns>An IQueryable.</returns>
        [UseFiltering]
        [UseSorting]
        [GraphQLName("getEvents")]
        public static IQueryable<Data.Entities.EventEntity> GetEvents([Service] DemoDbContext appDbContext, int countryId, int retailerId, int scenarioId)
        {
            // We can filter Events based on the model selection from here.
            return appDbContext.Events;
        }

        /// <summary>
        /// Gets the event by id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="appDbContext">The app db context.</param>
        /// <returns>An IQueryable.</returns>
        [UseFiltering]
        [UseSorting]
        [GraphQLName("getEventById")]
        public static IQueryable<Data.Entities.EventEntity> GetEventById([Service] DemoDbContext appDbContext, int id)
        {
            return appDbContext.Events.Where(x => x.ID == id);
        }
    }


    public static class QueryResolverExtensions
    {
        public static IObjectTypeDescriptor<TQuery> ResolveWithQuery<TQuery, TResolver>(this IObjectTypeDescriptor<TQuery> objectTypeDescriptor)
        {
            var resolverType = typeof(TResolver);
            var methods = resolverType.GetMethods()
                 .Select(t =>
                     (t.Name, t.GetCustomAttributes(true).OfType<GraphQLNameAttribute>().FirstOrDefault()?.Name ?? t.Name,
                      t.GetParameters())).ToList();

            foreach (var (mname, name, parameters) in methods)
            {
                List<object> parames = new List<object>();
                foreach (var param in parameters)
                {
                    parames.Add(param.ParameterType.GetDefaultValue());
                }

                var method = resolverType.GetMethod(mname);
                var r = Activator.CreateInstance<TResolver>();
                Func<TResolver, object?> func = (rs) => method.Invoke(rs, parames.ToArray());
                func.Invoke(r);
                objectTypeDescriptor.Field(name).ResolveWith<TResolver>(s => func.Invoke(s));
            }

            return objectTypeDescriptor;
        }

    }
    public static class TypeExtensions
    {
        public static object GetDefaultValue(this Type t)
        {
            if (Nullable.GetUnderlyingType(t) == null)
                return Activator.CreateInstance(t);
            else
                return null;
        }
    }

}
