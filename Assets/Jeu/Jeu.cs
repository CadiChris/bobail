using System.Collections;
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
		plateau.Deplacer (PionCourant.X, PionCourant.Y, destination.X, destination.Y);
		PionCourant.DeplacerVers (destination);
		PionCourant = null;
		TourSuivant ();
	}

	public bool TrouEstOccupe(int x, int y) {
		return plateau.PionSur(x, y) != Pion.Vide;
	}
}

class Plateau {
	Pion[,] plateau = new Pion[,] {
		{Pion.A, 	Pion.A, 	Pion.A, 		Pion.A, 	Pion.A},
		{Pion.Vide, Pion.Vide, 	Pion.Vide, 		Pion.Vide, 	Pion.Vide},
		{Pion.Vide, Pion.Vide, 	Pion.Bobail, 	Pion.Vide, 	Pion.Vide},
		{Pion.Vide, Pion.Vide, 	Pion.Vide, 		Pion.Vide, 	Pion.Vide},
		{Pion.B, 	Pion.B, 	Pion.B, 		Pion.B, 	Pion.B},
	};

	public Pion PionSur(int x, int y) {
		return plateau [y, x];
	}

	public void Deplacer(int xDepart, int yDepart, int xArrivee, int yArrivee) {
		plateau [yArrivee, xArrivee] = plateau [yDepart, xDepart];
		plateau [yDepart, xDepart] = Pion.Vide;
	}
}

public enum Pion { A, B, Vide, Bobail }
