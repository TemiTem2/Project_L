using UnityEngine;

[CreateAssetMenu(fileName = "StoryData", menuName = "Data/StoryData")]
public class StoryData : ScriptableObject
{
    public string storyName;
    public StoryLine[] pars;
}
