using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class ConsoleCheat : MonoBehaviour
{
    private bool _enableCheat;
    public TMP_InputField text;
    [SerializeField] List<CommandExcercute> commandList= new List<CommandExcercute>();
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.BackQuote))
        {
            Debug.Log("console");
            text.gameObject.SetActive(!text.IsActive());
        }
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            
            ConsoleExercute(text.text);
        }
    }
    public void ConsoleExercute(string x)
    {
       //devcommand_[parameter1]_[parameter2]..
       // coditional - run 
        if (x.IndexOf('/')!=0) return;

        x = x.Remove(0,1).ToLower();
        var parameterExcute = x.Split('_', StringSplitOptions.RemoveEmptyEntries);
        for(int i = 0; i < parameterExcute.Length; i++)
        {
            Debug.Log(parameterExcute[i]);
        }
        commandList.ForEach(x => {

            if (x.ConditionalCheck(parameterExcute[0]))
            {
                x.Run();
            }
        });
        //basic
        switch (parameterExcute[0])
        {
            case "enable":
                Debug.Log("Console is Enable");
                _enableCheat = true;
                break;
            case "gofast":
                Debug.Log("Go fast");
                if(Int32.TryParse(parameterExcute[1], out int i))
                Time.timeScale = i;
                break;
        }
    }
    
}
