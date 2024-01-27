using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;



// ID von Person oder Quest -> Dialogfetzen
// Sprung zwischen Fetzen über Taste/Knopf
// (Noch) keine Antwortmöglichkeiten

public class DialogConfig : ScriptableObject
{
    [Serializable]
    private class DialogMapping
    {
        // Quest-Namen/Id
        // Icon/Sprecher/Namen
        // Text

        public int DialogID;
        public int DialogCounter = -1;
        //public List<int> finishDialogIndexes;
        [TextArea] public List<string> DialogTexts;

        public int GetNumberOfDialogs()
        {
            return DialogTexts.Count;
        }
        public string GetDialogText()
        {
            string dialog = "";
            if (DialogCounter < GetNumberOfDialogs())
            {
                dialog = DialogTexts[DialogCounter];
            }
            else if(DialogCounter >= GetNumberOfDialogs())
            {
                dialog = DialogTexts[GetNumberOfDialogs() - 1];
            }
            return dialog;
        }
        public bool CheckIfDialogEnds()
        {
            return (DialogCounter + 1 >= GetNumberOfDialogs());
        }
    }

    [SerializeField] private List<DialogMapping> _dialogData;

    public int GetNumberOfDialogs(int dialogID)
    {
        var currentDialogMapping = GetDialogMappingByDialogID(dialogID);
        return currentDialogMapping.GetNumberOfDialogs();
    }

    public string GetNextDialog(int dialogID) 
    {
        var currentDialogMapping = GetDialogMappingByDialogID(dialogID);
        currentDialogMapping.DialogCounter += 1;
        return currentDialogMapping.GetDialogText();
    }

    private DialogMapping GetDialogMappingByDialogID(int dialogID)
    {
        return _dialogData.First(mapping => mapping.DialogID == dialogID);
    }

    public bool CheckIfDialogEnds(int dialogID)
    {
        var currentDialogMapping = GetDialogMappingByDialogID(dialogID);
        return currentDialogMapping.CheckIfDialogEnds();
    }

    public void ResetDialogs()
    {
        foreach (DialogMapping dialogMapping in _dialogData)
        {
            dialogMapping.DialogCounter = -1;
        }
    }
}