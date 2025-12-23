namespace PassengerTransportApp
{
    partial class PassengerListForm
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.panelTop = new System.Windows.Forms.Panel();
            this.btnEditPassenger = new System.Windows.Forms.Button();
            this.btnRefundTicket = new System.Windows.Forms.Button();
            this.dgvPassengers = new System.Windows.Forms.DataGridView();
            this.panelTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPassengers)).BeginInit();
            this.SuspendLayout();
            // 
            // panelTop
            // 
            this.panelTop.Controls.Add(this.btnEditPassenger);
            this.panelTop.Controls.Add(this.btnRefundTicket);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(700, 50);
            this.panelTop.TabIndex = 0;
            // 
            // btnEditPassenger
            // 
            this.btnEditPassenger.Location = new System.Drawing.Point(12, 12);
            this.btnEditPassenger.Name = "btnEditPassenger";
            this.btnEditPassenger.Size = new System.Drawing.Size(120, 26);
            this.btnEditPassenger.TabIndex = 0;
            this.btnEditPassenger.Text = "Изменить данные";
            this.btnEditPassenger.UseVisualStyleBackColor = true;
            this.btnEditPassenger.Click += new System.EventHandler(this.btnEditPassenger_Click);
            // 
            // btnRefundTicket
            // 
            this.btnRefundTicket.BackColor = System.Drawing.Color.MistyRose;
            this.btnRefundTicket.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefundTicket.Location = new System.Drawing.Point(138, 12);
            this.btnRefundTicket.Name = "btnRefundTicket";
            this.btnRefundTicket.Size = new System.Drawing.Size(120, 26);
            this.btnRefundTicket.TabIndex = 1;
            this.btnRefundTicket.Text = "Вернуть билет";
            this.btnRefundTicket.UseVisualStyleBackColor = false;
            this.btnRefundTicket.Click += new System.EventHandler(this.btnRefundTicket_Click);
            // 
            // dgvPassengers
            // 
            this.dgvPassengers.AllowUserToAddRows = false;
            this.dgvPassengers.AllowUserToDeleteRows = false;
            this.dgvPassengers.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvPassengers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPassengers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvPassengers.Location = new System.Drawing.Point(0, 50);
            this.dgvPassengers.MultiSelect = false;
            this.dgvPassengers.Name = "dgvPassengers";
            this.dgvPassengers.ReadOnly = true;
            this.dgvPassengers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPassengers.Size = new System.Drawing.Size(700, 400);
            this.dgvPassengers.TabIndex = 1;
            // 
            // PassengerListForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(700, 450);
            this.Controls.Add(this.dgvPassengers);
            this.Controls.Add(this.panelTop);
            this.Name = "PassengerListForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Список пассажиров рейса";
            this.Load += new System.EventHandler(this.PassengerListForm_Load);
            this.panelTop.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPassengers)).EndInit();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Button btnEditPassenger;
        private System.Windows.Forms.Button btnRefundTicket;
        private System.Windows.Forms.DataGridView dgvPassengers;
    }
}