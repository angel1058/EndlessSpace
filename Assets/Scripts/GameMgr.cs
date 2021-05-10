using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMgr : MonoBehaviour 
{

	public static GameMgr Instance { get; private set; }

 	private double fallersDestroyed = 0;
 	private double misslesFired = 0;
 	public Text fallenUI;
	public Text missilesText;
 	void Start()
 	{
		 ShowFallenCount();  // <-- WORKS
		ShowMissileCount();
	 }

	private void ShowFallenCount()
	{
		 fallenUI.text = "Fallers : " + fallersDestroyed.ToString();
	}
	private void ShowMissileCount()
	{
		 missilesText.text = "Missles : " + misslesFired.ToString();
	}
	 public void Fallen()
	 {
		 fallersDestroyed++;
		 ShowFallenCount();  // <-- FAILS - IT THINKS fallenUI isnot set to instance etc,.  undef.
	 }

	 public void Fire()
	 {
		 misslesFired++;
		 ShowMissileCount();  // <-- FAILS - IT THINKS fallenUI isnot set to instance etc,.  undef.
	 }
	 private void Awake()
    {
        Instance = this;
    }
}
