﻿using strange.extensions.command.impl;

namespace Sesto.RoadTo5k
{
    public class MoveCameraCommand : Command
    {
        [Inject]
        public CameraIdentifier newCameraIdentifier { get; set; }

        [Inject]
        public float transitionDuration { get; set; }

        [Inject]
        public IGameModel gameModel { get; set; }

        [Inject]
        public CameraMovedSignal cameraMovedSignal { get; set; }

        public override void Execute()
        {
            if (gameModel.currentCamera != newCameraIdentifier)
            {
                gameModel.currentCamera = newCameraIdentifier;
                cameraMovedSignal.Dispatch(newCameraIdentifier, transitionDuration);
            }
        }
    }
}
