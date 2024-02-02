using EndProject.Domain.Entitys.Common;

namespace EndProject.Domain.Entitys;

public class Person : BaseEntity
{
    public int MyProperty { get; set; }
    public string Name { get; set; }
    public string FirstName { get; set; }

    public Person(int myProperty, string name, string firstName)
    {
        MyProperty = myProperty;
        Name = name;
        FirstName = firstName;
    }
}
