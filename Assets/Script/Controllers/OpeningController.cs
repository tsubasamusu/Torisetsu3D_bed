using Cysharp.Threading.Tasks;
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

    }
}