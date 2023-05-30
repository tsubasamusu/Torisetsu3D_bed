using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

/// <summary>
/// オープニングを制御する
/// </summary>
public class OpeningController : MonoBehaviour
{
    /// <summary>
    /// VideoPlayer
    /// </summary>
    [SerializeField]
    private VideoPlayer videoPlayer;

    /// <summary>
    /// 背景のイメージ
    /// </summary>
    [SerializeField]
    private Image imgBackground;

    /// <summary>
    /// インフォメーション表示用の「RawImage」
    /// </summary>
    [SerializeField]
    private RawImage informationRawImage;

    /// <summary>
    /// 背景の色
    /// </summary>
    [SerializeField]
    private Color backgroundColor;

    /// <summary>
    /// ゲーム開始直後に呼び出される
    /// </summary>
    private void Start()
    {
        //オープニングを再生する
        PlayOpeningAsync(this.GetCancellationTokenOnDestroy()).Forget();
    }

    /// <summary>
    /// オープニングを再生する
    /// </summary>
    /// <param name="token">CancellationToken</param>
    /// <returns>待ち時間</returns>
    private async UniTaskVoid PlayOpeningAsync(CancellationToken token)
    {
        //背景の色を設定する
        imgBackground.color = backgroundColor;

        imgBackground.DOFade(0f, 0f);

        //一定時間待つ
        await UniTask.Delay(TimeSpan.FromSeconds(0.5f), cancellationToken: token);
    }
}