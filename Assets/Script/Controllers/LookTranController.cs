using System.Linq;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

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
    /// �����̒���
    /// </summary>
    [SerializeField]
    private float rayLength;

    /// <summary>
    /// �Q�[���J�n����ɌĂяo�����
    /// </summary>
    private void Start()
    {
        //�uUI�v�ȊO�̉�ʂ��^�b�v���ꂽ��A�J�����̑Ώۈʒu���X�V����
        this.UpdateAsObservable()
            .Where(_ => Input.GetMouseButtonDown(0) && Input.touchSupported && Input.touchCount == 1 && !cameraController.TouchingUI())
            .Subscribe(_ => UpdateLookTranPos())
            .AddTo(this);
    }

    /// <summary>
    /// �J�����̑Ώۈʒu���X�V����
    /// </summary>
    private void UpdateLookTranPos()
    {
        //�^�b�v�������W��������𔭎˂���
        Ray ray = Camera.main.ScreenPointToRay(Input.touches[0].position);

        //���������ɂ��G��Ȃ�������A�ȍ~�̏������s��Ȃ�
        if (!Physics.Raycast(ray, out RaycastHit hit, rayLength)) return;

        //���g�̍��W�i�J�����̑Ώۈʒu�j��ݒ肷��
        transform.position = hit.point;
    }
}