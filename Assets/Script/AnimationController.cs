using UniRx;
using UniRx.Triggers;
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

    [SerializeField]
    private float speedUpVelocity = 1.5f;

    [SerializeField]
    private float speedDownVelocity = 1.5f;

    private bool isReturn;

    /// <summary>
    /// �A�j���[�V�����̍Đ����x�̏����l
    /// </summary>
    private float defaultAnimationSpeed;

    /// <summary>
    /// ���݂̃A�j���[�V�����̍Đ����x
    /// </summary>
    private float currentAnimationSpeed;

    /// <summary>
    /// �Q�[���J�n����ɌĂяo�����
    /// </summary>
    private void Start()
    {
        //�A�j���[�V�����̍Đ����x�̏����l���擾����
        defaultAnimationSpeed = bedAnimator.speed;

        //�A�j���[�V�����̍Đ����x���u0�v�ɐݒ肷��
        bedAnimator.speed = 0;

        //���݂̃A�j���[�V�����̍Đ����x�𖈃t���[���擾����
        this.UpdateAsObservable()
            .Subscribe(_ => currentAnimationSpeed = bedAnimator.speed)
            .AddTo(this);
    }


    /// <summary>
    /// �A�j���[�V�������J�n����
    /// </summary>
    public void StartAnimation()
    {
        isReturn = false;

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
        isReturn = false;

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
        isReturn = false;

        //�A�j���[�V�����̍Đ����x���u1�v�ɐݒ肷��
        bedAnimator.SetFloat("Speed", 1f);

        //�A�j���[�V�����̍Đ����x��ݒ肷��
        bedAnimator.speed = defaultAnimationSpeed / speedDownVelocity;
    }

    /// <summary>
    /// 
    /// </summary>
    public void ReturnAnimation()
    {
        if (isReturn) return;

        isReturn = true;

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
}