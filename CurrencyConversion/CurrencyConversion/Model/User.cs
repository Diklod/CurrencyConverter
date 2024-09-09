namespace CurrencyConversion.Model
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Adress { get; set; }
        public string Phone { get; set; }
        public DateTime BirthDate { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string? Token { get; set; }

        public User(int id, string name, string adress, string phone, DateTime birthDate, string email, string password, string token)
        {
            Id = id;
            Name = name;
            Adress = adress;
            Phone = phone;
            BirthDate = birthDate;
            Email = email;
            Password = password;
            Token = token;
        }
    }
}
