namespace UnitTesting.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<UnitTesting.DataAccessLayer.BankContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(UnitTesting.DataAccessLayer.BankContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            /*
            VehicleType[] VehicleTypes = new VehicleType[]{
                new VehicleType { Id = 1, Name = "Car"},
                new VehicleType { Id = 2, Name = "Boat"},
                new VehicleType { Id = 3, Name = "Plane"},
                new VehicleType { Id = 4, Name = "Truck"},
                new VehicleType { Id = 5, Name = "Buss"}
            };

            Owner[] Owners = new Owner[]{
                new Owner { Id = 1, Name = "Reuben Spatz", PNR = "234534-2345" },
                new Owner { Id = 2, Name = "Kiley Emerick", PNR = "654654-5644" },
                new Owner { Id = 3, Name = "Tawanna Whitner", PNR = "634534-7345" },
                new Owner { Id = 4, Name = "Libby Yorke", PNR = "234784-5678" },
                new Owner { Id = 5, Name = "Tianna Courtney", PNR = "234236-8634" },
                new Owner { Id = 6, Name = "Earlene Greenland ", PNR = "578476-1236" },
                new Owner { Id = 7, Name = "Dodie Holderman", PNR = "127467-6436" },
                new Owner { Id = 8, Name = "Desiree Corby", PNR = "689574-3677" },
                new Owner { Id = 9, Name = "Pedro Baltes", PNR = "234794-6788" },
                new Owner { Id = 10, Name = "Riley Tito", PNR = "357789-6778" }
            };

            Vehicle[] Vehicles = new Vehicle[]{
                new Vehicle { Id = 1, Color = "Red", RegNr = "ABH234", Owner = Owners[0], OwnerId = Owners[0].Id, Type = VehicleTypes[0], TypeId = VehicleTypes[0].Id },
                new Vehicle { Id = 2, Color = "Green", RegNr = "AGH456", Owner = Owners[1], OwnerId = Owners[1].Id, Type = VehicleTypes[1], TypeId = VehicleTypes[1].Id },
                new Vehicle { Id = 3, Color = "Blue", RegNr = "GAH753", Owner = Owners[2], OwnerId = Owners[2].Id, Type = VehicleTypes[2], TypeId = VehicleTypes[2].Id },
                new Vehicle { Id = 4, Color = "White", RegNr = "SDF852", Owner = Owners[3], OwnerId = Owners[3].Id, Type = VehicleTypes[3], TypeId = VehicleTypes[3].Id },
                new Vehicle { Id = 5, Color = "Silver", RegNr = "YUI769", Owner = Owners[4], OwnerId = Owners[4].Id, Type = VehicleTypes[4], TypeId = VehicleTypes[4].Id },
                new Vehicle { Id = 6, Color = "Brown", RegNr = "TYJ669", Owner = Owners[5], OwnerId = Owners[5].Id, Type = VehicleTypes[0], TypeId = VehicleTypes[0].Id },
                new Vehicle { Id = 7, Color = "Yellow", RegNr = "HGF749", Owner = Owners[6], OwnerId = Owners[6].Id, Type = VehicleTypes[1], TypeId = VehicleTypes[1].Id },
                new Vehicle { Id = 8, Color = "Black", RegNr = "DFG349", Owner = Owners[7], OwnerId = Owners[7].Id, Type = VehicleTypes[1], TypeId = VehicleTypes[1].Id },
                new Vehicle { Id = 9, Color = "Pink", RegNr = "YKL469", Owner = Owners[7], OwnerId = Owners[7].Id, Type = VehicleTypes[2], TypeId = VehicleTypes[2].Id }
            };

            context.VehicleTypes.AddOrUpdate(VehicleTypes);
            context.SaveChanges();

            context.Owners.AddOrUpdate(Owners);
            context.SaveChanges();

            context.Vehicles.AddOrUpdate(Vehicles);
            context.SaveChanges();*/
        }
    }
}
