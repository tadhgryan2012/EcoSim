using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Plant : MonoBehaviour {
    private float timeElapsed = 0;
    public bool isAdult = false;
    public GameObject plantPrefab;

    void Start() {
        if (isAdult)
            this.transform.localScale = PlantData.maxVector;
        else
            this.transform.localScale = PlantData.minVector;
    }
    void Update() {
        if (!isAdult)
            grow();
    }

    IEnumerator createSpawn() {
        while (true) {
            this.transform.parent.GetComponent<Spawner>().spawnObject();
            yield return new WaitForSeconds(PlantData.reproduceTime);
        }
    }
    void grow() {
        if (timeElapsed < PlantData.growthTime) {
            this.transform.localScale = Vector3.Lerp(PlantData.minVector, PlantData.maxVector, timeElapsed / PlantData.growthTime);
            timeElapsed += Time.deltaTime;
        } else {
            isAdult = true;
            this.transform.localScale = PlantData.maxVector;
            StartCoroutine(createSpawn());
        }
    }
}
