using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class CoinsSpawner : MonoBehaviour
{
    [SerializeField] private Coin _prefab;
    [SerializeField] private Transform[] _pointsSpawn;

    private ObjectPool<Coin> _pool;
    private int _numberOfPoint;
    private int _maxCoins;
    private int _coinsSpawned;

    private void Awake()
    {
        _pool = new ObjectPool<Coin>(
            createFunc: () => Instantiate(_prefab),
            actionOnGet: (coin) => ActivateOnGet(coin),
            actionOnRelease: (coin) => coin.gameObject.SetActive(false),
            actionOnDestroy: (coin) => Destroy(coin.gameObject),
            collectionCheck: true);
  
        _maxCoins = _pointsSpawn.Length;
    }

    private void Start()
    {
        StartCoroutine(SpawnCoins());
    }

    private IEnumerator SpawnCoins()
    {
        WaitForFixedUpdate waitForFixedUpdate = new WaitForFixedUpdate();

        while (_coinsSpawned < _maxCoins)
        {
            yield return waitForFixedUpdate;
            _pool.Get();
            _coinsSpawned++;
        }
    }

    private void ActivateOnGet(Coin coin)
    {
        coin.PickedUp += ReturnToPool;

        _numberOfPoint = (++_numberOfPoint) % _pointsSpawn.Length;
        coin.transform.position = _pointsSpawn[_numberOfPoint].transform.position;

        coin.gameObject.SetActive(true);
    }

    private void ReturnToPool(Coin coin)
    {
        coin.PickedUp -= ReturnToPool;
        _pool.Release(coin);
    }
}   