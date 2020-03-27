using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Don't judge the Pool class
public class CubeExplosion : MonoBehaviour
{
    public GameObject ExplosionPiecePrefab;

    private Pool _pool;

    private void Awake(){
        _pool = new Pool(ExplosionPiecePrefab);
    }

    public void Explode(Vector3 p_direction){
        for(int i = 0; i < 30; i++){
            Rigidbody temp = _pool.Get();

            if(temp == null){
                continue;
            }
            temp.transform.position = transform.position 
                + Vector3.right * Random.Range(-0.5f, 0.5f)
                + Vector3.forward * Random.Range(-0.5f, 0.5f)
                + Vector3.up * Random.Range(-0.5f, 0.5f);

            temp.AddForce((temp.transform.position + Vector3.up * 0.2f - transform.position + p_direction) * 6f, ForceMode.Impulse);
        }
    }

    class Pool
    {
        const int POOL_SIZE = 30;

        private GameObject _poolParent;
        private List<Rigidbody> _available;

        public Pool(GameObject p_original)
        {
            _available = new List<Rigidbody>();

            _poolParent = new GameObject("PoolParent");

            for (int i = 0; i < POOL_SIZE; i++)
            {
                GameObject temp = Instantiate(p_original);
                temp.SetActive(false);

                temp.transform.SetParent(_poolParent.transform);

                temp.transform.Rotate(Random.Range(-360, 360),Random.Range(-360, 360),Random.Range(-360, 360));

                _available.Add(temp.GetComponent<Rigidbody>());
            }
        }

        public Rigidbody Get()
        {
            Rigidbody result = _available[0];
            result.gameObject.SetActive(true);
            _available.RemoveAt(0);
            return result;
        }

        public void Put(Rigidbody p_gameObject)
        {
            p_gameObject.gameObject.SetActive(false);
            _available.Add(p_gameObject);
        }
    }
}
