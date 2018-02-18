using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trou : MonoBehaviour {

	public int X;
	public int Y;

	Jeu jeu;

	void Start () {
		jeu = GameObject.FindObjectOfType<Jeu> ();	
	}
	
	void Update () {
		
	}

	void OnMouseDown() {
		if (!jeu.PretADeplacer ())
			return;

		if (jeu.TrouEstOccupe (X, Y))
			return;

		jeu.DeplacerActif (this);
	}
}
