using Game.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility.Singletons;

public class DialogSystem : SingletonMonoBehaviour<DialogSystem>
{

    int interactionID = 1;  //specify with who the player interacts (objects, persons, ...)

    // Start is called before the first frame update
    void Start()
    {
        Debug.LogError("Welcome to the dialog system! :)");
    }

    // Update is called once per frame
    void Update()
    {
        if (interactionID >= 0)
        {
            string message = ConfigSingletonInstaller.Instance.DialogConfig.GetNextDialog(interactionID);
            Debug.LogError(message);
            if (ConfigSingletonInstaller.Instance.DialogConfig.CheckIfDialogEnds(interactionID))
            {
                interactionID = -1;
            }
        }
    }
}