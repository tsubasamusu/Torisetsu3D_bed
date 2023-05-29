using UnityEngine;

/// <summary>
/// �A�j���[�V�����𐧌䂷��
/// </summary>
public class AnimationController : MonoBehaviour
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
    /// �Đ����x�A�b�v���̑��x
    /// </summary>
    [SerializeField]
    private float speedUpVelocity;

    /// <summary>
    /// �Đ����x�_�E�����̑��x
    /// </summary>
    [SerializeField]
    private float speedDownVelocity;

    /// <summary>
    /// �A�j���[�V�����I�����ɂ��̃A�j���[�V�����̏�Ԃ��ێ����Ԃ���
    /// </summary>
    [SerializeField]
    private float keepTimeAtEnd;

    /// <summary>
    /// �����߂������ǂ���
    /// </summary>
    private bool isRewinding;

    /// <summary>
    /// �A�j���[�V�����̍Đ����x�̏����l
    /// </summary>
    private float defaultAnimationSpeed;

    /// <summary>
    /// �Q�[���J�n����ɌĂяo�����
    /// </summary>
    private void Start()
    {
        //�A�j���[�V�����̍Đ����x�̏����l���擾����
        defaultAnimationSpeed = bedAnimator.speed;

        //�A�j���[�V�����̍Đ����x���u0�v�ɐݒ肷��
        bedAnimator.speed = 0;
    }

    /// <summary>
    /// �A�j���[�V�������J�n����
    /// </summary>
    public void StartAnimation()
    {
        //�����߂����ł͂Ȃ���Ԃɐ؂�ւ���
        isRewinding = false;

        //�A�j���[�V�����̍Đ����x���u1�v�ɐݒ肷��
        bedAnimator.SetFloat("Speed", 1f);

        //�A�j���[�V�����̍Đ����x�������l�ɐݒ肷��
        bedAnimator.speed = defaultAnimationSpeed;
    }

    /// <summary>
    /// �A�j���[�V�������~����
    /// </summary>
    public void StopAnimation()
    {
        //�A�j���[�V�����̍Đ����x���u0�v�ɐݒ肷��
        bedAnimator.speed = 0;
    }

    /// <summary>
    /// �A�j���[�V�����̍Đ����x���グ��
    /// </summary>
    public void SpeedUpAnimation()
    {
        //�����߂����ł͂Ȃ���Ԃɐ؂�ւ���
        isRewinding = false;

        //�A�j���[�V�����̍Đ����x���u1�v�ɐݒ肷��
        bedAnimator.SetFloat("Speed", 1f);

        //�A�j���[�V�����̍Đ����x��ݒ肷��
        bedAnimator.speed = speedUpVelocity * defaultAnimationSpeed;
    }

    /// <summary>
    /// �A�j���[�V�����̍Đ����x��������
    /// </summary>
    public void SlowDownAnimation()
    {
        //�����߂����ł͂Ȃ���Ԃɐ؂�ւ���
        isRewinding = false;

        //�A�j���[�V�����̍Đ����x���u1�v�ɐݒ肷��
        bedAnimator.SetFloat("Speed", 1f);

        //�A�j���[�V�����̍Đ����x��ݒ肷��
        bedAnimator.speed = defaultAnimationSpeed / speedDownVelocity;
    }

    /// <summary>
    /// �A�j���[�V�����������߂�
    /// </summary>
    public void RewindAnimation()
    {
        //�����߂����Ȃ�A�ȍ~�̏������s��Ȃ�
        if (isRewinding) return;

        //�����߂����ł����Ԃɐ؂�ւ���
        isRewinding = true;

        //�A�j���[�V�����̍Đ����x���u-1�v�ɐݒ肷��
        bedAnimator.SetFloat("Speed", -1f);

        //�A�j���[�V�����̍Đ����x�������l�ɐݒ肷��
        bedAnimator.speed = defaultAnimationSpeed;
    }

    /// <summary>
    /// �A�j���[�V���������Z�b�g����
    /// </summary>
    public void ResetAnimation()
    {
        //�A�j���[�V�������ŏ�����Đ�����
        bedAnimator.Play(animationName, 0, 0);

        //�A�j���[�V�����̍Đ����x�������l�ɐݒ肷��
        bedAnimator.speed = defaultAnimationSpeed;
    }

    /// <summary>
    /// �A�j���[�V�������ŏ�����Đ�����
    /// </summary>
    public void PlayAnimationFromOrigin()
    {
        //�����߂����ł͂Ȃ���Ԃɐ؂�ւ���
        isRewinding = false;

        //�A�j���[�V�����̍Đ����x���u1�v�ɐݒ肷��
        bedAnimator.SetFloat("Speed", 1f);

        //�A�j���[�V�����̍Đ����x�������l�ɐݒ肷��
        bedAnimator.speed = defaultAnimationSpeed;

        //�A�j���[�V�����̏�Ԃ��擾����
        AnimatorStateInfo stateInfo = bedAnimator.GetCurrentAnimatorStateInfo(0);

        //�X�N���[���o�[�̒l�ɑΉ������ꏊ����A�j���[�V�������Đ�����
        bedAnimator.Play(stateInfo.fullPathHash, -1, 0f);
    }

    public void OnAnimationEnd()
    {
        Debug.Log("END");
    }
}