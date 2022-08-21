[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(FlightBooker.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(FlightBooker.App_Start.NinjectWebCommon), "Stop")]

namespace FlightBooker.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using Ninject.Web.Common.WebHost;

    using FlightBooker.DAL;
    using FlightBooker.Buisness;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application.
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }

        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }

        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();
                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<FlightRepository>().To<FlightRepository>();
            kernel.Bind<FlightReservationRepository>().To<FlightReservationRepository>();
            kernel.Bind<CustomerRepository>().To<CustomerRepository>();
            kernel.Bind<ReservationRepository>().To<ReservationRepository>();
            kernel.Bind<SearchRepository>().To<SearchRepository>();
            kernel.Bind<PaymentRepository>().To<PaymentRepository>();
            kernel.Bind<SortRepository>().To<SortRepository>();
            kernel.Bind<PassengerRepository>().To<PassengerRepository>();
            kernel.Bind<PassengerReservationRepository>().To<PassengerReservationRepository>();
            kernel.Bind<ReservationHistoryRepository>().To<ReservationHistoryRepository>();
            kernel.Bind<ItineraryRepository>().To<ItineraryRepository>();
        }
    }
}