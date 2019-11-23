using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CodePlane : MonoBehaviour
{

    public Text codeText;
    string codeTextValue = "";
   
    bool comfirmFlag = false;
    bool addFlag = true;
    // Update is called once per frame
    void Update()
    {
        // Update code value
        codeText.text = codeTextValue;
        // If the length of code is less than 3
        if (codeTextValue.Length <= 3)
        {
            if (comfirmFlag)
            {
                if (codeTextValue == "581")
                {
                    // success and door open
                    codeTextValue = "suc";
                }
                else
                {
                    // fail and reset value
                    codeTextValue = "";
                    comfirmFlag = false;
                    addFlag = true;
                }
            }

            if (codeTextValue.Length == 3)
            {
                addFlag = false;
            }
        }
        else
        {
            addFlag = false;
        }

    }

    public void AddDigit(string digit)
    {
        if (digit == "Enter")
        {
            comfirmFlag = true;
            //addFlag = true;
        }
        else if (digit == "Reset")
        {
            codeTextValue = "";
            comfirmFlag = false;
            addFlag = true;
        }
        else
        {   // If the text is allowed to be added
            if (addFlag)
                codeTextValue += digit;
        }
    }
}
