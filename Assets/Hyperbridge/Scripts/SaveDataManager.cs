﻿#pragma warning disable 0168 // variable declared but not used.
#pragma warning disable 0219 // variable assigned but not used.
#pragma warning disable 0414 // private field assigned but not used.
#pragma warning disable 0649 // default value null


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class SaveDataManager : MonoBehaviour
{
    private Text saveText, loadText;
    private LoadData loader;
    private SaveData saver;

    public void CacheValues()
    {
    }

    public void SaveCurrentExtensionData()
    {
        this.saver = SaveData.SaveAtPath("Extensions");
    }

    public void SaveExtensionJSON(string ID,ExtensionInfo data)
    {
        this.saver = SaveData.SaveAtPath("/Resources/Extensions/"+ID+"/"+data.name);
        this.saver.Save<ExtensionInfo>(data.name, data);
    }

    public void DeleteSpecificSave(string saveName, string path)
    {
        Debug.Log(Application.dataPath + path + "/" + saveName + ".json");
        if (File.Exists(Application.dataPath  + path + "/" + saveName + ".json")) {
            File.Delete(Application.dataPath  + path + "/" + saveName + ".json");
        }
        else {
            throw new FileNotFoundException();
        }
    }

    public void LoadSavedData()
    {
        this.loader = LoadData.LoadFromPath("Extensions");
    }
}
