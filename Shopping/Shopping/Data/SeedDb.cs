using Shopping.Data.Entities;
using Shopping.Enum;
using Shopping.Helpers;

namespace Shopping.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;

        public SeedDb(DataContext context, IUserHelper userHelper)
        {
            _context = context;
            _userHelper = userHelper;
        }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            await CheckCatagoriesAsync();
            await CheckCountriesAsync();
            await CheckRolesAsync();
        }

        private async Task CheckRolesAsync()
        {
            await _userHelper.CheckRoleAsync(UserType.Admin.ToString());
            await _userHelper.CheckRoleAsync(UserType.User.ToString());
            await CheckUserAsync("1010", "Oscar", "Espinosa", "oscar@yopmail.com", "323 244 0427", "Calle luna calle sol", UserType.Admin);
            await CheckUserAsync("1010", "Juan", "Perez", "juan@yopmail.com", "310 579 5614", "Cra 4 15-21", UserType.User);

        }

        private async Task<User> CheckUserAsync(
            string document, 
            string firstName, 
            string lastName, 
            string email, 
            string phone, 
            string address, 
            UserType userType)
        {
            User user=await _userHelper.GetUserAsync(email);
            if (user == null)
            {
                user = new User
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    UserName = email,
                    PhoneNumber = phone,
                    Address = address,
                    Document = document,
                    City = _context.Cities.FirstOrDefault(),
                    UserType = userType
                };
                await _userHelper.AddUserAsync(user, "123456");
                await _userHelper.AddUserToRoleAsync(user, userType.ToString());
            }
            return user;
        }

        private async Task CheckCountriesAsync()
        {
            if (!_context.Countries.Any())
            {
                _context.Countries.Add(new Country
                {
                    Name = "Colombia",
                    States = new List<State>()
                    {
                        new State
                        {
                            Name = "Antioquia",
                            Cities = new List<City>()
                            {
                                new City{Name ="Medellín"},
                                new City{Name ="Envigado"},
                                new City{Name ="Itagui"},
                                new City{Name ="Bello"},
                                new City{Name ="Rionegro"}
                            }
                        },
                        new State
                        {
                            Name = "Bogotá",
                            Cities = new List<City>()
                            {
                                new City{Name ="Usaquen"},
                                new City{Name ="Chapinero"},
                                new City{Name ="Santa Fe"},
                                new City{Name ="Usme"},
                                new City{Name ="Bosa"}
                            }
                        }
                    }
                });
                _context.Countries.Add(new Country
                {
                    Name = "Estados Unidos",
                    States = new List<State>()
                    {
                        new State
                        {
                            Name = "Florida",
                            Cities = new List<City>()
                            {
                                new City{Name ="Orlando"},
                                new City{Name ="Miami"},
                                new City{Name ="Tampa"},
                                new City{Name ="Fort Lauderdale"},
                                new City{Name ="Key West"}
                            }
                        },
                        new State
                        {
                            Name = "Texas",
                            Cities = new List<City>()
                            {
                                new City{Name ="Houston"},
                                new City{Name ="San Antonio"},
                                new City{Name ="Dallas"},
                                new City{Name ="Austin"},
                                new City{Name ="El paso"}
                            }
                        }
                    }
                });
            }
            await _context.SaveChangesAsync();
        }

        private async Task CheckCatagoriesAsync()
        {
            if (!_context.Categories.Any())
            {
                _context.Categories.Add(new Category { Name = "Tecnología" });
                _context.Categories.Add(new Category { Name = "Ropa" });
                _context.Categories.Add(new Category { Name = "Calzado" });
                _context.Categories.Add(new Category { Name = "Belleza" });
                _context.Categories.Add(new Category { Name = "Nutrición" });
                _context.Categories.Add(new Category { Name = "Deportes" });
                _context.Categories.Add(new Category { Name = "Apple" });
                _context.Categories.Add(new Category { Name = "Mascotas" });
                await _context.SaveChangesAsync();
            }
        }
    }
}
