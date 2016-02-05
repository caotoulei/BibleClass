using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Practices.ServiceLocation;
using StructureMap;

namespace SS.Web.DependencyResolution
{
    /// <summary>
    /// The structure map dependency scope.
    /// </summary>
    public class StructureMapDependencyScope : ServiceLocatorImplBase
    {
        private readonly INestedContainerScope _nestedContainerScope;

        /// <summary>
        /// Initializes the scope using a <see cref="TransientNestedContainerScope"/> container)
        /// </summary>
        public StructureMapDependencyScope(IContainer container)
            : this(container, new TransientNestedContainerScope())
        {

        }

        public StructureMapDependencyScope(IContainer container, INestedContainerScope nestedContainerScope)
        {
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }
            Container = container;
            _nestedContainerScope = nestedContainerScope;
        }


        public IContainer Container { get; set; }

        public IContainer CurrentNestedContainer
        {
            get { return _nestedContainerScope.NestedContainer; }
            set { _nestedContainerScope.NestedContainer = value; }
        }

        public void CreateNestedContainer()
        {
            if (CurrentNestedContainer != null)
            {
                return;
            }

            CurrentNestedContainer = Container.GetNestedContainer();
        }

        public void Dispose()
        {
            DisposeNestedContainer();
        }

        public void DisposeNestedContainer()
        {
            if (CurrentNestedContainer != null)
            {
                CurrentNestedContainer.Dispose();
                CurrentNestedContainer = null;
            }
        }

        public void DisposeParentContainer()
        {
            if (Container != null)
            {
                Container.Dispose();
                Container = null;
            }
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return DoGetAllInstances(serviceType);
        }

        protected override IEnumerable<object> DoGetAllInstances(Type serviceType)
        {
            return (CurrentNestedContainer ?? Container).GetAllInstances(serviceType).Cast<object>();
        }

        protected override object DoGetInstance(Type serviceType, string key)
        {
            IContainer container = (CurrentNestedContainer ?? Container);

            if (string.IsNullOrEmpty(key))
            {
                return serviceType.IsAbstract || serviceType.IsInterface
                    ? container.TryGetInstance(serviceType)
                    : container.GetInstance(serviceType);
            }

            return container.GetInstance(serviceType, key);
        }

        public object TryGetInstance(Type type)
        {
            IContainer container = (CurrentNestedContainer ?? Container);
            return container.TryGetInstance(type);
        }
    }

    public interface INestedContainerScope
    {
        IContainer NestedContainer { get; set; }
    }

    public class TransientNestedContainerScope : INestedContainerScope
    {
        public IContainer NestedContainer { get; set; }
    }
}