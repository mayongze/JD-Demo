namespace JDDemo
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.txtJDUserName = new System.Windows.Forms.TextBox();
            this.txtJDUserPass = new System.Windows.Forms.TextBox();
            this.btnLogin = new System.Windows.Forms.Button();
            this.pieAuthcode = new System.Windows.Forms.PictureBox();
            this.txtAuthcode = new System.Windows.Forms.TextBox();
            this.btnGetCart = new System.Windows.Forms.Button();
            this.btnEmptyCart = new System.Windows.Forms.Button();
            this.lsvCart = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.txtWaresId = new System.Windows.Forms.TextBox();
            this.btnGetWaresInfo = new System.Windows.Forms.Button();
            this.btnAddCart = new System.Windows.Forms.Button();
            this.lsvFindWInfo = new System.Windows.Forms.ListView();
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnJieSuan = new System.Windows.Forms.Button();
            this.txtMessage = new System.Windows.Forms.TextBox();
            this.btnOrderList = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pieAuthcode)).BeginInit();
            this.SuspendLayout();
            // 
            // txtJDUserName
            // 
            this.txtJDUserName.Location = new System.Drawing.Point(12, 8);
            this.txtJDUserName.Name = "txtJDUserName";
            this.txtJDUserName.Size = new System.Drawing.Size(128, 21);
            this.txtJDUserName.TabIndex = 0;
            // 
            // txtJDUserPass
            // 
            this.txtJDUserPass.Location = new System.Drawing.Point(12, 35);
            this.txtJDUserPass.Name = "txtJDUserPass";
            this.txtJDUserPass.Size = new System.Drawing.Size(128, 21);
            this.txtJDUserPass.TabIndex = 1;
            // 
            // btnLogin
            // 
            this.btnLogin.Location = new System.Drawing.Point(67, 133);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnLogin.Size = new System.Drawing.Size(75, 23);
            this.btnLogin.TabIndex = 2;
            this.btnLogin.Text = "登录";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // pieAuthcode
            // 
            this.pieAuthcode.Location = new System.Drawing.Point(12, 62);
            this.pieAuthcode.Name = "pieAuthcode";
            this.pieAuthcode.Size = new System.Drawing.Size(130, 38);
            this.pieAuthcode.TabIndex = 3;
            this.pieAuthcode.TabStop = false;
            // 
            // txtAuthcode
            // 
            this.txtAuthcode.Location = new System.Drawing.Point(42, 106);
            this.txtAuthcode.Name = "txtAuthcode";
            this.txtAuthcode.Size = new System.Drawing.Size(100, 21);
            this.txtAuthcode.TabIndex = 4;
            // 
            // btnGetCart
            // 
            this.btnGetCart.Location = new System.Drawing.Point(179, 6);
            this.btnGetCart.Name = "btnGetCart";
            this.btnGetCart.Size = new System.Drawing.Size(75, 23);
            this.btnGetCart.TabIndex = 5;
            this.btnGetCart.Text = "获取购物车";
            this.btnGetCart.UseVisualStyleBackColor = true;
            this.btnGetCart.Click += new System.EventHandler(this.btnGetCart_Click);
            // 
            // btnEmptyCart
            // 
            this.btnEmptyCart.Location = new System.Drawing.Point(260, 6);
            this.btnEmptyCart.Name = "btnEmptyCart";
            this.btnEmptyCart.Size = new System.Drawing.Size(75, 23);
            this.btnEmptyCart.TabIndex = 7;
            this.btnEmptyCart.Text = "清空购物车";
            this.btnEmptyCart.UseVisualStyleBackColor = true;
            this.btnEmptyCart.Click += new System.EventHandler(this.btnEmptyCart_Click);
            // 
            // lsvCart
            // 
            this.lsvCart.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.lsvCart.FullRowSelect = true;
            this.lsvCart.GridLines = true;
            this.lsvCart.Location = new System.Drawing.Point(179, 35);
            this.lsvCart.Name = "lsvCart";
            this.lsvCart.Size = new System.Drawing.Size(447, 142);
            this.lsvCart.TabIndex = 8;
            this.lsvCart.UseCompatibleStateImageBehavior = false;
            this.lsvCart.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "商品ID";
            this.columnHeader1.Width = 80;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "商品名称";
            this.columnHeader2.Width = 200;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "商品价格";
            // 
            // txtWaresId
            // 
            this.txtWaresId.Location = new System.Drawing.Point(12, 192);
            this.txtWaresId.Name = "txtWaresId";
            this.txtWaresId.Size = new System.Drawing.Size(128, 21);
            this.txtWaresId.TabIndex = 9;
            this.txtWaresId.Text = "10955177564";
            // 
            // btnGetWaresInfo
            // 
            this.btnGetWaresInfo.Location = new System.Drawing.Point(163, 192);
            this.btnGetWaresInfo.Name = "btnGetWaresInfo";
            this.btnGetWaresInfo.Size = new System.Drawing.Size(91, 23);
            this.btnGetWaresInfo.TabIndex = 10;
            this.btnGetWaresInfo.Text = "获取商品信息";
            this.btnGetWaresInfo.UseVisualStyleBackColor = true;
            this.btnGetWaresInfo.Click += new System.EventHandler(this.btnGetWaresInfo_Click);
            // 
            // btnAddCart
            // 
            this.btnAddCart.Location = new System.Drawing.Point(260, 192);
            this.btnAddCart.Name = "btnAddCart";
            this.btnAddCart.Size = new System.Drawing.Size(75, 23);
            this.btnAddCart.TabIndex = 11;
            this.btnAddCart.Text = "添加购物车";
            this.btnAddCart.UseVisualStyleBackColor = true;
            this.btnAddCart.Click += new System.EventHandler(this.btnAddCart_Click);
            // 
            // lsvFindWInfo
            // 
            this.lsvFindWInfo.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6});
            this.lsvFindWInfo.FullRowSelect = true;
            this.lsvFindWInfo.GridLines = true;
            this.lsvFindWInfo.Location = new System.Drawing.Point(12, 230);
            this.lsvFindWInfo.Name = "lsvFindWInfo";
            this.lsvFindWInfo.Size = new System.Drawing.Size(614, 69);
            this.lsvFindWInfo.TabIndex = 12;
            this.lsvFindWInfo.UseCompatibleStateImageBehavior = false;
            this.lsvFindWInfo.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "商品ID";
            this.columnHeader4.Width = 80;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "商品名称";
            this.columnHeader5.Width = 200;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "价格";
            // 
            // btnJieSuan
            // 
            this.btnJieSuan.Location = new System.Drawing.Point(341, 6);
            this.btnJieSuan.Name = "btnJieSuan";
            this.btnJieSuan.Size = new System.Drawing.Size(75, 23);
            this.btnJieSuan.TabIndex = 13;
            this.btnJieSuan.Text = "结算";
            this.btnJieSuan.UseVisualStyleBackColor = true;
            this.btnJieSuan.Click += new System.EventHandler(this.btnJieSuan_Click);
            // 
            // txtMessage
            // 
            this.txtMessage.Location = new System.Drawing.Point(12, 325);
            this.txtMessage.Multiline = true;
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.Size = new System.Drawing.Size(613, 179);
            this.txtMessage.TabIndex = 14;
            // 
            // btnOrderList
            // 
            this.btnOrderList.Location = new System.Drawing.Point(341, 192);
            this.btnOrderList.Name = "btnOrderList";
            this.btnOrderList.Size = new System.Drawing.Size(88, 23);
            this.btnOrderList.TabIndex = 15;
            this.btnOrderList.Text = "获取订单列表";
            this.btnOrderList.UseVisualStyleBackColor = true;
            this.btnOrderList.Click += new System.EventHandler(this.btnOrderList_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(641, 516);
            this.Controls.Add(this.btnOrderList);
            this.Controls.Add(this.txtMessage);
            this.Controls.Add(this.btnJieSuan);
            this.Controls.Add(this.lsvFindWInfo);
            this.Controls.Add(this.btnAddCart);
            this.Controls.Add(this.btnGetWaresInfo);
            this.Controls.Add(this.txtWaresId);
            this.Controls.Add(this.lsvCart);
            this.Controls.Add(this.btnEmptyCart);
            this.Controls.Add(this.btnGetCart);
            this.Controls.Add(this.txtAuthcode);
            this.Controls.Add(this.pieAuthcode);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.txtJDUserPass);
            this.Controls.Add(this.txtJDUserName);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pieAuthcode)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtJDUserName;
        private System.Windows.Forms.TextBox txtJDUserPass;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.PictureBox pieAuthcode;
        private System.Windows.Forms.TextBox txtAuthcode;
        private System.Windows.Forms.Button btnGetCart;
        private System.Windows.Forms.Button btnEmptyCart;
        private System.Windows.Forms.ListView lsvCart;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.TextBox txtWaresId;
        private System.Windows.Forms.Button btnGetWaresInfo;
        private System.Windows.Forms.Button btnAddCart;
        private System.Windows.Forms.ListView lsvFindWInfo;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.Button btnJieSuan;
        private System.Windows.Forms.TextBox txtMessage;
        private System.Windows.Forms.Button btnOrderList;
    }
}

