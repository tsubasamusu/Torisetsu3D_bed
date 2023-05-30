using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

/// <summary>
/// �I�[�v�j���O�𐧌䂷��
/// </summary>
public class OpeningController : MonoBehaviour
{
    /// <summary>
    /// VideoPlayer
    /// </summary>
    [SerializeField]
    private VideoPlayer videoPlayer;

    /// <summary>
    /// �w�i�̃C���[�W
    /// </summary>
    [SerializeField]
    private Image imgBackground;

    /// <summary>
    /// �C���t�H���[�V�����\���p�́uRawImage�v
    /// </summary>
    [SerializeField]
    private RawImage informationRawImage;

    /// <summary>
    /// �w�i�̐F
    /// </summary>
    [SerializeField]
    private Color backgroundColor;

    /// <summary>
    /// �Q�[���J�n����ɌĂяo�����
    /// </summary>
    private void Start()
    {
        //�I�[�v�j���O���Đ�����
        PlayOpeningAsync(this.GetCancellationTokenOnDestroy()).Forget();
    }

    /// <summary>
    /// �I�[�v�j���O���Đ�����
    /// </summary>
    /// <param name="token">CancellationToken</param>
    /// <returns>�҂�����</returns>
    private async UniTaskVoid PlayOpeningAsync(CancellationToken token)
    {
        //�w�i�̐F��ݒ肷��
        imgBackground.color = backgroundColor;

        imgBackground.DOFade(0f, 0f);

        //��莞�ԑ҂�
        await UniTask.Delay(TimeSpan.FromSeconds(0.5f), cancellationToken: token);
    }
}