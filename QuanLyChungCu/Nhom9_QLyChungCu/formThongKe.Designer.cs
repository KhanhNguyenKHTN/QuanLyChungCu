namespace Nhom9_QLyChungCu
{
    partial class formThongKe
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.button1 = new System.Windows.Forms.Button();
            this.cbLoaiGiaTriHT = new System.Windows.Forms.ComboBox();
            this.cbBieuDo = new System.Windows.Forms.ComboBox();
            this.chartThongKe = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.btnThoat = new System.Windows.Forms.Button();
            this.btnDeleteHD = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSaveExcel = new System.Windows.Forms.Button();
            this.btnChiTiet = new System.Windows.Forms.Button();
            this.cbCanHo = new System.Windows.Forms.ComboBox();
            this.cbLoaiTK = new System.Windows.Forms.ComboBox();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.dgvBang = new System.Windows.Forms.DataGridView();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartThongKe)).BeginInit();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBang)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.button1);
            this.tabPage2.Controls.Add(this.cbLoaiGiaTriHT);
            this.tabPage2.Controls.Add(this.cbBieuDo);
            this.tabPage2.Controls.Add(this.chartThongKe);
            this.tabPage2.Location = new System.Drawing.Point(4, 27);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1098, 713);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Biểu đồ";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(928, 667);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(130, 40);
            this.button1.TabIndex = 4;
            this.button1.Text = "Lưu hình ảnh";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // cbLoaiGiaTriHT
            // 
            this.cbLoaiGiaTriHT.FormattingEnabled = true;
            this.cbLoaiGiaTriHT.Items.AddRange(new object[] {
            "Tháng",
            "Căn Hộ"});
            this.cbLoaiGiaTriHT.Location = new System.Drawing.Point(406, 6);
            this.cbLoaiGiaTriHT.Name = "cbLoaiGiaTriHT";
            this.cbLoaiGiaTriHT.Size = new System.Drawing.Size(214, 26);
            this.cbLoaiGiaTriHT.TabIndex = 1;
            this.cbLoaiGiaTriHT.Text = "Tháng";
            this.cbLoaiGiaTriHT.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged_1);
            // 
            // cbBieuDo
            // 
            this.cbBieuDo.FormattingEnabled = true;
            this.cbBieuDo.Items.AddRange(new object[] {
            "Biểu Đồ Cột",
            "Biểu Đồ Tròn",
            "Biểu Đồ Đường"});
            this.cbBieuDo.Location = new System.Drawing.Point(22, 6);
            this.cbBieuDo.Name = "cbBieuDo";
            this.cbBieuDo.Size = new System.Drawing.Size(214, 26);
            this.cbBieuDo.TabIndex = 0;
            this.cbBieuDo.Text = "Biểu Đồ Cột";
            this.cbBieuDo.SelectedIndexChanged += new System.EventHandler(this.cbBieuDo_SelectedIndexChanged);
            // 
            // chartThongKe
            // 
            chartArea1.Name = "ChartArea1";
            this.chartThongKe.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chartThongKe.Legends.Add(legend1);
            this.chartThongKe.Location = new System.Drawing.Point(22, 52);
            this.chartThongKe.Name = "chartThongKe";
            this.chartThongKe.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.None;
            series1.ChartArea = "ChartArea1";
            series1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chartThongKe.Series.Add(series1);
            this.chartThongKe.Size = new System.Drawing.Size(1036, 596);
            this.chartThongKe.TabIndex = 1;
            this.chartThongKe.Text = "Hình tròn";
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.btnThoat);
            this.tabPage1.Controls.Add(this.btnDeleteHD);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.btnSaveExcel);
            this.tabPage1.Controls.Add(this.btnChiTiet);
            this.tabPage1.Controls.Add(this.cbCanHo);
            this.tabPage1.Controls.Add(this.cbLoaiTK);
            this.tabPage1.Controls.Add(this.dateTimePicker1);
            this.tabPage1.Controls.Add(this.dgvBang);
            this.tabPage1.Location = new System.Drawing.Point(4, 27);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1098, 713);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Bảng";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // btnThoat
            // 
            this.btnThoat.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThoat.Location = new System.Drawing.Point(979, 673);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new System.Drawing.Size(100, 34);
            this.btnThoat.TabIndex = 3;
            this.btnThoat.Text = "Thoát";
            this.btnThoat.UseVisualStyleBackColor = true;
            this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
            // 
            // btnDeleteHD
            // 
            this.btnDeleteHD.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDeleteHD.Location = new System.Drawing.Point(873, 673);
            this.btnDeleteHD.Name = "btnDeleteHD";
            this.btnDeleteHD.Size = new System.Drawing.Size(100, 34);
            this.btnDeleteHD.TabIndex = 4;
            this.btnDeleteHD.Text = "Xóa";
            this.btnDeleteHD.UseVisualStyleBackColor = true;
            this.btnDeleteHD.Click += new System.EventHandler(this.btnDeleteHD_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(813, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(99, 18);
            this.label3.TabIndex = 10;
            this.label3.Text = "Chọn căn hộ:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(350, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(106, 18);
            this.label2.TabIndex = 8;
            this.label2.Text = "Loại thống kê:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 18);
            this.label1.TabIndex = 7;
            this.label1.Text = "Date:";
            // 
            // btnSaveExcel
            // 
            this.btnSaveExcel.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSaveExcel.Location = new System.Drawing.Point(607, 673);
            this.btnSaveExcel.Name = "btnSaveExcel";
            this.btnSaveExcel.Size = new System.Drawing.Size(154, 34);
            this.btnSaveExcel.TabIndex = 6;
            this.btnSaveExcel.Text = "Xuất File Excel";
            this.btnSaveExcel.UseVisualStyleBackColor = true;
            this.btnSaveExcel.Click += new System.EventHandler(this.btnSaveExcel_Click);
            // 
            // btnChiTiet
            // 
            this.btnChiTiet.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnChiTiet.Location = new System.Drawing.Point(767, 673);
            this.btnChiTiet.Name = "btnChiTiet";
            this.btnChiTiet.Size = new System.Drawing.Size(100, 34);
            this.btnChiTiet.TabIndex = 5;
            this.btnChiTiet.Text = "Chi Tiết";
            this.btnChiTiet.UseVisualStyleBackColor = true;
            this.btnChiTiet.Click += new System.EventHandler(this.btnChiTiet_Click);
            // 
            // cbCanHo
            // 
            this.cbCanHo.FormattingEnabled = true;
            this.cbCanHo.Items.AddRange(new object[] {
            "Căn hộ F101",
            "Căn hộ F102",
            "Căn hộ F103",
            "Căn hộ F104",
            "Căn hộ F105",
            "Căn hộ F106",
            "Căn hộ F201",
            "Căn hộ F202",
            "Căn hộ F203",
            "Căn hộ F204",
            "Căn hộ F205",
            "Căn hộ F206",
            "Căn hộ F301",
            "Căn hộ F302",
            "Căn hộ F303",
            "Căn hộ F304",
            "Căn hộ F305",
            "Căn hộ F306",
            "Căn hộ F401",
            "Căn hộ F402",
            "Căn hộ F403",
            "Căn hộ F404",
            "Căn hộ F405",
            "Căn hộ F406",
            "Căn hộ F501",
            "Căn hộ F502",
            "Căn hộ F503",
            "Căn hộ F504",
            "Căn hộ F505",
            "Căn hộ F506"});
            this.cbCanHo.Location = new System.Drawing.Point(918, 12);
            this.cbCanHo.Name = "cbCanHo";
            this.cbCanHo.Size = new System.Drawing.Size(167, 26);
            this.cbCanHo.TabIndex = 2;
            this.cbCanHo.Text = "Chọn căn hộ...";
            this.cbCanHo.SelectedIndexChanged += new System.EventHandler(this.cbCanHo_SelectedIndexChanged);
            // 
            // cbLoaiTK
            // 
            this.cbLoaiTK.FormattingEnabled = true;
            this.cbLoaiTK.Items.AddRange(new object[] {
            "Căn hộ",
            "Ngày",
            "Tháng",
            "Năm"});
            this.cbLoaiTK.Location = new System.Drawing.Point(462, 14);
            this.cbLoaiTK.Name = "cbLoaiTK";
            this.cbLoaiTK.Size = new System.Drawing.Size(245, 26);
            this.cbLoaiTK.TabIndex = 1;
            this.cbLoaiTK.Text = "Thống kê theo...";
            this.cbLoaiTK.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.CustomFormat = "dd/MM/yyyy";
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker1.Location = new System.Drawing.Point(58, 14);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(151, 26);
            this.dateTimePicker1.TabIndex = 0;
            // 
            // dgvBang
            // 
            this.dgvBang.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvBang.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvBang.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBang.Location = new System.Drawing.Point(6, 51);
            this.dgvBang.Name = "dgvBang";
            this.dgvBang.ReadOnly = true;
            this.dgvBang.Size = new System.Drawing.Size(1073, 616);
            this.dgvBang.TabIndex = 0;
            this.dgvBang.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvBang_CellContentClick);
            this.dgvBang.SelectionChanged += new System.EventHandler(this.dgvBang_SelectionChanged);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1106, 744);
            this.tabControl1.TabIndex = 0;
            // 
            // formThongKe
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1117, 762);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "formThongKe";
            this.ShowInTaskbar = false;
            this.Text = "Thông kê hóa đơn";
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartThongKe)).EndInit();
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBang)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ComboBox cbLoaiGiaTriHT;
        private System.Windows.Forms.ComboBox cbBieuDo;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartThongKe;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Button btnSaveExcel;
        private System.Windows.Forms.Button btnChiTiet;
        private System.Windows.Forms.ComboBox cbCanHo;
        private System.Windows.Forms.ComboBox cbLoaiTK;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.DataGridView dgvBang;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnDeleteHD;
        private System.Windows.Forms.Button btnThoat;
        private System.Windows.Forms.Button button1;
    }
}