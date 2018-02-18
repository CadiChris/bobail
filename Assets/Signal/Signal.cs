using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Signal : MonoBehaviour {

	public bool EstBobail;
	public string Joueur;

	private Jeu jeu;

	void Start () {
		jeu = GameObject.FindObjectOfType<Jeu> ();
	}
	
	void Update () {
		if (!EstBobail) {
			gameObject.SetActive (jeu.JoueurCourant == Joueur);
		} else {
			gameObject.SetActive (jeu.JoueurCourant == Joueur && EstBobail == jeu.CestMonTour (Pion.Bobail));
		}
	}
}
