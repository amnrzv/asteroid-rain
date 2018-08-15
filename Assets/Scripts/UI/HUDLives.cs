using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class HUDLives : MonoBehaviour
{
    private Text textbox;

    private void Awake ( )
    {
        textbox = GetComponent<Text> ( );
    }

    private void OnEnable ( )
    {
        EventsManager.LifeLostEvent += UpdateLives;
        EventsManager.GameStartEvent += UpdateLives;
    }

    private void OnDisable ( )
    {
        EventsManager.LifeLostEvent -= UpdateLives;
        EventsManager.GameStartEvent -= UpdateLives;
    }

    private void UpdateLives ( )
    {
        textbox.text = string.Format ( "Lives: {0}", GameManager.Lives );
    }
}
