using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {

    public Transform startPosition;

    void Start()
    {
        MoveCamera(startPosition, 0);
    }

    public void MoveCamera(Transform newPosition, float transitionDuration)
    {
        if (newPosition == cameraPosition)
        {
            return;
        }
        StopCoroutine("TransitionCamera");
        this.transitionDuration = transitionDuration;
        this.cameraPosition = newPosition;
        StartCoroutine("TransitionCamera");
    }

    // Transition handler
    private Transform cameraPosition;
    private float transitionDuration;

    IEnumerator TransitionCamera()
    {
        float t = 0.0f;
        Vector3 startingPos = transform.position;
        Quaternion startingRot = transform.rotation;
        while (t < 1.0f)
        {
            t += Time.deltaTime * (Time.timeScale / transitionDuration);

            transform.position = Vector3.Lerp(startingPos, cameraPosition.position, t);
            transform.rotation = Quaternion.Lerp(startingRot, cameraPosition.rotation, t);
            yield return 0;
        }
    }
}