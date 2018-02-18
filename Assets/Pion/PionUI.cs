using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PionUI : MonoBehaviour {

	public int X;
	public int Y;
	public Pion Pion;

	private Jeu jeu;

	void Start () {
		jeu = FindObjectOfType<Jeu>();
	}
	
	void Update () {
		if (jeu.EstActif (this))
			Lever ();
		else
			Baisser ();
	}

	void Lever() {
		transform.DOLocalMoveY (4f, .1f);
	}

	void Baisser() {
        transform.DOLocalMoveY(0f, .1f);
	}

	void OnMouseDown() {
		if (!jeu.CestMonTour (Pion))
			return;
		
		jeu.ActiverPion (this);
	}
        
	public void DeplacerVers(Trou destination) {
        Vector3 vecteurDestination = new Vector3(
            destination.transform.position.x,
            transform.position.y,
            destination.transform.position.z);

        transform.DOMove(vecteurDestination, .1f);

        X = destination.X;
        Y = destination.Y;
	}
}
