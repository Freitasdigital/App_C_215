using System;
using UnityEngine;

namespace Game.Scripts.Wallet
{
    public static class Wallet
    {
        public static event Action<int> OnChangedMoney;

        private const int MAX_MONEY_VALUE = 999999999;
        private const string WALLET_MONEY_KEY = "WalletMoney";
        
        public static int Money
        {
            get => PlayerPrefs.GetInt(WALLET_MONEY_KEY, 0);

            private set
            {
                if (value < 0)
                    value = 0;
                
                if (value > MAX_MONEY_VALUE)
                    value = MAX_MONEY_VALUE;

                PlayerPrefs.SetInt(WALLET_MONEY_KEY, value);
                PlayerPrefs.Save();

                OnChangedMoney?.Invoke(value);
            }
        }

        public static void AddMoney(int money)
        {
            if (money > 0)
            {
                Money += money;
            }
        }

        public static bool TryPurchase(int money)
        {
            if (Money >= money)
            {
                Money -= money;

                return true;
            }

            return false;
        }
    }
}