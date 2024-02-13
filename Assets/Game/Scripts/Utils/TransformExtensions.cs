using UnityEngine;

namespace Game.Scripts.Utils
{
    public static class TransformExtensions
    {
        public static void DestroyChildObjects(this Transform transform)
        {
            foreach (Transform child in transform)
            {
                Object.Destroy(child.gameObject);
            }
        }
    }
}