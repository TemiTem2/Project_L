using UnityEngine;

[System.Serializable]
public class StoryLine
{
    public string speakerName;

    [TextArea(2, 20)]
    public string[] contents;
}
