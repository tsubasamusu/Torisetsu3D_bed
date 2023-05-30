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
    /// ����̍Đ��O�E�Đ���̎��ԊԊu
    /// </summary>
    [SerializeField]
    private float bothSideVideoTime;

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
    /// ��������\���p�́uRawImage�v
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

        //��������\���p�́uRawImage�v��񊈐�������
        informationRawImage.gameObject.SetActive(false);

        //��莞�ԑ҂�
        await UniTask.Delay(TimeSpan.FromSeconds(bothSideVideoTime), cancellationToken: token);

        //����������Đ�����
        videoPlayer.Play();

        //��������\���p�́uRawImage�v������������
        informationRawImage.gameObject.SetActive(true);

        //�uOnVideoEnd()�v��o�^����
        videoPlayer.loopPointReached += OnVideoEnd;
    }

    /// <summary>
    /// ����̍Đ����I�������ۂɌĂяo�����
    /// </summary>
    /// <param name="videoPlayer">������Đ����Ă����uVideoPlayer�v</param>
    private void OnVideoEnd(VideoPlayer videoPlayer)
    {
        //��莞�Ԃ����āuRawImage�v�����X�ɔ�\���ɂ���
        informationRawImage.DOFade(0f, bothSideVideoTime).OnComplete(() => Destroy(informationRawImage.gameObject));

        //��莞�Ԃ����Ĕw�i�����X�ɔ�\���ɂ���
        imgBackground.DOFade(0f, bothSideVideoTime).OnComplete(() => Destroy(imgBackground.gameObject));
    }
}