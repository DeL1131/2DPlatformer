using System;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public Action<Coin> CoinPickedUp;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.TryGetComponent<Player>(out Player _))
        {
            CoinPickedUp?.Invoke(this);
        }
    }
}
