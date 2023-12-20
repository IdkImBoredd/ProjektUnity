using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;
    private float startZoom, targetZoom;
    public float zoomSpeed = 1f;
    public Camera cam;
    public Transform target;
    private void Awake()
    {
        instance = this; 
    }
    // Start is called before the first frame update
    void Start()
    {
        startZoom = cam.fieldOfView;
        targetZoom = startZoom;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = target.position;
        transform.rotation = target.rotation;

        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, targetZoom, zoomSpeed * Time.deltaTime);
    }
    public void ZoomIn(float newZoom)
    {
        targetZoom = newZoom;
    }

    public void ZoomOut()
    {
        targetZoom = startZoom;
    }
}
