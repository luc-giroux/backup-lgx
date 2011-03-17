Public Module UserContextMenu
    Public MyClipboard As String = ""
    Public CurrentCell As DataGridCell
End Module

Public Class UserControl1
    Inherits System.Windows.Forms.UserControl

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
    End Sub

    'UserControl overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents FilterTextBox As System.Windows.Forms.TextBox
    Friend WithEvents CopyButton As System.Windows.Forms.Label
    Friend WithEvents PasteButton As System.Windows.Forms.Label
    Friend WithEvents FilterOnCurrentButton As System.Windows.Forms.Label
    Friend WithEvents FilterOnButton As System.Windows.Forms.Label
    Friend WithEvents CancelFilterButton As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.FilterTextBox = New System.Windows.Forms.TextBox
        Me.CopyButton = New System.Windows.Forms.Label
        Me.PasteButton = New System.Windows.Forms.Label
        Me.FilterOnCurrentButton = New System.Windows.Forms.Label
        Me.FilterOnButton = New System.Windows.Forms.Label
        Me.CancelFilterButton = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'FilterTextBox
        '
        Me.FilterTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.FilterTextBox.Location = New System.Drawing.Point(60, 62)
        Me.FilterTextBox.Name = "FilterTextBox"
        Me.FilterTextBox.Size = New System.Drawing.Size(90, 20)
        Me.FilterTextBox.TabIndex = 0
        Me.FilterTextBox.Text = ""
        '
        'CopyButton
        '
        Me.CopyButton.Location = New System.Drawing.Point(0, 0)
        Me.CopyButton.Name = "CopyButton"
        Me.CopyButton.Size = New System.Drawing.Size(150, 20)
        Me.CopyButton.TabIndex = 7
        Me.CopyButton.Text = "Copy"
        Me.CopyButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'PasteButton
        '
        Me.PasteButton.Location = New System.Drawing.Point(0, 19)
        Me.PasteButton.Name = "PasteButton"
        Me.PasteButton.Size = New System.Drawing.Size(150, 20)
        Me.PasteButton.TabIndex = 8
        Me.PasteButton.Text = "Paste"
        Me.PasteButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'FilterOnCurrentButton
        '
        Me.FilterOnCurrentButton.Location = New System.Drawing.Point(0, 42)
        Me.FilterOnCurrentButton.Name = "FilterOnCurrentButton"
        Me.FilterOnCurrentButton.Size = New System.Drawing.Size(150, 20)
        Me.FilterOnCurrentButton.TabIndex = 9
        Me.FilterOnCurrentButton.Text = "Filter On Current Value"
        Me.FilterOnCurrentButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'FilterOnButton
        '
        Me.FilterOnButton.Location = New System.Drawing.Point(0, 62)
        Me.FilterOnButton.Name = "FilterOnButton"
        Me.FilterOnButton.Size = New System.Drawing.Size(60, 20)
        Me.FilterOnButton.TabIndex = 10
        Me.FilterOnButton.Text = "Filter On"
        Me.FilterOnButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'CancelFilterButton
        '
        Me.CancelFilterButton.Location = New System.Drawing.Point(0, 85)
        Me.CancelFilterButton.Name = "CancelFilterButton"
        Me.CancelFilterButton.Size = New System.Drawing.Size(150, 20)
        Me.CancelFilterButton.TabIndex = 11
        Me.CancelFilterButton.Text = "Cancel Filter"
        Me.CancelFilterButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'UserControl1
        '
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.Controls.Add(Me.CancelFilterButton)
        Me.Controls.Add(Me.FilterOnButton)
        Me.Controls.Add(Me.FilterOnCurrentButton)
        Me.Controls.Add(Me.PasteButton)
        Me.Controls.Add(Me.CopyButton)
        Me.Controls.Add(Me.FilterTextBox)
        Me.Name = "UserControl1"
        Me.Size = New System.Drawing.Size(150, 105)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Function EnabledPasteButton() As Boolean
        If MyClipboard.Length > 0 Then
            If CType(Me.MyParent.MyDataGrid.TableStyles(0).GridColumnStyles(CurrentCell.ColumnNumber), ColumnStyleInterface).IsReadOnly Then
                Return False
            Else
                Return True
            End If
        Else
                Return False
            End If
    End Function
    Private Sub LoadHandler(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        'Me.PasteButton.Enabled = EnabledPasteButton()
    End Sub
    Private MouseOverColor As Color = Color.FromKnownColor(KnownColor.ControlDark)
    Private MouseNotOverColor As Color = Color.FromKnownColor(KnownColor.Control)
    Private Sub CopyMouseEnterHandler(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CopyButton.MouseEnter
        Me.CopyButton.BackColor = MouseOverColor
    End Sub
    Private Sub CopyMouseLeaveHandler(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CopyButton.MouseLeave
        Me.CopyButton.BackColor = MouseNotOverColor
        CloseIfMouseHaveLeft()
    End Sub
    Private Sub PasteMouseEnterHandler(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PasteButton.MouseEnter
        Me.PasteButton.BackColor = MouseOverColor
    End Sub
    Private Sub PasteMouseLeaveHandler(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PasteButton.MouseLeave
        Me.PasteButton.BackColor = MouseNotOverColor
        CloseIfMouseHaveLeft()
    End Sub
    Private Sub FilterOnCurrentMouseEnterHandler(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FilterOnCurrentButton.MouseEnter
        Me.FilterOnCurrentButton.BackColor = MouseOverColor
    End Sub
    Private Sub FilterOnCurrentMouseLeaveHandler(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FilterOnCurrentButton.MouseLeave
        Me.FilterOnCurrentButton.BackColor = MouseNotOverColor
        CloseIfMouseHaveLeft()
    End Sub
    Private Sub FilterTextBoxMouseEnterHandler(ByVal sender As Object, ByVal e As EventArgs) Handles FilterTextBox.MouseEnter
        Me.FilterOnButton.BackColor = MouseOverColor
    End Sub
    Private Sub FilterTextBoxMouseLeaveHandler(ByVal sender As Object, ByVal e As EventArgs) Handles FilterTextBox.MouseLeave
        Me.FilterOnButton.BackColor = MouseNotOverColor
        CloseIfMouseHaveLeft()
    End Sub
    Private Sub FilterOnMouseEnterHandler(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FilterOnButton.MouseEnter
        Me.FilterOnButton.BackColor = MouseOverColor
    End Sub
    Private Sub FilterOnMouseLeaveHandler(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FilterOnButton.MouseLeave
        Me.FilterOnButton.BackColor = MouseNotOverColor
        CloseIfMouseHaveLeft()
    End Sub
    Private Sub CancelFilterMouseEnterHandler(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CancelFilterButton.MouseEnter
        Me.CancelFilterButton.BackColor = MouseOverColor
    End Sub
    Private Sub CancelFilterMouseLeaveHandler(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CancelFilterButton.MouseLeave
        Me.CancelFilterButton.BackColor = MouseNotOverColor
        CloseIfMouseHaveLeft()
    End Sub

    Private Sub CloseIfMouseHaveLeft()
        Dim MyPosition As Rectangle

        Dim TopLeft As Point = Me.PointToScreen(New Point(Me.Left, Me.Top))
        Dim BottomRight As Point = Me.PointToScreen(New Point(Me.Left + Me.Width, Me.Top + Me.Height))
        Dim MyMousePosition As Point = MousePosition
        MyPosition = New Rectangle(TopLeft.X, TopLeft.Y, BottomRight.X - TopLeft.X, BottomRight.Y - TopLeft.Y)
        If Not MyPosition.Contains(MyMousePosition) Then
            CloseParent()
        End If
    End Sub
    Private Sub LeavingHandler(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.MouseLeave
        ' Must Close the menu
        CloseIfMouseHaveLeft()
    End Sub
    Private Sub CopyClickedHandler(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CopyButton.Click
        ' copy processing
        MyClipboard = CType(Me.MyParent.MyDataGrid.Item(CurrentCell.RowNumber, CurrentCell.ColumnNumber), String)
        CloseParent()
    End Sub
    Private Sub PasteClickedHandler(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PasteButton.Click
        ' Paste processing
        Me.MyParent.MyDataGrid.Item(CurrentCell.RowNumber, CurrentCell.ColumnNumber) = MyClipboard
        ' Move out of the current cell to force a refresh
        If CurrentCell.ColumnNumber > Me.MyParent.MyDataGrid.FirstVisibleColumn + 1 Then ' at least one complete column on the left
            Me.MyParent.MyDataGrid.CurrentCell = New DataGridCell(CurrentCell.RowNumber, CurrentCell.ColumnNumber - 1)
        Else
            Me.MyParent.MyDataGrid.CurrentCell = New DataGridCell(CurrentCell.RowNumber, CurrentCell.ColumnNumber + 1)
        End If
        Me.MyParent.MyDataGrid.CurrentCell = CurrentCell

        CloseParent()
    End Sub
    Private Sub CancelFilterClickedHandler(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CancelFilterButton.Click
        ' Cancel filter processing
        MyParentForm.SetFilter(CurrentCell.ColumnNumber, "")
        CloseParent()
    End Sub
    Private Sub FilterOnCurrentClickedHandler(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FilterOnCurrentButton.Click
        ' Filter on current value processing
        Dim CurrentValue As String
        CurrentValue = CType(Me.MyParent.MyDataGrid.Item(CurrentCell.RowNumber, CurrentCell.ColumnNumber), String)
        MyParentForm.SetFilter(CurrentCell.ColumnNumber, CurrentValue)
        CloseParent()
    End Sub
    Private Sub FiterOnTextValidatedHandler(ByVal sender As Object, ByVal e As KeyPressEventArgs) Handles FilterTextBox.KeyPress
        ' New text filter processing
        If e.KeyChar = vbCr Then
            Dim CurrentValue As String
            CurrentValue = Me.FilterTextBox.Text
            MyParentForm.SetFilter(CurrentCell.ColumnNumber, CurrentValue)
            CloseParent()
        End If
    End Sub

    Private MyParent As myContextualMenu
    Private MyParentForm As FilterableForm
    Public Sub New(ByVal Parent As myContextualMenu, ByVal pDataGrid As DataGrid, ByVal pParentForm As FilterableForm)
        Me.New()
        MyParent = Parent
        MyParentForm = pParentForm
        Me.PasteButton.Enabled = EnabledPasteButton()
        If CType(Me.MyParent.MyDataGrid.TableStyles(0).GridColumnStyles(CurrentCell.ColumnNumber), ColumnStyleInterface).FilterActive Then
            Me.CancelFilterButton.Enabled = True
        Else
            Me.CancelFilterButton.Enabled = False
        End If

    End Sub
    Private Sub CloseParent()
        MyParent.Close()
    End Sub
End Class
Public Class myContextualMenu
    Inherits ContextMenu
    Private MyForm As Form
    Public Sub PopupHandler(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Popup
        If Not IsNothing(MyForm) Then
            MyForm.Dispose()
        End If
        CurrentCell = MyDataGrid.CurrentCell
        MyForm = New Form
        MyForm.TopMost = True
        MyForm.FormBorderStyle = FormBorderStyle.None
        Dim MyControl As New UserControl1(Me, MyDataGrid, MyParentForm)
        MyForm.Controls.Add(MyControl)
        MyControl.Location = New Point(1, 1)
        MyForm.StartPosition = FormStartPosition.Manual
        MyForm.Location = New Point(MyDataGrid.MousePosition.X - 1, MyDataGrid.MousePosition.Y - 1)
        MyForm.ClientSize = New Size(MyControl.Size.Width + 2, MyControl.Size.Height + 2)
        MyForm.BackColor = Color.DarkGray
        MyForm.Show()
    End Sub

    Public MyDataGrid As DataGrid
    Public MyParentForm As FilterableForm
    Public Sub Close()
        MyForm.Hide()
    End Sub
    Public Sub New(ByRef ParentControl As DataGrid, ByVal pParentForm As FilterableForm)
        MyDataGrid = ParentControl
        MyParentForm = pParentForm
    End Sub
End Class