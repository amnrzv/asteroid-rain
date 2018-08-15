using System;

public class EventsManager
{
    public static event Action AsteroidDestroyedEvent;
    public static event Action SuperAsteroidSplitEvent;
    public static event Action GameStartEvent;
    public static event Action GameOverEvent;
    public static event Action LifeLostEvent;

    public static void AsteroidDestroyed ( )
    {
        if ( AsteroidDestroyedEvent != null )
            AsteroidDestroyedEvent ( );
    }

    public static void SuperAsteroidSplit ( )
    {
        if ( SuperAsteroidSplitEvent != null)
            SuperAsteroidSplitEvent ( );
    }

    public static void StartGame ( )
    {
        if ( GameStartEvent != null )
            GameStartEvent ( );
    }

    public static void GameOver()
    {
        if ( GameOverEvent != null )
            GameOverEvent ( );
    }

    public static void LifeLost()
    {
        if ( LifeLostEvent != null )
            LifeLostEvent ( );
    }
}
