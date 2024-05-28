﻿using Restaurants.Domain.Entities;
using Restaurants.Infrastructure.Persistence;

namespace Restaurants.Infrastructure.Seeders
{
    internal class RestaurantSeeder(RestaurantsDbContext dbContext) : IRestaurantSeeder
    {
        public async Task Seed()
        {
            if (await dbContext.Database.CanConnectAsync())
            {
                if (!dbContext.Restaurants.Any())
                {
                    var restaurants = GetRestaurants();
                    dbContext.Restaurants.AddRange(restaurants);
                    await dbContext.SaveChangesAsync();
                }
            }
        }

        private IEnumerable<Restaurant> GetRestaurants()
        {
            var restaurants = new List<Restaurant>()
            {
                new Restaurant
                {
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
