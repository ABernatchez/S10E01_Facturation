using Commandes.Application;
using Commandes.Infrastructure;
using Commandes.Presentation;
using Xunit;

namespace Commandes.Tests;

public sealed class ObserverAtelierTests
{
    [Fact]
    [Trait("Categorie", "Atelier")]
    public void Ajout_RecoitLeMemeInstantaneDansDeuxObservateurs()
    {
        using ModeleCommande modele = CreerModele();
        ObservateurEspion premier = new ObservateurEspion();
        ObservateurEspion second = new ObservateurEspion();
        modele.Subscribe(premier);
        modele.Subscribe(second);

        modele.Ajouter(1, 2);

        Assert.Equal(1, premier.Etats.Count);
        Assert.Equal(1, second.Etats.Count);
        EtatCommandeDto produit1 = premier.Etats[0];
        EtatCommandeDto produit2 = second.Etats[0];
        Assert.Same(produit1, produit2);
        Assert.Equal(14.30m, produit1.Facture.Total);
    }

    [Fact]
    [Trait("Categorie", "Atelier")]
    public void Dispose_RetireUneVueEtLaisseLAutreAbonnee()
    {
        using ModeleCommande modele = CreerModele();
        ObservateurEspion premier = new ObservateurEspion();
        ObservateurEspion second = new ObservateurEspion();
        var premierDisposable = modele.Subscribe(premier);
        var secondDisposable = modele.Subscribe(second);

        premierDisposable.Dispose();
        premierDisposable.Dispose();
        modele.Ajouter(1, 2);

        Assert.Equal(0, premier.Etats.Count);
        Assert.Equal(1, second.Etats.Count);
    }

    private static ModeleCommande CreerModele()
    {
        ServiceCommandes commandes = new ServiceCommandes(new DepotCommandesMemoire(), new CatalogueProduitsMemoire());
        return new ModeleCommande(commandes, new ServiceFacturation(commandes), new JournalSilencieux());
    }

    private sealed class ObservateurEspion : IObserver<EtatCommandeDto>
    {
        public List<EtatCommandeDto> Etats { get; } = new List<EtatCommandeDto>();
        public void OnNext(EtatCommandeDto etat)
        {
            Etats.Add(etat);
        }
        public void OnCompleted()
        {
        }
        public void OnError(Exception erreur)
        {
        }
    }

    private sealed class JournalSilencieux : IJournalIncidents
    {
        public void Consigner(string operation, Exception erreur)
        {
        }
    }
}
