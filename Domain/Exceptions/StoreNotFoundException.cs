
namespace Domain.Exceptions
{
    public class StoreNotFoundException: NotFoundException
    {
        public StoreNotFoundException(Guid id): base($"Store with id {id} not found.") { }
    }
}
