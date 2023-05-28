using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UniRx.Triggers;
using UniRx;

/// <summary>
/// プログレスバーを制御する
/// </summary>
public class ProgressBarController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
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
    /// スクロールバー
    /// </summary>
    [SerializeField]
    private Scrollbar scrollbar;

    /// <summary>
    /// ドラッグ中かどうか
    /// </summary>
    private bool isDragging;

    /// <summary>
    /// ゲーム開始直後に呼び出される
    /// </summary>
    private void Start()
    {
        //毎フレーム、スクロールバーの値を更新する
        this.UpdateAsObservable()
            .Where(_ => !isDragging)
            .Subscribe(_ => UpdateScrollbarValue())
            .AddTo(this);
    }

    /// <summary>
    /// スクロールバーの値を更新する
    /// </summary>
    private void UpdateScrollbarValue()
    {
        //アニメーションの状態を取得する
        AnimatorStateInfo stateInfo = bedAnimator.GetCurrentAnimatorStateInfo(0);

        //取得したアニメーションの名前が設定値ではないなら、以降の処理を行わない
        if (!stateInfo.IsName(animationName)) return;

        //スクロールバーの値を設定する
        scrollbar.value = stateInfo.normalizedTime % 1;
    }

    /// <summary>
    /// スクロールバーを押された際に呼び出される
    /// </summary>
    /// <param name="eventData">対象のイベントのデータ</param>
    public void OnPointerDown(PointerEventData eventData)
    {
        //ドラッグ中である状態に切り替える
        isDragging = true;


        UIInteractionManager.Instance.IsScrollbarInteracting = true;
    }

    /// <summary>
    /// スクロールバーを離された際に呼び出される
    /// </summary>
    /// <param name="eventData">対象のイベントのデータ</param>
    public void OnPointerUp(PointerEventData eventData)
    {
        //ドラッグ中ではない状態に切り変える
        isDragging = false;

        UIInteractionManager.Instance.IsScrollbarInteracting = false;

        //スクロールバーの値に対応した地点からアニメーションを再生する
        bedAnimator.Play(animationName, -1, scrollbar.value);
    }
}