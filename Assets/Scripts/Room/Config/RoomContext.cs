using strange.extensions.context.impl;
using UnityEngine;

namespace Sesto.RoadTo5k
{
    /**
     * Sets up everything for dependency injection, configures mediators
     * (communication between controller and Unity views) and sets up signals
     * (communication between all components) and commands (controllers in MVC).
     * 
     * Example: https://github.com/strangeioc/strangerocks/blob/master/StrangeRocks/Assets/scripts/strangerocks/game/config/GameContext.cs
     **/
    public class RoomContext : SignalContext
    {

        public RoomContext(MonoBehaviour contextView)
            : base(contextView)
		{
		}

        protected override void mapBindings()
        {
            base.mapBindings();

            // Models
            injectionBinder.Bind<IGameModel>().To<GameModel>().ToSingleton();

            // Signals bound to commands
            commandBinder.Bind<StartSignal>().To<StartGameCommand>().Once();
            commandBinder.Bind<MoveCameraSignal>().To<MoveCameraCommand>();
            commandBinder.Bind<ChangeComputerScreenStateSignal>().To<ChangeComputerScreenStateCommand>();
            commandBinder.Bind<StartNewMatchSignal>().To<StartNewMatchCommand>();
            commandBinder.Bind<HeroPickReadySignal>().To<HeroPickReadyCommand>();

            // Signals not bound to commands need to be mapped for injection
            injectionBinder.Bind<CameraMovedSignal>().ToSingleton();
            injectionBinder.Bind<ComputerScreenStateChangedSignal>().ToSingleton();

            // Bind mediators (between signals/commands and views) to views
            mediationBinder.Bind<CameraView>().To<CameraMediator>();
            mediationBinder.Bind<CameraMoveView>().To<CameraMoveMediator>();
            mediationBinder.Bind<ComputerScreenView>().To<ComputerScreenMediator>();
            mediationBinder.Bind<ComputerScreenStateChangeView>().To<ComputerScreenStateChangeMediator>();
        }
    }
}