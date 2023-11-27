﻿using ItTakesAVillage.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace ItTakesAVillage.Data;

public class ItTakesAVillageContext : IdentityDbContext<ItTakesAVillageUser>
{
    public DbSet<Models.BaseEvent> Events { get; set; }
    public DbSet<Models.DinnerInvitation> DinnerInvitations { get; set; }
    public DbSet<Models.Group> Groups { get; set; }
    public DbSet<UserGroup> UserGroups { get; set; }
    public DbSet<Notification> Notifications { get; set; }

    public ItTakesAVillageContext(DbContextOptions<ItTakesAVillageContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Models.BaseEvent>().ToTable("Events");
        builder.Entity<Models.DinnerInvitation>().ToTable("Events");


    }
}
