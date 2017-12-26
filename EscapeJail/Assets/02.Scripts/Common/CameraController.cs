using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

public enum CameraState
{
    FollowPlayer,
    Shake
}

[RequireComponent(typeof(Camera))]
public class CameraController : MonoBehaviour
{
    public static CameraController Instance;

    private CameraState cameraState;

    private Transform target;


    //흔들기전용
    private Vector3 originPosit;
    private float shakeIntensity;
    private float shakeTime;
    private float shakeCount;


    [SerializeField]
    private float followSpeed = 2;

    [SerializeField]
    private PostProcessingProfile postProcessBehavior;

    private void Awake()
    {
        Instance = this;

        postProcessBehavior = GetComponent<PostProcessingBehaviour>().profile;
        postProcessBehavior.vignette.enabled = false;


    }   

    public void  SniperAimEffectOnOff(bool OnOff,Color color = default(Color))
    {
        if (postProcessBehavior != null)
        {
            if(OnOff == true)
            {
                var settings = postProcessBehavior.vignette.settings;

                if(color !=default(Color))
                settings.color = color;

                postProcessBehavior.vignette.settings = settings;
            }
       
            postProcessBehavior.vignette.enabled = OnOff;
        }      

    }


    private void ChangeCameraMode(CameraState cameraState)
    {
        this.cameraState = cameraState;

        if (cameraState == CameraState.Shake)
            originPosit = this.transform.position;
    }

    private void LinkPlayer()
    {
        target = GameObject.FindWithTag("Player").transform;
    }

    public void ShakeCamera(float intensity, float shakeTime)
    {
        ChangeCameraMode(CameraState.Shake);

        shakeIntensity = intensity;
        this.shakeTime = shakeTime;
        shakeCount = 0f;
    }

    private void Start()
    {

        LinkPlayer();

        ChangeCameraMode(CameraState.FollowPlayer);
        
     
    }

    

    private void FixedUpdate()
    {
        if (target == null) return;

        switch (cameraState)
        {
            case CameraState.FollowPlayer:
                {
                    this.transform.position = Vector3.Lerp(this.transform.position, target.position, Time.fixedUnscaledDeltaTime * followSpeed);
                } break;
            case CameraState.Shake:
                {
                    this.transform.position = Vector3.Lerp(this.transform.position, target.position, Time.fixedUnscaledDeltaTime * followSpeed);
                    this.transform.position = Vector3.Lerp(this.transform.position, this.transform.position + (Vector3)Random.insideUnitCircle*shakeIntensity, Time.deltaTime);

                    //타이머

                    shakeCount += Time.fixedUnscaledDeltaTime;
                    if (shakeCount >= shakeTime)
                    {
                        ChangeCameraMode(CameraState.FollowPlayer);
                    }

                }
                break;
        }

      
    }



    

}
