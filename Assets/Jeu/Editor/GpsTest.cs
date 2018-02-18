using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;

public class GpsTest {

    Gps gps;

    [SetUp]
    public void AvantChaqueTest() {
        gps = new Gps(new Plateau());
    }

    [Test]
    public void CalculeA360Degres() {
        var destinationsPossibles = gps.DestinationsDepuis(2, 2);

        CollectionAssert.AreEquivalent(
            new List<Coordonnees> {
                new Coordonnees { X = 3, Y = 2 },
                new Coordonnees { X = 1, Y = 2 },
                new Coordonnees { X = 2, Y = 0 },
                new Coordonnees { X = 2, Y = 4 },
                new Coordonnees { X = 3, Y = 1 },
                new Coordonnees { X = 3, Y = 3 },
                new Coordonnees { X = 1, Y = 1 },
                new Coordonnees { X = 1, Y = 3 },
            },
            destinationsPossibles
        );
    }

    [Test]
    public void NeSortPasDuPlateau() {
        var destinationsPossibles = gps.DestinationsDepuis(4, 0);

        CollectionAssert.DoesNotContain(
            destinationsPossibles,
            new Coordonnees { X = 5, Y = 0 });
    }

    [Test]
    public void NeContientPasLeDepart() {
        var destinationsPossibles = gps.DestinationsDepuis(4, 0);

        CollectionAssert.DoesNotContain(
            destinationsPossibles,
            new Coordonnees { X = 4, Y = 0 });
    }

    [Test]
    public void NePassePasATraversUnPion() {
        var destinationsPossibles = gps.DestinationsDepuis(0, 2);

        CollectionAssert.DoesNotContain(
            destinationsPossibles,
            new Coordonnees { X = 3, Y = 2 });
    }
}
