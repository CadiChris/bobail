using UnityEngine;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Gps {
    readonly Plateau _plateau;

    public Gps(Plateau plateau){
        _plateau = plateau;
    }

    public List<Coordonnees> DestinationsDepuis(int x, int y, bool estBobail = false) {

        var destinations = new List<Coordonnees>{
            VersLeHaut(x, y, estBobail),
            VersLeBas(x, y, estBobail),
            VersLaGauche(x, y, estBobail),
            VersLaDroite(x, y, estBobail),
            HautGauche(x, y, estBobail),
            HautDroite(x, y, estBobail),
            BasGauche(x, y, estBobail),
            BasDroite(x, y, estBobail)
        };

        var depart = new Coordonnees { X = x, Y = y };
        return destinations
            .Where(d => !d.Equals(depart))
            .ToList();
    }

    public Coordonnees VersLeHaut(int xDepart, int yDepart, bool estBobail = false) {
        var xArrivee = xDepart;
        if (estBobail) {
            xArrivee = Math.Min(xDepart + 1, 4);
            var peutAvancer = _plateau.PionSur(xArrivee, yDepart) == Pion.Vide;
            return new Coordonnees { X = peutAvancer ? xArrivee : xDepart, Y = yDepart };
        }
            
        for (int x = xDepart; x < 5; x++) {
            var peutAvancer = _plateau.PionSur(x, yDepart) == Pion.Vide;
            if (peutAvancer) xArrivee = x;
            else if (x != xDepart) break;
        }
        return new Coordonnees { X = xArrivee, Y = yDepart };
    }

    public Coordonnees VersLeBas(int xDepart, int yDepart, bool estBobail = false) {
        var xArrivee = xDepart;
        if(estBobail) {
            xArrivee = Math.Max(xDepart - 1, 0);
            var peutAvancer = _plateau.PionSur(xArrivee, yDepart) == Pion.Vide;
            return new Coordonnees { X = peutAvancer ? xArrivee : xDepart, Y = yDepart };
        }

        for (int x = xDepart; x >= 0; x--) {
            var peutAvancer = _plateau.PionSur(x, yDepart) == Pion.Vide;
            if (peutAvancer) xArrivee = x;
            else if (x != xDepart) break;
        }
        return new Coordonnees { X = xArrivee, Y = yDepart };
    }

    public Coordonnees VersLaGauche(int xDepart, int yDepart, bool estBobail = false){
        int yArrivee = yDepart;
        if (estBobail) {
            yArrivee = Math.Max(yDepart - 1, 0);
            var peutAvancer = _plateau.PionSur(xDepart, yArrivee) == Pion.Vide;
            return new Coordonnees { X = xDepart, Y = peutAvancer ? yArrivee : yDepart };
        }
        for (int y = yDepart; y >= 0; y--) {
            var peutAvancer = _plateau.PionSur(xDepart, y) == Pion.Vide;
            if (peutAvancer) yArrivee = y;
            else if (y != yDepart) break;
        }
        return new Coordonnees { X = xDepart, Y = yArrivee };
    }

    public Coordonnees VersLaDroite(int xDepart, int yDepart, bool estBobail = false) {
        int yArrivee = yDepart;
        if (estBobail) {
            yArrivee = Math.Min(yDepart + 1, 4);
            var peutAvancer = _plateau.PionSur(xDepart, yArrivee) == Pion.Vide;
            return new Coordonnees { X = xDepart, Y = peutAvancer ? yArrivee : yDepart };
        }
        for (int y = yDepart; y < 5; y++) {
            var peutAvancer = _plateau.PionSur(xDepart, y) == Pion.Vide;
            if (peutAvancer) yArrivee = y;
            else if (y != yDepart) break;
        }
        return new Coordonnees { X = xDepart, Y = yArrivee };
    }

    public Coordonnees HautGauche(int xDepart, int yDepart, bool estBobail = false) {
        var xArrivee = xDepart;
        int yArrivee = yDepart;
        if(estBobail) {
            xArrivee = Math.Min(xDepart + 1, 4);
            yArrivee = Math.Max(yArrivee - 1, 0);
            var peutAvancer = _plateau.PionSur(xArrivee, yArrivee) == Pion.Vide;
            return new Coordonnees
            {
                X = peutAvancer ? xArrivee : xDepart,
                Y = peutAvancer ? yArrivee : yDepart
            };
        }
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

    public Coordonnees HautDroite(int xDepart, int yDepart, bool estBobail = false) {
        var xArrivee = xDepart;
        int yArrivee = yDepart;
        if (estBobail) {
            xArrivee = Math.Min(xArrivee + 1, 4);
            yArrivee = Math.Min(yArrivee + 1, 4);
            var peutAvancer = _plateau.PionSur(xArrivee, yArrivee) == Pion.Vide;
            return new Coordonnees
            {
                X = peutAvancer ? xArrivee : xDepart,
                Y = peutAvancer ? yArrivee : yDepart
            };
        }

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

    public Coordonnees BasGauche(int xDepart, int yDepart, bool estBobail = false) {
        var xArrivee = xDepart;
        int yArrivee = yDepart;
        if(estBobail) {
            xArrivee = Math.Max(xArrivee - 1, 0);
            yArrivee = Math.Max(yArrivee - 1, 0);
            var peutAvancer = _plateau.PionSur(xArrivee, yArrivee) == Pion.Vide;
            return new Coordonnees
            {
                X = peutAvancer ? xArrivee : xDepart,
                Y = peutAvancer ? yArrivee : yDepart
            };
        }
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

    public Coordonnees BasDroite(int xDepart, int yDepart, bool estBobail = false) {
        var xArrivee = xDepart;
        int yArrivee = yDepart;
        if (estBobail) {
            xArrivee = Math.Max(xArrivee - 1, 0);
            yArrivee = Math.Min(yArrivee + 1, 4);
            var peutAvancer = _plateau.PionSur(xArrivee, yArrivee) == Pion.Vide;
            return new Coordonnees
            {
                X = peutAvancer ? xArrivee : xDepart,
                Y = peutAvancer ? yArrivee : yDepart
            };
        }
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