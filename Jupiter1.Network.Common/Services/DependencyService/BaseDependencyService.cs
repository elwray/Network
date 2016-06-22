using System;
using SimpleInjector;

namespace Jupiter1.Network.Common.Services.DependencyService
{
    public abstract class BaseDependencyService : IDependencyService
    {
        private Container _container;

        #region IDependencyService
        public void Initialize<TConfiguration>(TConfiguration configuration, bool verifyContainer = true)
            where TConfiguration : class
        {
            if (configuration == null)
                throw new ArgumentNullException(nameof(configuration));
            if (_container != null)
                throw new InvalidOperationException();

            _container = new Container
            {
                Options = { AllowOverridingRegistrations = true }
            };

            RegisterServices(_container, configuration);

            if (verifyContainer)
                _container.Verify();
        }

        public void RegisterSingleton<TService, TImplementation>() where TService : class
            where TImplementation : class, TService
        {
            _container.RegisterSingleton<TService, TImplementation>();
        }

        public void RegisterSingleton<TService>(TService instance) where TService : class
        {
            _container.RegisterSingleton(instance);
        }

        public T GetSingleton<T>() where T : class
        {
            var registration = _container.GetRegistration(typeof(T));
            if (registration.Lifestyle != Lifestyle.Singleton)
                throw new InvalidOperationException();

            return _container.GetInstance<T>();
        }
        #endregion

        protected abstract void RegisterServices<TConfiguration>(Container container, TConfiguration configuration)
            where TConfiguration : class;
    }
}