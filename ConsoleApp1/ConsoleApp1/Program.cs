using System;
using System.Collections.Generic;
using System.IO;

namespace PbInfo
{
    public class Program
    {
        static void Main(string[] args)
        {
            string cheminFichier = @"C:\Users\ewenr\Downloads\karate.txt";
            string[] lignes = File.ReadAllLines(cheminFichier);
            string[] premiereLigne = lignes[0].Split(' ');
            int nbSommets = int.Parse(premiereLigne[0]);
            List<Lien> liens = new List<Lien>();
            for (int i = 1; i < lignes.Length; i++)
            {
                string[] elementsLigne = lignes[i].Split(' ');
                if (elementsLigne.Length == 2)
                {
                    int sommet1 = int.Parse(elementsLigne[0]);
                    int sommet2 = int.Parse(elementsLigne[1]);
                    liens.Add(new Lien(sommet1, sommet2));
                }
            }
            Graphe graphe = new Graphe(liens, nbSommets);
            graphe.AfficherListeAdjacence();
            graphe.AfficherMatriceAdjacence();
            graphe.Largeur(1);
            graphe.Profondeur(1);
            if (graphe.ContientUnCycle())
                Console.WriteLine("Le graphe contient un cycle");
            else
                Console.WriteLine("Le graphe ne contient pas de cycle");
            graphe.AnalyserGraphe();
            graphe.VisualiserGraphe("graphe.png");
        }
    }
}







using System;
using PbInfo;
using System.Collections.Generic;
using Xunit;
namespace ProjetUnitaire
{
    public class UnitTestGraphe
    {
        [Fact]
        public void TestListeAdjacence()
        {
            List<Lien> liens = new List<Lien>
            {
                new Lien(1, 2),
                new Lien(1, 3),
                new Lien(2, 3),
                new Lien(3, 4)
            };
            Graphe graphe = new Graphe(liens, 4);
            var adjacence = graphe.GetListeAdjacence();
            Assert.Equal(new List<int> { 2, 3 }, adjacence[1]);
            Assert.Equal(new List<int> { 1, 3 }, adjacence[2]);
            Assert.Equal(new List<int> { 1, 2, 4 }, adjacence[3]);
            Assert.Equal(new List<int> { 3 }, adjacence[4]);
        }

        [Fact]
        public void TestMatriceAdjacence()
        {
            List<Lien> liens = new List<Lien>
            {
                new Lien(1, 2),
                new Lien(1, 3),
                new Lien(2, 3),
                new Lien(3, 4)
            };
            Graphe graphe = new Graphe(liens, 4);
            var matrice = graphe.GetMatriceAdjacence();
            Assert.Equal(1, matrice[0, 1]);
            Assert.Equal(1, matrice[0, 2]); 
            Assert.Equal(1, matrice[1, 2]); 
            Assert.Equal(1, matrice[2, 3]); 
            Assert.Equal(0, matrice[0, 3]); 
        }

        [Fact]
        public void TestConnexite()
        {
            List<Lien> liens1 = new List<Lien>
            {
                new Lien(1, 2),
                new Lien(2, 3),
                new Lien(3, 4)
            };
            Graphe grapheConnexe = new Graphe(liens1, 4);

            Assert.True(grapheConnexe.EstConnexe());
            List<Lien> liens2 = new List<Lien>
            {
                new Lien(1, 2),
                new Lien(3, 4)
            };
            Graphe grapheNonConnexe = new Graphe(liens2, 4);

            Assert.False(grapheNonConnexe.EstConnexe());
        }

        [Fact]
        public void TestDetectionCycle()
        {
            List<Lien> liens1 = new List<Lien>
            {
                new Lien(1, 2),
                new Lien(2, 3),
                new Lien(3, 1)
            };
            Graphe grapheAvecCycle = new Graphe(liens1, 3);
            Assert.True(grapheAvecCycle.ContientUnCycle());
            List<Lien> liens2 = new List<Lien>
            {
                new Lien(1, 2),
                new Lien(2, 3)
            };
            Graphe grapheSansCycle = new Graphe(liens2, 3);
            Assert.False(grapheSansCycle.ContientUnCycle());
        }

        [Fact]
        public void TestParcoursLargeur()
        {
            List<Lien> liens = new List<Lien>
            {
                new Lien(1, 2),
                new Lien(1, 3),
                new Lien(2, 4),
                new Lien(3, 5)
            };
            Graphe graphe = new Graphe(liens, 5);
            var parcours = graphe.GetParcoursLargeur(1);
            Assert.Equal(new List<int> { 1, 2, 3, 4, 5 }, parcours);
        }

        [Fact]
        public void TestParcoursProfondeur()
        {
            List<Lien> liens = new List<Lien>
            {
                new Lien(1, 2),
                new Lien(1, 3),
                new Lien(2, 4),
                new Lien(3, 5)
            };
            Graphe graphe = new Graphe(liens, 5);
            var parcours = graphe.GetParcoursProfondeur(1);
            Assert.Equal(new List<int> { 1, 2, 4, 3, 5 }, parcours);
        }
    }
}




using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PbInfo
{
    /// <summary>
    /// Représente un nœud dans un graphe.
    /// </summary>
    public class Noeud
    {
        /// <summary>
        /// Identifiant unique du nœud.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Constructeur de la classe Noeud.
        /// </summary>
        /// <param name="id">Identifiant du nœud.</param>
        public Noeud(int id)
        {
            Id = id;
        }
        /// <summary>
        /// Retourne une représentation du nœud.
        /// </summary>
        /// <returns>Chaîne représentant le nœud.</returns>
        public override string ToString()
        {
            return $"Noeud: {Id}";
        }
    }
}





