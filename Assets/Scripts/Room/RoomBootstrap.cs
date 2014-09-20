using UnityEngine;
using System.Collections;
using strange.extensions.context.impl;

namespace Sesto.RoadTo5k
{
    // Starts context, need to be in the scene base GameObject
    public class RoomBootstrap : ContextView
    {

        // Initializes game context
        void Start()
        {
            context = new RoomContext(this);
        }
    }
}