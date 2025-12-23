namespace PassengerTransportApp
{
    partial class TicketSaleForm
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.groupBoxPass = new System.Windows.Forms.GroupBox();
            this.numYear = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.txtPassport = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtMiddleName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtFirstName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtLastName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBoxTicket = new System.Windows.Forms.GroupBox();
            this.lblPrice = new System.Windows.Forms.Label();
            this.numPrice = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.cmbSeat = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cmbStops = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.groupBoxPass.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numYear)).BeginInit();
            this.groupBoxTicket.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numPrice)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBoxPass
            // 
            this.groupBoxPass.Controls.Add(this.numYear);
            this.groupBoxPass.Controls.Add(this.label5);
            this.groupBoxPass.Controls.Add(this.txtPassport);
            this.groupBoxPass.Controls.Add(this.label4);
            this.groupBoxPass.Controls.Add(this.txtMiddleName);
            this.groupBoxPass.Controls.Add(this.label3);
            this.groupBoxPass.Controls.Add(this.txtFirstName);
            this.groupBoxPass.Controls.Add(this.label2);
            this.groupBoxPass.Controls.Add(this.txtLastName);
            this.groupBoxPass.Controls.Add(this.label1);
            this.groupBoxPass.Location = new System.Drawing.Point(12, 12);
            this.groupBoxPass.Name = "groupBoxPass";
            this.groupBoxPass.Size = new System.Drawing.Size(320, 220);
            this.groupBoxPass.TabIndex = 0;
            this.groupBoxPass.TabStop = false;
            this.groupBoxPass.Text = "Данные пассажира";
            // 
            // numYear
            // 
            this.numYear.Location = new System.Drawing.Point(119, 178);
            this.numYear.Maximum = new decimal(new int[] { 2025, 0, 0, 0 });
            this.numYear.Minimum = new decimal(new int[] { 1900, 0, 0, 0 });
            this.numYear.Name = "numYear";
            this.numYear.Size = new System.Drawing.Size(182, 20);
            this.numYear.TabIndex = 4;
            this.numYear.Value = new decimal(new int[] { 1990, 0, 0, 0 });
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(16, 180);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(81, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Год рождения:";
            // 
            // txtPassport
            // 
            this.txtPassport.ForeColor = System.Drawing.Color.Gray;
            this.txtPassport.Location = new System.Drawing.Point(119, 142);
            this.txtPassport.Name = "txtPassport";
            this.txtPassport.Size = new System.Drawing.Size(182, 20);
            this.txtPassport.TabIndex = 3;
            this.txtPassport.Text = "Серия и номер";
            this.txtPassport.Enter += new System.EventHandler(this.txtPassport_Enter);
            this.txtPassport.Leave += new System.EventHandler(this.txtPassport_Leave);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 145);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(94, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Номер паспорта:";
            // 
            // txtMiddleName
            // 
            this.txtMiddleName.Location = new System.Drawing.Point(119, 106);
            this.txtMiddleName.Name = "txtMiddleName";
            this.txtMiddleName.Size = new System.Drawing.Size(182, 20);
            this.txtMiddleName.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 109);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Отчество:";
            // 
            // txtFirstName
            // 
            this.txtFirstName.Location = new System.Drawing.Point(119, 70);
            this.txtFirstName.Name = "txtFirstName";
            this.txtFirstName.Size = new System.Drawing.Size(182, 20);
            this.txtFirstName.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Имя:";
            // 
            // txtLastName
            // 
            this.txtLastName.Location = new System.Drawing.Point(119, 34);
            this.txtLastName.Name = "txtLastName";
            this.txtLastName.Size = new System.Drawing.Size(182, 20);
            this.txtLastName.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Фамилия:";
            // 
            // groupBoxTicket
            // 
            this.groupBoxTicket.Controls.Add(this.lblPrice);
            this.groupBoxTicket.Controls.Add(this.numPrice);
            this.groupBoxTicket.Controls.Add(this.label8);
            this.groupBoxTicket.Controls.Add(this.cmbSeat);
            this.groupBoxTicket.Controls.Add(this.label7);
            this.groupBoxTicket.Controls.Add(this.cmbStops);
            this.groupBoxTicket.Controls.Add(this.label6);
            this.groupBoxTicket.Location = new System.Drawing.Point(354, 12);
            this.groupBoxTicket.Name = "groupBoxTicket";
            this.groupBoxTicket.Size = new System.Drawing.Size(269, 160);
            this.groupBoxTicket.TabIndex = 1;
            this.groupBoxTicket.TabStop = false;
            this.groupBoxTicket.Text = "Детали билета";
            // 
            // lblPrice
            // 
            this.lblPrice.AutoSize = true;
            this.lblPrice.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblPrice.ForeColor = System.Drawing.Color.Green;
            this.lblPrice.Location = new System.Drawing.Point(220, 113);
            this.lblPrice.Name = "lblPrice";
            this.lblPrice.Size = new System.Drawing.Size(32, 13);
            this.lblPrice.TabIndex = 6;
            this.lblPrice.Text = "руб.";
            // 
            // numPrice
            // 
            this.numPrice.DecimalPlaces = 2;
            this.numPrice.Location = new System.Drawing.Point(94, 111);
            this.numPrice.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            this.numPrice.Name = "numPrice";
            this.numPrice.Size = new System.Drawing.Size(120, 20);
            this.numPrice.TabIndex = 2;
            this.numPrice.Value = new decimal(new int[] { 1000, 0, 0, 0 });
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(17, 113);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(65, 13);
            this.label8.TabIndex = 4;
            this.label8.Text = "Стоимость:";
            // 
            // cmbSeat
            // 
            this.cmbSeat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSeat.FormattingEnabled = true;
            this.cmbSeat.Location = new System.Drawing.Point(94, 72);
            this.cmbSeat.Name = "cmbSeat";
            this.cmbSeat.Size = new System.Drawing.Size(120, 21);
            this.cmbSeat.TabIndex = 1;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(17, 75);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(42, 13);
            this.label7.TabIndex = 2;
            this.label7.Text = "Место:";
            // 
            // cmbStops
            // 
            this.cmbStops.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStops.FormattingEnabled = true;
            this.cmbStops.Location = new System.Drawing.Point(94, 34);
            this.cmbStops.Name = "cmbStops";
            this.cmbStops.Size = new System.Drawing.Size(158, 21);
            this.cmbStops.TabIndex = 0;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(17, 37);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(49, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "Едет до:";
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(354, 185);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(269, 47);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "ОФОРМИТЬ БИЛЕТ";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // TicketSaleForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(639, 246);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.groupBoxTicket);
            this.Controls.Add(this.groupBoxPass);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "TicketSaleForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Продажа билета";
            this.Load += new System.EventHandler(this.TicketSaleForm_Load);
            this.groupBoxPass.ResumeLayout(false);
            this.groupBoxPass.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numYear)).EndInit();
            this.groupBoxTicket.ResumeLayout(false);
            this.groupBoxTicket.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numPrice)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxPass;
        private System.Windows.Forms.TextBox txtLastName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtPassport;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtMiddleName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtFirstName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numYear;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBoxTicket;
        private System.Windows.Forms.ComboBox cmbStops;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cmbSeat;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblPrice;
        private System.Windows.Forms.NumericUpDown numPrice;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnSave;
    }
}