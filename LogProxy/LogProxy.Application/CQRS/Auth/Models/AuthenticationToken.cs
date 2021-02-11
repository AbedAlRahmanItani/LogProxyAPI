using System;

namespace LogProxy.Application.CQRS.Auth.Models
{
    public class AuthenticationToken
    {
        public string Token { get; set; }
        public DateTime? ExpiryDate { get; set; }
    }
}