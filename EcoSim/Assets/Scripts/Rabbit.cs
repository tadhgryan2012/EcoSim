using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rabbit : MonoBehaviour {
    public Animator animator;
    public FieldOfView plantFOV;
    public FieldOfView rabbitFOV;
    public HungerSlider hungerSlider;
    private float direction;
    private float hunger;
    public bool isAdult = false;
    private float speed = 2.0f;
    private float timeElapsed;
    private float mateTimer;
    private bool canMate;
    private Vector3 currentTarget;
    private bool canMove;

    void Start() {
        if (isAdult)
            this.transform.localScale = RabbitData.maxVector;
        else
            this.transform.localScale = RabbitData.minVector;

        hunger = RabbitData.maxHunger;
        hungerSlider.setMaxValue(hunger);

        animator.speed = RabbitData.animationNormalSpeed * speed;

        StartCoroutine(updateBehaviour());
    }

    void FixedUpdate() {
        updateHunger();
        updateDirection(currentTarget);
        rotate();
        age();
    }

    IEnumerator updateBehaviour() {
        float waitTime;
        while (true) {
            Transform closestPlant = getClosestTarget(plantFOV.visibleTargets, false);
            Transform closestRabbit = getClosestTarget(rabbitFOV.visibleTargets, true);
            bool reallyNeedFood = closestPlant != null && hunger < RabbitData.maxHunger*0.2f;
            bool kindaNeedFood = closestPlant != null && hunger < RabbitData.maxHunger*0.8f;
            canMate = (mateTimer > RabbitData.mateCooldown) && (closestRabbit != null) && isAdult;
            if (!reallyNeedFood && canMate) {
                mate(closestRabbit);
                waitTime = 1.0f;
            } else if (kindaNeedFood) {
                goToFood(closestPlant);
                waitTime = 1.0f;
            } else {
                idle();
                waitTime = Random.Range(1.0f, 6.0f);
            }
            if (canMove)
                move();

            yield return new WaitForSeconds(waitTime);
        }
    }
    void idle() {
        float angle = Random.Range(-90.0f, 90.0f);
        direction = angle + transform.eulerAngles.y;
    }
    void goToFood(Transform target) {
        animator.speed = RabbitData.animationRunSpeed * speed;

        updateDirection(target.position);
    }
    void move() {
        animator.SetTrigger("doMove");
        animator.speed = RabbitData.animationNormalSpeed * speed;
    }
    void mate(Transform lover) {
        animator.speed = RabbitData.animationRunSpeed * speed;

        updateDirection(lover.position);
    }
    void updateDirection(Vector3 pos) {
        currentTarget = pos;
        Vector3 dirToTarget = (pos - transform.position).normalized;
        direction = Vector3.Angle(transform.forward, dirToTarget) + transform.eulerAngles.y;
        direction = direction % 360.0f;
    }
    void rotate() {
        Quaternion target = Quaternion.Euler(0, direction, 0);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, target, Time.deltaTime * RabbitData.anglerSpeed);

        canMove = (Mathf.Abs(transform.eulerAngles.y - target.eulerAngles.y) < 5);
        // Debug.Log("Direction: "+ direction+ "\tAngle: "+ Mathf.Abs(transform.eulerAngles.y - target.eulerAngles.y));

    }
    void updateHunger() {
        hunger -= RabbitData.hungerDrainRate * Time.deltaTime * speed;
        hungerSlider.setValue(hunger);
        if (hunger <= 0)
            die();
    }

    void age() {
        timeElapsed += Time.deltaTime;
        if (isAdult)
            mateTimer += Time.deltaTime;
        if (!isAdult) {
            if (timeElapsed < RabbitData.ageOfMaturity) {
                this.transform.localScale = Vector3.Lerp(RabbitData.minVector, RabbitData.maxVector, timeElapsed / RabbitData.ageOfMaturity);
            } else {
                isAdult = true;
                this.transform.localScale = RabbitData.maxVector;
            }
        } else if (timeElapsed > RabbitData.ageOfDeath) {
            die();
        }
    }

    void die() {
        animator.SetTrigger("doDead");
        Destroy(this.gameObject);
    }
    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Plant") {
            if (hunger > RabbitData.maxHunger * 0.95f)
                return;
            float size = (other.gameObject.transform.localScale.x - PlantData.minVector.x) / (PlantData.maxVector.x - PlantData.minVector.x);
            Mathf.Lerp(0, RabbitData.hungerRefilAmount, size);
            hunger += RabbitData.hungerRefilAmount;
            Destroy(other.gameObject);
        }
    }
    void OnCollisionEnter(Collision other) {
        if (other.gameObject.tag == "Walls") {
            updateDirection(other.contacts[0].point);
        } else if (other.gameObject.tag == "Rabbit" && canMate && other.gameObject.GetComponent<Rabbit>().canMate) {
            this.transform.parent.GetComponent<Spawner>().spawnObject(this.transform.position);
            canMate = false;
            mateTimer = 0;
        }
    }

    public Transform getClosestTarget(List<Transform> visibleTargets, bool isRabbit) {
        if (visibleTargets.Count == 0)
            return null;
        if (visibleTargets.Count == 1) {
            if ((isRabbit && visibleTargets[0].GetComponent<Rabbit>().canMate) || !isRabbit)
                return visibleTargets[0];
            else return null;
        }

        Transform closestTarget = visibleTargets[0];
        for (int i=1; i<visibleTargets.Count; i++) {
            if (!(isRabbit && visibleTargets[0].GetComponent<Rabbit>().canMate))
                continue;
            if (visibleTargets[i] == null || closestTarget == null)
                continue;
            float closestMagnitude = (closestTarget.position - transform.position).sqrMagnitude;
            float newMagnitude = (visibleTargets[i].position - transform.position).sqrMagnitude;
            if (closestMagnitude > newMagnitude)
                closestTarget = visibleTargets[i];
        }
        return closestTarget;
    }
}
