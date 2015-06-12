using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class Logger
{

    public static Logger instance;
    private Text textObject;


    public static Logger GetInstance()
    {
        if (instance == null)
        {
            instance = new Logger();
        }
        return instance;
    }

    private Logger()
    {
        textObject = GameObject.FindWithTag("LogText").GetComponent<Text>();
    }

    public void PutLine(string newLine)
    {
        string content = textObject.text;
        if (content.Length < 2)
        {
            textObject.text += newLine + "\n";
            return;
        }

        string lastLine;
        int multiplicator;

        int indexBeginningLastLine = content.LastIndexOf("\n", content.Length - 2) + 1;
        int indexLastOpeningBracket = content.LastIndexOf('*');
        int indexLastClosingBracket = content.LastIndexOf(']');

        //The last line already has a multiplicator [*X]
        if (indexBeginningLastLine > indexLastOpeningBracket)
        {
            lastLine = content.Substring(indexBeginningLastLine).TrimEnd();
            multiplicator = 2;
            if (lastLine.Equals(newLine))
            {
                textObject.text = content.TrimEnd() + " [*" + multiplicator + "]\n";
                return;
            }
        }
        else
        {
            //Get the last line without multiplicator to compare with the newLine
            lastLine = content.Substring(indexBeginningLastLine, indexLastOpeningBracket - indexBeginningLastLine - 2);
            string numberString = content.Substring(indexLastOpeningBracket + 1, indexLastClosingBracket - indexLastOpeningBracket - 1);
            multiplicator = Convert.ToInt32(numberString) + 1;
            if (lastLine.Equals(newLine))
            {
                textObject.text = content.Substring(0, indexLastOpeningBracket - 1).TrimEnd() + " [*" + multiplicator + "]\n";
                return;
            }
        }
        textObject.text += newLine + "\n";
    }

}
