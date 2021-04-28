using System;

namespace Kwetter.Services.AuthService.Domain
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Avatar { get; set; }
        public string OpenId { get; set; }
    }
}