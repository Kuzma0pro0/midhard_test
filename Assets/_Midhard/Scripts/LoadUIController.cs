using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using System;
using DG.Tweening;

namespace Midhard_TEST
{
    public class LoadUIController : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _tips;
        [SerializeField]
        private TMP_Text _progress;
        [SerializeField]
        private Image _jumper;

        [SerializeField, Range(0.1f, 2.5f)]
        private float _tipsDelay = 1f;

        private readonly List<string> _tipTexts = new List<string>
    {
        "Иногда люди не понимают на сколько многое они могут сделать, хотя даже не пытались.",
        "Колобок повесился...",
        "Юмор он как вода, течет не остановить"
    };

        private void Start()
        {
            LoaderContoller.instance.OnProgress += SetProgress;
            TipsUpdate().Forget();

            Animate();
        }

        private void Animate()
        {
            var tr = _jumper.transform;
            tr.DOLocalJump(tr.localPosition, 50, 1, 0.8f).SetEase(Ease.Linear).OnComplete(() =>
            {
                Animate();
            });
        }

        private void SetProgress(string text)
        {
            _progress.text = text;
        }

        private async UniTask TipsUpdate()
        {
            var count = 0;
            while (count < _tipTexts.Count)
            {
                _tips.text = _tipTexts[count];
                count++;
                await UniTask.Delay(TimeSpan.FromSeconds(_tipsDelay), ignoreTimeScale: false, cancellationToken: this.GetCancellationTokenOnDestroy());
            }
        }

        private void OnDestroy()
        {
            LoaderContoller.instance.OnProgress -= SetProgress;
        }
    }
}
