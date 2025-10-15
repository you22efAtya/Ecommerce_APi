using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Shared.Dtos;

namespace Services
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<IProductService> _productService;
        private readonly Lazy<IBasketService> _basketService;
        private readonly Lazy<IAuthenticationService> _authenticationService;
        private readonly Lazy<IOrderService> _orderService;
        private readonly Lazy<IPaymentService> _paymentService;
        private readonly Lazy<ICacheService> _cacheService;




        public ServiceManager(IUnitOfWork unitOfWork, IMapper mapper,IBasketRepository basketRepository,ICacheRepository cacheRepository, UserManager<User> userManager,IOptions<JwtOptions> options,IConfiguration configuration)
        {
            _productService = new Lazy<IProductService>(() => new ProductService(unitOfWork, mapper));
            _basketService = new Lazy<IBasketService>(() => new BasketService(basketRepository, mapper));
            _authenticationService = new Lazy<IAuthenticationService>(() => new AuthenticationService(userManager, options,mapper));
            _orderService = new Lazy<IOrderService>(() => new OrderService(mapper, basketRepository, unitOfWork));
            _paymentService = new Lazy<IPaymentService>(() => new PaymentService(basketRepository, unitOfWork, mapper, configuration));
            _cacheService = new Lazy<ICacheService>(() => new CacheService(cacheRepository));
        }

        public IProductService ProductService => _productService.Value;

        public IBasketService BasketService => _basketService.Value;

        public IAuthenticationService AuthenticationService => _authenticationService.Value;

        public IOrderService OrderService => _orderService.Value;

        public IPaymentService paymentService => _paymentService.Value;

        public ICacheService cacheService => _cacheService.Value;
    }
}
