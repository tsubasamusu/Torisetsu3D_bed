using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// スクロールバーをもとにアニメーションを制御する
/// </summary>
public class ScrollbarToAnimation : MonoBehaviour
{
    /// <summary>
    /// ベッドのアニメーター
    /// </summary>
    [SerializeField]
    private Animator bedAnimator;

    /// <summary>
    /// スクロールバー
    /// </summary>
    [SerializeField]
    private Scrollbar scrollbar;

    /// <summary>
    /// スクロールバーが操作されているかどうか
    /// </summary>
    private bool scrollbarIsInteract;

    /// <summary>
    /// ゲーム開始直後に呼び出される
    /// </summary>
    private void Start()
    {
        //スクロールバーの値が変更された際に呼び出されるメソッドを登録する
        scrollbar.onValueChanged.AddListener(OnScrollbarValueChange);

        //毎フレーム行う処理
        this.UpdateAsObservable()
            .Subscribe(_ =>
            {
                //アニメーションの状態を取得する
                AnimatorStateInfo stateInfo = bedAnimator.GetCurrentAnimatorStateInfo(0);

                //スクロールバーが操作されていないなら、スクロールバーの値を更新する
                if (!scrollbarIsInteract) scrollbar.value = stateInfo.normalizedTime % 1;

                //1本指で触れていないなら、スクロールバーが操作されていない状態に切り替える
                if (!Input.GetMouseButton(0)) scrollbarIsInteract = false;
            })
            .AddTo(this);
    }

    /// <summary>
    /// スクロールバーの値が変更された際に呼び出される
    /// </summary>
    /// <param name="value"></param>
    private void OnScrollbarValueChange(float value)
    {
        //スクロールバーが操作されている状態に切り替える
        scrollbarIsInteract = true;

        //アニメーションの状態を取得する
        AnimatorStateInfo stateInfo = bedAnimator.GetCurrentAnimatorStateInfo(0);

        //スクロールバーの値に対応した場所からアニメーションを再生する
        bedAnimator.Play(stateInfo.fullPathHash, -1, value);
    }
}