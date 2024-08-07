﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Contants;
using Restaurants.Domain.Entities;
using Restaurants.Infrastructure.Persistence;

namespace Restaurants.Infrastructure.Seeders
{
    internal class RestaurantSeeder(RestaurantsDbContext dbContext, UserManager<User> userManager) : IRestaurantSeeder
    {
        public async Task Seed()
        {
            if (dbContext.Database.GetPendingMigrations().Any())
            {
                await dbContext.Database.MigrateAsync();
            }

            if (await dbContext.Database.CanConnectAsync())
            {
                if (!dbContext.Restaurants.Any())
                {
                    var restaurants = GetRestaurants();
                    dbContext.Restaurants.AddRange(restaurants);
                    await dbContext.SaveChangesAsync();
                }

                if (!dbContext.Roles.Any())
                {
                    var roles = GetRoles();
                    dbContext.Roles.AddRange(roles);
                    await dbContext.SaveChangesAsync();
                }

                if (!dbContext.Users.Any())
                {
                    var user = new User()
                    {
                        Email = "admin@test.com",
                    };

                    await userManager.CreateAsync(user, "Password1!");
                    await dbContext.SaveChangesAsync();
                }
            }
        }

        private IEnumerable<IdentityRole<Guid>> GetRoles()
        {
            List<IdentityRole<Guid>> roles =
                [
                    new IdentityRole<Guid>(UserRoles.User){
                        NormalizedName = UserRoles.User.ToUpper()
                    },
                    new IdentityRole<Guid>(UserRoles.Owner){
                        NormalizedName = UserRoles.Owner.ToUpper()
                    },
                    new IdentityRole<Guid>(UserRoles.Admin){
                        NormalizedName = UserRoles.Admin.ToUpper()
                    }
                ];

            return roles;
        }

        private IEnumerable<Restaurant> GetRestaurants()
        {
            User owner = new User()
            {
                Email = "seed-user@test.com",
            };

            var restaurants = new List<Restaurant>()
            {
                new Restaurant
                {
                    Owner = owner,
                    Name = "KFC",
                    Category = "Fast Food",
                    Description = "KFC (short for Kentucky Fried Chicken)",
                    ContactEmail = "contack@kfc.com",
                    HasDelivery = true,
                    Dishes = new List<Dish>()
                    {
                        new Dish
                        {
                            Name = "Nashville Hot Chicken",
                            Description = "Nashville Hot Chicken (10 pcs.)",
                            Price = 10.30M,
                        },
                        new Dish
                        {
                            Name = "Chicken Nuggets",
                            Description = "Chicken Nuggets (5pcs)",
                            Price = 5.30M,
                        },
                    },
                    Address = new Address()
                    {
                        City = "Kraków",
                        Street = "Długa 5",
                        PostalCode = "30-001",
                    }
                },

                new Restaurant
                {
                    Owner = owner,
                    Name = "MCDonald",
                    Category = "Fast Food",
                    Description = "MCDonald",
                    ContactEmail = "contack@mc.com",
                    HasDelivery = true,
                    Address = new Address()
                    {
                        City = "Rzeszów",
                        Street = "Długa 5",
                        PostalCode = "30-001",
                    }
                }
            };

            return restaurants;
        }
    }
}
