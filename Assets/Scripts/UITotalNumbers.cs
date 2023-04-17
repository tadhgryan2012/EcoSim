using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UITotalNumbers : MonoBehaviour {
    private TextMeshProUGUI adultRabbitTextBoxes;
    private TextMeshProUGUI babyRabbitTextBoxes;
    private TextMeshProUGUI adultPlantTextBoxes;
    private TextMeshProUGUI babyPlantTextBoxes;
    
    private Spawner plantSpawner;
    private Spawner rabbitSpawner;

    void Start() {
        adultRabbitTextBoxes = this.gameObject.transform.Find("AdultRabbit").GetComponent<TextMeshProUGUI>();
        babyRabbitTextBoxes = this.gameObject.transform.Find("BabyRabbit").GetComponent<TextMeshProUGUI>();
        adultPlantTextBoxes = this.gameObject.transform.Find("AdultPlant").GetComponent<TextMeshProUGUI>();
        babyPlantTextBoxes = this.gameObject.transform.Find("BabyPlant").GetComponent<TextMeshProUGUI>();
        
        plantSpawner = GameObject.Find("Plant Handler").GetComponent<Spawner>();
        rabbitSpawner = GameObject.Find("Rabbit Handler").GetComponent<Spawner>();
    }
    void Update() {
        adultRabbitTextBoxes.text = rabbitSpawner.getNumberOfEntities(true).ToString();
        babyRabbitTextBoxes.text = rabbitSpawner.getNumberOfEntities(false).ToString();
        adultPlantTextBoxes.text = plantSpawner.getNumberOfEntities(true).ToString();
        babyPlantTextBoxes.text = plantSpawner.getNumberOfEntities(false).ToString();
    }
}
