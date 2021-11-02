using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(PlayerStatsLoader))]
public class CarHealth : MonoBehaviour {
	[SerializeField] int health;
	[SerializeField] int maxHealth;
	[SerializeField] int damage;
	[SerializeField] int burningThresholdHealth = 60;
	private PlayerStats _playerStats;

	private void Awake() {
		health = maxHealth;
		_playerStats = GetComponent<PlayerStatsLoader>().playerStats;
		// _playerStats.InitializePlayerStats();
	}

	private void LateUpdate()
	{
		if (health <= 0) {
			Destroy(this.gameObject);
			CarExplodes();
		}
	}

	private void OnCollisionEnter(Collision other) {
		if (other.gameObject.CompareTag("Vehicle") || other.gameObject.CompareTag("Wall")) {
			TakeDamage(damage);
			CarBurns();
		}
		else if (other.gameObject.CompareTag("Water")) {
			Destroy(this.gameObject);
			_playerStats.KillPlayer(gameObject);
		}
	}

	private void OnParticleCollision(GameObject other)
	{
		TakeDamage(damage);
	}

	public void TakeDamage(int damage) {
		health -= damage;
		health = Mathf.Clamp(health, 0 , maxHealth);
	}

	void CarExplodes() {
		Debug.Log("iExploded");
		if (GetComponent<Vehicle>().PlayerIsInCar())
		{
			_playerStats.KillPlayer(gameObject);
		}
		//whateverfancysmancystyff.
		//add Michael Bay...
	}

	void CarBurns()
	{
		if (health <= burningThresholdHealth)
		{
			//health -= firedamage
			Debug.Log("Car is on fire");
		}
	}
}
