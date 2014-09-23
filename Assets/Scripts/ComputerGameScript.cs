using UnityEngine;
using System.Collections;

public class ComputerGameScript : MonoBehaviour {

    public GameObject lobby;
    public GameObject match;

    void Awake()
    {
        lobby.SetActive(true);
        match.SetActive(false);
    }

    public void PlayMatch()
    {
        lobby.SetActive(false);
        match.SetActive(true);
    }
}
