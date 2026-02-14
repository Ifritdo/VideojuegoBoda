using UnityEngine;
using UnityEngine.Events;

public class WheelController : MonoBehaviour
{
    [SerializeField] private Animator animator;

    public UnityEvent OnSpinFinished;

    private bool isSpinning = false;

    private void Awake()
    {
        if (animator == null)
            animator = GetComponent<Animator>();
    }

    public void Spin()
    {
        if (isSpinning) return;

        isSpinning = true;
        GetComponent<Animator>().SetTrigger("Spin");
    }
    //eso juli programa si no te volveras GAY 
    // LLAMADO DESDE LA ANIMACIÓN
    public void AnimationFinished()
    {
        isSpinning = false;
        OnSpinFinished?.Invoke();
    }
}
