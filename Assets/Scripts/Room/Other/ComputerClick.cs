using UnityEngine;

namespace Sesto.RoadTo5k
{
    // TODO Will probably be a real view, handles mouse clicks on computer.
    public class ComputerClick : MonoBehaviour
    {

        public CameraMoveView cameraMoveView;

        void OnMouseUp()
        {
            cameraMoveView.MoveCamera();
        }
    }
}

