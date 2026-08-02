namespace Common
{
    public interface IisDeleted
    {
        public bool IsDeleted { get; set; }
        public DateTime? DeletedAt { get; set; }
    }

}