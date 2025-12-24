namespace PassengerTransportApp
{
    partial class BusManagerForm
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
            this.dgvBuses = new System.Windows.Forms.DataGridView();
            this.grpBusInfo = new System.Windows.Forms.GroupBox();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.numSeats = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.txtModel = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtLicense = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.picBus = new System.Windows.Forms.PictureBox();
            this.btnUpload = new System.Windows.Forms.Button();
            this.btnClearPhoto = new System.Windows.Forms.Button(); // Новая кнопка
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBuses)).BeginInit();
            this.grpBusInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSeats)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBus)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvBuses
            // 
            this.dgvBuses.AllowUserToAddRows = false;
            this.dgvBuses.AllowUserToDeleteRows = false;
            this.dgvBuses.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvBuses.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBuses.Location = new System.Drawing.Point(12, 12);
            this.dgvBuses.MultiSelect = false;
            this.dgvBuses.Name = "dgvBuses";
            this.dgvBuses.ReadOnly = true;
            this.dgvBuses.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvBuses.Size = new System.Drawing.Size(430, 500);
            this.dgvBuses.TabIndex = 0;
            this.dgvBuses.SelectionChanged += new System.EventHandler(this.dgvBuses_SelectionChanged);
            // 
            // grpBusInfo
            // 
            this.grpBusInfo.Controls.Add(this.btnDelete);
            this.grpBusInfo.Controls.Add(this.btnEdit);
            this.grpBusInfo.Controls.Add(this.btnAdd);
            this.grpBusInfo.Controls.Add(this.numSeats);
            this.grpBusInfo.Controls.Add(this.label3);
            this.grpBusInfo.Controls.Add(this.txtModel);
            this.grpBusInfo.Controls.Add(this.label2);
            this.grpBusInfo.Controls.Add(this.txtLicense);
            this.grpBusInfo.Controls.Add(this.label1);
            this.grpBusInfo.Location = new System.Drawing.Point(458, 12);
            this.grpBusInfo.Name = "grpBusInfo";
            this.grpBusInfo.Size = new System.Drawing.Size(314, 200);
            this.grpBusInfo.TabIndex = 1;
            this.grpBusInfo.TabStop = false;
            this.grpBusInfo.Text = "Данные автобуса";
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.MistyRose;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.Location = new System.Drawing.Point(213, 155);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(85, 30);
            this.btnDelete.TabIndex = 8;
            this.btnDelete.Text = "Удалить";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(112, 155);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(85, 30);
            this.btnEdit.TabIndex = 7;
            this.btnEdit.Text = "Изменить";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.Color.LightSkyBlue;
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.Location = new System.Drawing.Point(10, 155);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(85, 30);
            this.btnAdd.TabIndex = 6;
            this.btnAdd.Text = "Добавить";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // numSeats
            // 
            this.numSeats.Location = new System.Drawing.Point(10, 119);
            this.numSeats.Maximum = new decimal(new int[] { 200, 0, 0, 0 });
            this.numSeats.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.numSeats.Name = "numSeats";
            this.numSeats.Size = new System.Drawing.Size(100, 20);
            this.numSeats.TabIndex = 5;
            this.numSeats.Value = new decimal(new int[] { 20, 0, 0, 0 });
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 103);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(97, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Количество мест:";
            // 
            // txtModel
            // 
            this.txtModel.Location = new System.Drawing.Point(10, 80);
            this.txtModel.Name = "txtModel";
            this.txtModel.Size = new System.Drawing.Size(288, 20);
            this.txtModel.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Модель:";
            // 
            // txtLicense
            // 
            this.txtLicense.Location = new System.Drawing.Point(10, 41);
            this.txtLicense.Name = "txtLicense";
            this.txtLicense.Size = new System.Drawing.Size(288, 20);
            this.txtLicense.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Гос. номер:";
            // 
            // picBus
            // 
            this.picBus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picBus.Location = new System.Drawing.Point(458, 241);
            this.picBus.Name = "picBus";
            this.picBus.Size = new System.Drawing.Size(314, 222);
            this.picBus.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picBus.TabIndex = 2;
            this.picBus.TabStop = false;
            // 
            // btnUpload
            // 
            this.btnUpload.Location = new System.Drawing.Point(458, 469);
            this.btnUpload.Name = "btnUpload";
            this.btnUpload.Size = new System.Drawing.Size(150, 43); // Уменьшил ширину
            this.btnUpload.TabIndex = 3;
            this.btnUpload.Text = "Загрузить фото";
            this.btnUpload.UseVisualStyleBackColor = true;
            this.btnUpload.Click += new System.EventHandler(this.btnUpload_Click);
            // 
            // btnClearPhoto
            // 
            this.btnClearPhoto.Location = new System.Drawing.Point(622, 469);
            this.btnClearPhoto.Name = "btnClearPhoto";
            this.btnClearPhoto.Size = new System.Drawing.Size(150, 43);
            this.btnClearPhoto.TabIndex = 9;
            this.btnClearPhoto.Text = "Удалить фото";
            this.btnClearPhoto.UseVisualStyleBackColor = true;
            this.btnClearPhoto.Click += new System.EventHandler(this.btnClearPhoto_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(458, 222);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(88, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Фото автобуса:";
            // 
            // BusManagerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 524);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnClearPhoto); // Добавили на форму
            this.Controls.Add(this.btnUpload);
            this.Controls.Add(this.picBus);
            this.Controls.Add(this.grpBusInfo);
            this.Controls.Add(this.dgvBuses);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "BusManagerForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Управление автопарком";
            this.Load += new System.EventHandler(this.BusManagerForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvBuses)).EndInit();
            this.grpBusInfo.ResumeLayout(false);
            this.grpBusInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSeats)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBus)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.DataGridView dgvBuses;
        private System.Windows.Forms.GroupBox grpBusInfo;
        private System.Windows.Forms.TextBox txtLicense;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnClearPhoto;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.NumericUpDown numSeats;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtModel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox picBus;
        private System.Windows.Forms.Button btnUpload;
        private System.Windows.Forms.Label label4;
    }
}