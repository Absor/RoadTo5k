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
     * Changes the computer screen state
     * 
     * ComputerScreenStateIdentifier: new state identifier
     **/
    public class ChangeComputerScreenStateSignal : Signal<ComputerScreenStateIdentifier> { }
    // Same but after it is done (accepted) - can be used to update view
    public class ComputerScreenStateChangedSignal : Signal<ComputerScreenStateIdentifier> { }

    /**
     * Start a new MOBA match.
     **/
    public class StartNewMatchSignal : Signal { }

    /**
     * Hero pick ready attempt signal.
     **/
    public class HeroPickReadySignal : Signal { }
}