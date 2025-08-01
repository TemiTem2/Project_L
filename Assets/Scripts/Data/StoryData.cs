using UnityEngine;

[CreateAssetMenu(fileName = "StoryData", menuName = "Data/StoryData")]
public class StoryData : ScriptableObject
{
    [TextArea(2, 20)]
    public string[] texts;
}
