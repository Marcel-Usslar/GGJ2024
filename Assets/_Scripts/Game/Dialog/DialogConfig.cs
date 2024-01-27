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
        public List<int> finishDialogIndexes;
        [TextArea] public List<string> DialogTexts;
    }

    [SerializeField] private List<DialogMapping> _dialogData;

    public int GetNumberOfDialogs(int dialogID)
    {
        var currentDialogMapping = _dialogData.First(mapping => mapping.DialogID == dialogID);
        return currentDialogMapping.DialogTexts.Count;
    }

    public string GetNextDialog(int dialogID){
        string dialog = "";
        var currentDialogMapping = GetDialogMappingByDialogID(dialogID);
        currentDialogMapping.DialogCounter += 1;
        if (currentDialogMapping.DialogCounter < GetNumberOfDialogs(dialogID))
        {
            dialog = currentDialogMapping.DialogTexts.ElementAt(currentDialogMapping.DialogCounter);
        }
        else if (currentDialogMapping.DialogCounter >= GetNumberOfDialogs(dialogID))
        {
            dialog = currentDialogMapping.DialogTexts.Last();
        }
        return dialog;
    }

    private DialogMapping GetDialogMappingByDialogID(int dialogID)
    {
        return _dialogData.First(mapping => mapping.DialogID == dialogID);
    }

    public bool CheckIfDialogEnds(int dialogID)
    {
        var currentDialogMapping = GetDialogMappingByDialogID(dialogID);
        return currentDialogMapping.finishDialogIndexes.Contains(currentDialogMapping.DialogID); 
    }
}
