using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �{�^���̐F�̕ύX�𐧌䂷��
/// </summary>
public class ButtonColorChanger : MonoBehaviour
{
    /// <summary>
    /// �ʏ펞�̃{�^���̃e�N�X�`��
    /// </summary>
    [SerializeField]
    private Texture buttonTexture_Normal;

    /// <summary>
    /// �L�����̃{�^���̃e�N�X�`��
    /// </summary>
    [SerializeField]
    private Texture buttonTexture_Active;

    /// <summary>
    /// ���̃{�^���́uRawImage�v
    /// </summary>
    [SerializeField]
    private RawImage rawImage;

    /// <summary>
    /// ���̃{�^�����z�[���{�^�����ǂ���
    /// </summary>
    [SerializeField]
    private bool isHomeButton;

    /// <summary>
    /// ���̃{�^���ȊO�̃{�^���̃��X�g
    /// </summary>
    [SerializeField, Header("���̃{�^���ȊO�̃{�^���̃��X�g")]
    private List<ButtonColorChanger> otherButtons = new();

    /// <summary>
    /// ���̃{�^�����L�����ǂ���
    /// </summary>
    private bool isActive;

    /// <summary>
    /// �Q�[���J�n����ɌĂяo�����
    /// </summary>
    private void Start()
    {
        //���̃{�^�����z�[���{�^���Ȃ�A�ȍ~�̏������s��Ȃ�
        if (isHomeButton) return;

        //�{�^���̃e�N�X�`����ʏ펞�̂���ɐݒ肷��
        rawImage.texture = buttonTexture_Normal;

        //�{�^�����L���ł͂Ȃ���Ԃɐ؂�ւ���
        isActive = false;
    }

    /// <summary>
    /// �ʏ펞�̃e�N�X�`���ɐ؂�ւ���
    /// </summary>
    public void SetNormalTexture()
    {
        //�{�^�����L���ł͂Ȃ���Ԃɐ؂�ւ���
        isActive = false;

        //�{�^���̃e�N�X�`����ʏ펞�̂���ɐݒ肷��
        rawImage.texture = buttonTexture_Normal;
    }

    /// <summary>
    /// ���̃{�^���������ꂽ�ۂɌĂяo�����
    /// </summary>
    public void OnClicked()
    {
        //���̃{�^���ȊO�̑S�Ẵ{�^���̃e�N�X�`����ʏ펞�̂���ɐݒ肷��
        foreach (ButtonColorChanger button in otherButtons) { button.SetNormalTexture(); }

        //���̃{�^�����z�[���{�^���Ȃ�A�ȍ~�̏������s��Ȃ�
        if (isHomeButton) return;

        //���ɂ��̃{�^�����L���Ȃ�A�ȍ~�̏������s��Ȃ�
        if (isActive) return;

        //�{�^�����L�����ǂ�����؂�ւ���
        isActive = !isActive;

        //�{�^���̃e�N�X�`����L�����̂���ɐݒ肷��
        rawImage.texture = buttonTexture_Active;
    }
}