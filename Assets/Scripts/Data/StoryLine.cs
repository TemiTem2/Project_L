using UnityEngine;

public enum ContentType
{
    None,
    Choice,
    End
}
[System.Serializable]
public class StoryLine
{
    public ContentType contentType = ContentType.None;
    public string speakerName;
    public Sprite speakerImage;

    [TextArea(2, 20)]
    public string[] contents;

    public ChoiceData[] choices;
}
