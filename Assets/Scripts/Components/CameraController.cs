using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraController : MonoBehaviour
{
    private Camera cam;
    private float shakeStartTime;
    private Vector3 startPosition;
    private float shakeDuration = 0.2f;

    
    private void Awake ( )
    {
        cam = GetComponent<Camera> ( );
        startPosition = transform.position;
    }

    private void OnEnable ( )
    {
        EventsManager.AsteroidDestroyedEvent += CameraShake;
        EventsManager.SuperAsteroidSplitEvent += CameraShake;
    }

    private void OnDisable ( )
    {
        EventsManager.AsteroidDestroyedEvent -= CameraShake;
        EventsManager.SuperAsteroidSplitEvent -= CameraShake;
    }

    private void CameraShake ( )
    {
        shakeStartTime = Time.time;
        StopCoroutine ( Shake ( ) );
        StartCoroutine ( Shake ( ) );
    }

    private IEnumerator Shake()
    {
        while (Time.time - shakeStartTime < shakeDuration)
        {
            transform.position = startPosition + Random.insideUnitSphere / 10;
            yield return new WaitForEndOfFrame ( );
        }

        yield break;
    }
}
