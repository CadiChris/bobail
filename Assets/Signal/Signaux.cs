using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Signaux : MonoBehaviour {

	public GameObject A_Pion;
	public GameObject A_Bobail;
	public GameObject B_Pion;
	public GameObject B_Bobail;

	Jeu jeu;

	void Start () {
		jeu = GameObject.FindObjectOfType<Jeu> ();
	}
	
	void Update () {
		A_Pion.SetActive (jeu.JoueurCourant == "A" && !jeu.CestMonTour(Pion.Bobail));
		A_Bobail.SetActive (jeu.JoueurCourant == "A" && jeu.CestMonTour(Pion.Bobail));
		B_Pion.SetActive (jeu.JoueurCourant == "B" && !jeu.CestMonTour(Pion.Bobail));
		B_Bobail.SetActive (jeu.JoueurCourant == "B" && jeu.CestMonTour(Pion.Bobail));
	}
}
