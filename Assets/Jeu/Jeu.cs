﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
		PionCourant.DeplacerVers (destination);
		PionCourant = null;
		TourSuivant ();
	}
}

class Plateau {
	TrouBack[,] plateau = new TrouBack[,] {
		{new TrouBack(Pion.A), new TrouBack(Pion.A), new TrouBack(Pion.A), new TrouBack(Pion.A), new TrouBack(Pion.A)},
		{new TrouBack(), new TrouBack(), new TrouBack(), new TrouBack(), new TrouBack()},
		{new TrouBack(), new TrouBack(), new TrouBack(Pion.Bobail), new TrouBack(), new TrouBack()},
		{new TrouBack(), new TrouBack(), new TrouBack(), new TrouBack(), new TrouBack()},
		{new TrouBack(Pion.B), new TrouBack(Pion.B), new TrouBack(Pion.B), new TrouBack(Pion.B), new TrouBack(Pion.A)},
	};

}

class TrouBack {
	public readonly Pion Pion;
	public TrouBack(Pion pion) {
		Pion = pion;
	}
	public TrouBack() : this(Pion.Vide) {
	}
}

public enum Pion { A, B, Vide, Bobail }