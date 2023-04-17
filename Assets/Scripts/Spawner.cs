using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {
    public int amount;
    public GameObject prefab;

    void Start() {
        for (int i=0; i<amount; i++) {
            spawnObject();
        }
    }
    void Update() {
        amount = this.transform.childCount;
    }

    public int getNumberOfEntities(bool isAdult) {
        int count = 0;
        for (int i=0; i<this.gameObject.transform.childCount; i++) {
            Transform entity = this.gameObject.transform.GetChild(i);

            if (entity.GetComponent<Plant>() != null) {
                if (entity.GetComponent<Plant>().isAdult)
                    count++;
            } else {
                if (entity.GetComponent<Rabbit>().isAdult)
                    count++;
            }
        }
        if (isAdult)
            return count;
        else 
            return amount - count;
    }

    public void spawnObject() {
        Vector3 position = new Vector3(Random.Range(-24, 24), 0, Random.Range(-24, 24));
        Quaternion angle = Quaternion.Euler(0, Random.Range(0, 360), 0);
        Instantiate(prefab, position, angle, this.transform);
    }
    public void spawnObject(Vector3 position) {
        Quaternion angle = Quaternion.Euler(0, Random.Range(0, 360), 0);
        Instantiate(prefab, position, angle, this.transform);
    }
}
