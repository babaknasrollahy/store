namespace Store.Domain.Entities.Users
{
    public class Role
    {
        public int Id { get; set; }
        public required string Name { get; set; }

        public virtual ICollection<UserInRole> UserInRoles { get; set; }
    }
}
