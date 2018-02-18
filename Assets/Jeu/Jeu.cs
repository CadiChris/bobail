﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Jeu : MonoBehaviour {

	Plateau plateau;
	Pion pionCourant;
	Queue joueurs;
	Queue mouvements;
	public string JoueurCourant;
	public PionUI PionCourant;

	void Start() {
		plateau = new Plateau ();
		pionCourant = Pion.A;
		joueurs = new Queue ();
		joueurs.Enqueue ("A");
		joueurs.Enqueue ("B");
		mouvements = new Queue ();
		mouvements.Enqueue (Pion.A);
		mouvements.Enqueue (Pion.Bobail);
		mouvements.Enqueue (Pion.B);

		PremierTour ();
	}
	void PremierTour() {
		JoueurCourant = (string)joueurs.Dequeue ();
		joueurs.Enqueue (JoueurCourant);
		pionCourant = (Pion)mouvements.Dequeue ();
	}

	void TourSuivant() {
		bool joueurFini = pionCourant != Pion.Bobail;
		if (joueurFini)
			NouveauTour ();
		pionCourant = (Pion)mouvements.Dequeue ();
	}

	void NouveauTour() {
		JoueurCourant = (string)joueurs.Dequeue ();
		joueurs.Enqueue (JoueurCourant);

		mouvements.Enqueue (Pion.Bobail);
		mouvements.Enqueue (pionCourant);
	}

	public bool CestMonTour(Pion pion) {
		return pionCourant == pion;
	}
		
	public void ActiverPion(PionUI pion) {
		PionCourant = pion;
	}

	public bool EstActif(PionUI pion) {
		return pion == PionCourant;
	}

	public bool PretADeplacer() {
		return PionCourant != null;
	}

	public void DeplacerActif(Trou destination) {
		plateau.Deplacer (PionCourant.X, PionCourant.Y, destination.X, destination.Y);
		PionCourant.DeplacerVers (destination);
		PionCourant = null;
		if (plateau.Vainqueur () != Pion.Vide)
			GameOver (plateau.Vainqueur ());
		else
			TourSuivant ();
	}

	void GameOver(Pion vainqueur) {
		StartCoroutine (NouvellePartie ());
	}
	IEnumerator NouvellePartie() {
		AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Bobail");

		while (!asyncLoad.isDone)
			yield return null;
	}

	public bool TrouEstOccupe(int x, int y) {
		return plateau.PionSur(x, y) != Pion.Vide;
	}

    public bool ActifPeutAtteindre(int x, int y) {
        return x == 1 && y == 1 && PionCourant != null;
    }
}

class Plateau {
    readonly Pion[,] plateau = new Pion[,] {
        {Pion.A,    Pion.A,     Pion.A,         Pion.A,     Pion.A},
        {Pion.Vide, Pion.Vide,  Pion.Vide,      Pion.Vide,  Pion.Vide},
        {Pion.Vide, Pion.Vide,  Pion.Bobail,    Pion.Vide,  Pion.Vide},
        {Pion.Vide, Pion.Vide,  Pion.Vide,      Pion.Vide,  Pion.Vide},
        {Pion.B,    Pion.B,     Pion.B,         Pion.B,     Pion.B},
    };

    public Pion PionSur(int x, int y) {
		return plateau [x, y];
	}

	public void Deplacer(int xDepart, int yDepart, int xArrivee, int yArrivee) {
		plateau [xArrivee, yArrivee] = plateau [xDepart, yDepart];
		plateau [xDepart, yDepart] = Pion.Vide;
	}

	public Pion Vainqueur() {
		return Pion.Vide;
	}

    public override string ToString(){
        string representation = "";
        for (int x = plateau.GetLength(0)-1; x >= 0; x--)
        {
            for (int y = 0; y < plateau.GetLength(1); y++)
                representation += ("("+x+","+y+") " + plateau[x, y]).PadRight(18,' ');

            representation += "\r\n";
        }
       
        return representation;
    }
}

public enum Pion { A, B, Vide, Bobail }
