using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trou : MonoBehaviour {

	public int X;
	public int Y;
    public Light lumiere;

	Jeu jeu;
    Material mat;
    bool estAllume;

	void Start () {
		jeu = FindObjectOfType<Jeu>();
        mat = GetComponent<Renderer>().material;
        Eteindre();
	}
	
	void Update () {
        if (!estAllume && jeu.ActifPeutAtteindre(X, Y))
            Allumer();
        if (estAllume && !jeu.ActifPeutAtteindre(X, Y))
            Eteindre();
	}

	void OnMouseDown() {
		if (!jeu.PretADeplacer ())
			return;

		if (jeu.ActifPeutAtteindre (X, Y))
            jeu.DeplacerActif(this);
	}

    void Allumer() {
        lumiere.enabled = true;
        estAllume = true;

    }

    void Eteindre() {
        lumiere.enabled = false;
        estAllume = false;
    }
}