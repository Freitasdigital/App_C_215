using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Game.LastChance.Scripts.Animation
{
    [RequireComponent(typeof(Image))]
    public class SpriteAnimation : MonoBehaviour
    {
        private Image _image;
        
        private void Awake()
        {
            _image = GetComponent<Image>();
        }

        public void SetSpritesSettings(Sprite[] sprites, float timeToShow, int winIndex)
        {
            StartCoroutine(AnimateSprites(sprites, timeToShow, winIndex));
        }
        
        private IEnumerator AnimateSprites(Sprite[] sprites, float timeToShow, int winIndex)
        {
            var timer = 0f;

            while (timer < timeToShow)
            {
                var randomIndex = Random.Range(0, sprites.Length);
                _image.sprite = sprites[randomIndex];

                var randomTime = Random.Range(0.1f, 0.3f);
                yield return new WaitForSecondsRealtime(randomTime);

                timer += randomTime;
            }
            
            _image.sprite = sprites[winIndex];
        }
    }
}