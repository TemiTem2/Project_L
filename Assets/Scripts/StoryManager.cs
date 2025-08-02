using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StoryManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI nameText;

    [SerializeField]
    private StoryData[] storyDatas;
    private int currentStoryIndex = 0;

    private int currentTime;
}
