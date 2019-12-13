using System;
using System.Linq;
using System.Threading.Tasks;
using Backend.Context;
using Backend.Models.View;
using Backend.Services;

namespace Backend
{
    public class Creating
    {
        private readonly ApplicationContext _applicationContext;

        private readonly ProviderService _providerService;
        private readonly RoleService _roleService;
        private readonly UserService _userService;
        private readonly ProductService _productService;
        private readonly AssessmentService _assessmentService;
        private readonly CommentService _commentService;
        private readonly OrderService _orderService;
        private readonly BasketService _basketService;

        public Creating(ApplicationContext applicationContext, ProviderService providerService, RoleService roleService, UserService userService, ProductService productService, AssessmentService assessmentService, CommentService commentService, OrderService orderService, BasketService basketService)
        {
            _applicationContext = applicationContext;
            _providerService = providerService;
            _roleService = roleService;
            _userService = userService;
            _productService = productService;
            _assessmentService = assessmentService;
            _commentService = commentService;
            _orderService = orderService;
            _basketService = basketService;
        }

        public async Task CreateProvider()
        {
            await ProviderCreate();
        }
        public async Task CreateRole()
        {
            await RoleCreate();
        }
        public async Task CreateUser()
        {
            await UserCreate();
        }
        public async Task CreateProduct()
        {
            await ProductCreate();
        }
        public async Task CreateAssessment()
        {
            await AssessmentCreate();
        }
        public async Task CreateComment()
        {
            await CommentCreate();
        }
        public async Task CreateOrder()
        {
            await OrderCreate();
        }

        public async Task CreateBasket()
        {
            await BasketCreate();
        }

        public async Task CreateAdmin()
        {
            await AdminCreate();
        }

        public async Task CreateAll()
        {
            await ProviderCreate();
            await RoleCreate();
            await UserCreate();
            await ProductCreate();
            await AssessmentCreate();
            await CommentCreate();
            await OrderCreate();
            await BasketCreate();
            await AdminCreate();
        }



        private async Task ProviderCreate()
        {
            for (int i = 1; i <= 10; i++)
            {
                await _providerService.Create($"SomeProviderName{i}");
            }
        }

        private async Task RoleCreate()
        {
            await _roleService.Create("Admin");
            await _roleService.Create("User");
        }

        private async Task UserCreate()
        {
            using (_applicationContext)
            {
                for (int i = 1; i <= 20; i++)
                {
                    var user = new RegistrationUserViewModel
                    {
                        UserName = $"SomeUserName{i}",
                        Email = $"SomeEmail{i}",
                        FirstName = $"SomeFirstName{i}",
                        Password = $"SomePassword{i}",
                        State = "Belarus",
                        City = "Minsk",
                        HouseNumber = $"{i}",
                        LastName = $"SomeLastName{i}",
                        Address = "SomeAddress",
                        PhoneNumber = "+375(29)123-45-67"
                    };
                    await _userService.Registration(user, "User");
                }
            }
        }

        private async Task ProductCreate()
        {
            for (int i = 1; i <= 100; i++)
            {
                var product = new ProductCreateViewModel
                {
                    Name = $"SomeProductName{i}",
                    ProductInformation = new ProductInformationViewModel
                    {
                        ScreenSize = "15",
                        Memory = 128,
                        NumberOfCores = 2,
                        Ram = 4,
                        OperatingSystem = "Android"
                    },
                    Price = Rnd(100, 2000),
                    ProviderId = _applicationContext.Providers
                        .FirstOrDefault(w => string.Equals(w.Name, $"SomeProviderName{Rnd(1,11)}",
                            StringComparison.InvariantCultureIgnoreCase)).Id,
                    Description = $"SomeDescription By Product{i}",
                    UrlImage = "https://content2.onliner.by/catalog/device/main/febab87ed7324e7912223b66e425b72a.jpeg"
                };
                await _productService.Create(product);
            }
        }

        private async Task AssessmentCreate()
        {
            for (int i = 1; i <= 20; i++)
            {
                for (int j = 1; j <= 100; j++)
                {
                    await _assessmentService.Create(
                        _applicationContext.Users.FirstOrDefault(w => w.UserName == $"SomeUserName{i}").Id,
                        _applicationContext.Products.FirstOrDefault(w => w.Name == $"SomeProductName{j}").Id,
                        Convert.ToByte(Rnd(1, 6)));
                }
            }
        }

        private async Task CommentCreate()
        {
            for (int i = 1; i <= 20; i++)
            {
                for (int j = 1; j <= 100; j++)
                {
                    await _commentService.Create(
                        _applicationContext.Users.FirstOrDefault(w => w.UserName == $"SomeUserName{i}").Id,
                        _applicationContext.Products.FirstOrDefault(w => w.Name == $"SomeProductName{j}").Id,
                        $"SomeComment By UserName{i}");
                }
            }
        }

        private async Task OrderCreate()
        {
            for (int i = 1; i <= 20; i++)
            {
                await _orderService.Create(
                    _applicationContext.Users.FirstOrDefault(w => w.UserName == $"SomeUserName{i}").Id,
                    new[]
                    {
                        _applicationContext.Products
                            .FirstOrDefault(w => w.Name == $"SomeProductName{Rnd(1, 101)}").Id
                    });
            }
        }

        private async Task BasketCreate()
        {
            for (int i = 1; i <= 9; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    await _basketService.Add(
                        _applicationContext.Users.FirstOrDefault(w => w.UserName == $"SomeUserName{j + 1}").Id,
                        new[]
                        {
                            _applicationContext.Products
                                .FirstOrDefault(w => w.Name == $"SomeProductName{i}{j}").Id
                        });
                }
            }
        }

        private async Task AdminCreate()
        {
            using (_applicationContext)
            {
                var user = new RegistrationUserViewModel
                {
                    UserName = "Admin",
                    Email = "Admin",
                    FirstName = "Admin",
                    Password = "Admin",
                    State = "Admin",
                    City = "Admin",
                    HouseNumber = "6",
                    LastName = "Admin",
                    Address = "Admin",
                    PhoneNumber = "+666(66)666-66-66"
                };
                await _userService.Registration(user, "Admin");
            }
        }

        private int Rnd(int min, int max)
        {
            var rnd = new Random();
            return rnd.Next(min, max);
        }
    }
}