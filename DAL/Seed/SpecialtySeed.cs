using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Seed
{
    public static class SpecialtySeed
    {
        public static void SeedSpecialty(this ModelBuilder builder)
        {
            builder.Entity<Specialty>().HasData(
        new Specialty
        {
            Id = 1,
            Name = "أمراض القلب",
            ImageUrl = "http://res.cloudinary.com/dx3xe3xxp/image/upload/v1680960692/da-net7/goeulqkn8z9bdisnryiy.png" // Cardiovascular Health
        },
        new Specialty
        {
            Id = 2,
            Name = "الأمراض النفسية",
            ImageUrl = "http://res.cloudinary.com/dx3xe3xxp/image/upload/v1680960705/da-net7/vgyf9fpn4yznhtihnhds.png" // Mental Health
        },
        new Specialty
        {
            Id = 3,
            Name = "الأمراض الجلدية",
            ImageUrl = "http://res.cloudinary.com/dx3xe3xxp/image/upload/v1680960810/da-net7/k7l6rs6oaliku1zgvtef.png" // Dermatological Health
        },
        new Specialty
        {
            Id = 4,
            Name = "طب النساء والتوليد",
            ImageUrl = "https://res.cloudinary.com/dx3xe3xxp/image/upload/v1680960909/da-net7/eqkehqhlgqjro8yvzqhh.png" // Women's Health
        },
        new Specialty
        {
            Id = 5,
            Name = "طب الأطفال",
            ImageUrl = "http://res.cloudinary.com/dx3xe3xxp/image/upload/v1680960853/da-net7/sz1zgbptcmdt71coxmcf.png" // Pediatrics
        },
        new Specialty
        {
            Id = 6,
            Name = "علم الأورام",
            ImageUrl = "http://res.cloudinary.com/dx3xe3xxp/image/upload/v1680960844/da-net7/jfvznth944kstrmwv3yt.png" // Oncology
        },
        new Specialty
        {
            Id = 7,
            Name = "الأمراض العصبية",
            ImageUrl = "http://res.cloudinary.com/dx3xe3xxp/image/upload/v1681515080/da-net7/njgxuneb3oppmbxvycfq.png" // Neurological Health
        },
        new Specialty
        {
            Id = 8,
            Name = "طب العيون",
            ImageUrl = "http://res.cloudinary.com/dx3xe3xxp/image/upload/v1680960887/da-net7/illqiu1xxq1pchw1mwqj.png" // Ophthalmological Health
        },
        new Specialty
        {
            Id = 9,
            Name = "طب الجهاز الهضمي",
            ImageUrl = "http://res.cloudinary.com/dx3xe3xxp/image/upload/v1680960896/da-net7/aeqgqwcdfl5hdreupoca.png" // Gastrointestinal Health
        }
    );
        }
    }
}