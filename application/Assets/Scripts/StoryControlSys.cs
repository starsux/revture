using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class StoryControlSys
{
    public StoryDialogCollection Diag { get; internal set; }
    public StoryDialogCollection _Diag;
    // Index of current point of history
    public int StoryIndex { get; internal set; }
    public int _StoryIndex { get; internal set; }

}

[Serializable]
public class StoryDialogCollection
{
    private List<StoryDialog> dialogs;

    public StoryDialogCollection() 
    {
        dialogs = new List<StoryDialog>();
    }

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

    public bool Done { get; internal set; }
    public bool _Done;
    public DialogImage Character { get; internal set; }
    public DialogImage _Character;
    private string Raw_text;

    public override string ToString()
    {
        return Raw_text;
    }

}