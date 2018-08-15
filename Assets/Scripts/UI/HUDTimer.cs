using UnityEngine;
using UnityEngine.UI;

public class HUDTimer : MonoBehaviour
{
    Image timerImage;
    bool isPlaying;

    private void OnEnable ( )
    {
        timerImage = GetComponent<Image> ( );
        EventsManager.GameStartEvent += GameManager_GameStartedEvent;
        EventsManager.GameOverEvent += EventsManager_GameOverEvent;
    }

    private void OnDisable ( )
    {
        EventsManager.GameStartEvent -= GameManager_GameStartedEvent;
        EventsManager.GameOverEvent -= EventsManager_GameOverEvent;
    }

    private void GameManager_GameStartedEvent ( )
    {
        isPlaying = true;
        timerImage.fillAmount = 1;
    }

    private void EventsManager_GameOverEvent ( )
    {
        isPlaying = false;
    }

    private void Update ( )
    {
        if ( isPlaying )
            timerImage.fillAmount -= Time.deltaTime / GameManager.GAME_TIME;
    }
}
