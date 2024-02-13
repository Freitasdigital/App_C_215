using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.UI.Windows
{
    public class WindowsController : MonoBehaviour
    {
        [SerializeField] private List<Window> windows;

        private void OnValidate()
        {
            if (windows.Count <= 0)
            {
                Debug.LogWarning($"{nameof(windows)} is empty");
            }
        }

        public void OpenWindow(Window currentWindow)
        {
            foreach (var window in windows)
            {
                window.gameObject.SetActive(window == currentWindow);
            }
        }
        
        public void OpenAdditionalWindow(Window window)
        {
            window.gameObject.SetActive(true);
        }
        
        public void CloseAdditionalWindow(Window window)
        {
            window.gameObject.SetActive(false);
        }
    }
}