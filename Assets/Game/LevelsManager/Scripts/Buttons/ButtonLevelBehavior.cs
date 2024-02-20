using System;
using Game.LevelsManager.Scripts.Enums;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.LevelsManager.Scripts.Buttons
{
	[RequireComponent(typeof(Button))]
	public class ButtonLevelBehavior : MonoBehaviour
	{
		[SerializeField, Header("Level Settings")] private Levels level;
		
		[SerializeField, Header("Level Text")] private TextMeshProUGUI levelText;
		
		private Button _button;
		private Image _image;
		
		private StateContainer _stateContainer;

		private void OnValidate()
		{
			if (levelText == null)
			{
				Debug.LogWarning($"{nameof(levelText)} is null");
			}
		}

		private void Awake()
		{
			_button = GetComponent<Button>();
			_image = _button.image;
			
			_button.onClick.AddListener(SetLevel);
		}

		private void Start()
		{
			LoadLevelInfo();
		}

		private void LoadLevelInfo()
		{
			if (LevelsManager.Instance != null)
			{
				var currentLevel = LevelsManager.Instance.GetLevelInfo(level);
				_stateContainer = LevelsManager.Instance.StateContainer;

				if (currentLevel == null)
					throw new ArgumentNullException(nameof(currentLevel));

				CheckState(currentLevel);
				SetButtonState(currentLevel.IsDone || currentLevel.IsOpen);
			}
			else
			{
				Debug.LogWarning($"{nameof(LevelsManager)} is null");
			}
		}

		private void CheckState(LevelInfo currentLevel)
		{
			if (currentLevel.IsDone)
			{
				SetDoneState();
			}
			else if (currentLevel.IsOpen)
			{
				SetOpenState();
			}
			else
			{
				SetLockState();
			}
		}

		private void SetDoneState()
		{
			SetSprite(_stateContainer.DoneSprite);
			SetTextColor(_stateContainer.DoneColor);
		}

		private void SetOpenState()
		{
			SetSprite(_stateContainer.OpenSprite);
			SetTextColor(_stateContainer.OpenColor);
		}

		private void SetLockState()
		{
			SetSprite(_stateContainer.LockSprite);
			SetTextColor(_stateContainer.LockColor);
		}

		private void SetLevel()
		{
			if (LevelsManager.Instance != null)
			{
				LevelsManager.Instance.SetLevel(level);
			}
			else
			{
				Debug.LogWarning($"{nameof(LevelsManager)} is null");
			}
		}

		private void SetButtonState(bool state)
		{
			_button.enabled = state;
		}

		private void SetSprite(Sprite sprite)
		{
			_image.sprite = sprite;
		}
		
		private void SetTextColor(Color color)
		{
			levelText.color = color;
		}
	}
}