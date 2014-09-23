using UnityEngine;
using strange.extensions.context.impl;
using strange.extensions.command.api;
using strange.extensions.command.impl;

namespace Sesto.RoadTo5k
{
    // Base class for all the contexts
    public class SignalContext : MVCSContext
    {
        // Calls MVCSContext constructor with contextView
        public SignalContext(MonoBehaviour contextView)
            : base(contextView)
        {
        }

        protected override void addCoreComponents()
        {
            // Swap to use StrangeIOC signals instead of events
            base.addCoreComponents();
            injectionBinder.Unbind<ICommandBinder>();
            injectionBinder.Bind<ICommandBinder>().To<SignalCommandBinder>().ToSingleton();
        }

        public override void Launch()
        {
            base.Launch();
            // Launch of the context dispatches StartSignal
            StartSignal startSignal = (StartSignal)injectionBinder.GetInstance<StartSignal>();
            startSignal.Dispatch();
        }

        protected override void mapBindings()
        {
            base.mapBindings();
            // Scans for implicit bindings http://strangeioc.wordpress.com/2013/12/16/implicitly-delicious/
            implicitBinder.ScanForAnnotatedClasses(new string[] { "sesto.RoadTo5K" });
        }
    }
}