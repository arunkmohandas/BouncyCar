using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BouncyCarController : MonoBehaviour
{
    public Camera cam;
    private Transform trans;
    public MeshRenderer bg1;
    public float bg1ScrollingSpeed = .6f;
    public MeshRenderer bg2;
    public float bg2ScrollingSpeed = 1f;
    public MeshRenderer road;
    public float roadScrollingSpeed = 1.2f;
    public MeshRenderer fg;
    public float fgScrollingSpeed = 1.4f;
    public float scrollSpeed = 1;
    private float rightSideSpeedMultiplier = 1f;
    public float roadYMax = .3f;
    public float roadYMin = -2;
    public float roadRightSideYMax = -.7f;
    public float posXVal = 0;
    public float posZVal = 0;
    public float carSteerSpeed = 10;
    public bool isGameRunning;
    private ParticleSystem dustParticle;
    public Animator bouncyAnimator;
    public Transform[] wheels;
    public float wheelRotationSpeed;
    private float carCenterPointOffset = -1f;  
    private void Awake()
    {
        trans = transform;
        dustParticle = GetComponentInChildren<ParticleSystem>();
        dustParticle.Stop();
        bouncyAnimator.enabled = false;
        isGameRunning = false;
    }
    private void Update()
    {
        if (!isGameRunning)
            return;
        if (Input.GetMouseButtonDown(0))
        {
            // Debug.Log("click");
            StopAllCoroutines();
            Vector3 pos = cam.ScreenToWorldPoint(Input.mousePosition);
            //Debug.Log(pos);
            if (pos.y > roadYMax)
                pos.y = roadYMax;
            else if (pos.y < roadYMin)
                pos.y = roadYMin;
            pos.x = posXVal;
            pos.z = posZVal;
             //Debug.Log(pos.y);
            // objectTrans.position = Vector3.Lerp(objectTrans.position, pos, Time.deltaTime * 10);
            StartCoroutine(MoveToPos(pos));
            // objectTrans.position=new Vector3(538,pos.y,210);
            if (pos.y > roadRightSideYMax)
                rightSideSpeedMultiplier = .8f;
            else
                rightSideSpeedMultiplier = 1;
            
        }

        

        float offset = Time.time * bg1ScrollingSpeed * rightSideSpeedMultiplier;
        bg1.material.SetTextureOffset("_MainTex", new Vector2(offset, 0));
        offset = Time.time * bg2ScrollingSpeed * rightSideSpeedMultiplier;
        bg2.material.SetTextureOffset("_MainTex", new Vector2(offset, 0));
        offset = Time.time * roadScrollingSpeed * rightSideSpeedMultiplier;
        road.material.SetTextureOffset("_MainTex", new Vector2(offset, 0));
        offset = Time.time * fgScrollingSpeed * rightSideSpeedMultiplier;
        fg.material.SetTextureOffset("_MainTex", new Vector2(offset, 0));

        foreach(Transform wheel in wheels)
        {
            wheel.Rotate(Vector3.back * wheelRotationSpeed * Time.deltaTime*rightSideSpeedMultiplier);
        }

    }

    IEnumerator MoveToPos(Vector3 pos)
    {
        //Debug.Log(pos);
        while (Vector3.Distance(trans.position, pos) > .001f)
        {
            trans.position = Vector3.Lerp(trans.position, pos, Time.deltaTime * carSteerSpeed);
            yield return new WaitForFixedUpdate();
        }
        yield return new WaitForFixedUpdate();
    }
    public void InitCar()
    {
        gameObject.GetComponent<Animator>().enabled = true;
        // CarOnRoad();
    }
    public void CarOnRoad()
    {

        dustParticle.Play();
        bouncyAnimator.enabled = true;
        isGameRunning = true;
        gameObject.GetComponent<Animator>().enabled = false;
    }
    public void StopCar()
    {
        isGameRunning = false;
        gameObject.GetComponent<Animator>().enabled = false;
    }
}
