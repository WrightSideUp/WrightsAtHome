using System;
using System.ComponentModel.DataAnnotations;

namespace WrightsAtHome.Server.Domain.Entities
{
    public interface IBaseEntity
    {
        int Id { get; set; }
        
        DateTime LastModified { get; set; }

        int LastModifiedUserId { get; set; }
    }
}