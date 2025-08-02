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
    private GameObject panelChoiceTwo;
    [SerializeField]
    private TextMeshProUGUI textChoice0;
    [SerializeField]
    private TextMeshProUGUI textChoice1;

    [SerializeField]
    private StoryData[] storyDatas;
    private int currentStoryIndex = 0;
    private int currentParIndex = 0;
    private int currentLineIndex = 0;

    private int currentTime;
    private StoryData currentStoryData;
    private StoryLine currentParData;
    private string currentStoryLine;
    private ContentType contentType;

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
        if(isEventEnd)
        {

        }
    }

    public void LoadByContentType()
    {
        switch (contentType)
        {
            case ContentType.None:
                LoadNext();
                break;
            case ContentType.Choice:
                LoadButton();
                break;
            case ContentType.End:
                isEventEnd = true;
                break;
        }
    }


    private void LoadPar()
    {
        if(currentStoryData != null)
        {
            currentLineIndex = 0;
            currentParData = currentStoryData.pars[currentParIndex];
            contentType = currentParData.contentType;
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

    private void LoadButton()
    {
        panelChoiceTwo.SetActive(true);
        textChoice0.text = currentParData.choices[0].choiceText;
        textChoice1.text = currentParData.choices[1].choiceText;
    }

    public void LoadByButton(int buttonIndex)
    {
        currentParIndex = currentParData.choices[buttonIndex].jumpIndex;
        LoadPar();
        panelChoiceTwo.SetActive(false);
    }
}



