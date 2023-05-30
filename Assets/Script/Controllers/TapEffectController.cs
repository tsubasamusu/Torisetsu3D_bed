using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Threading;
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
    [SerializeField, Range(0f, 0.01f)]
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
    /// <param name="canvasTran">「Canvas」の位置情報</param>
    /// <param name="pos">エフェクトの座標</param>
    public void SetUpTapEffectController(Transform canvasTran, Vector3 pos)
    {
        //自身の親を設定する
        transform.SetParent(canvasTran);

        //自身の座標を設定する
        transform.localPosition = pos;

        //タップ時のエフェクトを行う
        PlayTapEffectAsync(this.GetCancellationTokenOnDestroy()).Forget();
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