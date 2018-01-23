namespace rich_uncle
{
    partial class getInfo
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.buttonExit = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.numOfPlayers = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.bankDeposit = new System.Windows.Forms.NumericUpDown();
            this.playerInitDeposit = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.numOfPlayers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bankDeposit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.playerInitDeposit)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Number Of Players";
            // 
            // buttonExit
            // 
            this.buttonExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonExit.Location = new System.Drawing.Point(207, 135);
            this.buttonExit.Name = "buttonExit";
            this.buttonExit.Size = new System.Drawing.Size(75, 23);
            this.buttonExit.TabIndex = 4;
            this.buttonExit.Text = "Exit";
            this.buttonExit.UseVisualStyleBackColor = true;
            this.buttonExit.Click += new System.EventHandler(this.buttonExit_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSave.Location = new System.Drawing.Point(126, 135);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 3;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // numOfPlayers
            // 
            this.numOfPlayers.Location = new System.Drawing.Point(139, 10);
            this.numOfPlayers.Maximum = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.numOfPlayers.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numOfPlayers.Name = "numOfPlayers";
            this.numOfPlayers.Size = new System.Drawing.Size(120, 20);
            this.numOfPlayers.TabIndex = 0;
            this.numOfPlayers.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(28, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Bank Deposit";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(21, 81);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(102, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Player Initial Deposit";
            // 
            // bankDeposit
            // 
            this.bankDeposit.Location = new System.Drawing.Point(139, 45);
            this.bankDeposit.Maximum = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.bankDeposit.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.bankDeposit.Name = "bankDeposit";
            this.bankDeposit.Size = new System.Drawing.Size(120, 20);
            this.bankDeposit.TabIndex = 1;
            this.bankDeposit.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // playerInitDeposit
            // 
            this.playerInitDeposit.Location = new System.Drawing.Point(139, 79);
            this.playerInitDeposit.Maximum = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.playerInitDeposit.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.playerInitDeposit.Name = "playerInitDeposit";
            this.playerInitDeposit.Size = new System.Drawing.Size(120, 20);
            this.playerInitDeposit.TabIndex = 2;
            this.playerInitDeposit.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // getInfo
            // 
            this.AcceptButton = this.buttonSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.CancelButton = this.buttonExit;
            this.ClientSize = new System.Drawing.Size(284, 161);
            this.Controls.Add(this.playerInitDeposit);
            this.Controls.Add(this.bankDeposit);
            this.Controls.Add(this.numOfPlayers);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.buttonExit);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "getInfo";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Game Info";
            ((System.ComponentModel.ISupportInitialize)(this.numOfPlayers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bankDeposit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.playerInitDeposit)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonExit;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.NumericUpDown numOfPlayers;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown bankDeposit;
        private System.Windows.Forms.NumericUpDown playerInitDeposit;
    }
}