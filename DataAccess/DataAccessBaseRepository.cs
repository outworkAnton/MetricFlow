namespace DataAccess
{
    public class DataAccessBaseRepository
    {
        protected readonly DataAccessContext Context = new DataAccessContext();
    }
}