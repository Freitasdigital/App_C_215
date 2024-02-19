using Game.Scripts.Managers;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Game.Scripts.Game.Popups
{
	[RequireComponent(typeof(Button))]
	public class ResultActionButton : MonoBehaviour
	{
		[SerializeField, Header("Price")] private UnityEvent onBuyPurchased;
		
		[SerializeField, Header("Price"), Min(0)] private int price = 25;
		
		[SerializeField, Header("State Sprites")] private Sprite nextSprite;
		[SerializeField] private Sprite buySprite;
		[SerializeField] private Sprite inactiveSprite;
		
		private Button _button;
		private Image _image;

		private bool _isLevelDone;
		private bool _isBallBought;

		private void OnValidate()
		{
			if (nextSprite == null)
			{
				Debug.LogWarning($"{nameof(nextSprite)} is null");
			}
			
			if (buySprite == null)
			{
				Debug.LogWarning($"{nameof(buySprite)} is null");
			}
			
			if (inactiveSprite == null)
			{
				Debug.LogWarning($"{nameof(inactiveSprite)} is null");
			}
		}

		private void Awake()
		{
			_button = GetComponent<Button>();
			_image = _button.image;
			
			_button.onClick.AddListener(Action);
		}

		public void SetState(bool isLevelDone)
		{
			_isLevelDone = isLevelDone;
			
			if (_isLevelDone)
			{
				SetStateSprite(nextSprite);
			}
			else if (!_isBallBought && Wallet.Wallet.Money >= price)
			{
				SetStateSprite(buySprite);
			}
			else
			{
				_button.enabled = false;
				SetStateSprite(inactiveSprite);
			}
		}

		private void Action()
		{
			if (_isLevelDone)
			{
				LoadNextLevel();
			}
			else if (!_isBallBought && Wallet.Wallet.Money >= price)
			{
				BuyBall();
			}
		}

		private void BuyBall()
		{
			if (Player.Player.Instance != null)
			{
				_isBallBought = true;
				
				Wallet.Wallet.TryPurchase(price);
				Player.Player.Instance.AddBall();
				onBuyPurchased?.Invoke();
			}
			else
			{
				Debug.LogWarning($"{nameof(Player)} is null");
			}
		}

		private void LoadNextLevel()
		{
			if (LevelsManager.Scripts.LevelsManager.Instance != null)
			{
				LevelsManager.Scripts.LevelsManager.Instance.LoadNextLevel();
				
				var currentLevel = LevelsManager.Scripts.LevelsManager.Instance.GetCurrentLevel();
				LoadScene(currentLevel.Level.ToString());
			}
			else
			{
				Debug.LogWarning($"{nameof(LevelsManager)} is null");
			}
		}

		private void LoadScene(string scene)
		{
			if (ScenesManager.Instance != null)
			{
				ScenesManager.Instance.LoadScene(scene);
			}
			else
			{
				Debug.LogWarning($"{nameof(ScenesManager)} is null");
			}
		}

		private void SetStateSprite(Sprite sprite)
		{
			_image.sprite = sprite;
		}
	}
}