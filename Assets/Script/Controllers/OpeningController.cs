using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �I�[�v�j���O�𐧌䂷��
/// </summary>
public class OpeningController : MonoBehaviour
{
    /// <summary>
    /// �t�F�[�h�A�E�g����
    /// </summary>
    [SerializeField]
    private float fadeOutTime;

    /// <summary>
    /// �X�v���C�g�Ԃ̕b��
    /// </summary>
    [SerializeField, Range(0f, 0.1f)]
    private float timeBtweenSprites;

    /// <summary>
    /// �w�i�̃C���[�W
    /// </summary>
    [SerializeField]
    private Image imgBackground;

    /// <summary>
    /// �����摜�\���p�C���[�W
    /// </summary>
    [SerializeField]
    private Image imgInformation;

    /// <summary>
    /// AnimationController
    /// </summary>
    [SerializeField]
    private AnimationController animationController;

    /// <summary>
    /// �Đ��{�^���́uButtonColorChanger�v
    /// </summary>
    [SerializeField]
    private ButtonColorChanger btnStart;

    /// <summary>
    /// �����摜�̃X�v���C�g�̃��X�g
    /// </summary>
    [SerializeField]
    private List<Sprite> informationSprites = new();

    /// <summary>
    /// �Q�[���J�n����ɌĂяo�����
    /// </summary>
    private void Start()
    {
        //�I�[�v�j���O���J�n����
        StartOpeningAsync(this.GetCancellationTokenOnDestroy()).Forget();
    }

    /// <summary>
    /// �I�[�v�j���O���J�n����
    /// </summary>
    /// <param name="token">CancellationToken</param>
    /// <returns>�҂�����</returns>
    private async UniTaskVoid StartOpeningAsync(CancellationToken token)
    {
        //�����摜�̐������J��Ԃ�
        for (int i = 0; i < informationSprites.Count; i++)
        {
            //�����摜�\���p�C���[�W�̃X�v���C�g��ݒ肷��
            imgInformation.sprite = informationSprites[i];

            //��莞�ԑ҂�
            await UniTask.Delay(TimeSpan.FromSeconds(timeBtweenSprites), cancellationToken: token);
        }

        //�w�i�Ɛ����摜����莞�Ԃ����ď��X�ɔ�\���ɂ���
        imgBackground.DOFade(0f, fadeOutTime).OnComplete(() => Destroy(imgBackground.gameObject));
        imgInformation.DOFade(0f, fadeOutTime).OnComplete(() => Destroy(imgInformation.gameObject));

        //��莞�ԑ҂�
        await UniTask.Delay(TimeSpan.FromSeconds(fadeOutTime), cancellationToken: token);

        //�A�j���[�V�������J�n����
        animationController.StartAnimation();

        //�Đ��{�^���������ꂽ�ۂ̏������s��
        btnStart.OnClicked();

        //�s�v�ɂȂ����Q�[���I�u�W�F�N�g������
        Destroy(gameObject);
    }
}