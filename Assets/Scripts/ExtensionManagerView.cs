﻿#pragma warning disable 0168 // variable declared but not used.
#pragma warning disable 0219 // variable assigned but not used.
#pragma warning disable 0414 // private field assigned but not used.


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;    
public class ExtensionManagerView : MonoBehaviour {

    List<ExtensionInfo> availableExtensions= new   List<ExtensionInfo>();
    public GameObject extensionInfoPrefab,extensionInfoOrganizer;
    LoadData loader;
    SaveData saver;
    Coroutine activeRoutine;
    public ExtensionInformationView extensionInfoView;
	void Start () {
      

        activeRoutine  = StartCoroutine(LoadExtensions());
        loader = LoadData.LoadFromPath("Extensions");
	}
	
	public void UpdateExtensionList()
    {
        if(activeRoutine!=null)  StopCoroutine(activeRoutine);
       activeRoutine =  StartCoroutine(LoadExtensions());
    }

    IEnumerator LoadExtensions()
    {
        if(extensionInfoOrganizer.transform.childCount > 0)
        {
            foreach (Transform child in extensionInfoOrganizer.transform)
            {
                GameObject.Destroy(child.gameObject);
            }
        }
        
        /*
        string url = RoomSettings.AbsoluteFilenamePath;

        if (Application.isEditor)
        {
            url = "file:///" + url;
        }

        var www = new WWW(url);
        yield return www;
        // Do some code, when file loaded*/ //<- This is for JSON when the JSON is online
        availableExtensions = null;
        yield return new WaitForThreadedTask(() => {
           // availableExtensions = JsonUtility.FromJson<List<ExtensionInfo>>(json);
        });
        List<ExtensionInfo> json = loader.LoadAllFromFolder<ExtensionInfo>();
        yield return json;
        foreach(ExtensionInfo extension in json)
        {
            GameObject container = Instantiate(extensionInfoPrefab, extensionInfoOrganizer.transform);
            container.GetComponent<ExtensionInfoContainer>().extensionInfoView = extensionInfoView;

            container.GetComponent<ExtensionInfoContainer>().SetupExtension(extension);
            container.SetActive(true);
        }
       
      


        activeRoutine = null;   
    }
}