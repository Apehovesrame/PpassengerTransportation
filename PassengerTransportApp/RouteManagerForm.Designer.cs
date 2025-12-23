namespace PassengerTransportApp
{
    partial class RouteManagerForm
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing) { if (disposing && (components != null)) components.Dispose(); base.Dispose(disposing); }

        private void InitializeComponent()
        {
            this.dgvRoutes = new System.Windows.Forms.DataGridView();
            this.dgvStops = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnAddStop = new System.Windows.Forms.Button();
            this.btnDelStop = new System.Windows.Forms.Button();
            this.btnDelRoute = new System.Windows.Forms.Button();
            this.btnAddRoute = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRoutes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStops)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvRoutes
            // 
            this.dgvRoutes.AllowUserToAddRows = false;
            this.dgvRoutes.AllowUserToDeleteRows = false;
            this.dgvRoutes.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvRoutes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRoutes.Location = new System.Drawing.Point(24, 73);
            this.dgvRoutes.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.dgvRoutes.MultiSelect = false;
            this.dgvRoutes.Name = "dgvRoutes";
            this.dgvRoutes.ReadOnly = true;
            this.dgvRoutes.RowHeadersWidth = 82;
            this.dgvRoutes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvRoutes.Size = new System.Drawing.Size(700, 769);
            this.dgvRoutes.TabIndex = 0;
            this.dgvRoutes.SelectionChanged += new System.EventHandler(this.dgvRoutes_SelectionChanged);
            // 
            // dgvStops
            // 
            this.dgvStops.AllowUserToAddRows = false;
            this.dgvStops.AllowUserToDeleteRows = false;
            this.dgvStops.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvStops.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvStops.Location = new System.Drawing.Point(760, 73);
            this.dgvStops.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.dgvStops.MultiSelect = false;
            this.dgvStops.Name = "dgvStops";
            this.dgvStops.ReadOnly = true;
            this.dgvStops.RowHeadersWidth = 82;
            this.dgvStops.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvStops.Size = new System.Drawing.Size(980, 673);
            this.dgvStops.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(24, 35);
            this.label1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(156, 31);
            this.label1.TabIndex = 2;
            this.label1.Text = "Маршруты";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(754, 35);
            this.label2.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(294, 31);
            this.label2.TabIndex = 3;
            this.label2.Text = "Остановки на рейсе:";
            // 
            // btnAddStop
            // 
            this.btnAddStop.Location = new System.Drawing.Point(760, 758);
            this.btnAddStop.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.btnAddStop.Name = "btnAddStop";
            this.btnAddStop.Size = new System.Drawing.Size(300, 85);
            this.btnAddStop.TabIndex = 4;
            this.btnAddStop.Text = "Добавить остановку";
            this.btnAddStop.UseVisualStyleBackColor = true;
            this.btnAddStop.Click += new System.EventHandler(this.btnAddStop_Click);
            // 
            // btnDelStop
            // 
            this.btnDelStop.Location = new System.Drawing.Point(1072, 758);
            this.btnDelStop.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.btnDelStop.Name = "btnDelStop";
            this.btnDelStop.Size = new System.Drawing.Size(300, 85);
            this.btnDelStop.TabIndex = 5;
            this.btnDelStop.Text = "Удалить остановку";
            this.btnDelStop.UseVisualStyleBackColor = true;
            this.btnDelStop.Click += new System.EventHandler(this.btnDelStop_Click);
            // 
            // btnDelRoute
            // 
            this.btnDelRoute.BackColor = System.Drawing.Color.MistyRose;
            this.btnDelRoute.Location = new System.Drawing.Point(524, 17);
            this.btnDelRoute.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.btnDelRoute.Name = "btnDelRoute";
            this.btnDelRoute.Size = new System.Drawing.Size(200, 48);
            this.btnDelRoute.TabIndex = 6;
            this.btnDelRoute.Text = "Удалить";
            this.btnDelRoute.UseVisualStyleBackColor = false;
            this.btnDelRoute.Click += new System.EventHandler(this.btnDelRoute_Click);
            // 
            // btnAddRoute
            // 
            this.btnAddRoute.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.btnAddRoute.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddRoute.ForeColor = System.Drawing.Color.White;
            this.btnAddRoute.Location = new System.Drawing.Point(24, 854);
            this.btnAddRoute.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.btnAddRoute.Name = "btnAddRoute";
            this.btnAddRoute.Size = new System.Drawing.Size(700, 85);
            this.btnAddRoute.TabIndex = 7;
            this.btnAddRoute.Text = "+ СОЗДАТЬ МАРШРУТ";
            this.btnAddRoute.UseVisualStyleBackColor = false;
            this.btnAddRoute.Click += new System.EventHandler(this.btnAddRoute_Click);
            // 
            // RouteManagerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1768, 951);
            this.Controls.Add(this.btnAddRoute);
            this.Controls.Add(this.btnDelRoute);
            this.Controls.Add(this.btnDelStop);
            this.Controls.Add(this.btnAddStop);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgvStops);
            this.Controls.Add(this.dgvRoutes);
            this.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.Name = "RouteManagerForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Управление маршрутами и остановками";
            this.Load += new System.EventHandler(this.RouteManagerForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRoutes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStops)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.DataGridView dgvRoutes;
        private System.Windows.Forms.DataGridView dgvStops;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnAddStop;
        private System.Windows.Forms.Button btnDelStop;
        private System.Windows.Forms.Button btnDelRoute;
        private System.Windows.Forms.Button btnAddRoute;
    }
}