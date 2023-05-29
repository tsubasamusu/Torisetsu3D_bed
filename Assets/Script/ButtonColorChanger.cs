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
    /// ���̃{�^�����L�����ǂ���
    /// </summary>
    private bool isActive;

    /// <summary>
    /// �Q�[���J�n����ɌĂяo�����
    /// </summary>
    private void Start()
    {
        //�{�^���̃e�N�X�`����ʏ펞�̂���ɐݒ肷��
        rawImage.texture = buttonTexture_Normal;

        //�{�^�����L���ł͂Ȃ���Ԃɐ؂�ւ���
        isActive = false;
    }

    /// <summary>
    /// ���̃{�^���������ꂽ�ۂɌĂяo�����
    /// </summary>
    public void OnClicked()
    {
        //�{�^�����L�����ǂ�����؂�ւ���
        isActive = !isActive;

        //�{�^���̃e�N�X�`����ݒ肷��
        rawImage.texture = isActive ? buttonTexture_Active : buttonTexture_Normal;
    }
}