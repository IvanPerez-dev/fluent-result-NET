namespace Osom.FluentRestult.Domain.Entities
{
    public abstract class Entity<TKey>
    {
        public TKey Id { get; set; }
    }
}
