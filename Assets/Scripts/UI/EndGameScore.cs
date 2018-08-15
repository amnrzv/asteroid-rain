using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class EndGameScore : MonoBehaviour
{
    private Text textBox;

    private void Awake( )
    {
        textBox = GetComponent<Text> ( );
    }

    private void OnEnable ( )
    {
        textBox.text = string.Format ( "SCORE: {0}", GameManager.Score );
    }
}
