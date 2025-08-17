using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI gameOverText;
    [SerializeField] TextMeshProUGUI highScoreText;
    void Start()
    {
        gameOverText.text = "우리는 " + GameManager.Instance.currentDayIndex + "일 동안 생존했다.";
        if (GameManager.Instance.highScore > GameManager.Instance.maxDayIndex)
            highScoreText.text = "최고 기록: 게임 클리어";
        else
            highScoreText.text = "최고 기록: " + GameManager.Instance.highScore + "일";
    }
}
