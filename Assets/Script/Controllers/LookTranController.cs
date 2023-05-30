using System.Linq;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

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
    /// 「Canvas」の「RectTransform」
    /// </summary>
    [SerializeField]
    private RectTransform canvasRectTran;

    /// <summary>
    /// タップ時のエフェクトのプレハブ
    /// </summary>
    [SerializeField]
    private TapEffectController tapEffectPrefab;

    /// <summary>
    /// 光線の長さ
    /// </summary>
    [SerializeField]
    private float rayLength;

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
            .Subscribe(_ => UpdateLookTranPos())
            .AddTo(this);
    }

    /// <summary>
    /// カメラの対象位置を更新する
    /// </summary>
    private void UpdateLookTranPos()
    {
        //タップした座標から光線を発射する
        Ray ray = Camera.main.ScreenPointToRay(Input.touches[0].position);

        //光線が何にも触れなかったら、以降の処理を行わない
        if (!Physics.Raycast(ray, out RaycastHit hit, rayLength)) return;

        //自身の座標（カメラの対象位置）を設定する
        transform.position = hit.point;

        //エフェクトを生成する
        GenerateEffect();
    }

    /// <summary>
    /// エフェクトを生成する
    /// </summary>
    private void GenerateEffect()
    {
        //タップ時のエフェクトを生成する
        TapEffectController tapEffectController = Instantiate(tapEffectPrefab);

        //生成したエフェクトの初期設定を行う
        tapEffectController.SetUpTapEffectController(canvasRectTran, Vector3.zero);
    }

    /// <summary>
    /// カメラの対象位置を初期値に戻す
    /// </summary>
    public void ResetLookTranPos() { transform.position = defaultLookTranPos; }
}