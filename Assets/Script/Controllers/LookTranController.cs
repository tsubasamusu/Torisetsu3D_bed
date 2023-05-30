using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
using System.Linq;
using System.Threading;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �J�����̑Ώۈʒu�𐧌䂷��
/// </summary>
public class LookTranController : MonoBehaviour
{
    /// <summary>
    /// CameraController
    /// </summary>
    [SerializeField]
    private CameraController cameraController;

    /// <summary>
    /// �^�b�v���̃G�t�F�N�g�̃v���n�u
    /// </summary>
    [SerializeField]
    private TapEffectController tapEffectPrefab;

    /// <summary>
    /// �uCanvas�v�́uRectTransform�v
    /// </summary>
    [SerializeField]
    private RectTransform canvasRectTran;

    /// <summary>
    /// �����̒���
    /// </summary>
    [SerializeField]
    private float rayLength;

    /// <summary>
    /// �J�����̑Ώۈʒu�X�V���̈ړ�����
    /// </summary>
    [SerializeField]
    private float moveTime;

    /// <summary>
    /// �J�����̑Ώۈʒu�̏����l
    /// </summary>
    private Vector3 defaultLookTranPos;

    /// <summary>
    /// �Q�[���J�n����ɌĂяo�����
    /// </summary>
    private void Start()
    {
        //�J�����̑Ώۈʒu�̏����l���擾����
        defaultLookTranPos = transform.position;

        //�uUI�v�ȊO�̉�ʂ��^�b�v���ꂽ��A�J�����̑Ώۈʒu���X�V����
        this.UpdateAsObservable()
            .Where(_ => Input.GetMouseButtonDown(0) && Input.touchSupported && Input.touchCount == 1 && !cameraController.TouchingUI())
            .Subscribe(_ => UpdateLookTranPosAsync(this.GetCancellationTokenOnDestroy()).Forget())
            .AddTo(this);
    }

    /// <summary>
    /// �J�����̑Ώۈʒu���X�V����
    /// </summary>
    /// <param name="token">CancellationToken</param>
    /// <returns>�҂�����</returns>
    private async UniTaskVoid UpdateLookTranPosAsync(CancellationToken token)
    {
        //�G�ꂽ�w�������Ă���Ȃ�A�ȍ~�̏������s��Ȃ�
        if (Input.GetTouch(0).phase == TouchPhase.Moved) return;

        //�^�b�v�������W��������𔭎˂���
        Ray ray = Camera.main.ScreenPointToRay(Input.touches[0].position);

        //���������ɂ��G��Ȃ�������A�ȍ~�̏������s��Ȃ�
        if (!Physics.Raycast(ray, out RaycastHit hit, rayLength)) return;

        //�J�����̑Ώۈʒu�̍��W���X�V��A�^�b�v���̃G�t�F�N�g�𐶐����ď����ݒ���s��
        transform.DOMove(hit.point, moveTime).OnComplete(() => Instantiate(tapEffectPrefab).SetUpTapEffectController(canvasRectTran, Vector3.zero));

        //�J�����̑Ώۈʒu�̈ړ�����������܂ő҂�
        await UniTask.Delay(TimeSpan.FromSeconds(moveTime), cancellationToken: token);
    }

    /// <summary>
    /// �J�����̑Ώۈʒu�������l�ɖ߂�
    /// </summary>
    public void ResetLookTranPos() { transform.DOMove(defaultLookTranPos, moveTime); }
}