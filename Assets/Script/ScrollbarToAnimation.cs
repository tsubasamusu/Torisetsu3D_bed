using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �X�N���[���o�[�����ƂɃA�j���[�V�����𐧌䂷��
/// </summary>
public class ScrollbarToAnimation : MonoBehaviour
{
    /// <summary>
    /// �x�b�h�̃A�j���[�^�[
    /// </summary>
    [SerializeField]
    private Animator bedAnimator;

    /// <summary>
    /// �X�N���[���o�[
    /// </summary>
    [SerializeField]
    private Scrollbar scrollbar;

    /// <summary>
    /// �X�N���[���o�[�����삳��Ă��邩�ǂ���
    /// </summary>
    private bool scrollbarIsInteract;

    /// <summary>
    /// �Q�[���J�n����ɌĂяo�����
    /// </summary>
    private void Start()
    {
        //�X�N���[���o�[�̒l���ύX���ꂽ�ۂɌĂяo����郁�\�b�h��o�^����
        scrollbar.onValueChanged.AddListener(OnScrollbarValueChange);

        //���t���[���s������
        this.UpdateAsObservable()
            .Subscribe(_ =>
            {
                //�A�j���[�V�����̏�Ԃ��擾����
                AnimatorStateInfo stateInfo = bedAnimator.GetCurrentAnimatorStateInfo(0);

                //�X�N���[���o�[�����삳��Ă��Ȃ��Ȃ�A�X�N���[���o�[�̒l���X�V����
                if (!scrollbarIsInteract) scrollbar.value = stateInfo.normalizedTime % 1;

                //1�{�w�ŐG��Ă��Ȃ��Ȃ�A�X�N���[���o�[�����삳��Ă��Ȃ���Ԃɐ؂�ւ���
                if (!Input.GetMouseButton(0)) scrollbarIsInteract = false;
            })
            .AddTo(this);
    }

    /// <summary>
    /// �X�N���[���o�[�̒l���ύX���ꂽ�ۂɌĂяo�����
    /// </summary>
    /// <param name="value"></param>
    private void OnScrollbarValueChange(float value)
    {
        //�X�N���[���o�[�����삳��Ă����Ԃɐ؂�ւ���
        scrollbarIsInteract = true;

        //�A�j���[�V�����̏�Ԃ��擾����
        AnimatorStateInfo stateInfo = bedAnimator.GetCurrentAnimatorStateInfo(0);

        //�X�N���[���o�[�̒l�ɑΉ������ꏊ����A�j���[�V�������Đ�����
        bedAnimator.Play(stateInfo.fullPathHash, -1, value);
    }
}