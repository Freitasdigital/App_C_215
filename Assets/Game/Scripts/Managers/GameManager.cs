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
        
        public event Action<int> OnPlaneIdChanged;
        public event Action<int> OnLocationIdChanged;
        public event Action<int> OnBallIdChanged;

        public int PlayerId { get; private set; }
        public int PlaneId { get; private set; }
        public int LocationId { get; private set; }
        public int BallId { get; private set; }
        
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
        
        public void SetPlayerId(int id)
        {
            if (id < 0)
                throw new ArgumentOutOfRangeException(nameof(id));
            
            PlayerId = id;
            
            PlayerPrefsManager.SetInt(Keys.PLAYER_ID_KEY, PlayerId);
        }
        
        public void SetPlaneId(int id)
        {
            if (id < 0)
                throw new ArgumentOutOfRangeException(nameof(id));
            
            PlaneId = id;
            
            OnPlaneIdChanged?.Invoke(PlaneId);
            PlayerPrefsManager.SetInt(Keys.PLANE_ID_KEY, PlaneId);
        }
        
        public void SetLocationId(int id)
        {
            if (id < 0)
                throw new ArgumentOutOfRangeException(nameof(id));
            
            LocationId = id;
            
            OnLocationIdChanged?.Invoke(LocationId);
            PlayerPrefsManager.SetInt(Keys.LOCATION_ID_KEY, LocationId);
        }
        
        public void SetBallId(int id)
        {
            if (id < 0)
                throw new ArgumentOutOfRangeException(nameof(id));
            
            BallId = id;
            
            OnBallIdChanged?.Invoke(BallId);
            PlayerPrefsManager.SetInt(Keys.BALL_ID_KEY, BallId);
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
            PlayerId = PlayerPrefsManager.GetInt(Keys.PLANE_ID_KEY);
            PlaneId = PlayerPrefsManager.GetInt(Keys.PLANE_ID_KEY);
            LocationId = PlayerPrefsManager.GetInt(Keys.LOCATION_ID_KEY);
            BallId = PlayerPrefsManager.GetInt(Keys.BALL_ID_KEY);
        }
    }
}