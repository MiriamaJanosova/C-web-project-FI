namespace Infrastructure
{
    public interface IEntity
    {
        int ID { get; set; }
        
        string TableName { get; }
    }
}
