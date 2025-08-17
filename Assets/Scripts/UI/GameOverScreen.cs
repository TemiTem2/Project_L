using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI gameOverText;
    [SerializeField] TextMeshProUGUI highScoreText;
    void Start()
    {
        gameOverText.text = "�츮�� " + GameManager.Instance.currentDayIndex + "�� ���� �����ߴ�.";
        if (GameManager.Instance.highScore > GameManager.Instance.maxDayIndex)
            highScoreText.text = "�ְ� ���: ���� Ŭ����";
        else
            highScoreText.text = "�ְ� ���: " + GameManager.Instance.highScore + "��";
    }
}
