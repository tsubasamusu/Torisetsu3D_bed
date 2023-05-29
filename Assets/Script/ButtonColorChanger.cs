using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ボタンの色の変更を制御する
/// </summary>
public class ButtonColorChanger : MonoBehaviour
{
    /// <summary>
    /// 通常時のボタンのテクスチャ
    /// </summary>
    [SerializeField]
    private Texture buttonTexture_Normal;

    /// <summary>
    /// 有効時のボタンのテクスチャ
    /// </summary>
    [SerializeField]
    private Texture buttonTexture_Active;

    /// <summary>
    /// このボタンの「RawImage」
    /// </summary>
    [SerializeField]
    private RawImage rawImage;

    /// <summary>
    /// このボタンが有効かどうか
    /// </summary>
    private bool isActive;

    /// <summary>
    /// ゲーム開始直後に呼び出される
    /// </summary>
    private void Start()
    {
        //ボタンのテクスチャを通常時のそれに設定する
        rawImage.texture = buttonTexture_Normal;

        //ボタンが有効ではない状態に切り替える
        isActive = false;
    }

    /// <summary>
    /// このボタンを押された際に呼び出される
    /// </summary>
    public void OnClicked()
    {
        //ボタンが有効かどうかを切り替える
        isActive = !isActive;

        //ボタンのテクスチャを設定する
        rawImage.texture = isActive ? buttonTexture_Active : buttonTexture_Normal;
    }
}