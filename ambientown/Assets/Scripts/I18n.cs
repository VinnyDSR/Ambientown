using System;
using System.Collections.Generic;
using UnityEngine;

class I18n
{
    public static Dictionary<string, string> Fields { get; private set; }

    static I18n()
    {
        LoadLanguage("en");
    }

    public static void LoadLanguage(string lang)
    {
        if (Fields == null)
            Fields = new Dictionary<string, string>();

        Fields.Clear();

        var textAsset = Resources.Load(@"I18n/" + lang);
        string allTexts = "";
        if (textAsset == null)
            textAsset = Resources.Load(@"I18n/en") as TextAsset;
        if (textAsset == null)
            Debug.LogError("File not found for I18n: Assets/Resources/I18n/" + lang + ".txt");
        allTexts = (textAsset as TextAsset).text;
        string[] lines = allTexts.Split(new string[] { "\r\n", "\n" },
            StringSplitOptions.None);
        string key, value;
        for (int i = 0; i < lines.Length; i++)
        {
            if (lines[i].IndexOf("=") >= 0 && !lines[i].StartsWith("#"))
            {
                key = lines[i].Substring(0, lines[i].IndexOf("="));
                value = lines[i].Substring(lines[i].IndexOf("=") + 1,
                        lines[i].Length - lines[i].IndexOf("=") - 1).Replace("\\n", Environment.NewLine);
                Fields.Add(key, value);
            }
        }
    }


}