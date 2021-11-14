public class Role : BaseEntity
{
    public string Name { get; set; }

    public Role()
    {
    }

    public Role(string name) => Name = name;
}
