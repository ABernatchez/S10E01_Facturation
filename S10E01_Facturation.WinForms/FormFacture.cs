using Commandes.Presentation;

namespace S10E01_Facturation.WinForms;

public sealed partial class FormFacture : Form, IObserver<EtatCommandeDto>
{
    private IDisposable? m_abonnement;

    private readonly ModeleCommande? m_modele;

    // Constructeur réservé au designer.
    public FormFacture()
    {
        InitializeComponent();
    }

    public FormFacture(ModeleCommande modele) : this()
    {
        ArgumentNullException.ThrowIfNull(modele);
        m_modele = modele;
    }

    private void FormFacture_Load(object? sender, EventArgs e)
    {
        if (m_modele is null)
        {
            return; // Le constructeur sans modèle est réservé au designer.
        }

        AfficherEtat(m_modele.Consulter());
        m_abonnement = m_modele.Subscribe(this);
    }

    public void OnNext(EtatCommandeDto etat)
    {
        AfficherEtat(etat);
    }

    private void AfficherEtat(EtatCommandeDto etat)
    {
        ArgumentNullException.ThrowIfNull(etat);
        lResumeFacture.Text = $"Numéro commande: {etat.Facture.NumeroCommande}\n";
        lResumeFacture.Text += $"Sous-total: {etat.Facture.SousTotal:0.00}\n";
        lResumeFacture.Text += $"Frais: {etat.Facture.Frais:0.00}\n";
        lResumeFacture.Text += $"Total: {etat.Facture.Total:0.00}\n";
        lResumeFacture.Text += $"Est provisoire: {etat.Facture.EstProvisoire}\n";
    }

    private void FormFacture_Disposed(object? sender, EventArgs e)
    {
        SeDesabonner();
    }

    // Fourni : le nettoyage peut être demandé plusieurs fois.
    private void SeDesabonner()
    {
        if (m_abonnement is not null)
        {
            m_abonnement.Dispose();
            m_abonnement = null;
        }
    }

    // Fournis : une fin du flux est distincte d'un refus métier.
    public void OnError(Exception erreur)
    {
        ArgumentNullException.ThrowIfNull(erreur);
        SeDesabonner();
        lResumeFacture.Text = "Les notifications de la facture sont interrompues.";
    }

    public void OnCompleted()
    {
        SeDesabonner();
        lResumeFacture.Text += "\nNotifications terminées.";
    }

    private void lResumeFacture_Click(object sender, EventArgs e)
    {

    }
}
