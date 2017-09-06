using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ToDo.Models;

    public class Authdb : DbContext
    {
        public Authdb (DbContextOptions<Authdb> options)
            : base(options)
        {
        }

        public DbSet<ToDo.Models.ToDoModel> ToDoModel { get; set; }
    }
