using UnityEngine;

public class StoryManager : MonoBehaviour
{
    [SerializeField]
    private StoryUIUpdator storyUIUpdator;

    [SerializeField]
    private StoryData[] storyDatas;

    private int currentStoryIndex = 0;
    private int currentParIndex = 0;
    private int currentLineIndex = 0;

    private StoryData currentStoryData;
    private StoryLine currentParData;
    private ContentType contentType;

    private Sprite currentImage;
    private string currentStoryLine;
    private string currentName;

    public bool isEventEnd = false; //랜덤이벤트 종료

    private void Start()
    {
        currentStoryIndex = RandomSelector();
        currentStoryData = storyDatas[currentStoryIndex];
        LoadPar();
    }



    private void LoadPar()
    {
        currentLineIndex = 0;
        currentParData = currentStoryData.pars[currentParIndex];
        contentType = currentParData.contentType;
        currentImage = currentParData.speakerImage;
        LoadLine();
    }

    private void LoadLine()
    {
        currentStoryLine = currentParData.contents[currentLineIndex];
        currentName = currentParData.speakerName;
        storyUIUpdator.UpdateUI(currentName, currentStoryLine, currentImage);
    }

    public void LoadByContentType()
    {
        switch (contentType)
        {
            case ContentType.None:
                LoadNext();
                break;
            case ContentType.Choice:
                storyUIUpdator.EnableChoice(currentParData.choices[0].choiceText, currentParData.choices[1].choiceText);
                break;
            case ContentType.End:
                isEventEnd = true;
                break;
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

    public void LoadByButton(int buttonIndex)
    {
        currentParIndex = currentParData.choices[buttonIndex].jumpIndex;
        LoadPar();
        storyUIUpdator.DisableChoice();
    }
}



