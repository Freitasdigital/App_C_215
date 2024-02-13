using System;
using UnityEngine;
using UnityEngine.Events;

namespace Game.DailyReward.Scripts
{
	public class DailyAvailabilityController : MonoBehaviour
	{
        [SerializeField, Header("Events")] private UnityEvent<bool> onDailyRewardAvailable;
        [SerializeField] private UnityEvent<int> onDayCounterChanged;
        
        private DateTime _lastLogin = DateTime.MinValue;
        private int _dayCounter;
        
        private const string LAST_LOGIN_KEY = "LAST_LOGIN_KEY";
        private const string DAY_COUNTER_KEY = "DAY_COUNTER_KEY";

        private void Awake()
        {
            Load();
            CheckLastDayReward();
        }

        public void SetLastDayReward()
        {
            Save();
        }

        private void CheckLastDayReward()
        {
            var elapsed = DateTime.Now.Date - _lastLogin.Date;

            if (elapsed.Days > 0)
            {
                if (elapsed.Days == 1)
                {
                    _dayCounter++;
                }
                else if (elapsed.Days > 1)
                {
                    _dayCounter = 0;
                }
                
                onDailyRewardAvailable?.Invoke(true);
            }
            else
            {
                onDailyRewardAvailable?.Invoke(false);
                Debug.Log("Reward already claimed for today.");
            }
            
            onDayCounterChanged?.Invoke(_dayCounter);
        }

        private void Load()
        {
            _lastLogin = DateTime.Parse(PlayerPrefs.GetString(LAST_LOGIN_KEY, DateTime.MinValue.ToString()));
            _dayCounter = PlayerPrefs.GetInt(DAY_COUNTER_KEY, 0);
        }

        private void Save()
        {
            PlayerPrefs.SetString(LAST_LOGIN_KEY, DateTime.Now.ToString());
            PlayerPrefs.SetInt(DAY_COUNTER_KEY, _dayCounter);

            PlayerPrefs.Save();
        }
    }
}