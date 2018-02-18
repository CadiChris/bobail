﻿using UnityEngine;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Gps {
    readonly Plateau _plateau;

    public Gps(Plateau plateau){
        _plateau = plateau;
    }

    public List<Coordonnees> DestinationsDepuis(int x, int y) {

        var destinations = new List<Coordonnees>{
            VersLeHaut(x, y),
            VersLeBas(x, y),
            VersLaGauche(x, y),
            VersLaDroite(x, y),
            HautGauche(x, y),
            HautDroite(x, y),
            BasGauche(x, y),
            BasDroite(x, y)
        };

        var depart = new Coordonnees { X = x, Y = y };
        return destinations
            .Where(d => !d.Equals(depart))
            .ToList();
    }

    public Coordonnees VersLeHaut(int xDepart, int yDepart) {
        var xArrivee = xDepart;
        for (int x = xDepart; x < 5; x++) {
            var peutAvancer = _plateau.PionSur(x, yDepart) == Pion.Vide;
            if (peutAvancer) xArrivee = x;
            else if (x != xDepart) break;
        }
        return new Coordonnees { X = xArrivee, Y = yDepart };
    }

    public Coordonnees VersLeBas(int xDepart, int yDepart) {
        var xArrivee = xDepart;
        for (int x = xDepart; x >= 0; x--) {
            var peutAvancer = _plateau.PionSur(x, yDepart) == Pion.Vide;
            if (peutAvancer) xArrivee = x;
            else if (x != xDepart) break;
        }
        return new Coordonnees { X = xArrivee, Y = yDepart };
    }

    public Coordonnees VersLaGauche(int xDepart, int yDepart){
        int yArrivee = yDepart;
        for (int y = yDepart; y >= 0; y--) {
            var peutAvancer = _plateau.PionSur(xDepart, y) == Pion.Vide;
            if (peutAvancer) yArrivee = y;
            else if (y != yDepart) break;
        }
        return new Coordonnees { X = xDepart, Y = yArrivee };
    }

    public Coordonnees VersLaDroite(int xDepart, int yDepart) {
        int yArrivee = yDepart;
        for (int y = yDepart; y < 5; y++) {
            var peutAvancer = _plateau.PionSur(xDepart, y) == Pion.Vide;
            if (peutAvancer) yArrivee = y;
            else if (y != yDepart) break;
        }
        return new Coordonnees { X = xDepart, Y = yArrivee };
    }

    public Coordonnees HautGauche(int xDepart, int yDepart) {
        var xArrivee = xDepart;
        int yArrivee = yDepart;
        for (int x = xDepart, y = yDepart; x < 5 && y >= 0; x++, y--) {
            var peutAvancer = _plateau.PionSur(x, y) == Pion.Vide;
            if (peutAvancer) {
                xArrivee = x;
                yArrivee = y;
            }
            else if (y != yDepart) break;
        }
        return new Coordonnees { X = xArrivee, Y = yArrivee };
    }

    public Coordonnees HautDroite(int xDepart, int yDepart) {
        var xArrivee = xDepart;
        int yArrivee = yDepart;
        for (int x = xDepart, y = yDepart; x < 5 && y < 5; x++, y++) {
            var peutAvancer = _plateau.PionSur(x, y) == Pion.Vide;
            if (peutAvancer) {
                xArrivee = x;
                yArrivee = y;
            }
            else if (y != yDepart) break;
        }
        return new Coordonnees { X = xArrivee, Y = yArrivee };
    }

    public Coordonnees BasGauche(int xDepart, int yDepart) {
        var xArrivee = xDepart;
        int yArrivee = yDepart;
        for (int x = xDepart, y = yDepart; x >=0 && y >= 0; x--, y--) {
            var peutAvancer = _plateau.PionSur(x, y) == Pion.Vide;
            if (peutAvancer)
            {
                xArrivee = x;
                yArrivee = y;
            }
            else if (y != yDepart) break;
        }

        return new Coordonnees { X = xArrivee, Y = yArrivee };
    }

    public Coordonnees BasDroite(int xDepart, int yDepart) {
        var xArrivee = xDepart;
        int yArrivee = yDepart;
        for (int x = xDepart, y = yDepart; x >= 0 && y < 5; x--, y++) {
            var peutAvancer = _plateau.PionSur(x, y) == Pion.Vide;
            if (peutAvancer) {
                xArrivee = x;
                yArrivee = y;
            }
            else if (y != yDepart) break;
        }

        return new Coordonnees { X = xArrivee, Y = yArrivee };
    }
}

public class Coordonnees {
    public int X;
    public int Y;

    public override bool Equals(object obj)
    {
        if(obj.GetType() != typeof(Coordonnees))
            return false;
        
        var autre = (Coordonnees)obj;
        return autre.X == X && autre.Y == Y;
    }

    public override string ToString()
    {
        return "[" + X + "," + Y + "]";
    }
}