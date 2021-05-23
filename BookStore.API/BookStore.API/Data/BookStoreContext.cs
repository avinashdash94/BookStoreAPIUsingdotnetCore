using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.API.Data
{
    //To tell about all Database(Entity frameWork) we have to add that service in Startup.cs
    public class BookStoreContext: DbContext
    {
        //We have to pass some options using DbContextOptions
        //Also pass the same options(settings) to the base class
        public BookStoreContext(DbContextOptions<BookStoreContext> options)
            : base(options)
        {

        }

        //Here we tell BookStoreContext we will use Books Class as context 
        //For this we use DbSet
        //This Property Books Will create a new Table in the Db
        public DbSet<Books> Books { get; set; }

        //Create connection string with database
        //For connection we override the following method
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    //The parametter for UseSqlServer() is connection string which contains Server authentication for here '.' indicates Local server and Database Name and Integrated Security indicates windows authentication
        //    optionsBuilder.UseSqlServer("Server=.;Database=BooksStoreAPI;Integrated Security=True");
        //    base.OnConfiguring(optionsBuilder);
        //}
    }
}
