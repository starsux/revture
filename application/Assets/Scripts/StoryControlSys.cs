using System;
using System.Collections.Generic;

[Serializable]
public class StoryControlSys
{
    public StoryDialogCollection Diag = new StoryDialogCollection();
    // Index of current point of history
    public int StoryIndex;

    public StoryControlSys()
    {
        // Here all dialogs
        Diag.Add("This is a test text dialog, this only show once in all game", StoryDialog.DialogImage.arantia);
        Diag.Add("The exit from the cave is very narrow, use the transmuter. (Press E)", StoryDialog.DialogImage.stenpek);
    }

}

[Serializable]
public class StoryDialogCollection
{
    public List<StoryDialog> dialogs = new();

    public StoryDialog this[int index]
    {
        get
        {
            return dialogs[index];
        }

        set
        {
            dialogs[index] = value;
        }
    }

    internal void Add(string Raw_text, StoryDialog.DialogImage Character)
    {
        dialogs.Add(new StoryDialog(Raw_text, Character));
    }
}


[Serializable]
public class StoryDialog
{
    public enum DialogImage
    {
        arantia,
        ren,
        pikun,
        stenpek
    }

    public bool Done { get { return _done; } set { _done = value; } }
    public bool _done = false;
    public DialogImage Character;
    private string Raw_text;

    public StoryDialog(string raw_text, DialogImage character)
    {
        Raw_text = raw_text;
        Character = character;
    }

    public override string ToString()
    {
        return Raw_text;
    }

}