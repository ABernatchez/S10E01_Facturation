#nullable disable
namespace S10E01_Facturation.WinForms;

partial class FormFacture
{
    private System.ComponentModel.IContainer components = null;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }

        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        lResumeFacture = new Label();
        SuspendLayout();
        // 
        // lResumeFacture
        // 
        lResumeFacture.AccessibleName = "Résumé de facture";
        lResumeFacture.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        lResumeFacture.Location = new Point(23, 27);
        lResumeFacture.Name = "lResumeFacture";
        lResumeFacture.Size = new Size(503, 320);
        lResumeFacture.TabIndex = 0;
        lResumeFacture.Text = "Facture de consultation";
        lResumeFacture.Click += lResumeFacture_Click;
        // 
        // FormFacture
        // 
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(549, 373);
        Controls.Add(lResumeFacture);
        Margin = new Padding(3, 4, 3, 4);
        MinimumSize = new Size(523, 384);
        Name = "FormFacture";
        StartPosition = FormStartPosition.CenterParent;
        Text = "Facture de consultation";
        Load += FormFacture_Load;
        Disposed += FormFacture_Disposed;
        ResumeLayout(false);
    }

    private Label lResumeFacture;
}
