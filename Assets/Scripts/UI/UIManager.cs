using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    List<UIScreen> screens = new List<UIScreen>();
    public UIScreenType startScreen;

    private void Awake()
    {
        GetComponentsInChildren( true, screens);
        ShowScreen ( startScreen );
    }

    private void OnEnable ( )
    {
        EventsManager.GameOverEvent += EventsManager_GameOverEvent;
    }

    private void OnDisable ( )
    {
        EventsManager.GameOverEvent -= EventsManager_GameOverEvent;
    }

    private void EventsManager_GameOverEvent ( )
    {
        ShowScreen ( UIScreenType.GameOver );
    }

    void ShowScreen(UIScreenType type)
    {
        for ( int i = 0 ; i < screens.Count ; i++ )
        {
            screens [ i ].gameObject.SetActive ( screens [ i ].type == type );
        }
    }

    public void StartGame()
    {
        ShowScreen ( UIScreenType.GamePlay );
        EventsManager.StartGame ( );
    }
}
