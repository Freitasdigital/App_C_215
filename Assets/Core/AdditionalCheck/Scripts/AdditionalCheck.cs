using System;
using System.Collections;
using Core;
using UnityEngine.Networking;

public static class AdditionalCheck
{
    private static int timeout = 10;
    
    private static UnityWebRequest webRequest;

    private static Action _callback;

    public static IEnumerator Check(string url, Action callback)
    {
        _callback = callback;
        
        if (!string.IsNullOrEmpty(url))
        {
            webRequest = UnityWebRequest.Head(url);

            webRequest.timeout = timeout;

            yield return webRequest.SendWebRequest();

            var statusCode = (int)webRequest.responseCode;
            
            if (webRequest.result == UnityWebRequest.Result.ConnectionError || webRequest.result == UnityWebRequest.Result.ProtocolError)
            {
                if (statusCode == 403)
                {
                    HandleForbidden();
                }
                else
                {
                    HandleError(webRequest.error);
                }
            }
            else
            {
                
                if ((int)webRequest.responseCode == 200)
                {
                    HandleSuccess();
                }
                else
                {
                    HandleUnknownStatus(statusCode);
                }
            }
        }
        else
        {
            Debugger.Log($"Url is Null or Empty");
            
            AppState.SetCurrentWhiteState();
        }
        
        _callback?.Invoke();
    }

    private static void HandleSuccess() //200
    {
        Debugger.Log("Статус код 200. Продовжуємо роботу з бекендом.");
        
        AppState.SetAppStartedSuccess();
    }

    private static void HandleForbidden() //403
    {
        Debugger.Log("Статус код 403. Переходимо на white.");
        
        AppState.SetConstantWhiteState();
        
        AppState.SetAppStartedSuccess();
    }

    private static void HandleUnknownStatus(int statusCode)
    {
        Debugger.Log("Отримано невідомий статус код: " + statusCode);
        
        AppState.SetCurrentWhiteState();
    }


    private static void HandleError(string errorMessage)
    {
        Debugger.LogError("Помилка при виконанні запиту: " + errorMessage);
        
        AppState.SetCurrentWhiteState();
    }
}