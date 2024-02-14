using System;
using UnityEngine;
using Game.Scripts.Preferences;

namespace Game.Scripts.Managers
{
    [DefaultExecutionOrder(-100)]
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        [SerializeField, Header("Delete Saves")] private bool deleteSavesOnStart;
        
        [SerializeField, Header("Debug Logging")] private bool enableDebugLogging = true;
        
        public event Action<int> OnLocationIdChanged;
        
        public int LocationId { get; private set; }
        
        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
            }
            
            Init();
        }
        
        public void SetLocationId(int id)
        {
            if (id < 0)
                throw new ArgumentOutOfRangeException(nameof(id));
            
            LocationId = id;
            
            OnLocationIdChanged?.Invoke(LocationId);
            PlayerPrefsManager.SetInt(Keys.LOCATION_ID_KEY, LocationId);
        }

        private void Init()
        {
            Load();
            CheckSaveOption();
            CheckDebugOption();
            
            Application.targetFrameRate = 120;
        }
        
        private void CheckSaveOption()
        {
            if (!deleteSavesOnStart) return;
            
            Debug.Log("All saves have been deleted");
            PlayerPrefs.DeleteAll();
        }

        private void CheckDebugOption()
        {
            if (enableDebugLogging) return;
            
            Debug.Log($"Debug is enable {enableDebugLogging}");
            Debug.unityLogger.logEnabled = enableDebugLogging;
        }
        
        private void Load()
        {
            LocationId = PlayerPrefsManager.GetInt(Keys.LOCATION_ID_KEY);
        }
    }
}