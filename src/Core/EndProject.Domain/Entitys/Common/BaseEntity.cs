namespace EndProject.Domain.Entitys.Common;

public abstract class BaseEntity
{
    public Guid Id { get; set; } = new Guid();
    public DateTime CreatedDate { get; set; } = DateTime.Now;
    public DateTime ModifiedDate { get; set;} = DateTime.Now;
    public virtual bool IsDeleted { get; set; } 
}
