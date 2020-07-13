using System;
using System.Collections.Generic;

namespace CodeAnalyzer.Services {
    public class ServiceContainer {
        static ServiceContainer defaultContainer;
        Dictionary<Type,object> defaultServiceFactories;
        public static ServiceContainer Default {
            get {
                if(defaultContainer == null)
                    defaultContainer = new ServiceContainer();
                return defaultContainer;
            }
            set {
                defaultContainer = value;
            }
        }
        Dictionary<Type, object> DefaultServiceFactories {
            get {
                if(defaultServiceFactories == null)
                    PopulateDefaultServices();
                return defaultServiceFactories;
            }
        }
        protected virtual void PopulateDefaultServices() {
            defaultServiceFactories = new Dictionary<Type, object>();
            defaultServiceFactories.Add(typeof(IContextService), new GdiContextServiceFactory());
        }
        public virtual IContextService GetContextService(string type) {
            var factory = DefaultServiceFactories[typeof(IContextService)] as IServiceFactory<IContextService>;
            return factory?.CreateService(type);
        }
    }
}