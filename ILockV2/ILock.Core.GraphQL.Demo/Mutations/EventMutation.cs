using HotChocolate.Subscriptions;
using ILock.Core.GraphQL.Demo.Data;
using ILock.Core.GraphQL.Extensions.Mutations;

namespace ILock.Core.GraphQL.Demo.Mutations
{
    /// <summary>
    /// The event module mutations.
    /// </summary>
    [ExtendObjectType(nameof(Mutation))]
    public class EventMutation
    {
        /// <summary>
        /// Adds the event async.
        /// </summary>
        /// <param name="addEventPayload">The add event payload.</param>
        /// <param name="context">The context.</param>
        /// <param name="eventSender">The event sender.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A Task.</returns>
        public async Task<Data.Entities.EventEntity> AddEventAsync(Data.Entities.EventEntity addEventPayload, [Service] DemoDbContext context)
        {
            context.Events.Add(addEventPayload);
            await context.SaveChangesAsync();
            //await eventSender.SendAsync(nameof(EventSubscription.EventAdded), addEventPayload, cancellationToken);
            return addEventPayload;
        }

        /// <summary>
        /// Updates the event async.
        /// </summary>
        /// <param name="updateEventPayload">The update event payload.</param>
        /// <param name="context">The context.</param>
        /// <returns>A Task.</returns>
        public async Task<Data.Entities.EventEntity> UpdateEventAsync(Data.Entities.EventEntity updateEventPayload, [Service] DemoDbContext context)
        {

            context.Events.Update(updateEventPayload);
            await context.SaveChangesAsync();

            return updateEventPayload;
        }
    }
}
