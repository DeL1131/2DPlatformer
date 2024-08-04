using UnityEngine;

public class Player : MonoBehaviour
{
    public int Coins {  get; private set; }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.TryGetComponent<Coin>(out Coin coin))
        {            
            coin.ReturnToPool();
            Coins++;
        }
    }
}