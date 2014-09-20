using strange.extensions.mediation.impl;
using strange.extensions.signal.impl;
using UnityEngine;

namespace Sesto.RoadTo5k
{
    public class ComputerGameView : View
    {
        public GameObject lobbyScreen;
        public GameObject heroPickScreen;
        public GameObject gameScreen;
        public GameObject gameEndScreen;

        public void ActivateState(ComputerGameStateIdentifier active)
        {
            lobbyScreen.SetActive(active == ComputerGameStateIdentifier.LOBBY);
            heroPickScreen.SetActive(active == ComputerGameStateIdentifier.HERO_PICK);
            gameScreen.SetActive(active == ComputerGameStateIdentifier.GAME);
            gameEndScreen.SetActive(active == ComputerGameStateIdentifier.GAME_END);
        }
    }
}
