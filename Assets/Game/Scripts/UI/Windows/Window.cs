using UnityEngine;

namespace Game.Scripts.UI.Windows
{
    public class Window : MonoBehaviour
    {
        [SerializeField] private Enums.Windows window;

        public Enums.Windows CurrentWindow => window;
    }
}