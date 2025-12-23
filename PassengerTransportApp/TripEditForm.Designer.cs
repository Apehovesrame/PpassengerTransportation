namespace PassengerTransportApp
{
    partial class TripEditForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.cmbRoutes = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbBuses = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.dtpDeparture = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.clbDrivers = new System.Windows.Forms.CheckedListBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Маршрут:";
            // 
            // cmbRoutes
            // 
            this.cmbRoutes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRoutes.FormattingEnabled = true;
            this.cmbRoutes.Location = new System.Drawing.Point(26, 42);
            this.cmbRoutes.Name = "cmbRoutes";
            this.cmbRoutes.Size = new System.Drawing.Size(287, 21);
            this.cmbRoutes.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(23, 79);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Автобус:";
            // 
            // cmbBuses
            // 
            this.cmbBuses.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBuses.FormattingEnabled = true;
            this.cmbBuses.Location = new System.Drawing.Point(26, 95);
            this.cmbBuses.Name = "cmbBuses";
            this.cmbBuses.Size = new System.Drawing.Size(287, 21);
            this.cmbBuses.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(23, 133);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(104, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Дата отправления:";
            // 
            // dtpDeparture
            // 
            this.dtpDeparture.CustomFormat = "dd.MM.yyyy HH:mm";
            this.dtpDeparture.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDeparture.Location = new System.Drawing.Point(26, 149);
            this.dtpDeparture.Name = "dtpDeparture";
            this.dtpDeparture.Size = new System.Drawing.Size(287, 20);
            this.dtpDeparture.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(23, 187);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(117, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Назначить водителей:";
            // 
            // clbDrivers
            // 
            this.clbDrivers.CheckOnClick = true;
            this.clbDrivers.FormattingEnabled = true;
            this.clbDrivers.Location = new System.Drawing.Point(26, 203);
            this.clbDrivers.Name = "clbDrivers";
            this.clbDrivers.Size = new System.Drawing.Size(287, 109);
            this.clbDrivers.TabIndex = 7;
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.SteelBlue;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(26, 335);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(287, 43);
            this.btnSave.TabIndex = 8;
            this.btnSave.Text = "СОЗДАТЬ РЕЙС";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // TripEditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(342, 401);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.clbDrivers);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.dtpDeparture);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cmbBuses);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmbRoutes);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "TripEditForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Планирование рейса";
            this.Load += new System.EventHandler(this.TripEditForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbRoutes;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbBuses;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtpDeparture;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckedListBox clbDrivers;
        private System.Windows.Forms.Button btnSave;
    }
}