using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Midhard_TEST
{
    public class LoaderContoller : MonoBehaviour
    {
        public static LoaderContoller instance;

        public Action<string> OnProgress;

        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            StartAsync().Forget();
        }

        private async UniTaskVoid StartAsync()
        {
            OnProgress?.Invoke("Wait Resources");
            var task_1 = UniTask.Delay(TimeSpan.FromSeconds(5), ignoreTimeScale: false);
            await task_1;

            OnProgress?.Invoke("Load Game Scene");
            var task_2 = SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive).ToUniTask();
            await task_2;

            OnProgress?.Invoke("Unload Load Scene");
            var task_4 = SceneManager.UnloadSceneAsync(0).ToUniTask();
            await task_4;
        }
    }
}
