using HotChocolate.Execution.Configuration;
using ILock.Core.Data;
using ILock.Core.GraphQL.Extensions.Queries;
using ILock.Core.GraphQL.Extensions.Types;
using Microsoft.Extensions.DependencyInjection;

namespace ILock.Core.GraphQL.Extensions
{
    /// <summary>
    /// The request execution builder extensions.
    /// </summary>
    public static class RequestExecutionBuilderExtensions
    {
        /// <summary>
        /// Adds the g q l authorization.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <returns>An IRequestExecutorBuilder.</returns>
        public static IRequestExecutorBuilder AddGQLAuthorization(this IRequestExecutorBuilder builder)
        {
            builder.AddAuthorization();
            return builder;
        }

        /// <summary>
        /// Adds the ILock queries with given default auth extensions.
        /// Use this extension method if you want to use authorization on ILock Queries
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <returns>An IRequestExecutorBuilder.</returns>
        public static IRequestExecutorBuilder AddILockAuthQueries(this IRequestExecutorBuilder builder)
        {
            var type = typeof(ILockAuthQueryType);

            builder.AddQueryType(typeof(Query));

            var types = typeof(RequestExecutionBuilderExtensions).Assembly.GetTypes().Where(t => type.IsAssignableFrom(t) && t.IsClass);

            foreach (var queryType in types)
            {
                builder.AddTypeExtension(queryType);
            }

            return builder;
        }

        /// <summary>
        /// Adds the ILock queries without auth.
        /// Use this extension method if you don't want to use Authorization with ILock Queries.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <returns>An IRequestExecutorBuilder.</returns>
        public static IRequestExecutorBuilder AddILockQueries(this IRequestExecutorBuilder builder)
        {
            var type = typeof(ILockQueryType);

            builder.AddQueryType(typeof(Query));

            var types = typeof(RequestExecutionBuilderExtensions).Assembly.GetTypes().Where(t => type.IsAssignableFrom(t) && t.IsClass);

            foreach (var queryType in types)
            {
                builder.AddTypeExtension(queryType);
            }

            return builder;
        }

        /// <summary>
        /// Adds the schema types.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <returns>An IRequestExecutorBuilder.</returns>
        public static IRequestExecutorBuilder AddILockSchemaTypes(this IRequestExecutorBuilder builder)
        {
            var type = typeof(ILockSchemaType);
            var types = System.Reflection.Assembly.GetAssembly(type).GetTypes().Where(t => type.IsAssignableFrom(t) && t.IsClass);

            foreach (var classType in types)
            {
                builder.AddType(classType);
            }

            return builder;
        }

        /// <summary>
        /// Adds the ILock mutations without auth.
        /// Use this extension method if you don't want to use Authorization with ILock Mutations.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <returns>An IRequestExecutorBuilder.</returns>
        public static IRequestExecutorBuilder AddILockMutationTypes(this IRequestExecutorBuilder builder)
        {
            var type = typeof(ILockMutationType);

            builder.AddMutationType(typeof(Mutations.Mutation));
            var types = typeof(RequestExecutionBuilderExtensions).Assembly.GetTypes().Where(t => type.IsAssignableFrom(t) && t.IsClass);

            foreach (var mutationType in types)
            {
                builder.AddTypeExtension(mutationType);
            }

            return builder;
        }

        /// <summary>
        /// Adds the ILock mutations with given default auth extensions.
        /// Use this extension method if you want to use Authorization with ILock Mutations.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <returns>An IRequestExecutorBuilder.</returns>
        public static IRequestExecutorBuilder AddILockAuthMutationTypes(this IRequestExecutorBuilder builder)
        {
            var type = typeof(ILockAuthMutationType);
            builder.AddMutationType(typeof(Mutations.Mutation));
            var types = typeof(RequestExecutionBuilderExtensions).Assembly.GetTypes().Where(t => type.IsAssignableFrom(t) && t.IsClass);

            foreach (var mutationType in types)
            {
                builder.AddTypeExtension(mutationType);
            }

            return builder;
        }

        /// <summary>
        /// Adds the i lock subscription types.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <returns>An IRequestExecutorBuilder.</returns>
        public static IRequestExecutorBuilder AddILockSubscriptionTypes<TSubscription>(this IRequestExecutorBuilder builder)
        {
            var type = typeof(TSubscription);
            var types = typeof(RequestExecutionBuilderExtensions).Assembly.GetTypes().Where(t => type.IsAssignableFrom(t) && t.IsClass);

            foreach (var subscriptionType in types)
            {
                builder.AddSubscriptionType(subscriptionType);
            }
            return builder;
        }
    }
}
