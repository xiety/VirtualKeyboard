namespace VirtualKeyboard;

partial class MainForm
{
    private System.ComponentModel.IContainer components = null;
    private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel;

    private void InitializeComponent()
    {
        this.components = new System.ComponentModel.Container();
        this.flowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
        this.SuspendLayout();

        this.flowLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
        this.flowLayoutPanel.Location = new System.Drawing.Point(0, 0);
        this.flowLayoutPanel.AutoSize = true;
        this.flowLayoutPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;

        this.Controls.Add(this.flowLayoutPanel);
        this.MaximizeBox = false;
        this.Text = "Virtual Keyboard";
        this.AutoSize = true;
        this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
        this.TopMost = true;

        this.ResumeLayout(false);
        this.PerformLayout();
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing && components != null)
            components.Dispose();

        base.Dispose(disposing);
    }
}
