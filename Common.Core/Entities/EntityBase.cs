using System;
namespace Common.Core.Entities
{
    public class EntityBase
    {
        public int Id { get; set; }
        public DateTimeOffset UpdateDate { get; set; }
        public DateTimeOffset CreationDate { get; set; }
    }
}
