using UnityEngine;

/// <summary>
/// アニメーションを制御する
/// </summary>
public class AnimationController : MonoBehaviour
{
    /// <summary>
    /// ベッドのアニメーター
    /// </summary>
    [SerializeField]
    private Animator bedAnimator;

    /// <summary>
    /// アニメーションの名前
    /// </summary>
    [SerializeField]
    private string animationName;

    /// <summary>
    /// 再生速度アップ時の速度
    /// </summary>
    [SerializeField]
    private float speedUpVelocity;

    /// <summary>
    /// 再生速度ダウン時の速度
    /// </summary>
    [SerializeField]
    private float speedDownVelocity;

    /// <summary>
    /// アニメーション終了時にそのアニメーションの状態を維持時間する
    /// </summary>
    [SerializeField]
    private float keepTimeAtEnd;

    /// <summary>
    /// 巻き戻し中かどうか
    /// </summary>
    private bool isRewinding;

    /// <summary>
    /// アニメーションの再生速度の初期値
    /// </summary>
    private float defaultAnimationSpeed;

    /// <summary>
    /// ゲーム開始直後に呼び出される
    /// </summary>
    private void Start()
    {
        //アニメーションの再生速度の初期値を取得する
        defaultAnimationSpeed = bedAnimator.speed;

        //アニメーションの再生速度を「0」に設定する
        bedAnimator.speed = 0;
    }

    /// <summary>
    /// アニメーションを開始する
    /// </summary>
    public void StartAnimation()
    {
        //巻き戻し中ではない状態に切り替える
        isRewinding = false;

        //アニメーションの再生速度を「1」に設定する
        bedAnimator.SetFloat("Speed", 1f);

        //アニメーションの再生速度を初期値に設定する
        bedAnimator.speed = defaultAnimationSpeed;
    }

    /// <summary>
    /// アニメーションを停止する
    /// </summary>
    public void StopAnimation()
    {
        //アニメーションの再生速度を「0」に設定する
        bedAnimator.speed = 0;
    }

    /// <summary>
    /// アニメーションの再生速度を上げる
    /// </summary>
    public void SpeedUpAnimation()
    {
        //巻き戻し中ではない状態に切り替える
        isRewinding = false;

        //アニメーションの再生速度を「1」に設定する
        bedAnimator.SetFloat("Speed", 1f);

        //アニメーションの再生速度を設定する
        bedAnimator.speed = speedUpVelocity * defaultAnimationSpeed;
    }

    /// <summary>
    /// アニメーションの再生速度を下げる
    /// </summary>
    public void SlowDownAnimation()
    {
        //巻き戻し中ではない状態に切り替える
        isRewinding = false;

        //アニメーションの再生速度を「1」に設定する
        bedAnimator.SetFloat("Speed", 1f);

        //アニメーションの再生速度を設定する
        bedAnimator.speed = defaultAnimationSpeed / speedDownVelocity;
    }

    /// <summary>
    /// アニメーションを巻き戻す
    /// </summary>
    public void RewindAnimation()
    {
        //巻き戻し中なら、以降の処理を行わない
        if (isRewinding) return;

        //巻き戻し中である状態に切り替える
        isRewinding = true;

        //アニメーションの再生速度を「-1」に設定する
        bedAnimator.SetFloat("Speed", -1f);

        //アニメーションの再生速度を初期値に設定する
        bedAnimator.speed = defaultAnimationSpeed;
    }

    /// <summary>
    /// アニメーションをリセットする
    /// </summary>
    public void ResetAnimation()
    {
        //アニメーションを最初から再生する
        bedAnimator.Play(animationName, 0, 0);

        //アニメーションの再生速度を初期値に設定する
        bedAnimator.speed = defaultAnimationSpeed;
    }

    /// <summary>
    /// アニメーションを最初から再生する
    /// </summary>
    public void PlayAnimationFromOrigin()
    {
        //巻き戻し中ではない状態に切り替える
        isRewinding = false;

        //アニメーションの再生速度を「1」に設定する
        bedAnimator.SetFloat("Speed", 1f);

        //アニメーションの再生速度を初期値に設定する
        bedAnimator.speed = defaultAnimationSpeed;

        //アニメーションの状態を取得する
        AnimatorStateInfo stateInfo = bedAnimator.GetCurrentAnimatorStateInfo(0);

        //スクロールバーの値に対応した場所からアニメーションを再生する
        bedAnimator.Play(stateInfo.fullPathHash, -1, 0f);
    }

    public void OnAnimationEnd()
    {
        Debug.Log("END");
    }
}