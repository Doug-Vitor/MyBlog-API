public class Role : BaseEntity
{
    public string Name { get; set; }

    public Role()
    {
    }

    public Role(int id, string name) : base(id) => Name = name;
}
