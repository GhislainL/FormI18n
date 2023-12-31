﻿using Microsoft.EntityFrameworkCore;
using XLocalizer.DB.Models;

namespace DBLocalizationSample.Data
{
    public static class XLocalizerDbExtensions
    {
        public static void SeedCultures(this ModelBuilder modelBuilder)
        {
            // Seed initial data for localization stores
            modelBuilder.Entity<XDbCulture>().HasData(
        new XDbCulture { IsActive = true, IsDefault = true, ID = "en", EnglishName = "English" },
        new XDbCulture { IsActive = true, IsDefault = false, ID = "fr", EnglishName = "French" },
        new XDbCulture { IsActive = true, IsDefault = false, ID = "es", EnglishName = "Spanish" },
        new XDbCulture { IsActive = true, IsDefault = false, ID = "it", EnglishName = "Italian" }
        );
        }

        public static void SeedResourceData(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<XDbResource>().HasData(
                    new XDbResource { ID = 1, Key = "Welcome", Value = "Hoşgeldin", CultureID = "tr", IsActive = true, Comment = "Created by XLocalizer" }
                );
        }
    }
}
