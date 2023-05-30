using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Threading;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// タップ時のエフェクトを制御する
/// </summary>
public class TapEffectController : MonoBehaviour
{
    /// <summary>
    /// スプライト間の秒数
    /// </summary>
    [SerializeField, Range(0f, 0.1f)]
    private float timeBtweenSprites;

    /// <summary>
    /// タップ時のエフェクト表示用のイメージ
    /// </summary>
    [SerializeField]
    private Image imgTapEffect;

    /// <summary>
    /// タップ時のエフェクトのスプライトのリスト
    /// </summary>
    [SerializeField]
    private List<Sprite> tapEffectSprites = new();

    /// <summary>
    /// 「TapEffectController」クラスの初期設定を行う
    /// </summary>
    public void SetUpTapEffectController()
    {
        //タップ時のエフェクトを行う
        PlayTapEffectAsync(this.GetCancellationTokenOnDestroy()).Forget();

        //常にメインカメラの方向を向く
        this.UpdateAsObservable()
            .Subscribe(_ => transform.LookAt(Camera.main.transform.position))
            .AddTo(this);
    }

    /// <summary>
    /// タップ時のエフェクトを行う
    /// </summary>
    /// <param name="token">CancellationToken</param>
    /// <returns>待ち時間</returns>
    private async UniTaskVoid PlayTapEffectAsync(CancellationToken token)
    {
        //タップ時のエフェクトのスプライトの数だけ繰り返す
        for (int i = 0; i < tapEffectSprites.Count; i++)
        {
            //イメージのスプライトを設定する
            imgTapEffect.sprite = tapEffectSprites[i];

            //一定時間待つ
            await UniTask.Delay(TimeSpan.FromSeconds(timeBtweenSprites), cancellationToken: token);
        }

        //タップ時のエフェクトのスプライトの数だけ繰り返す
        for (int i = 0; i < tapEffectSprites.Count; i++)
        {
            //イメージのスプライトを設定する
            imgTapEffect.sprite = tapEffectSprites[(tapEffectSprites.Count - 1) - i];

            //一定時間待つ
            await UniTask.Delay(TimeSpan.FromSeconds(timeBtweenSprites), cancellationToken: token);
        }

        //このゲームオブジェクトを消す
        Destroy(gameObject);
    }
}