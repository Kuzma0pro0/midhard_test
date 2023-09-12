using UnityEngine;
using UnityEngine.SceneManagement;

namespace Midhard_TEST.ECS.Services
{
    public sealed class SceneService
    {
        public void ReloadScene()
        {
            Debug.Log($"{GetType().Name}.{nameof(ReloadScene)}");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}

