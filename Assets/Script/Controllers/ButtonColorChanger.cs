using System.Collections.Generic;
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
    /// このボタンがホームボタンかどうか
    /// </summary>
    [SerializeField]
    private bool isHomeButton;

    /// <summary>
    /// このボタン以外のボタンのリスト
    /// </summary>
    [SerializeField, Header("このボタン以外のボタンのリスト")]
    private List<ButtonColorChanger> otherButtons = new();

    /// <summary>
    /// このボタンが有効かどうか
    /// </summary>
    private bool isActive;

    /// <summary>
    /// ゲーム開始直後に呼び出される
    /// </summary>
    private void Start()
    {
        //このボタンがホームボタンなら、以降の処理を行わない
        if (isHomeButton) return;

        //ボタンのテクスチャを通常時のそれに設定する
        rawImage.texture = buttonTexture_Normal;

        //ボタンが有効ではない状態に切り替える
        isActive = false;
    }

    /// <summary>
    /// 通常時のテクスチャに切り替える
    /// </summary>
    public void SetNormalTexture()
    {
        //ボタンが有効ではない状態に切り替える
        isActive = false;

        //ボタンのテクスチャを通常時のそれに設定する
        rawImage.texture = buttonTexture_Normal;
    }

    /// <summary>
    /// このボタンを押された際に呼び出される
    /// </summary>
    public void OnClicked()
    {
        //このボタン以外の全てのボタンのテクスチャを通常時のそれに設定する
        foreach (ButtonColorChanger button in otherButtons) { button.SetNormalTexture(); }

        //このボタンがホームボタンなら、以降の処理を行わない
        if (isHomeButton) return;

        //既にこのボタンが有効なら、以降の処理を行わない
        if (isActive) return;

        //ボタンが有効かどうかを切り替える
        isActive = !isActive;

        //ボタンのテクスチャを有効時のそれに設定する
        rawImage.texture = buttonTexture_Active;
    }
}