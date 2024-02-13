using UnityEngine;

namespace Game.Scripts.Game.Player
{
    [System.Serializable]
    public class Animations
    {
        [SerializeField] private Enums.State state;
        [SerializeField] private string animationName;
        [SerializeField] private bool loop;
        [SerializeField, Min(0)] private float timeScale = 1f;

        public Enums.State State => state;
        public string AnimationName => animationName;
        public bool Loop => loop;
        public float TimeScale => timeScale;
    }
}