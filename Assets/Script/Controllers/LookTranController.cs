using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
using System.Linq;
using System.Threading;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// カメラの対象位置を制御する
/// </summary>
public class LookTranController : MonoBehaviour
{
    /// <summary>
    /// CameraController
    /// </summary>
    [SerializeField]
    private CameraController cameraController;

    /// <summary>
    /// タップ時のエフェクトのプレハブ
    /// </summary>
    [SerializeField]
    private TapEffectController tapEffectPrefab;

    /// <summary>
    /// 「Canvas」の「RectTransform」
    /// </summary>
    [SerializeField]
    private RectTransform canvasRectTran;

    /// <summary>
    /// 光線の長さ
    /// </summary>
    [SerializeField]
    private float rayLength;

    /// <summary>
    /// カメラの対象位置更新時の移動時間
    /// </summary>
    [SerializeField]
    private float moveTime;

    /// <summary>
    /// カメラの対象位置の初期値
    /// </summary>
    private Vector3 defaultLookTranPos;

    /// <summary>
    /// ゲーム開始直後に呼び出される
    /// </summary>
    private void Start()
    {
        //カメラの対象位置の初期値を取得する
        defaultLookTranPos = transform.position;

        //「UI」以外の画面をタップされたら、カメラの対象位置を更新する
        this.UpdateAsObservable()
            .Where(_ => Input.GetMouseButtonDown(0) && Input.touchSupported && Input.touchCount == 1 && !cameraController.TouchingUI())
            .Subscribe(_ => UpdateLookTranPosAsync(this.GetCancellationTokenOnDestroy()).Forget())
            .AddTo(this);
    }

    /// <summary>
    /// カメラの対象位置を更新する
    /// </summary>
    /// <param name="token">CancellationToken</param>
    /// <returns>待ち時間</returns>
    private async UniTaskVoid UpdateLookTranPosAsync(CancellationToken token)
    {
        //触れた指が動いているなら、以降の処理を行わない
        if (Input.GetTouch(0).phase == TouchPhase.Moved) return;

        //タップした座標から光線を発射する
        Ray ray = Camera.main.ScreenPointToRay(Input.touches[0].position);

        //光線が何にも触れなかったら、以降の処理を行わない
        if (!Physics.Raycast(ray, out RaycastHit hit, rayLength)) return;

        //カメラの対象位置の座標を更新後、タップ時のエフェクトを生成して初期設定を行う
        transform.DOMove(hit.point, moveTime).OnComplete(() => Instantiate(tapEffectPrefab).SetUpTapEffectController(canvasRectTran, Vector3.zero));

        //カメラの対象位置の移動が完了するまで待つ
        await UniTask.Delay(TimeSpan.FromSeconds(moveTime), cancellationToken: token);
    }

    /// <summary>
    /// カメラの対象位置を初期値に戻す
    /// </summary>
    public void ResetLookTranPos() { transform.DOMove(defaultLookTranPos, moveTime); }
}