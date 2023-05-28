using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UniRx.Triggers;
using UniRx;

/// <summary>
/// �v���O���X�o�[�𐧌䂷��
/// </summary>
public class ProgressBarController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    /// <summary>
    /// �x�b�h�̃A�j���[�^�[
    /// </summary>
    [SerializeField]
    private Animator bedAnimator;

    /// <summary>
    /// �A�j���[�V�����̖��O
    /// </summary>
    [SerializeField]
    private string animationName;

    /// <summary>
    /// �X�N���[���o�[
    /// </summary>
    [SerializeField]
    private Scrollbar scrollbar;

    /// <summary>
    /// �h���b�O�����ǂ���
    /// </summary>
    private bool isDragging;

    /// <summary>
    /// �Q�[���J�n����ɌĂяo�����
    /// </summary>
    private void Start()
    {
        //���t���[���A�X�N���[���o�[�̒l���X�V����
        this.UpdateAsObservable()
            .Where(_ => !isDragging)
            .Subscribe(_ => UpdateScrollbarValue())
            .AddTo(this);
    }

    /// <summary>
    /// �X�N���[���o�[�̒l���X�V����
    /// </summary>
    private void UpdateScrollbarValue()
    {
        //�A�j���[�V�����̏�Ԃ��擾����
        AnimatorStateInfo stateInfo = bedAnimator.GetCurrentAnimatorStateInfo(0);

        //�擾�����A�j���[�V�����̖��O���ݒ�l�ł͂Ȃ��Ȃ�A�ȍ~�̏������s��Ȃ�
        if (!stateInfo.IsName(animationName)) return;

        //�X�N���[���o�[�̒l��ݒ肷��
        scrollbar.value = stateInfo.normalizedTime % 1;
    }

    /// <summary>
    /// �X�N���[���o�[�������ꂽ�ۂɌĂяo�����
    /// </summary>
    /// <param name="eventData">�Ώۂ̃C�x���g�̃f�[�^</param>
    public void OnPointerDown(PointerEventData eventData)
    {
        //�h���b�O���ł����Ԃɐ؂�ւ���
        isDragging = true;


        UIInteractionManager.Instance.IsScrollbarInteracting = true;
    }

    /// <summary>
    /// �X�N���[���o�[�𗣂��ꂽ�ۂɌĂяo�����
    /// </summary>
    /// <param name="eventData">�Ώۂ̃C�x���g�̃f�[�^</param>
    public void OnPointerUp(PointerEventData eventData)
    {
        //�h���b�O���ł͂Ȃ���Ԃɐ؂�ς���
        isDragging = false;

        UIInteractionManager.Instance.IsScrollbarInteracting = false;

        //�X�N���[���o�[�̒l�ɑΉ������n�_����A�j���[�V�������Đ�����
        bedAnimator.Play(animationName, -1, scrollbar.value);
    }
}