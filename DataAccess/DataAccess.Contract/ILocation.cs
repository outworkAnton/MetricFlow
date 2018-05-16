namespace DataAccess.Contract
{
    public interface ILocation
    {
        string Id { get; set; }
        string Name { get; set; }
        int Active { get; set; }
    }
}