using UnityEngine;

namespace Sesto.RoadTo5k
{
    public class ComputerClick : MonoBehaviour
    {

        public CameraMoveView cameraMoveView;

        void OnMouseUp()
        {
            cameraMoveView.MoveCamera();
        }
    }
}

