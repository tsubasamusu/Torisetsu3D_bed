using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UniRx.Triggers;
using UniRx;

/// <summary>
/// �X�N���[���o�[�𐧌䂷��
/// </summary>
public class ScrollbarController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
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

        //�X�N���[���o�[�����삳��Ă����Ԃɐ؂�ւ���
        ScrollbarInteractionManager.instance.scrollbarIsInteract = true;
    }

    /// <summary>
    /// �X�N���[���o�[�𗣂��ꂽ�ۂɌĂяo�����
    /// </summary>
    /// <param name="eventData">�Ώۂ̃C�x���g�̃f�[�^</param>
    public void OnPointerUp(PointerEventData eventData)
    {
        //�h���b�O���ł͂Ȃ���Ԃɐ؂�ς���
        isDragging = false;

        //�X�N���[���o�[�����삳��Ă��Ȃ���Ԃɐ؂�ւ���
        ScrollbarInteractionManager.instance.scrollbarIsInteract = false;

        //�X�N���[���o�[�̒l�ɑΉ������n�_����A�j���[�V�������Đ�����
        bedAnimator.Play(animationName, -1, scrollbar.value);
    }
}