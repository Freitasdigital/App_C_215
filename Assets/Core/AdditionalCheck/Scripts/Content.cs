using System;
using Core;
using Prototype.SceneLoaderCore.Helpers;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Content : MonoBehaviour
{
    //[SerializeField] private bool isTest;
    
    //[SerializeField] private bool isError;

    //[SerializeField] private string _url403 = "https://duocriativolda.com";
    
    //[SerializeField] private string _url200 = "https://simoldesacos.com";

    private void Start()
    {
        Debugger.Log($"Start Content");
        
        AppState.Clear();
        
        if (AppState.GetAppStartedSuccess())
        {
            Init();
        }
        else
        {
            StartCoroutine(AdditionalCheck.Check(GetUrl(), Init));
        }
    }

    private void Init()
    {
        PrintMessage($"Init Content");

        PrintMessage($"     ConstantState: {AppState.GetConstantState()}");
        
        PrintMessage($"     CurrentState: {AppState.GetCurrentState()}");
        
        if (AppState.GetConstantState() != State.None)
        {
            if (AppState.GetConstantState() == State.White)
            {
                ShowWhiteApp();
            }
            else
            {
                PrintMessage($"Передаєм керування бекенду");
            
                if(InternetConnectionMonitor.Instance)
                    InternetConnectionMonitor.Instance.StartCheckConnect();
            }
        }
        else
        {
            if (AppState.GetCurrentState() == State.White)
            {
                ShowWhiteApp();
            }
            else
            {
                PrintMessage($"Передаєм керування бекенду");
            
                if(InternetConnectionMonitor.Instance)
                    InternetConnectionMonitor.Instance.StartCheckConnect();
            }
        }
    }

    public void ShowWhiteApp()
    {
        PrintMessage($"Show White pp");
        
        if (SceneLoader.Instance)
        {
            SceneLoader.Instance.SwitchToScene(SceneLoader.Instance.mainScene);
        }
        else
        {
            SceneManager.LoadScene("Game");
        }
    }
    
    private string GetUrl()
    {
        return Settings.AdditionalUrl();
        //return isTest? isError? _url403 : _url200 : Settings.AdditionalUrl();
    }
    
    private void PrintMessage(string message)
    {
        Debugger.Log($"@@@ Content -> message: {message}", new Color(0.8f, 0.5f, 0));
    }
}
