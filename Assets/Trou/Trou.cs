using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Trou : MonoBehaviour {

	public int X;
	public int Y;

	Jeu jeu;
    Material mat;
    Color couleurEteint;
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
        mat.DOColor(new Color(50, 50, 50, 50), "_EmissionColor", .5f)
           .SetEase(Ease.InSine);
        estAllume = true;
    }

    void Eteindre() {
        mat.DOColor(couleurEteint, "_EmissionColor", .5f)
           .SetEase(Ease.InSine);
        estAllume = false;
    }
}