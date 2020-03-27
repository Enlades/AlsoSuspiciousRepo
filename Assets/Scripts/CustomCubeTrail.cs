using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomCubeTrail : MonoBehaviour
{
    public GameObject TrailPrefab;

    private Pool _pool;

    private void Awake(){
        _pool = new Pool(TrailPrefab);
    }
    
    public void StartTrail(){
        StartCoroutine(CreateTrail());
    }

    private IEnumerator CreateTrail(){
        while(gameObject.activeSelf){
            GameObject temp = _pool.Get();
            temp.transform.position = transform.position + Vector3.right * Random.Range(-0.5f, 0.5f) + Vector3.down * Random.Range(0.5f, 1f);

            StartCoroutine(TrailPieceMovement(temp));

            yield return new WaitForFixedUpdate();
        }
        yield return null;
    }

    private IEnumerator TrailPieceMovement(GameObject p_piece){

        float timer = 0.3f;
        float maxTimer = timer;

        Vector3 targetPosition = p_piece.transform.position + Vector3.up * 0.5f;
        Vector3 startPosition = p_piece.transform.position;

        while(timer > 0f){

            p_piece.transform.position = Vector3.Lerp(startPosition, targetPosition, (maxTimer - timer) / maxTimer);
            
            timer -= Time.deltaTime;

            yield return null;
        }

        _pool.Put(p_piece);
    }

    class Pool{

        const int POOL_SIZE = 50;

        private GameObject _poolParent;
        private List<GameObject> _available;

        public Pool(GameObject p_original){
            _available = new List<GameObject>();

            _poolParent = new GameObject("PoolParent");

            for(int i = 0; i < POOL_SIZE; i++){
                GameObject temp = Instantiate(p_original);
                temp.SetActive(false);

                temp.transform.SetParent(_poolParent.transform);

                _available.Add(temp);
            }      
        }

        public GameObject Get(){
            GameObject result = _available[0];
            result.SetActive(true);
            _available.RemoveAt(0);
            return result;
        }

        public void Put(GameObject p_gameObject){
            p_gameObject.SetActive(false);
            _available.Add(p_gameObject);
        }
    }
}
