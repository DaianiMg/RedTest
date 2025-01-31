using UnityEngine;

public class CheckGround : MonoBehaviour
{
    [SerializeField] float groundDistance = 0.3f;
    [SerializeField] LayerMask groundLayers;

    public bool IsGrounded {  get; private set; }

    private void FixedUpdate()
    {
        IsGrounded = Physics.CheckSphere(transform.position, groundDistance, groundLayers);
    }
  
    private void OnDrawGizmos()
    {
        Gizmos.color = IsGrounded ? Color.green : Color.red;
        Gizmos.DrawWireSphere(transform.position, groundDistance);
    }
}
