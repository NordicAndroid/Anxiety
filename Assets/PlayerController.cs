using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int speed;
    public float moveSpeedMultiplier = 1;
    public float[] speedMultiArray;
    Vector3 startMaxSpeed = new Vector3(1, 1, 1);
    Vector3 maxSpeed;
    Vector3 currentDir;
    public float reboundEffekt;
    public GameObject healtbar;

    Rigidbody rb;
    public int frameRate;

    public bool isAnxius;
    public AnxietyField currentfield;
    public float anxietyLevel;
    public int[] anxietyInteval = new int[]{0,60,120,180, 1000};
    public float anxietyInput;
    public float DecreaseMultiplier;
    public float stanAnxietyDecreaseTime;

    private void Awake(){
        rb = GetComponent<Rigidbody>();
        startMaxSpeed *= speed;
        maxSpeed = startMaxSpeed;
        mumbling = GetComponent<AudioSource>();
        Application.targetFrameRate = frameRate;
    }
 
    void Update(){
        Move(); AudioControl();
    }

    private void FixedUpdate()
    {
        if (anxietyLevel > 0){
            DecAnxiety();
            isAnxius = true; 
        }
        else{
            isAnxius = false;
        }
    }

    void Move()
    {
        Vector3 movDirHori = new Vector3(Input.GetAxis("Horizontal"), 0, 0);
        Vector3 movDirVeti = new Vector3(0, 0, Input.GetAxis("Vertical"));

        for (int i = 0; i < anxietyInteval.Length; i++){
            if (anxietyLevel > anxietyInteval[i] && anxietyLevel < anxietyInteval[i+1]){
                moveSpeedMultiplier = speedMultiArray[i];
                maxSpeed = startMaxSpeed * speedMultiArray[i];
            }
            else if (anxietyLevel == 0){
                moveSpeedMultiplier = 1;
                maxSpeed = startMaxSpeed;
            }
        }
        
        if (rb.velocity.x < maxSpeed.x && rb.velocity.x > -maxSpeed.x && Mathf.Sqrt(Mathf.Pow(rb.velocity.x, 2f) + Mathf.Pow(rb.velocity.z, 2f)) < maxSpeed.x){
            rb.AddForce(movDirHori*speed*moveSpeedMultiplier, ForceMode.Force);
            //Debug.Log(rb.velocity + " Max speed");
        }
        if (rb.velocity.z < maxSpeed.z && rb.velocity.z > -maxSpeed.z && Mathf.Sqrt(Mathf.Pow(rb.velocity.x, 2f) + Mathf.Pow(rb.velocity.z, 2f)) < maxSpeed.x){
            rb.AddForce(movDirVeti*speed*moveSpeedMultiplier, ForceMode.Force);
            //Debug.Log(rb.velocity + " Max speed");
        }
    }   
    void reBound(){
        Vector3 disVecktor = new Vector3(gameObject.transform.position.x - currentfield.centerPoint.position.x, 0, currentfield.centerPoint.position.z + gameObject.transform.position.z);
        rb.AddForce(disVecktor.normalized*reboundEffekt, ForceMode.VelocityChange);
    }
    public void IncAnxiety(int crossWorth){ 
        anxietyLevel += crossWorth;
        if (anxietyLevel >= 180) {
            reBound();
        }
        if (anxietyLevel >= 180) {
            healtbar.GetComponent<Effekts>().HitsInc();
        }
    }
    float axibreak;
    public void DecAnxiety(){
        if (anxietyLevel > 0){
            anxietyLevel -= Time.fixedDeltaTime * anxietyInput/(stanAnxietyDecreaseTime * DecreaseMultiplier);
            if (anxietyLevel < 1){
                anxietyLevel = 0;
            }
        }
        else if (anxietyLevel > 180)
        {
            axibreak += Time.fixedDeltaTime;
            audiotime = 0.001f;
            if (axibreak > 5)
            {
                anxietyLevel = 180;
                
                axibreak = 0;
            }
        }
    }

    AudioSource mumbling;
    public float[] audioLevel;
    public float audiotime;
    void AudioControl()
    {
        for (int i = 0; i < anxietyInteval.Length; i++)
        {
            if (anxietyLevel > anxietyInteval[i] && anxietyLevel < anxietyInteval[i + 1])
            {
                float startvolume = mumbling.volume;
                mumbling.volume = Mathf.Lerp(startvolume, audioLevel[i],audiotime);
            }
            else if (anxietyLevel == 0)
            {
                mumbling.volume = audioLevel[0];
            }
            else if (anxietyLevel > 180)
            {
                mumbling.volume = 0;
            }
        }
    }

}
