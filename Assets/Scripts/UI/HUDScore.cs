using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class HUDScore : MonoBehaviour
{
    private Text textbox;

    private void Awake ( )
    {
        textbox = GetComponent<Text> ( );
    }

    private void OnEnable ( )
    {
        EventsManager.AsteroidDestroyedEvent += UpdateScore;
        EventsManager.GameStartEvent += UpdateScore;
    }

    private void OnDisable ( )
    {
        EventsManager.AsteroidDestroyedEvent -= UpdateScore;
        EventsManager.GameStartEvent -= UpdateScore;
    }

    private void UpdateScore ( )
    {
        textbox.text = string.Format ( "Score: {0}", GameManager.Score );
    }
}
