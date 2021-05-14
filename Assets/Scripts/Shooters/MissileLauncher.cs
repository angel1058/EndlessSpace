using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissileLauncher : MonoBehaviour {


   	private string logPrefix = "[EN-UNITY] [MISSILE LAUNCH] - ";

	[Header("Auto Game Objects")]
	public Transform target;


	[Header("Setable Game Objects")]
    public GameObject missilePreFab;
	public Transform spawnPointer;


	[Header("Missile Value")]
	[Range(1f,3f)]
	public float MissileStrength = 1f;
	public float MaxStrength = 3f;
	public float MinStrength = 1f;
	public float Colour;


	[Header("Fire Rate")]
	public float MinPerSecond = 1f;
	public float MaxPerSecond = 5f;
	public bool AIFiringRate = false;

	[Range(0,1)]
	public float RateMultiplier = 0f;

	private bool wasEmpty = false;

	[Header("Energy Stuff")]
	public Image healthBar;
	public float fillPerSecond = 1;
	public float maxEnergy = 100;
	public float energyCostPerMissile = 1;
	public float SilentPercentage = 25;

	[Header("Debug")]
	public float DistanceToTarget = 0;
	public float DistanceToSpawnPoint = 0;
	public float currentEnergy = 100;
	public float PercentageDistanceToTarget = 0;
	public bool DisableCoolDown = false;



	void Update()
	{
		healthDelta(fillPerSecond * Time.deltaTime);
		if (currentEnergy > SilentPercentage)
			wasEmpty = false;
	}

	void healthDelta(float delta)
	{
		currentEnergy += delta;
		if (currentEnergy > maxEnergy)
			currentEnergy = maxEnergy;
		healthBar.fillAmount = currentEnergy / maxEnergy;
	}

	float fireRate()
	{
		if (AIFiringRate == true && target != null) {
			DistanceToTarget = target.position.y - transform.position.y;
			RateMultiplier = 1-(DistanceToTarget / DistanceToSpawnPoint);
		}

		float Range = MaxPerSecond - MinPerSecond;

		return 1.0f/(MinPerSecond + (Range * RateMultiplier));
	}

    void Start()
    {
		//start 
		DistanceToSpawnPoint = spawnPointer.position.y - transform.position.y;
        StartCoroutine( Shoot() ) ;
		InvokeRepeating("Shoot", 1.0f, fireRate());
		currentEnergy = maxEnergy;
		healthBar.fillAmount = currentEnergy / maxEnergy;
    }

 
    IEnumerator   Shoot()
    {
        yield return new WaitForSeconds(1);
        while (true)
        {
			if (DisableCoolDown == false && (currentEnergy < 1 || (wasEmpty == true && currentEnergy < SilentPercentage))) {
				wasEmpty = true;
			} else {
    
				if (target != null)
					 {
					GameObject missile = Instantiate (missilePreFab, transform.position, transform.rotation);

					Missile m = missile.gameObject.GetComponent<Missile> ();
						
					m.SetFirePoint (gameObject);
					Colour =  1-((MissileStrength- MinStrength)/(MaxStrength - MinStrength));

					m.SetMissileStrength(MissileStrength , Colour);
					GameMgr.Instance.Fire ();
					healthDelta (-energyCostPerMissile);
					healthBar.fillAmount = currentEnergy / maxEnergy;
				}
			}     
			yield return new WaitForSeconds(fireRate());
        }
	}
    }

