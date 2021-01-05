using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public Text text;

    public void addScore(float score)
    {
        text.text = "Score: " + score.ToString();
    }
}
