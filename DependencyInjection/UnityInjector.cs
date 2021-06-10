using BussinessLogic;
using StockMarket;
using StockMarket.Interface;
using Unity;

namespace DependencyInjection
{
    public class UnityInjector
    {
        private static readonly UnityContainer Ic = new UnityContainer();
        public UnityInjector()
        {
            RegisterTypes();
        }

        private void RegisterTypes()
        {
            Ic.RegisterType<ILoginService, LoginService>();
        }
    }
}
