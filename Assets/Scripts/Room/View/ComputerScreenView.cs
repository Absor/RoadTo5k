using strange.extensions.mediation.impl;
using strange.extensions.signal.impl;
using UnityEngine;

namespace Sesto.RoadTo5k
{
    /**
     * Controls computer screen activity.
     * Shows the right stuff on screen by state.
     **/
    public class ComputerScreenView : View
    {
        public GameObject lobbyScreen;
        public GameObject heroPickScreen;
        public GameObject gameScreen;
        public GameObject gameEndScreen;

        public void ActivateState(ComputerScreenStateIdentifier active)
        {
            lobbyScreen.SetActive(active == ComputerScreenStateIdentifier.LOBBY);
            heroPickScreen.SetActive(active == ComputerScreenStateIdentifier.HERO_PICK);
            gameScreen.SetActive(active == ComputerScreenStateIdentifier.GAME);
            gameEndScreen.SetActive(active == ComputerScreenStateIdentifier.GAME_END);
        }
    }
}
