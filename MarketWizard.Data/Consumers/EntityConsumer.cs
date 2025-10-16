using HotChocolate.Subscriptions;
using MarketWizard.Application.Contracts.Events;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace MarketWizard.Data.Consumers;

public abstract class EntityConsumer(ITopicEventSender sender, ILogger<EntityConsumer> logger)
    : IConsumer<EntityEvent>
{
    public async Task Consume(ConsumeContext<EntityEvent> context)
    {
        var entityEvent = context.Message;

        if (entityEvent.EventType == EntityEventType.Portfolio)
        {
            var topicName = entityEvent.Status switch
            {
                EntityEventStatus.Created => "PortfolioCreated",
                EntityEventStatus.Deleted => "PortfolioDeleted",
                EntityEventStatus.Updated => "PortfolioUpdated",
                _ => throw new ArgumentOutOfRangeException(nameof(entityEvent.Status))
            };

            await sender.SendAsync(topicName, context.Message.Id, context.CancellationToken);
            
            logger.LogInformation("Entity event published: {EventType} {Id}", entityEvent.EventType, entityEvent.Id);
        }
    }
}