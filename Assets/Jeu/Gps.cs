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
            VersLeHaut(x, y)
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