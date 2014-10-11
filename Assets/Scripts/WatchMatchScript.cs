using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class WatchMatchScript : MonoBehaviour, IPointerClickHandler {
    public void OnPointerClick(PointerEventData eventData)
    {
        GameEvent watchEvent = RandomGeneratorScript.Instance.GetWatchEvent();
        GameEventManagerScript.Instance.ResolveGameEvent(watchEvent);
    }
}
