using UniRx;
using UniRx.Triggers;
using UnityEngine;

/// <summary>
/// カメラを制御する
/// </summary>
public class CameraController : MonoBehaviour
{
    /// <summary>
    /// 対象物の位置
    /// </summary>
    [SerializeField]
    private Transform lookTran;

    /// <summary>
    /// 対象物との距離
    /// </summary>
    [SerializeField]
    private float distance = 5.0f;

    /// <summary>
    /// ズーム速度
    /// </summary>
    [SerializeField]
    private float zoomSpeed = 1.0f;

    /// <summary>
    /// 対象物との最小距離
    /// </summary>
    [SerializeField]
    private float minDistance = 1.0f;

    /// <summary>
    /// 対象物との最大距離
    /// </summary>
    [SerializeField]
    private float maxDistance = 20.0f;

    /// <summary>
    /// 「x」方向の速度
    /// </summary>
    [SerializeField]
    private float xSpeed = 120.0f;

    /// <summary>
    /// 「y」方向の速度
    /// </summary>
    [SerializeField]
    private float ySpeed = 120.0f;

    /// <summary>
    /// 「y」方向の最小角度
    /// </summary>
    [SerializeField]
    private float minAngleY = -20f;

    /// <summary>
    /// 「y」方向の最大角度
    /// </summary>
    [SerializeField]
    private float maxAngleY = 80f;

    /// <summary>
    /// 現在の「x」角度
    /// </summary>
    private float currentAngleX = 0.0f;

    /// <summary>
    /// 現在の「y」角度
    /// </summary>
    private float currentAngleY = 0.0f;

    /// <summary>
    /// ゲーム開始直後に呼び出される
    /// </summary>
    private void Start()
    {
        //カメラの角度の初期値を取得する
        Vector3 firstCameraAngles = transform.eulerAngles;

        //カメラの「x」角度の初期値を保持する
        currentAngleX = firstCameraAngles.y;

        //カメラの「y」角度の初期値を保持する
        currentAngleY = firstCameraAngles.x;

        //毎フレーム、対象物との距離を更新する
        this.UpdateAsObservable()
            .Subscribe(_ => UpdateDistance())
            .AddTo(this);

        //毎フレームの計算後にカメラの角度と座標を更新する
        this.LateUpdateAsObservable()
            .Subscribe(_ => UpdateAngleAndPos())
            .AddTo(this);
    }

    /// <summary>
    /// 対象物との距離を更新する
    /// </summary>
    private void UpdateDistance()
    {
        //スクロールされた量を取得する
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        //スクロールされた量をもとに、対象物との距離を取得する
        distance = Mathf.Clamp(distance - (scroll * zoomSpeed * Time.deltaTime), minDistance, maxDistance);

        //2本指でタッチされていないなら、以降の処理を行わない
        if (!Input.touchSupported || Input.touchCount != 2) return;

        //1本目の指のタッチの情報を取得する
        Touch touch1 = Input.GetTouch(0);

        //2本目の指のタッチの情報を取得する
        Touch touch2 = Input.GetTouch(1);

        //1本目か2本目の指が動いていないなら、以降の処理を行わない
        if (touch1.phase != TouchPhase.Moved || touch2.phase != TouchPhase.Moved) return;

        //対象物との適切な距離を取得する
        Vector2 prevTouch1 = touch1.position - touch1.deltaPosition;
        Vector2 prevTouch2 = touch2.position - touch2.deltaPosition;
        float prevTouchDeltaMag = (prevTouch1 - prevTouch2).magnitude;
        float touchDeltaMag = (touch1.position - touch2.position).magnitude;
        float deltaMagnitudeDiff = touchDeltaMag - prevTouchDeltaMag;
        distance = Mathf.Clamp(distance + (deltaMagnitudeDiff * zoomSpeed * Time.deltaTime), minDistance, maxDistance);
    }

    /// <summary>
    /// カメラの角度と座標を更新する
    /// </summary>
    private void UpdateAngleAndPos()
    {
        //スクロールバーが操作されているなら、以降の処理を行わない
        if (ScrollbarInteractionManager.instance.scrollbarIsInteract) return;

        //1本指でタッチされていないなら、以降の処理を行わない
        if (!Input.GetMouseButton(0) && (!Input.touchSupported || Input.touchCount != 1)) return;

        //if (Input.GetMouseButton(0) || (Input.touchSupported && Input.touchCount == 1))

        //「x」方向の移動量を取得する
        float moveValueX = Input.GetAxis("Mouse X") * xSpeed * Time.deltaTime;

        //「y」方向の移動量を取得する
        float moveValueY = Input.GetAxis("Mouse Y") * ySpeed * Time.deltaTime;

        //使用デバイスがタッチ入力をサポートしているなら
        if (Input.touchSupported)
        {
            //タッチの情報を取得する
            Touch touch = Input.GetTouch(0);

            //各方向の移動量を調整する
            moveValueX = touch.phase == TouchPhase.Moved ? -touch.deltaPosition.x * xSpeed * 0.005f : 0f;
            moveValueY = touch.phase == TouchPhase.Moved ? -touch.deltaPosition.y * ySpeed * 0.005f : 0f;
        }

        //現在の各角度を更新する
        currentAngleX += moveValueX;
        currentAngleY -= moveValueY;

        //現在の「y」角度を調整する
        currentAngleY = ClampAngle(currentAngleY, minAngleY, maxAngleY);

        //適切な対象物との距離を取得する
        distance = Mathf.Clamp(distance, minDistance, maxDistance);

        //適切なカメラの角度を取得する
        Quaternion rotation = Quaternion.Euler(currentAngleY, currentAngleX, 0);

        //カメラの角度を設定する
        transform.rotation = rotation;

        //カメラの座標を設定する
        transform.position = rotation * new Vector3(0.0f, 0.0f, -distance) + lookTran.position;
    }

    /// <summary>
    /// 角度を制限する
    /// </summary>
    /// <param name="angle">角度</param>
    /// <param name="min">最小値</param>
    /// <param name="max">最大値</param>
    /// <returns>制限後の角度</returns>
    private float ClampAngle(float angle, float min, float max)
    {
        //受け取った角度を適切なものに調整する
        if (angle < -360f) angle += 360f;
        if (angle > 360f) angle -= 360f;

        //調整後の角度を制限して返す
        return Mathf.Clamp(angle, min, max);
    }
}