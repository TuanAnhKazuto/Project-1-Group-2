using UnityEngine;
using System.Collections;

public class CameraZoom : MonoBehaviour
{
    public Camera mainCamera;
    public float zoomFactor = 2f;   // Tỷ lệ zoom (2x là phóng to gấp đôi)
    public float zoomSpeed = 1f;    // Thời gian để zoom vào hoặc ra

    private float originalSize;
    private bool isZoomedIn = false;

    void Start()
    {
        if (mainCamera != null)
        {
            originalSize = mainCamera.orthographicSize;
        }
        else
        {
            Debug.LogError("Camera chưa được gán!");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ZoomTrigger") && !isZoomedIn)
        {
            StartCoroutine(ZoomIn());
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("ZoomTrigger") && isZoomedIn)
        {
            StartCoroutine(ZoomOut());
        }
    }

    private IEnumerator ZoomIn()
    {
        isZoomedIn = true;
        float startSize = mainCamera.orthographicSize;
        float targetSize = startSize / zoomFactor;
        float elapsedTime = 0f;

        while (elapsedTime < zoomSpeed)
        {
            mainCamera.orthographicSize = Mathf.Lerp(startSize, targetSize, elapsedTime / zoomSpeed);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        mainCamera.orthographicSize = targetSize;
    }

    private IEnumerator ZoomOut()
    {
        isZoomedIn = false;
        float startSize = mainCamera.orthographicSize;
        float targetSize = originalSize;
        float elapsedTime = 0f;

        while (elapsedTime < zoomSpeed)
        {
            mainCamera.orthographicSize = Mathf.Lerp(startSize, targetSize, elapsedTime / zoomSpeed);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        mainCamera.orthographicSize = targetSize;
    }
}
