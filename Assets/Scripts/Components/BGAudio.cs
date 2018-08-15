using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(AudioSource))]
public class BGAudio : MonoBehaviour
{
    private AudioSource audioSource;

    public AudioMixerSnapshot gameSnapshot;
    public AudioMixerSnapshot menuSnapshot;

    private void Awake ( )
    {
        audioSource = GetComponent<AudioSource> ( );
    }

    private void OnEnable ( )
    {
        EventsManager.GameStartEvent += EventsManager_GameStartedEvent;
        EventsManager.GameOverEvent += EventsManager_GameOverEvent;
    }

    private void OnDisable ( )
    {
        EventsManager.GameStartEvent -= EventsManager_GameStartedEvent;
        EventsManager.GameOverEvent -= EventsManager_GameOverEvent;
    }

    private void EventsManager_GameOverEvent ( )
    {
        menuSnapshot.TransitionTo ( 1f );
    }

    private void EventsManager_GameStartedEvent ( )
    {
        gameSnapshot.TransitionTo ( 0.3f );
        audioSource.Play ( );
    }
}
