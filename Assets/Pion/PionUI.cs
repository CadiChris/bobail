using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PionUI : MonoBehaviour {

	public int X;
	public int Y;
	public Pion Pion;

	Jeu jeu;
    bool estLeve;
    bool mouseOver;
    float hauteurDepart;

	void Start () {
		jeu = FindObjectOfType<Jeu>();
        estLeve = false;
        mouseOver = false;
        hauteurDepart = transform.position.y;
	}
	
	void Update () {
		if (jeu.EstActif (this) && !estLeve)
			Lever ();
        if (!jeu.EstActif(this) && estLeve && !mouseOver) 
			Baisser ();
	}

	void Lever(float vitesse = .1f) {
		transform.DOMoveY(hauteurDepart + 1f, vitesse);
        estLeve = true;
	}

	void Baisser() {
        transform.DOMoveY(hauteurDepart, .1f);
        estLeve = false;
	}

    private void OnMouseEnter() {
        if (!jeu.CestMonTour(Pion))
            return;

        if (estLeve) return;
        
        mouseOver = true;
        if (!estLeve) Lever();
    }

    private void OnMouseExit () {
        mouseOver = false;
        if(estLeve && !jeu.EstActif(this)) Baisser();
    }

    void OnMouseDown() {
		if (!jeu.CestMonTour (Pion))
			return;
		
		jeu.ActiverPion (this);
	}
        
	public void DeplacerVers(Trou destination) {
        Vector3 arrivee = new Vector3(
            destination.transform.position.x,
            hauteurDepart,
            destination.transform.position.z);

        transform.position = arrivee;
        Arrivee(destination);
	}

    void Arrivee(Trou destination) {
        X = destination.X;
        Y = destination.Y;
        estLeve = false;
    }
}
