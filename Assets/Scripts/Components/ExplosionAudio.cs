using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class ExplosionAudio : MonoBehaviour
{
    AudioSource audioSource;

    private void Awake ( )
    {
        audioSource = GetComponent<AudioSource> ( );
    }

    private void OnEnable ( )
    {
        EventsManager.AsteroidDestroyedEvent += EventsManager_AsteroidDestroyedEvent;
        EventsManager.SuperAsteroidSplitEvent += EventsManager_AsteroidDestroyedEvent;
    }

    private void OnDisable ( )
    {
        EventsManager.AsteroidDestroyedEvent -= EventsManager_AsteroidDestroyedEvent;
        EventsManager.SuperAsteroidSplitEvent -= EventsManager_AsteroidDestroyedEvent;
    }

    private void EventsManager_AsteroidDestroyedEvent ( )
    {
        audioSource.Play ( );
    }
}
