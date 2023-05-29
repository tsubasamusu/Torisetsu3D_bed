using UnityEngine;

/// <summary>
/// スクロールバーが
/// </summary>
public class ScrollbarInteractionManager : MonoBehaviour
{
    /// <summary>
    /// 「ScrollbarInteractionManager」クラスのインスタンス
    /// </summary>
    public static ScrollbarInteractionManager instance;

    /// <summary>
    /// スクロールバーが操作されているかどうか
    /// </summary>
    [HideInInspector]
    public bool scrollbarIsInteract;

    /// <summary>
    /// シングルトンに必須の記述
    /// </summary>
    private void Awake() { if (instance == null) instance = this; else Destroy(gameObject); }
}