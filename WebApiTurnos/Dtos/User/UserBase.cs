namespace WebApiTurnos.Dtos.User
{
    public class UserBase
    {
        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string DocumentId { get; set; } = null!;

        public string Password { get; set; } = null!;
    }
}
