using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// オープニングを制御する
/// </summary>
public class OpeningController : MonoBehaviour
{
    /// <summary>
    /// フェードアウト時間
    /// </summary>
    [SerializeField]
    private float fadeOutTime;

    /// <summary>
    /// スプライト間の秒数
    /// </summary>
    [SerializeField, Range(0f, 0.1f)]
    private float timeBtweenSprites;

    /// <summary>
    /// 背景のイメージ
    /// </summary>
    [SerializeField]
    private Image imgBackground;

    /// <summary>
    /// 説明画像表示用イメージ
    /// </summary>
    [SerializeField]
    private Image imgInformation;

    /// <summary>
    /// AnimationController
    /// </summary>
    [SerializeField]
    private AnimationController animationController;

    /// <summary>
    /// 再生ボタンの「ButtonColorChanger」
    /// </summary>
    [SerializeField]
    private ButtonColorChanger btnStart;

    /// <summary>
    /// 説明画像のスプライトのリスト
    /// </summary>
    [SerializeField]
    private List<Sprite> informationSprites = new();

    /// <summary>
    /// ゲーム開始直後に呼び出される
    /// </summary>
    private void Start()
    {
        //オープニングを開始する
        StartOpeningAsync(this.GetCancellationTokenOnDestroy()).Forget();
    }

    /// <summary>
    /// オープニングを開始する
    /// </summary>
    /// <param name="token">CancellationToken</param>
    /// <returns>待ち時間</returns>
    private async UniTaskVoid StartOpeningAsync(CancellationToken token)
    {
        //説明画像の数だけ繰り返す
        for (int i = 0; i < informationSprites.Count; i++)
        {
            //説明画像表示用イメージのスプライトを設定する
            imgInformation.sprite = informationSprites[i];

            //一定時間待つ
            await UniTask.Delay(TimeSpan.FromSeconds(timeBtweenSprites), cancellationToken: token);
        }

        //背景と説明画像を一定時間かけて徐々に非表示にする
        imgBackground.DOFade(0f, fadeOutTime).OnComplete(() => Destroy(imgBackground.gameObject));
        imgInformation.DOFade(0f, fadeOutTime).OnComplete(() => Destroy(imgInformation.gameObject));

        //一定時間待つ
        await UniTask.Delay(TimeSpan.FromSeconds(fadeOutTime), cancellationToken: token);

        //アニメーションを開始する
        animationController.StartAnimation();

        //再生ボタンが押された際の処理を行う
        btnStart.OnClicked();

        //不要になったゲームオブジェクトを消す
        Destroy(gameObject);
    }
}