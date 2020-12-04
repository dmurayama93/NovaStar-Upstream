﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    //[SerializeField]
    //private GameObject _shotPrefab;

    // Player attack values
    //[SerializeField]
    private float _fireRate = 0.25f;

    private float _laserFireRate = 4.0f;

    private float _canFire = -1.0f;

    private bool _canShoot = true;

    private float _canFireLaser = -1.0f;

    private float startTime;

    private int weaponLevel = 6;

    private bool laserCharging = false;

    private bool laserFiring = false;

    private float chargeTime;

    private float chargeRate = 2.0f;

    private float maxCharge = 3.0f;

    private GameObject newCharge;

    private float totalCharge;

    [SerializeField]
    private GameObject _laserPrefab;

    [SerializeField]
    private Animator laserAnim;


    [SerializeField]
    private GameObject chargeSprite;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (weaponLevel == 6)
        {
            if (Input.GetKeyDown(KeyCode.Space) && _canShoot)
            {
                _canShoot = false;
                startTime = Time.time;
                laserCharging = true;
                chargeSprite.SetActive(true);


                //StartCoroutine(ChargeLaser());
            }

            if (Input.GetKeyUp(KeyCode.Space) && laserCharging)
            {
                totalCharge = Time.time - startTime;


                if (totalCharge >= 3.0f)
                {
                    totalCharge = 3.0f;
                }
                Debug.Log("Time: " + totalCharge);
                chargeSprite.SetActive(false);
                laserCharging = false;
                FireLaser();
            }
        }

        if (!_canShoot)
        {
            //FireShot();
            //ChargeLaser();

            //StartCoroutine(TimerRoutine());
        }


    }


    void FireLaser()
    {
        Debug.Log("Start Laser");

        _laserPrefab.SetActive(true);

        //newCharge = Instantiate(_laserPrefab, transform);

        laserAnim.SetTrigger("laserStart");

        laserFiring = true;


        if (laserAnim.GetBool("laserEnd") == true)
        {
            laserAnim.ResetTrigger("laserEnd");
        }

        StartCoroutine(Timer());
        //Destroy(_laserPrefab);
    }


    IEnumerator Timer()
    {
        float startTime = Time.time;

        float fireTime = totalCharge;

        Debug.Log("Laser fire duration: " + fireTime);

        while (fireTime > 0)
        {
            fireTime -= Time.deltaTime;
            yield return null;
        }

        laserFiring = false;

        laserAnim.SetTrigger("laserEnd");

        if (laserAnim.GetBool("laserStart") == true)
        {
            laserAnim.ResetTrigger("laserStart");
        }

        _laserPrefab.SetActive(false);
        _canShoot = true;
    }


    /*
    IEnumerator TimerRoutine()
    {
        _canShoot = true;
        if (Input.GetKeyDown(KeyCode.Space))
        {
           
            yield return new WaitForSeconds(2f);
            Debug.Log("Start Charge");
            chargeTime += chargeRate;
        }

        if (Input.GetKeyUp(KeyCode.Space) && Time.time > 2f)
        {
            Debug.Log("Fired Laser");
            chargeTime = 0;
        }

        if(Input.GetKeyUp(KeyCode.Space) && Time.time < 2f)
        {
            Debug.Log("Charge Cancelled");
            chargeTime = 0;
        }

        _canShoot = false;
    }

    */

    /*  void ChargeLaser()
      {
          _canFireLaser = Time.time + _laserFireRate;

          Debug.Log("Start Charge");
          laserCharging = true;
          startTime = Time.time;

          chargeTime = startTime + maxCharge;


          if (Input.GetKeyUp(KeyCode.Space))
          {
              chargeTime = Time.time - startTime;
              Debug.Log("Total Charge Time: " + chargeTime);
          }

          if(Time.time >= chargeTime)
          {
              Debug.Log("Fully charged");
              laserCharging = false;
          }

          /*
          //chargeSprite.SetActive(true);
          //var newCharge = Instantiate(chargeSprite, transform);
  /*
          if(Input.GetKey(KeyCode.Space))
          {

              chargeTime = Time.time - startTime;

              if (chargeTime >= 3.0f || Input.GetKeyUp(KeyCode.Space))
              {
                  Debug.Log("Fully charged");
                  laserCharging = false;
              }

          }

          if (chargeTime > 3.0f)
          {
              totalCharge = 3.0f;

              Debug.Log("Total Charge Time: " + totalCharge);
          }
          else
          {
              totalCharge = chargeTime;
              Debug.Log("Total Charge Time: " + totalCharge);
          }
  */
    //chargeSprite.SetActive(false);
    //Destroy(newCharge);
    //FireLaser();

    //}



}