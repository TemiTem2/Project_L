using System.Collections.Generic;
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
    private int rewardIndex = 0;

    private StoryData currentStoryData;
    private StoryLine currentParData;
    private ContentType contentType;

    private Sprite currentImage;
    private string currentStoryLine;
    private string currentName;

    public Dictionary<RewardType, int> reward = new();
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
        if (isEventEnd)
        {
            GameManager.Instance.ChangeState(GameState.Stat);
            return;
        }
        switch (contentType)
        {
            case ContentType.None:
                LoadNext();
                break;
            case ContentType.Choice:
                storyUIUpdator.EnableChoice(currentParData.choices[0].choiceText, currentParData.choices[1].choiceText);
                break;
            case ContentType.Reward:
                RewardCheck();
                break;
            case ContentType.End:
                EndCheck();
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

    private void AddReward()
    {
        if(reward.ContainsKey(currentParData.rewards[rewardIndex].rewardType))
        {
            reward[currentParData.rewards[rewardIndex].rewardType] += currentParData.rewards[rewardIndex].amount;
            foreach (KeyValuePair<RewardType, int> entry in reward)
            {
                Debug.Log($"Key: {entry.Key}, Value: {entry.Value}");
            }

        }
        else
        {
            reward.Add(currentParData.rewards[rewardIndex].rewardType, currentParData.rewards[rewardIndex].amount);
            foreach (KeyValuePair<RewardType, int> entry in reward)
            {
                Debug.Log($"Key: {entry.Key}, Value: {entry.Value}");
            }
        }
    }

    private void RewardCheck()
    {
        if (rewardIndex < currentParData.rewards.Length)
        {
            AddReward();
            storyUIUpdator.ShowRewardPanel(currentParData.rewards[rewardIndex].rewardType, currentParData.rewards[rewardIndex].amount);
            rewardIndex++;
        }
        else
        {
            storyUIUpdator.HideRewardPanel();
            LoadNext();
            rewardIndex = 0;
        }
    }

    private void EndCheck()
    {
        if (rewardIndex < currentParData.rewards.Length)
        {
            AddReward();
            storyUIUpdator.ShowRewardPanel(currentParData.rewards[rewardIndex].rewardType, currentParData.rewards[rewardIndex].amount);
            rewardIndex++;
        }
        else
        {
            storyUIUpdator.HideRewardPanel();
            isEventEnd = true;
        }
    }
}



