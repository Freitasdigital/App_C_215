using Game.LastChance.Scripts.Enums;
using UnityEngine;

namespace Game.LastChance.Scripts
{
    [CreateAssetMenu(menuName = "LastChance/Element")]
    public class LastChanceElement : ScriptableObject
    {
        [SerializeField] private RewardType rewardType;
        [SerializeField] private Sprite sprite;
        [SerializeField, Range(0, 100)] private int probability;
        [SerializeField, Min(0)] private int reward;

        public RewardType RewardType => rewardType;
        public Sprite Sprite => sprite;
        public int Probability => probability;
        public int Reward => reward;
    }
}