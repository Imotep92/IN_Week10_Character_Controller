using UnityEngine;
using System.Collections;
using System;

public class RaycastShooting : MonoBehaviour
{

    [SerializeField] int gunDamage;
    [SerializeField] float fireRate;
    [SerializeField] float weaponRange;
    [SerializeField] float hitforce;
    [SerializeField] Transform gunEnd;


    Camera fpsCam;


    WaitForSeconds shotDuration = new WaitForSeconds(.07f);
    AudioSource gunAudio;
    LineRenderer laserLine;
    [SerializeField] float nextFire;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        laserLine = GetComponent<LineRenderer>();
        gunAudio = GetComponent<AudioSource>();
        fpsCam = GetComponentInParent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 rayOrigin = fpsCam.ViewportToWorldPoint(new Vector3(.5f, .5f, 0));

        laserLine.SetPosition(0, gunEnd.position);

        if (Input.GetButtonDown("Fire1") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            StartCoroutine(ShotEffect());

            RaycastHit hit;

            if (Physics.Raycast(rayOrigin, fpsCam.transform.forward, out hit, weaponRange))
            {
                laserLine.SetPosition(1, hit.point);

                ShootableBox health = hit.collider.GetComponent<ShootableBox>();

                if (health != null)
                {
                    health.Damage(gunDamage);
                }
                
                if (hit.rigidbody != null)
                {
                    hit.rigidbody.AddForce(-hit.normal * hitforce);
                }

                RagdollToggle ragdollSwitcher = hit.collider.GetComponent<RagdollToggle>();
                if (ragdollSwitcher != null)
                {
                    ragdollSwitcher.triggerRagdoll();
                }

            }
            else
            {
                laserLine.SetPosition(1, fpsCam.transform.forward * weaponRange);
            }
        } 
    }

    private IEnumerator ShotEffect()
    {
        gunAudio.Play();
        laserLine.enabled = true;

        yield return shotDuration;

        laserLine.enabled = false;
    }
}
