using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class GpsTest {

    Gps gps;

    [SetUp]
    public void AvantChaqueTest() {
        gps = new Gps(new Plateau());
    }

    [Test]
    public void CalculeVersLeHaut() {
        var destinationsPossibles = gps.DestinationsDepuis(0, 0);

        CollectionAssert.Contains(
            destinationsPossibles,
            new Coordonnees { X = 3, Y = 0 });
    }

    [Test]
    public void CalculeVersLeBas() {
        var destinationsPossibles = gps.DestinationsDepuis(4, 0);
        CollectionAssert.Contains(
            destinationsPossibles,
            new Coordonnees { X = 1, Y = 0 }
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
