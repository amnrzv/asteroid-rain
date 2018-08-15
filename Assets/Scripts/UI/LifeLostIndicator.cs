using UnityEngine;

[RequireComponent(typeof(Animator))]
public class LifeLostIndicator : MonoBehaviour
{
    Animator anim;

    private void Awake ( )
    {
        anim = GetComponent<Animator> ( );
    }

    private void OnEnable ( )
    {
        anim.Play ( "Idle" );
        EventsManager.LifeLostEvent += EventsManager_LifeLostEvent;
    }

    private void OnDisable ( )
    {
        EventsManager.LifeLostEvent -= EventsManager_LifeLostEvent;
    }

    private void EventsManager_LifeLostEvent ( )
    {
        anim.Play ( "Flash" );
    }
}
