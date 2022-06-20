// <copyright file="DependencyInjectionServiceFactory.cs" company="Retail Insight Ltd">
// Copyright (c) Retail Insight Ltd. All rights reserved.
// </copyright>

using Microsoft.Extensions.DependencyInjection;

namespace ILock.Core.Services
{
    /// <summary>
    /// The auth service factory.
    /// </summary>
    public class DependencyInjectionServiceFactory
    {
        private static DependencyInjectionServiceFactory serviceFactory;
        private static object lockObject = new object();
        private readonly IServiceProvider serviceProvider;
        private bool isInitialized = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="DependencyInjectionServiceFactory"/> class.
        /// </summary>
        /// <param name="serviceProvider">The service provider.</param>
        public DependencyInjectionServiceFactory(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        /// <summary>
        /// Prevents a default instance of the <see cref="DependencyInjectionServiceFactory"/> class from being created.
        /// </summary>
        private DependencyInjectionServiceFactory()
        {
        }

        /// <summary>
        /// Gets the default object of AuthServiceFactory
        /// </summary>
        public static DependencyInjectionServiceFactory Default
        {
            get
            {
                lock (lockObject)
                {
                    if (serviceFactory == null)
                    {
                        throw new InvalidOperationException("You are accessing Default from different thread than its initialized.");
                    }

                    return serviceFactory;
                }
            }
        }

        /// <summary>
        /// Inits the AuthServiceFactory
        /// </summary>
        /// <param name="serviceProvider">The service provider.</param>
        public static void Init(IServiceProvider serviceProvider)
        {
            lock (lockObject)
            {
                if (serviceFactory == null || !serviceFactory.isInitialized)
                {
                    serviceFactory = new DependencyInjectionServiceFactory(serviceProvider);
                    serviceFactory.isInitialized = true;
                }
            }
        }

        /// <summary>
        /// Gets the service with generic type TService
        /// </summary>
        /// <returns>A TService.</returns>
        public TService GetService<TService>() => this.isInitialized ?
            this.serviceProvider.GetService<TService>() :
            throw new InvalidOperationException("Initilized not called");
    }
}
