using System;
using Microsoft.AspNetCore.Identity;
using SOL.Core.Entity;

namespace SOL.Identity.SOL.Identity.Domain.Entities
{
    public class User : IdentityUser, IEntity
    {
        public string Password;

        string IEntity.Id { get => Id; set => Id = value; }
    }
}