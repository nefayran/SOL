using Microsoft.AspNetCore.Identity;
using SOL.Core.Entity;
using System;

namespace SOL.Identity.Domain.Entities
{
    public class User : IdentityUser, IEntity
    {
        public string Password;
        
        string IEntity.Id { get => Id; set => Id = value; }
    }
}