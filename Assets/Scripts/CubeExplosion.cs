using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeExplosion : MonoBehaviour
{
    public GameObject ExplosionPiecePrefab;

    private Pool _pool;

    private void Awake(){
        _pool = new Pool(ExplosionPiecePrefab);
    }

    class Pool
    {
        const int POOL_SIZE = 50;

        private GameObject _poolParent;
        private List<GameObject> _available;

        public Pool(GameObject p_original)
        {
            _available = new List<GameObject>();

            _poolParent = new GameObject("PoolParent");

            for (int i = 0; i < POOL_SIZE; i++)
            {
                GameObject temp = Instantiate(p_original);
                temp.SetActive(false);

                temp.transform.SetParent(_poolParent.transform);

                _available.Add(temp);
            }
        }

        public GameObject Get()
        {
            GameObject result = _available[0];
            result.SetActive(true);
            _available.RemoveAt(0);
            return result;
        }

        public void Put(GameObject p_gameObject)
        {
            p_gameObject.SetActive(false);
            _available.Add(p_gameObject);
        }
    }
}
