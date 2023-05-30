using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �^�b�v���̃G�t�F�N�g�𐧌䂷��
/// </summary>
public class TapEffectController : MonoBehaviour
{
    /// <summary>
    /// �X�v���C�g�Ԃ̕b��
    /// </summary>
    [SerializeField, Range(0f, 0.01f)]
    private float timeBtweenSprites;

    /// <summary>
    /// �^�b�v���̃G�t�F�N�g�\���p�̃C���[�W
    /// </summary>
    [SerializeField]
    private Image imgTapEffect;

    /// <summary>
    /// �^�b�v���̃G�t�F�N�g�̃X�v���C�g�̃��X�g
    /// </summary>
    [SerializeField]
    private List<Sprite> tapEffectSprites = new();

    /// <summary>
    /// �uTapEffectController�v�N���X�̏����ݒ���s��
    /// </summary>
    /// <param name="canvasTran">�uCanvas�v�̈ʒu���</param>
    /// <param name="pos">�G�t�F�N�g�̍��W</param>
    public void SetUpTapEffectController(Transform canvasTran, Vector3 pos)
    {
        //���g�̐e��ݒ肷��
        transform.SetParent(canvasTran);

        //���g�̍��W��ݒ肷��
        transform.localPosition = pos;

        //�^�b�v���̃G�t�F�N�g���s��
        PlayTapEffectAsync(this.GetCancellationTokenOnDestroy()).Forget();
    }

    /// <summary>
    /// �^�b�v���̃G�t�F�N�g���s��
    /// </summary>
    /// <param name="token">CancellationToken</param>
    /// <returns>�҂�����</returns>
    private async UniTaskVoid PlayTapEffectAsync(CancellationToken token)
    {
        //�^�b�v���̃G�t�F�N�g�̃X�v���C�g�̐������J��Ԃ�
        for (int i = 0; i < tapEffectSprites.Count; i++)
        {
            //�C���[�W�̃X�v���C�g��ݒ肷��
            imgTapEffect.sprite = tapEffectSprites[i];

            //��莞�ԑ҂�
            await UniTask.Delay(TimeSpan.FromSeconds(timeBtweenSprites), cancellationToken: token);
        }

        //�^�b�v���̃G�t�F�N�g�̃X�v���C�g�̐������J��Ԃ�
        for (int i = 0; i < tapEffectSprites.Count; i++)
        {
            //�C���[�W�̃X�v���C�g��ݒ肷��
            imgTapEffect.sprite = tapEffectSprites[(tapEffectSprites.Count - 1) - i];

            //��莞�ԑ҂�
            await UniTask.Delay(TimeSpan.FromSeconds(timeBtweenSprites), cancellationToken: token);
        }

        //���̃Q�[���I�u�W�F�N�g������
        Destroy(gameObject);
    }
}