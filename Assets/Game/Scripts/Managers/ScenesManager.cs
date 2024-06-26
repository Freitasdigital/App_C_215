using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Scripts.Managers
{
    public class ScenesManager : MonoBehaviour
    {
        public static ScenesManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
            }
        }

        public void LoadScene(string scene)
        {
            SceneManager.LoadScene(scene);
        }
    }
}