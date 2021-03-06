using System.Collections.Generic;

namespace BonoApp.API.User.Domain.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public IList<Bono.Domain.Models.Bond> Bonds { get; set; } = new List<Bono.Domain.Models.Bond>();
    }
}