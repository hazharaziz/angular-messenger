﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebServer.Models.DBModels
{
    public partial class User
    {
        public User()
        {
            Followers = new HashSet<Follower>();
            Messages = new HashSet<Message>();
        }

        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Required]
        [StringLength(30)]
        public string Username { get; set; }

        [Required]
        [StringLength(30)]
        public string Password { get; set; }

        [Required]
        [StringLength(60)]
        public string Name { get; set; }
        public int IsPublic { get; set; } = 0;

        [InverseProperty(nameof(Follower.FollowerNavigation))]
        public virtual ICollection<Follower> Followers { get; set; }
        [InverseProperty(nameof(Message.Composer))]
        public virtual ICollection<Message> Messages { get; set; }
    }
}
