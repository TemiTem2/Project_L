using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StoryManager : MonoBehaviour
{
    private InputManager inputManager;

    [SerializeField]
    private TextMeshProUGUI nameText;
    [SerializeField]
    private TextMeshProUGUI storyText;
    [SerializeField]
    private Button buttonOne;
    [SerializeField]
    private Button buttonTwo;

    [SerializeField]
    private StoryData[] storyDatas;
    private int currentStoryIndex = 0;
    private int currentParIndex = 0;
    private int currentLineIndex = 0;

    private int currentTime;
    private StoryData currentStoryData;
    private StoryLine currentParData;
    private string currentStoryLine;

    public bool isEventEnd = false;

    private void Start()
    {
        inputManager = FindFirstObjectByType<InputManager>();

        currentStoryIndex = RandomSelector();
        currentStoryData = storyDatas[currentStoryIndex];
        LoadPar();
    }

    private void Update()
    {
        if(!isEventEnd && inputManager.isClicked)
        {
            LoadNext();
        }
    }

    //private void DictStory()
    //{

    //}


    private void LoadPar()
    {
        if(currentStoryData != null)
        {
            currentParData = currentStoryData.pars[currentParIndex];
            LoadLine();
        }
    }

    private void LoadLine()
    {
        if (currentParData != null)
        {
            currentStoryLine = currentParData.contents[currentLineIndex];
            nameText.text = currentParData.speakerName;
            storyText.text = currentStoryLine;
        }
    }

    private void LoadNext()
    {
        currentLineIndex++;
        if (currentLineIndex >= currentParData.contents.Length)
        {
            currentLineIndex = 0;
            currentParIndex++;
            if (currentParIndex >= currentStoryData.pars.Length)
            {
                isEventEnd = true;
                return;
            }
            LoadPar();
        }
        else
        {
            LoadLine();
        }
    }

    private int RandomSelector()
    {
        int index = storyDatas.Length;
        return Random.Range(0, index);
    }
}
