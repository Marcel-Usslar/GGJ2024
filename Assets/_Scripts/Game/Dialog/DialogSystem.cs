using Game.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility.Singletons;

public class DialogSystem : SingletonMonoBehaviour<DialogSystem>
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.LogError("Welcome to the dialog system! :)");
    }

    // Update is called once per frame
    public void getDialogText(int interactionID)
    {
        if (interactionID >= 0)
        {
            string message = ConfigSingletonInstaller.Instance.DialogConfig.GetNextDialog(interactionID);
            Debug.LogError(message);
        }
    }
}