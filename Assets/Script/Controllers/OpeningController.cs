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
    /// 動画の再生前・再生後の時間間隔
    /// </summary>
    [SerializeField]
    private float bothSideVideoTime;

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
    /// 説明動画表示用の「RawImage」
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

        //説明動画表示用の「RawImage」を非活性化する
        informationRawImage.gameObject.SetActive(false);

        //一定時間待つ
        await UniTask.Delay(TimeSpan.FromSeconds(bothSideVideoTime), cancellationToken: token);

        //説明動画を再生する
        videoPlayer.Play();

        //説明動画表示用の「RawImage」を活性化する
        informationRawImage.gameObject.SetActive(true);

        //「OnVideoEnd()」を登録する
        videoPlayer.loopPointReached += OnVideoEnd;
    }

    /// <summary>
    /// 動画の再生が終了した際に呼び出される
    /// </summary>
    /// <param name="videoPlayer">動画を再生していた「VideoPlayer」</param>
    private void OnVideoEnd(VideoPlayer videoPlayer)
    {
        //一定時間かけて「RawImage」を徐々に非表示にする
        informationRawImage.DOFade(0f, bothSideVideoTime).OnComplete(() => Destroy(informationRawImage.gameObject));

        //一定時間かけて背景を徐々に非表示にする
        imgBackground.DOFade(0f, bothSideVideoTime).OnComplete(() => Destroy(imgBackground.gameObject));
    }
}