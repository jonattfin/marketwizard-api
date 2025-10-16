namespace MarketWizard.Application.Contracts.Events;

public enum EntityEventType
{
    Portfolio
}

public enum EntityEventStatus
{
    Created,
    Updated,
    Deleted
}

public class EntityEvent
{
    public Guid Id { get; set; }
    
    public EntityEventType EventType { get; set; }
    
    public EntityEventStatus Status { get; set; }
}