namespace DataSchemaGui.DetailPages
{
    partial class ColumnTypePage
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.textBoxDescription = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBoxBaseType = new System.Windows.Forms.ComboBox();
            this.textBoxMaxLength = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.checkBoxCanBeNull = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.checkBoxIsDbGenerated = new System.Windows.Forms.CheckBox();
            this.textBoxEnumTypeName = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.textBoxPrecision = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.textBoxScale = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.dataGridViewTargets = new System.Windows.Forms.DataGridView();
            this.bindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.checkBoxMaxLengthIsInherited = new System.Windows.Forms.CheckBox();
            this.checkBoxCanBeNullIsInherited = new System.Windows.Forms.CheckBox();
            this.checkBoxIsDbGeneratedIsInherited = new System.Windows.Forms.CheckBox();
            this.checkBoxEnumTypeNameIsInherited = new System.Windows.Forms.CheckBox();
            this.checkBoxPrecisionIsInherited = new System.Windows.Forms.CheckBox();
            this.checkBoxScaleIsInherited = new System.Windows.Forms.CheckBox();
            this.dataSet1 = new System.Data.DataSet();
            this.dataTableTargets = new System.Data.DataTable();
            this.dataColumnTargetSystem = new System.Data.DataColumn();
            this.dataColumn1 = new System.Data.DataColumn();
            this.dataColumn2 = new System.Data.DataColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTargets)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTableTargets)).BeginInit();
            this.SuspendLayout();
            // 
            // textBoxDescription
            // 
            this.textBoxDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxDescription.Location = new System.Drawing.Point(202, 61);
            this.textBoxDescription.Name = "textBoxDescription";
            this.textBoxDescription.Size = new System.Drawing.Size(571, 20);
            this.textBoxDescription.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(23, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Description:";
            // 
            // textBoxName
            // 
            this.textBoxName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxName.Location = new System.Drawing.Point(202, 17);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(571, 20);
            this.textBoxName.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Name:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(23, 90);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Base type:";
            // 
            // comboBoxBaseType
            // 
            this.comboBoxBaseType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxBaseType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxBaseType.FormattingEnabled = true;
            this.comboBoxBaseType.Location = new System.Drawing.Point(202, 87);
            this.comboBoxBaseType.Name = "comboBoxBaseType";
            this.comboBoxBaseType.Size = new System.Drawing.Size(571, 21);
            this.comboBoxBaseType.TabIndex = 9;
            // 
            // textBoxMaxLength
            // 
            this.textBoxMaxLength.Location = new System.Drawing.Point(202, 114);
            this.textBoxMaxLength.Name = "textBoxMaxLength";
            this.textBoxMaxLength.Size = new System.Drawing.Size(109, 20);
            this.textBoxMaxLength.TabIndex = 11;
            this.textBoxMaxLength.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(23, 117);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(62, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Max length:";
            // 
            // checkBoxCanBeNull
            // 
            this.checkBoxCanBeNull.AutoSize = true;
            this.checkBoxCanBeNull.Location = new System.Drawing.Point(202, 142);
            this.checkBoxCanBeNull.Name = "checkBoxCanBeNull";
            this.checkBoxCanBeNull.Size = new System.Drawing.Size(15, 14);
            this.checkBoxCanBeNull.TabIndex = 12;
            this.checkBoxCanBeNull.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(23, 143);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(63, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "Can be null:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(23, 169);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(121, 13);
            this.label6.TabIndex = 15;
            this.label6.Text = "Generated by database:";
            // 
            // checkBoxIsDbGenerated
            // 
            this.checkBoxIsDbGenerated.AutoSize = true;
            this.checkBoxIsDbGenerated.Location = new System.Drawing.Point(202, 168);
            this.checkBoxIsDbGenerated.Name = "checkBoxIsDbGenerated";
            this.checkBoxIsDbGenerated.Size = new System.Drawing.Size(15, 14);
            this.checkBoxIsDbGenerated.TabIndex = 14;
            this.checkBoxIsDbGenerated.UseVisualStyleBackColor = true;
            // 
            // textBoxEnumTypeName
            // 
            this.textBoxEnumTypeName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxEnumTypeName.Location = new System.Drawing.Point(202, 188);
            this.textBoxEnumTypeName.Name = "textBoxEnumTypeName";
            this.textBoxEnumTypeName.Size = new System.Drawing.Size(433, 20);
            this.textBoxEnumTypeName.TabIndex = 17;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(23, 191);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(121, 13);
            this.label7.TabIndex = 16;
            this.label7.Text = "Enumeration type name:";
            // 
            // textBoxPrecision
            // 
            this.textBoxPrecision.Location = new System.Drawing.Point(202, 214);
            this.textBoxPrecision.Name = "textBoxPrecision";
            this.textBoxPrecision.Size = new System.Drawing.Size(109, 20);
            this.textBoxPrecision.TabIndex = 19;
            this.textBoxPrecision.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(23, 217);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 13);
            this.label8.TabIndex = 18;
            this.label8.Text = "Precision:";
            // 
            // textBoxScale
            // 
            this.textBoxScale.Location = new System.Drawing.Point(202, 240);
            this.textBoxScale.Name = "textBoxScale";
            this.textBoxScale.Size = new System.Drawing.Size(109, 20);
            this.textBoxScale.TabIndex = 21;
            this.textBoxScale.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(23, 243);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(37, 13);
            this.label9.TabIndex = 20;
            this.label9.Text = "Scale:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(23, 269);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(46, 13);
            this.label10.TabIndex = 22;
            this.label10.Text = "Targets:";
            // 
            // dataGridViewTargets
            // 
            this.dataGridViewTargets.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewTargets.Location = new System.Drawing.Point(202, 269);
            this.dataGridViewTargets.Name = "dataGridViewTargets";
            this.dataGridViewTargets.Size = new System.Drawing.Size(571, 308);
            this.dataGridViewTargets.TabIndex = 23;
            // 
            // checkBoxMaxLengthIsInherited
            // 
            this.checkBoxMaxLengthIsInherited.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxMaxLengthIsInherited.AutoSize = true;
            this.checkBoxMaxLengthIsInherited.Location = new System.Drawing.Point(656, 116);
            this.checkBoxMaxLengthIsInherited.Name = "checkBoxMaxLengthIsInherited";
            this.checkBoxMaxLengthIsInherited.Size = new System.Drawing.Size(67, 17);
            this.checkBoxMaxLengthIsInherited.TabIndex = 24;
            this.checkBoxMaxLengthIsInherited.Text = "Inherited";
            this.checkBoxMaxLengthIsInherited.UseVisualStyleBackColor = true;
            // 
            // checkBoxCanBeNullIsInherited
            // 
            this.checkBoxCanBeNullIsInherited.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxCanBeNullIsInherited.AutoSize = true;
            this.checkBoxCanBeNullIsInherited.Location = new System.Drawing.Point(656, 139);
            this.checkBoxCanBeNullIsInherited.Name = "checkBoxCanBeNullIsInherited";
            this.checkBoxCanBeNullIsInherited.Size = new System.Drawing.Size(67, 17);
            this.checkBoxCanBeNullIsInherited.TabIndex = 25;
            this.checkBoxCanBeNullIsInherited.Text = "Inherited";
            this.checkBoxCanBeNullIsInherited.UseVisualStyleBackColor = true;
            // 
            // checkBoxIsDbGeneratedIsInherited
            // 
            this.checkBoxIsDbGeneratedIsInherited.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxIsDbGeneratedIsInherited.AutoSize = true;
            this.checkBoxIsDbGeneratedIsInherited.Location = new System.Drawing.Point(656, 165);
            this.checkBoxIsDbGeneratedIsInherited.Name = "checkBoxIsDbGeneratedIsInherited";
            this.checkBoxIsDbGeneratedIsInherited.Size = new System.Drawing.Size(67, 17);
            this.checkBoxIsDbGeneratedIsInherited.TabIndex = 26;
            this.checkBoxIsDbGeneratedIsInherited.Text = "Inherited";
            this.checkBoxIsDbGeneratedIsInherited.UseVisualStyleBackColor = true;
            // 
            // checkBoxEnumTypeNameIsInherited
            // 
            this.checkBoxEnumTypeNameIsInherited.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxEnumTypeNameIsInherited.AutoSize = true;
            this.checkBoxEnumTypeNameIsInherited.Location = new System.Drawing.Point(656, 190);
            this.checkBoxEnumTypeNameIsInherited.Name = "checkBoxEnumTypeNameIsInherited";
            this.checkBoxEnumTypeNameIsInherited.Size = new System.Drawing.Size(67, 17);
            this.checkBoxEnumTypeNameIsInherited.TabIndex = 27;
            this.checkBoxEnumTypeNameIsInherited.Text = "Inherited";
            this.checkBoxEnumTypeNameIsInherited.UseVisualStyleBackColor = true;
            // 
            // checkBoxPrecisionIsInherited
            // 
            this.checkBoxPrecisionIsInherited.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxPrecisionIsInherited.AutoSize = true;
            this.checkBoxPrecisionIsInherited.Location = new System.Drawing.Point(656, 216);
            this.checkBoxPrecisionIsInherited.Name = "checkBoxPrecisionIsInherited";
            this.checkBoxPrecisionIsInherited.Size = new System.Drawing.Size(67, 17);
            this.checkBoxPrecisionIsInherited.TabIndex = 28;
            this.checkBoxPrecisionIsInherited.Text = "Inherited";
            this.checkBoxPrecisionIsInherited.UseVisualStyleBackColor = true;
            // 
            // checkBoxScaleIsInherited
            // 
            this.checkBoxScaleIsInherited.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxScaleIsInherited.AutoSize = true;
            this.checkBoxScaleIsInherited.Location = new System.Drawing.Point(656, 242);
            this.checkBoxScaleIsInherited.Name = "checkBoxScaleIsInherited";
            this.checkBoxScaleIsInherited.Size = new System.Drawing.Size(67, 17);
            this.checkBoxScaleIsInherited.TabIndex = 29;
            this.checkBoxScaleIsInherited.Text = "Inherited";
            this.checkBoxScaleIsInherited.UseVisualStyleBackColor = true;
            // 
            // dataSet1
            // 
            this.dataSet1.DataSetName = "NewDataSet";
            this.dataSet1.Tables.AddRange(new System.Data.DataTable[] {
            this.dataTableTargets});
            // 
            // dataTableTargets
            // 
            this.dataTableTargets.Columns.AddRange(new System.Data.DataColumn[] {
            this.dataColumnTargetSystem,
            this.dataColumn1,
            this.dataColumn2});
            this.dataTableTargets.TableName = "Targets";
            // 
            // dataColumnTargetSystem
            // 
            this.dataColumnTargetSystem.Caption = "Target System";
            this.dataColumnTargetSystem.ColumnName = "TargetSystem";
            // 
            // dataColumn1
            // 
            this.dataColumn1.Caption = "Data Type";
            this.dataColumn1.ColumnName = "DataType";
            // 
            // dataColumn2
            // 
            this.dataColumn2.Caption = "Data Type when Referenced";
            this.dataColumn2.ColumnName = "DataTypeWhenReferenced";
            // 
            // ColumnTypePage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.checkBoxScaleIsInherited);
            this.Controls.Add(this.checkBoxPrecisionIsInherited);
            this.Controls.Add(this.checkBoxEnumTypeNameIsInherited);
            this.Controls.Add(this.checkBoxIsDbGeneratedIsInherited);
            this.Controls.Add(this.checkBoxCanBeNullIsInherited);
            this.Controls.Add(this.checkBoxMaxLengthIsInherited);
            this.Controls.Add(this.dataGridViewTargets);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.textBoxScale);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.textBoxPrecision);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.textBoxEnumTypeName);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.checkBoxIsDbGenerated);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.checkBoxCanBeNull);
            this.Controls.Add(this.textBoxMaxLength);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.comboBoxBaseType);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBoxDescription);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxName);
            this.Controls.Add(this.label1);
            this.Name = "ColumnTypePage";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTargets)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTableTargets)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxDescription;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBoxBaseType;
        private System.Windows.Forms.TextBox textBoxMaxLength;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox checkBoxCanBeNull;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox checkBoxIsDbGenerated;
        private System.Windows.Forms.TextBox textBoxEnumTypeName;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBoxPrecision;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBoxScale;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.DataGridView dataGridViewTargets;
        private System.Windows.Forms.CheckBox checkBoxMaxLengthIsInherited;
        private System.Windows.Forms.CheckBox checkBoxCanBeNullIsInherited;
        private System.Windows.Forms.CheckBox checkBoxIsDbGeneratedIsInherited;
        private System.Windows.Forms.CheckBox checkBoxEnumTypeNameIsInherited;
        private System.Windows.Forms.CheckBox checkBoxPrecisionIsInherited;
        private System.Windows.Forms.CheckBox checkBoxScaleIsInherited;
        private System.Windows.Forms.BindingSource bindingSource;
        private System.Data.DataSet dataSet1;
        private System.Data.DataTable dataTableTargets;
        private System.Data.DataColumn dataColumnTargetSystem;
        private System.Data.DataColumn dataColumn1;
        private System.Data.DataColumn dataColumn2;
    }
}
