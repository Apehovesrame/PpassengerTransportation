namespace PassengerTransportApp
{
    partial class RouteAddForm
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing) { if (disposing && (components != null)) components.Dispose(); base.Dispose(disposing); }

        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.txtNumber = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbFrom = new System.Windows.Forms.ComboBox(); // Стало ComboBox
            this.label3 = new System.Windows.Forms.Label();
            this.cmbTo = new System.Windows.Forms.ComboBox();   // Стало ComboBox
            this.label4 = new System.Windows.Forms.Label();
            this.numHours = new System.Windows.Forms.NumericUpDown(); // Часы
            this.labelPrice = new System.Windows.Forms.Label();
            this.numPrice = new System.Windows.Forms.NumericUpDown();
            this.btnSave = new System.Windows.Forms.Button();
            this.numMinutes = new System.Windows.Forms.NumericUpDown(); // Минуты
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numHours)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPrice)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMinutes)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Номер маршрута:";
            // 
            // txtNumber
            // 
            this.txtNumber.Location = new System.Drawing.Point(16, 29);
            this.txtNumber.Name = "txtNumber";
            this.txtNumber.Size = new System.Drawing.Size(236, 20);
            this.txtNumber.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(108, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Пункт отправления:";
            // 
            // cmbFrom
            // 
            this.cmbFrom.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbFrom.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbFrom.FormattingEnabled = true;
            this.cmbFrom.Location = new System.Drawing.Point(16, 77);
            this.cmbFrom.Name = "cmbFrom";
            this.cmbFrom.Size = new System.Drawing.Size(236, 21);
            this.cmbFrom.TabIndex = 2;
            this.cmbFrom.Text = "Муром"; // <-- Теперь просто "Муром"
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 110);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(102, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Пункт назначения:";
            // 
            // cmbTo
            // 
            this.cmbTo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbTo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbTo.FormattingEnabled = true;
            this.cmbTo.Location = new System.Drawing.Point(16, 126);
            this.cmbTo.Name = "cmbTo";
            this.cmbTo.Size = new System.Drawing.Size(236, 21);
            this.cmbTo.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 160);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(111, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Время в пути (ч : м):";
            // 
            // numHours
            // 
            this.numHours.Location = new System.Drawing.Point(16, 176);
            this.numHours.Maximum = new decimal(new int[] { 100, 0, 0, 0 });
            this.numHours.Name = "numHours";
            this.numHours.Size = new System.Drawing.Size(60, 20);
            this.numHours.TabIndex = 4;
            this.numHours.Value = new decimal(new int[] { 2, 0, 0, 0 });
            // 
            // labelPrice
            // 
            this.labelPrice.AutoSize = true;
            this.labelPrice.Location = new System.Drawing.Point(13, 210);
            this.labelPrice.Name = "labelPrice";
            this.labelPrice.Size = new System.Drawing.Size(155, 13);
            this.labelPrice.TabIndex = 8;
            this.labelPrice.Text = "Стоимость полного билета:";
            // 
            // numPrice
            // 
            this.numPrice.DecimalPlaces = 2;
            this.numPrice.Location = new System.Drawing.Point(16, 226);
            this.numPrice.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            this.numPrice.Name = "numPrice";
            this.numPrice.Size = new System.Drawing.Size(236, 20);
            this.numPrice.TabIndex = 6;
            this.numPrice.Value = new decimal(new int[] { 500, 0, 0, 0 });
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(16, 265);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(236, 38);
            this.btnSave.TabIndex = 7;
            this.btnSave.Text = "Сохранить маршрут";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // numMinutes
            // 
            this.numMinutes.Location = new System.Drawing.Point(105, 176);
            this.numMinutes.Maximum = new decimal(new int[] { 59, 0, 0, 0 });
            this.numMinutes.Name = "numMinutes";
            this.numMinutes.Size = new System.Drawing.Size(60, 20);
            this.numMinutes.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(82, 178);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(15, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "ч.";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(171, 178);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(30, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "мин.";
            // 
            // RouteAddForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(274, 320);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.numMinutes);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.numPrice);
            this.Controls.Add(this.labelPrice);
            this.Controls.Add(this.numHours);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cmbTo);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cmbFrom);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtNumber);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "RouteAddForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Маршрут";
            this.Load += new System.EventHandler(this.RouteAddForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numHours)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPrice)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMinutes)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtNumber;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbFrom; // ComboBox
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbTo;   // ComboBox
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown numHours;   // Часы
        private System.Windows.Forms.NumericUpDown numMinutes; // Минуты
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label labelPrice;
        private System.Windows.Forms.NumericUpDown numPrice;
        private System.Windows.Forms.Button btnSave;
    }
}