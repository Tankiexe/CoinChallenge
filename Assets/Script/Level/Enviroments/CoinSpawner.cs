using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public List<GameObject> CoinsList;
    public Transform coinsParent;
    void Start()
    {

        SpawnCoin();
        
    }

    void SpawnCoin()
    {
        Vector3 pos = transform.position;
        Quaternion rot = Quaternion.Euler(90, transform.rotation.y, transform.rotation.z);
        int index = Random.Range(0, CoinsList.Count);
        Instantiate(CoinsList[index], pos, rot, coinsParent);
        Destroy(gameObject);
    }

}
