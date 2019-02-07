using System;

namespace Spider.Domain.Entities
{
    public class BaseEntity<T>: IAuditable
    {
        public T Id { get; set; }
        public DateTime SysCreateDateTime { get; set; }
        public DateTime? SysEditedDateTime { get; set; }
    }

    public interface IAuditable
    {
        DateTime SysCreateDateTime { get; set; }
        DateTime? SysEditedDateTime { get; set; }
    }
}