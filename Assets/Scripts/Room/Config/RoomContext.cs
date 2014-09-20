using strange.extensions.context.impl;
using UnityEngine;

namespace Sesto.RoadTo5k
{
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
            commandBinder.Bind<ChangeComputerGameStateSignal>().To<ChangeComputerGameStateCommand>();

            // Signals not bound to commands need to be mapped for injection
            injectionBinder.Bind<CameraMovedSignal>().ToSingleton();
            injectionBinder.Bind<ComputerGameStateChangedSignal>().ToSingleton();

            // Bind mediators (between signals/commands and views) to views
            mediationBinder.Bind<CameraView>().To<CameraMediator>();
            mediationBinder.Bind<CameraMoveView>().To<CameraMoveMediator>();
            mediationBinder.Bind<ComputerGameView>().To<ComputerGameMediator>();
            mediationBinder.Bind<ComputerGameStateChangeView>().To<ComputerGameStateChangeMediator>();
        }
    }
}