using Game.Scripts.Audio;
using Game.Scripts.Enums;
using Game.Scripts.Wallet;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Shop.Scripts
{
	public abstract class BuyItem : MonoBehaviour
	{
        [SerializeField, Header("Price Text")] private TextMeshProUGUI priceText;
        
        [SerializeField, Header("Item Settings"), Min(0)] private int price = 100;
        [SerializeField, Min(0)] protected int id = 1;

        [SerializeField, Header("Action Button")] private Button actionButton;

        private Image _buttonImage;
        private SpritesContainer _spritesContainer;
        
        private bool _isBought;
        private int _currentBalance;

        private void OnValidate()
        {
            if (priceText == null)
            {
                Debug.LogWarning($"{nameof(priceText)} is null");
            }
            
            if (actionButton == null)
            {
                Debug.LogWarning($"{nameof(actionButton)} is null");
            }
        }

        private void OnDestroy()
        {
            UnsubscribeFromEvent();
        }

        private void Awake()
        {
            _buttonImage = actionButton.image;
            actionButton.onClick.AddListener(Buy);
        }
        
        private void Start()
        {
            Load();
            CheckIfDefaultId();
            
            CheckBalance(Wallet.Money);
            SubscribeToEvent();
        }
        
        public void SetStateSprites(SpritesContainer spritesContainer)
        {
            _spritesContainer = spritesContainer;
        }

        protected virtual void SubscribeToEvent()
        {
            Wallet.OnChangedMoney += CheckBalance;
        }

        protected virtual void UnsubscribeFromEvent()
        {
            Wallet.OnChangedMoney -= CheckBalance;
        }
        
        private void SetPriceText()
        {
            priceText.gameObject.SetActive(true);
            
            SetPriceTextAlphaColor(1f);
            priceText.text = price.ToString();
        }
        
        private void SetInactivePriceText()
        {
            priceText.gameObject.SetActive(false);
        }
        
        private void SetPriceTextAlphaColor(float alpha)
        {
            var currentColor = priceText.color;
            currentColor.a = alpha;
            
            priceText.color = currentColor;
        }
        
        private void CheckState()
        {
            if (IsCurrentIdSelected())
                SelectState();
            else if (_isBought)
                ChooseState();
            else if (_currentBalance >= price)
                CanBuyState();
            else
                DisableState();
        }

        protected abstract bool IsCurrentIdSelected();

        private void SelectState()
        {
            actionButton.enabled = false;
            
            SetInactivePriceText();
            SetSprite(_spritesContainer.SelectedButtonSprite);
        }

        private void ChooseState()
        {
            actionButton.enabled = true;
            
            SetInactivePriceText();
            SetSprite(_spritesContainer.ChooseButtonSprite);
        }

        private void CanBuyState()
        {
            actionButton.enabled = true;

            SetPriceText();
            SetSprite(_spritesContainer.ActiveButtonSprite);
        }

        private void DisableState()
        {
            actionButton.enabled = false;
            
            SetPriceText();
            SetPriceTextAlphaColor(0.4f);
            SetSprite(_spritesContainer.InactiveButtonSprite);
        }
        
        private void SetSprite(Sprite sprite)
        {
            _buttonImage.sprite = sprite;
        }

        private void CheckBalance(int balance)
        {
            _currentBalance = balance;
            
            CheckState();
        }

        protected void CheckId(int index)
        {
            CheckState();
        }

        private void Buy()
        {
            if (!_isBought)
            {
                _isBought = true;
                Wallet.TryPurchase(price);
                AudioEffectsManager.PlaySound(AudioClips.Buy.ToString());
                
                Save();
            }
            
            SetId();
            CheckState();
        }

        protected abstract void SetId();

        private void CheckIfDefaultId()
        {
            if (_isBought || price > 0) return;
            
            _isBought = true;
            Save();
        }
        
        private void Save()
        {
            PlayerPrefs.SetInt($"{gameObject.name}", _isBought ? 1 : 0);
            
            PlayerPrefs.Save();
        }
        
        private void Load()
        {
            _isBought = PlayerPrefs.GetInt($"{gameObject.name}") != 0;
        }
	}
}