using UnityEngine;
using System.Collections;

public class WallClockScript : MonoBehaviour {

    private GameObject pointerSeconds;
    private GameObject pointerMinutes;
    private GameObject pointerHours;
    
    void Awake()
    {
        pointerSeconds = transform.Find("rotation_axis_pointer_seconds").gameObject;
        pointerMinutes = transform.Find("rotation_axis_pointer_minutes").gameObject;
        pointerHours = transform.Find("rotation_axis_pointer_hour").gameObject;
    }

    public void UpdateTime(int hour, int minute)
    {
        // TODO if we want also to control seconds
        int second = 0;

        // Calculate pointer angles
        float rotationSeconds = (360.0f / 60.0f) * second;
        float rotationMinutes = (360.0f / 60.0f) * minute;
        float rotationHours = ((360.0f / 12.0f) * hour) + ((360.0f / (60.0f * 12.0f)) * minute);

        // Rotate pointers
        pointerSeconds.transform.localEulerAngles = new Vector3(0.0f, 0.0f, rotationSeconds);
        pointerMinutes.transform.localEulerAngles = new Vector3(0.0f, 0.0f, rotationMinutes);
        pointerHours.transform.localEulerAngles = new Vector3(0.0f, 0.0f, rotationHours);
    }
}
