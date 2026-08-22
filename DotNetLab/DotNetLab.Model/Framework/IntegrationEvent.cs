namespace DotNetLab.Model.Framework;

public abstract class IntegrationEvent<T>
{
    public T Data { get; set; }
    public Guid CorrelationId { get; set; }
}