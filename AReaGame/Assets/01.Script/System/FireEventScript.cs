﻿using UnityEngine;
using System.Collections;
using Vuforia;

public class FireEventScript : MonoBehaviour {
    public float timeBetweenBullets = 0.1f;
    public GameObject flare;//플레어효과

    float timer;
    float speed;
    RaycastHit touchHit;// 터치해서 맞은곳
    RaycastHit shootHit;//실제로 쏴서 맞은곳
    Ray touchRay;//터치 레이캐스트
    Ray shootRay;//발사 레이캐스트
    int shootableMask;//맞는 유닛인지 확인
    LineRenderer gunLine;//총알 효과
    AudioSource gunAudio;//총소리

    float effectsDisplayTime = 0.15f;
    Vector3 shootTarget;//쏠 위치
    Vector3 shootDir;//쏘는 방향
    //나중에 실행됐다는 상태 변수도 추가해야함
    void Awake()
    {
        shootableMask = LayerMask.GetMask("Shootable");
        gunLine = GetComponent<LineRenderer>();
        gunAudio = GetComponent<AudioSource>();
    }

	
    // Update is called once per frame
    void Update()
    {
            timer += Time.deltaTime;
            if ( Input.GetMouseButtonDown(0) && timer >= timeBetweenBullets)
            {
                Shoot();
            }
            if (timer >= timeBetweenBullets * effectsDisplayTime)
            {
                DisableEffects();
            }
    }

    public void DisableEffects()
    {
        gunLine.enabled = false;
    }

    void Shoot()
    {
        timer = 0f;

        gunAudio.Play();

        gunLine.enabled = true;
        gunLine.SetPosition(0, transform.position);

        GameObject cm = VuforiaManager.Instance.ARCameraTransform.gameObject;
        Camera[] cam = cm.GetComponentsInChildren<Camera>();
        touchRay = cam[0].ScreenPointToRay(Input.mousePosition);
        
        Physics.Raycast(touchRay, out touchHit);//터치한 지점 탐색
        shootRay.origin = transform.position;
        shootRay.direction = touchHit.point - transform.position;


        if (Physics.Raycast(shootRay, out shootHit))
        {
            Debug.Log("Hit : " + shootHit.collider.gameObject.name);
            gunLine.SetPosition(1, shootHit.point);
            Instantiate(flare, shootHit.point, transform.rotation);
            //takedamage ---> 적이 맞았을때 함수를 추가한다.
        }
        
    }

}
