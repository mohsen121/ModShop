using ModShop.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace ModShop.Domain
{
    public abstract class BaseEntity
    {
        public DateTime Created { get; set; }

        public DateTime? LastModified { get; set; }

        public abstract bool Search(object obj);
    }

    public abstract class BaseEntity<TKey> : BaseEntity
    {
        public TKey Id { get; set; }


    }

    public abstract class BaseEntity<TKey, TUser, TUserKey> : BaseEntity<TKey>
        where TUser : IdentityUser<TUserKey>
        where TUserKey : IEquatable<TUserKey>
    {
        public TUserKey CreatedById { get; set; }
        public virtual TUser CreatedBy { get; set; }
    }
}
