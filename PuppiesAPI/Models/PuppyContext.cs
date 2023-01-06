using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace PuppiesAPI.Models;

public class PuppyContext : DbContext
{
    public DbSet<Puppy> Puppies { get; set; }

    public string DbPath { get; }

    public PuppyContext()
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        DbPath = System.IO.Path.Join(path, "puppies.db");
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options) => options.UseSqlite($"Data Source={DbPath}");
}
