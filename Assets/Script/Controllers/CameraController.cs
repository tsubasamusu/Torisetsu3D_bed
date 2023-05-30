using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.EventSystems;

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
    private float distance;

    /// <summary>
    /// ズーム速度
    /// </summary>
    [SerializeField]
    private float zoomSpeed;

    /// <summary>
    /// 対象物との最小距離
    /// </summary>
    [SerializeField]
    private float minDistance;

    /// <summary>
    /// 対象物との最大距離
    /// </summary>
    [SerializeField]
    private float maxDistance;

    /// <summary>
    /// 「x」方向の速度
    /// </summary>
    [SerializeField]
    private float xSpeed;

    /// <summary>
    /// 「y」方向の速度
    /// </summary>
    [SerializeField]
    private float ySpeed;

    /// <summary>
    /// 「y」方向の最小角度
    /// </summary>
    [SerializeField]
    private float minAngleY;

    /// <summary>
    /// 「y」方向の最大角度
    /// </summary>
    [SerializeField]
    private float maxAngleY;

    /// <summary>
    /// 現在の「x」角度
    /// </summary>
    private float currentAngleX;

    /// <summary>
    /// 現在の「y」角度
    /// </summary>
    private float currentAngleY;

    /// <summary>
    /// 対象物との距離の初期値
    /// </summary>
    private float defaultDistance;

    /// <summary>
    /// カメラの座標の初期値
    /// </summary>
    private Vector3 defaultCameraPos;

    /// <summary>
    /// カメラの角度の初期値
    /// </summary>
    private Vector3 defaultAngles;

    /// <summary>
    /// ゲーム開始直後に呼び出される
    /// </summary>
    private void Start()
    {
        //対象物との距離の初期値を取得する
        distance = defaultDistance = (transform.position - lookTran.position).magnitude;

        //カメラの座標の初期値を取得する
        defaultCameraPos = transform.position;

        //カメラの角度の初期値を取得する
        defaultAngles = transform.eulerAngles;

        //カメラの「x」角度の初期値を保持する
        currentAngleX = transform.eulerAngles.y;

        //カメラの「y」角度の初期値を保持する
        currentAngleY = transform.eulerAngles.x;

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
        //指が「UI」に触れているなら、以降の処理を行わない
        if (TouchingUI()) return;

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
        //カメラの座標を維持する
        transform.position = transform.rotation * new Vector3(0.0f, 0.0f, -distance) + lookTran.position;

        //指が「UI」に触れているなら、以降の処理を行わない
        if (TouchingUI()) return;

        //1本指でタッチされていないなら、以降の処理を行わない
        if (!Input.touchSupported || Input.touchCount != 1) return;

        //タッチの情報を取得する
        Touch touch = Input.GetTouch(0);

        //各方向の移動量を調整する
        float moveValueX = touch.phase == TouchPhase.Moved ? -touch.deltaPosition.x : 0f;
        float moveValueY = touch.phase == TouchPhase.Moved ? -touch.deltaPosition.y : 0f;

        //現在の各角度を更新する
        currentAngleX += moveValueX * xSpeed * Time.deltaTime;
        currentAngleY -= moveValueY * ySpeed * Time.deltaTime;

        //現在の「y」角度を調整する
        currentAngleY = ClampAngle(currentAngleY, minAngleY, maxAngleY);

        //適切な対象物との距離を取得する
        distance = Mathf.Clamp(distance, minDistance, maxDistance);

        //適切なカメラの角度を取得する
        Quaternion rotation = Quaternion.Euler(currentAngleY, currentAngleX, 0);

        //カメラの角度を設定する
        transform.rotation = rotation;

        //カメラの座標を設定する
        transform.position = (rotation * new Vector3(0.0f, 0.0f, -distance)) + lookTran.position;
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

    /// <summary>
    /// 「プレイヤーが『UI』をに触れているかどうか」を取得する
    /// </summary>
    /// <returns>プレイヤーが「UI」に触れているかどうか</returns>
    public bool TouchingUI()
    {
        //全ての「Touch」を取得する
        Touch[] touches = Input.touches;

        //画面に触れている指の数だけ繰り返す
        for (int i = 0; i < touches.Length; i++)
        {
            //「PointerEventData」を作成する
            PointerEventData pointData = new(EventSystem.current)
            {
                //「PointerEventData」の座標を設定する
                position = touches[i].position
            };

            //「RaycastAll」の結果格納用のリストを作成する
            List<RaycastResult> rayResults = new();

            //指に触れている全てのオブジェクト取得する
            EventSystem.current.RaycastAll(pointData, rayResults);

            //指に触れているオブジェクトの数だけ繰り返す
            for (int j = 0; j < rayResults.Count; j++)
            {
                //指に触れているオブジェクトの1つが「UI」なら、「true」を返す
                if (rayResults[j].gameObject.CompareTag("UI")) return true;
            }
        }

        //「flase」を返す
        return false;
    }

    /// <summary>
    /// カメラの座標を初期化する
    /// </summary>
    public void ResetCameraPos()
    {
        //対象物との距離を初期値に設定する
        distance = defaultDistance;

        //カメラの座標を初期値に設定する
        transform.position = defaultCameraPos;

        //現在のカメラの「x」角度を初期値に設定する
        currentAngleX = defaultAngles.y;

        //現在のカメラの「y」角度を初期値に設定する
        currentAngleY = defaultAngles.x;

        //カメラの角度を初期値に設定する
        transform.eulerAngles = defaultAngles;
    }
}