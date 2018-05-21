<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPLU
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.txtSearch = New System.Windows.Forms.TextBox()
        Me.btnSearch = New System.Windows.Forms.Button()
        Me.lvitemList = New System.Windows.Forms.ListView()
        Me.ColumnHeader1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader2 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader3 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader5 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader6 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader7 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Pb_itm = New System.Windows.Forms.ProgressBar()
        Me.btnselect = New System.Windows.Forms.Button()
        Me.btncancel = New System.Windows.Forms.Button()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.txtSearch)
        Me.GroupBox1.Controls.Add(Me.btnSearch)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 3)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(691, 48)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        '
        'txtSearch
        '
        Me.txtSearch.Location = New System.Drawing.Point(6, 19)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(598, 20)
        Me.txtSearch.TabIndex = 0
        '
        'btnSearch
        '
        Me.btnSearch.Location = New System.Drawing.Point(610, 17)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(75, 23)
        Me.btnSearch.TabIndex = 1
        Me.btnSearch.Text = "&Search"
        Me.btnSearch.UseVisualStyleBackColor = True
        '
        'lvitemList
        '
        Me.lvitemList.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lvitemList.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1, Me.ColumnHeader2, Me.ColumnHeader3, Me.ColumnHeader5, Me.ColumnHeader6, Me.ColumnHeader7})
        Me.lvitemList.FullRowSelect = True
        Me.lvitemList.GridLines = True
        Me.lvitemList.Location = New System.Drawing.Point(12, 53)
        Me.lvitemList.Name = "lvitemList"
        Me.lvitemList.Size = New System.Drawing.Size(691, 313)
        Me.lvitemList.TabIndex = 5
        Me.lvitemList.UseCompatibleStateImageBehavior = False
        Me.lvitemList.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Text = "Itemcode"
        Me.ColumnHeader1.Width = 105
        '
        'ColumnHeader2
        '
        Me.ColumnHeader2.Text = "Description"
        Me.ColumnHeader2.Width = 176
        '
        'ColumnHeader3
        '
        Me.ColumnHeader3.Text = "Category"
        '
        'ColumnHeader5
        '
        Me.ColumnHeader5.Text = "UOM"
        Me.ColumnHeader5.Width = 104
        '
        'ColumnHeader6
        '
        Me.ColumnHeader6.Text = "Onhand"
        Me.ColumnHeader6.Width = 92
        '
        'ColumnHeader7
        '
        Me.ColumnHeader7.Text = "SalePrice"
        Me.ColumnHeader7.Width = 123
        '
        'Pb_itm
        '
        Me.Pb_itm.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Pb_itm.Location = New System.Drawing.Point(2, 400)
        Me.Pb_itm.Name = "Pb_itm"
        Me.Pb_itm.Size = New System.Drawing.Size(715, 16)
        Me.Pb_itm.TabIndex = 6
        '
        'btnselect
        '
        Me.btnselect.Location = New System.Drawing.Point(541, 372)
        Me.btnselect.Name = "btnselect"
        Me.btnselect.Size = New System.Drawing.Size(75, 23)
        Me.btnselect.TabIndex = 2
        Me.btnselect.Text = "&Select"
        Me.btnselect.UseVisualStyleBackColor = True
        '
        'btncancel
        '
        Me.btncancel.Location = New System.Drawing.Point(622, 371)
        Me.btncancel.Name = "btncancel"
        Me.btncancel.Size = New System.Drawing.Size(75, 23)
        Me.btncancel.TabIndex = 7
        Me.btncancel.Text = "&Cancel"
        Me.btncancel.UseVisualStyleBackColor = True
        '
        'frmPLU
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(715, 415)
        Me.Controls.Add(Me.btncancel)
        Me.Controls.Add(Me.btnselect)
        Me.Controls.Add(Me.Pb_itm)
        Me.Controls.Add(Me.lvitemList)
        Me.Controls.Add(Me.GroupBox1)
        Me.Name = "frmPLU"
        Me.Text = "Item Search"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents txtSearch As System.Windows.Forms.TextBox
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents lvitemList As System.Windows.Forms.ListView
    Friend WithEvents ColumnHeader1 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader2 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader3 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader5 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader6 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader7 As System.Windows.Forms.ColumnHeader
    Friend WithEvents Pb_itm As System.Windows.Forms.ProgressBar
    Friend WithEvents btnselect As System.Windows.Forms.Button
    Friend WithEvents btncancel As System.Windows.Forms.Button
End Class
