namespace MyMarketplase.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int RoleID { get; set; }
        public ICollection<Order> Orders { get; set; }

        public virtual Role Role { get; set; }
    }
}
