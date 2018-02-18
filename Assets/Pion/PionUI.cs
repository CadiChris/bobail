using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PionUI : MonoBehaviour {

	public int X;
	public int Y;
	public Pion Pion;

	private Jeu jeu;
	const float Y_BAS = 4f;
	const float Y_HAUT = 8f;

	void Start () {
		jeu = GameObject.FindObjectOfType<Jeu> ();
	}
	
	void Update () {
		if (jeu.EstActif (this))
			Lever ();
		else
			Baisser ();
	}

	void Lever() {
		transform.position = new Vector3 (
			transform.position.x,
			Y_HAUT,
			transform.position.z);
	}

	void Baisser() {
		transform.position = new Vector3 (
			transform.position.x, 
			Y_BAS,
			transform.position.z);
	}

	void OnMouseDown() {
		if (!jeu.CestMonTour (Pion))
			return;
		
		jeu.ActiverPion (this);
	}

	public void DeplacerVers(Trou destination) {
		transform.position = new Vector3 (
			destination.transform.position.x, 
			Y_BAS, 
			destination.transform.position.z); 
	}
}
