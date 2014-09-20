using strange.extensions.signal.impl;
using UnityEngine;

namespace Sesto.RoadTo5k
{
    // All the signals of the Room context
    // Beginning of everything (scene loading and so on)
    public class StartSignal : Signal { }

    /**
     * Moves camera to a new position
     * 
     * CameraIdentifier: position identifier
     * float: transition time
     **/
    public class MoveCameraSignal : Signal<CameraIdentifier, float> { }
    // Same but after it is done (accepted) - can be used to update view
    public class CameraMovedSignal : Signal<CameraIdentifier, float> { }

    /**
     * Changes the computer game state
     * 
     * ComputerGameStateIdentifier: new state identifier
     **/
    public class ChangeComputerGameStateSignal : Signal<ComputerGameStateIdentifier> { }
    // Same but after it is done (accepted) - can be used to update view
    public class ComputerGameStateChangedSignal : Signal<ComputerGameStateIdentifier> { }
}