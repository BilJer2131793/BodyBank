using Microsoft.EntityFrameworkCore;
using BodyBank.Models;
using BodyBank.Model;
using Microsoft.Extensions.DependencyInjection;

namespace BodyBank.Data
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new MVCBodyBankContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<MVCBodyBankContext>>()))
            {
                //if (context.Type is not null)
                //{
                //    return;
                //}
                if (context?.Type?.Count() > 0)
                {
                    return;
                }
                context.Donneur.AddRange(new Donneur());


                context.Type.AddRange(
                    new Model.Type
                    {
                        Nom = "Coeur",
                        PrixBase = 1000.00,
                        Desc = "Coeur",
                        Image = "coeur.jpg"
                    },
                    new Model.Type
                    {
                        Nom = "Poumon",
                        PrixBase = 500.00,
                        Desc = "Poumon",
                        Image = "poumon.jpg"
                    },
                    new Model.Type
                    {
                        Nom = "Rein",
                        PrixBase = 800.00,
                        Desc = "Rein",
                        Image = "rein.jpg"
                    },
                    new Model.Type
                    {
                        Nom = "Foie",
                        PrixBase = 1200.00,
                        Desc = "Foie",
                        Image = "foie.jpg"
                    },
                    new Model.Type
                    {
                        Nom = "Intestin",
                        PrixBase = 1500.00,
                        Desc = "Intestin",
                        Image = "intestin.jpg"
                    }
                );
                context.SaveChanges();
            }
            InitializeOrganes(serviceProvider);
        }
        public static void InitializeOrganes(IServiceProvider serviceProvider)
        {
            using (var context = new MVCBodyBankContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<MVCBodyBankContext>>()))
            {
                var donneur = context.Donneur.FirstOrDefault();
                var coeurType = context.Type.FirstOrDefault(t => t.Nom == "Coeur");
                var poumonType = context.Type.FirstOrDefault(t => t.Nom == "Poumon");
                var reinType = context.Type.FirstOrDefault(t => t.Nom == "Rein");
                var foieType = context.Type.FirstOrDefault(t => t.Nom == "Foie");
                var intestinType = context.Type.FirstOrDefault(t => t.Nom == "Intestin");

                context.AddRange(
                    new Organne
                    {
                        Disponible = true,
                        Prix = coeurType.PrixBase,
                        Type = coeurType,
                        Donneur = donneur
                    },
                    new Organne
                    {
                        Disponible = true,
                        Prix = coeurType.PrixBase + 1000,
                        Type = coeurType,
                        Donneur = donneur
                    },
                    new Organne
                    {
                        Disponible = true,
                        Prix = poumonType.PrixBase,
                        Type = poumonType,
                        Donneur = donneur
                    }, new Organne
                    {
                        Disponible = true,
                        Prix = poumonType.PrixBase + 1000,
                        Type = poumonType,
                        Donneur = donneur
                    }, new Organne
                    {
                        Disponible = true,
                        Prix = reinType.PrixBase,
                        Type = reinType,
                        Donneur = donneur
                    }, new Organne
                    {
                        Disponible = true,
                        Prix = reinType.PrixBase + 1000,
                        Type = reinType,
                        Donneur = donneur
                    }, new Organne
                    {
                        Disponible = true,
                        Prix = foieType.PrixBase,
                        Type = foieType,
                        Donneur = donneur
                    }, new Organne
                    {
                        Disponible = true,
                        Prix = foieType.PrixBase + 1000,
                        Type = foieType,
                        Donneur = donneur
                    }, new Organne
                    {
                        Disponible = true,
                        Prix = intestinType.PrixBase,
                        Type = intestinType,
                        Donneur = donneur
                    }, new Organne
                    {
                        Disponible = true,
                        Prix = intestinType.PrixBase + 1000,
                        Type = intestinType,
                        Donneur = donneur
                    }
                    ); ;
                context.SaveChanges();
            }
        }

    }
}
