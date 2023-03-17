using UnityEngine;

public class CanvasFaceCamera : MonoBehaviour
{
    private GameObject mainCamera;
    void Awake()
    {
        this.mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        if (mainCamera != null)
        {
            Debug.Log("Found the camera");
        }
    }

    void Update()
    {
        transform.LookAt(mainCamera.transform);
    }
}
