using System;
using UnityEngine;

public class GroundDetectionHandler : MonoBehaviour
{
    public event Action<bool> GroundedChanged;

    [SerializeField] private LayerMask _groundLayerMask;

    public bool IsGround {  get; private set; } = false;

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (_groundLayerMask == (_groundLayerMask | (1 << collision.gameObject.layer)))
        {
            IsGround = true;
            GroundedChanged?.Invoke(IsGround);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        IsGround = false;
        GroundedChanged?.Invoke(IsGround);
    }
}