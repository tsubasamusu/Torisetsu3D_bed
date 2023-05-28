using UnityEngine;

public class UIInteractionManager : MonoBehaviour
{
    public static UIInteractionManager Instance { get; private set; }
    public bool IsScrollbarInteracting { get; set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}