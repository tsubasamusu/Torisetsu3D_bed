using UnityEngine;

/// <summary>
/// �X�N���[���o�[��
/// </summary>
public class ScrollbarInteractionManager : MonoBehaviour
{
    /// <summary>
    /// �uScrollbarInteractionManager�v�N���X�̃C���X�^���X
    /// </summary>
    public static ScrollbarInteractionManager instance;

    /// <summary>
    /// �X�N���[���o�[�����삳��Ă��邩�ǂ���
    /// </summary>
    [HideInInspector]
    public bool scrollbarIsInteract;

    /// <summary>
    /// �V���O���g���ɕK�{�̋L�q
    /// </summary>
    private void Awake() { if (instance == null) instance = this; else Destroy(gameObject); }
}