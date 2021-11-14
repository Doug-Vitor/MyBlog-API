﻿public class User : BaseEntity
{
    public string Username { get; set; }
    public string PasswordHash { get; set; }
    public string Email { get; set; }
    public ICollection<int> RolesId { get; set; }

    public User()
    {
    }

    public User(int id, string username, string passwordHash, string email, ICollection<int> rolesId) 
        : base(id) => (Username, PasswordHash, Email, RolesId) = (username, passwordHash, email, rolesId);
}