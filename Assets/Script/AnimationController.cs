using UniRx;
using UniRx.Triggers;
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

    [SerializeField]
    private float speedUpVelocity = 1.5f;

    [SerializeField]
    private float speedDownVelocity = 1.5f;

    private bool isReturn;

    /// <summary>
    /// アニメーションの再生速度の初期値
    /// </summary>
    private float defaultAnimationSpeed;

    /// <summary>
    /// 現在のアニメーションの再生速度
    /// </summary>
    private float currentAnimationSpeed;

    /// <summary>
    /// ゲーム開始直後に呼び出される
    /// </summary>
    private void Start()
    {
        //アニメーションの再生速度の初期値を取得する
        defaultAnimationSpeed = bedAnimator.speed;

        //アニメーションの再生速度を「0」に設定する
        bedAnimator.speed = 0;

        //現在のアニメーションの再生速度を毎フレーム取得する
        this.UpdateAsObservable()
            .Subscribe(_ => currentAnimationSpeed = bedAnimator.speed)
            .AddTo(this);
    }


    /// <summary>
    /// アニメーションを開始する
    /// </summary>
    public void StartAnimation()
    {
        isReturn = false;

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
        isReturn = false;

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
        isReturn = false;

        //アニメーションの再生速度を「1」に設定する
        bedAnimator.SetFloat("Speed", 1f);

        //アニメーションの再生速度を設定する
        bedAnimator.speed = defaultAnimationSpeed / speedDownVelocity;
    }

    /// <summary>
    /// 
    /// </summary>
    public void ReturnAnimation()
    {
        if (isReturn) return;

        isReturn = true;

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
}