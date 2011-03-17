Option Strict Off
Option Explicit On 

Imports System.Threading
Imports Microsoft.VisualBasic
Imports System
Imports System.ComponentModel
Imports System.Data
Imports System.Data.Common
Imports System.Data.OleDb
Imports System.Drawing
Imports System.Windows.Forms

Public Class InvoiceEditionForm
    Inherits System.Windows.Forms.Form
    Implements FilterableForm
    Public StatusBar As New ProgressStatus
    Friend WithEvents ClearFilterButton As System.Windows.Forms.Button
    Friend WithEvents YearWeekTableAdapter As NewLookupDatasetTableAdapters.YearWeekTableAdapter
    Friend WithEvents InvoicesHoursTableAdapter As InvoicedHoursTableAdapters.InvoiceHoursTableAdapter
    Public info As New System.Windows.Forms.StatusBarPanel
    Friend WithEvents InvoiceHoursTableAdapter As PAF.InvoicedHoursTableAdapters.InvoiceHoursTableAdapter

    Private PrivilegedSQLConnection As SqlClient.SqlConnection

    Public Enum ColumnOrder
        HoursEntryID
        Contract     
        AssignedLocation
        PayCurrencyID
        DisciplineCode
        DisciplineDescription
        CompanyID
        EmployeeID
        EmployeeName
        Year
        Week
        PostedDate
        Hours
        LabourCost
        OtherOfficeSpace
        OtherOfficeExpenses
        OtherIT
        OtherITCore
        Adjustment
        Invoiced
        InvoiceClosed
        DetailButton
    End Enum
    Private MustInherit Class DataGridButtonColumnClass
        Inherits System.Windows.Forms.DataGridColumnStyle
        Implements ColumnStyleInterface

        Private Column As ColumnOrder
        Private ThisRowIsDown As Integer = -1
        Private ThisColumnIsDown As Integer = -1
        Friend MyDataGrid As DataGrid

        Public Sub New(ByRef pMyDataGrid As DataGrid, ByVal Col As ColumnOrder)
            MyBase.New()
            MyDataGrid = pMyDataGrid
            Column = Col
        End Sub

        Protected Overrides Sub SetDataGridInColumn(ByVal Value As DataGrid)
            MyBase.SetDataGridInColumn(Value)
            AddHandler Me.DataGridTableStyle.DataGrid.MouseDown, AddressOf MouseDown
            AddHandler Me.DataGridTableStyle.DataGrid.MouseUp, AddressOf MouseUp
            MyDataGrid = Value
        End Sub
        Private Sub MouseUp(ByVal Sender As Object, ByVal e As MouseEventArgs)
            Dim HitTestInfo As DataGrid.HitTestInfo = Me.DataGridTableStyle.DataGrid.HitTest(New Point(e.X, e.Y))
            Dim BindingContext As BindingManagerBase = Me.DataGridTableStyle.DataGrid.BindingContext(DataGridTableStyle.DataGrid.DataSource, DataGridTableStyle.DataGrid.DataMember)
            If HitTestInfo.Type = DataGrid.HitTestType.Cell AndAlso HitTestInfo.Column = Column AndAlso HitTestInfo.Row < BindingContext.Count And ThisRowIsDown <> -1 Then
                ThisRowIsDown = -1
                ThisColumnIsDown = -1
                Me.Invalidate()
                ButtonClicked()
            End If
        End Sub
        Private Sub MouseDown(ByVal Sender As Object, ByVal e As MouseEventArgs)
            Dim HitTestInfo As DataGrid.HitTestInfo = Me.DataGridTableStyle.DataGrid.HitTest(New Point(e.X, e.Y))
            ThisRowIsDown = HitTestInfo.Row
            ThisColumnIsDown = HitTestInfo.Column
            Dim BindingContext As BindingManagerBase = Me.DataGridTableStyle.DataGrid.BindingContext(DataGridTableStyle.DataGrid.DataSource, DataGridTableStyle.DataGrid.DataMember)
            If HitTestInfo.Type = DataGrid.HitTestType.Cell AndAlso HitTestInfo.Column = Column AndAlso HitTestInfo.Row < BindingContext.Count Then
                Me.Invalidate()
            End If
        End Sub

        Protected Overrides Sub Abort(ByVal rowNum As Integer)
            Me.Invalidate()
        End Sub

        Protected Overrides Function Commit(ByVal dataSource As System.Windows.Forms.CurrencyManager, ByVal rowNum As Integer) As Boolean
            Me.Invalidate()
            Return True
        End Function

        Protected Overloads Overrides Sub Edit(ByVal source As System.Windows.Forms.CurrencyManager, ByVal rowNum As Integer, ByVal bounds As System.Drawing.Rectangle, ByVal [readOnly] As Boolean, ByVal instantText As String, ByVal cellIsVisible As Boolean)
            Me.Invalidate()
        End Sub

        Protected Overrides Function GetMinimumHeight() As Integer
            Return 0
        End Function

        Protected Overrides Function GetPreferredHeight(ByVal g As System.Drawing.Graphics, ByVal value As Object) As Integer
            Return 10
        End Function

        Protected Overrides Function GetPreferredSize(ByVal g As System.Drawing.Graphics, ByVal value As Object) As System.Drawing.Size
            GetPreferredSize.Height = GetPreferredHeight(g, value)
            GetPreferredSize.Width = 10
        End Function

        Protected Overloads Overrides Sub Paint(ByVal g As System.Drawing.Graphics, ByVal bounds As System.Drawing.Rectangle, ByVal source As System.Windows.Forms.CurrencyManager, ByVal rowNum As Integer)
            If rowNum = ThisRowIsDown And Me.Column = ThisColumnIsDown Then
                ControlPaint.DrawButton(g, bounds, ButtonState.Pushed)
            Else
                ControlPaint.DrawButton(g, bounds, ButtonState.Normal)
            End If
            g.DrawString(ButtonText(source, rowNum), Me.DataGridTableStyle.DataGrid.Font, Brushes.Black, bounds.Left, bounds.Top)
        End Sub

        Protected Overloads Overrides Sub Paint(ByVal g As System.Drawing.Graphics, ByVal bounds As System.Drawing.Rectangle, ByVal source As System.Windows.Forms.CurrencyManager, ByVal rowNum As Integer, ByVal alignToRight As Boolean)
            Paint(g, bounds, source, rowNum)
        End Sub
        MustOverride Function ButtonText(ByRef source As System.Windows.Forms.CurrencyManager, ByVal rowNum As Integer) As String
        MustOverride Sub ButtonClicked()
        Protected l_ColumnID As ColumnOrder
        Protected l_FilterActive As Boolean
        Protected l_FilterString As String
        Protected l_ReadOnly As Boolean
        Protected l_Sorted As Boolean
        Public Sub ClearFilter() Implements ColumnStyleInterface.ClearFilter
            l_FilterActive = False
            l_FilterString = ""
        End Sub
        Public ReadOnly Property FilterActive() As Boolean Implements ColumnStyleInterface.FilterActive
            Get
                Return l_FilterActive
            End Get
        End Property
        Public Property FilterString() As String Implements ColumnStyleInterface.FilterString
            Get
                Return l_FilterString
            End Get
            Set(ByVal Value As String)
                If Value.Length > 0 Then
                    l_FilterActive = True
                Else
                    l_FilterActive = False
                End If
                l_FilterString = Value
            End Set
        End Property
        Public ReadOnly Property IsReadOnly() As Boolean Implements ColumnStyleInterface.IsReadOnly
            Get
                Return l_ReadOnly
            End Get
        End Property
        Public Property Sorted() As Boolean Implements ColumnStyleInterface.Sorted
            Get
                Return l_Sorted
            End Get
            Set(ByVal Value As Boolean)
                l_Sorted = Value
                If l_Sorted Then
                    Dim c As ColumnStyleInterface
                    For Each c In Me.DataGridTableStyle.GridColumnStyles
                        If c.ColumnID <> Me.ColumnID Then
                            c.Sorted = False
                        End If
                    Next
                End If
            End Set
        End Property
        Public ReadOnly Property ColumnID() As Integer Implements ColumnStyleInterface.ColumnID
            Get
                Return l_ColumnID
            End Get
        End Property

    End Class
    Private Class ShowDetailsButtonColumnClass
        Inherits DataGridButtonColumnClass
        Public Overrides Sub ButtonClicked()
            Dim currentRow As Integer = MyDataGrid.CurrentRowIndex()
            Dim HoursEntryID As Integer = CType(MyDataGrid.Item(currentRow, 0), Integer)
            Dim Adjustment As String

            If Not IsDBNull(HoursEntryID) Then
                Dim conn As SqlClient.SqlConnection = GetSqlConnection()
                'Modified by Yang 27/02/08
                'Dim cmd As New Data.SqlClient.SqlCommand("SELECT LabourCost, OtherOfficeSpace, OtherOfficeExpenses, OtherIT, OtherITCore, Adjustment FROM dbo.Hours WHERE EntryID = @1 for xml auto", conn)
                Dim cmd As New Data.SqlClient.SqlCommand("SELECT LabourCost, OtherOfficeSpace, OtherOfficeExpenses, OtherIT, OtherITCore, Adjustment, RatePayCurrencyID, RateAssignCurrencyID FROM dbo.Hours WHERE EntryID = @1 for xml auto", conn)
                cmd.Parameters.AddWithValue("@1", HoursEntryID)
                Dim xmlread As Xml.XmlReader = cmd.ExecuteXmlReader()
                xmlread.Read()
                If Trim(xmlread.GetAttribute("Adjustment")) = Nothing Then
                    Adjustment = "NO"
                Else
                    Adjustment = "YES"
                End If
                'MsgBox("Labour Cost = " & Trim(xmlread.GetAttribute("LabourCost")) & vbCrLf & "OtherOfficeSpace Cost = " & Trim(xmlread.GetAttribute("OtherOfficeSpace")) & vbCrLf & "OtherOfficeExpenses Cost = " & Trim(xmlread.GetAttribute("OtherOfficeExpenses")) & vbCrLf & "OtherIT Cost = " & Trim(xmlread.GetAttribute("OtherIT")) & vbCrLf & "OtherITCore Cost = " & Trim(xmlread.GetAttribute("OtherITCore")) & vbCrLf & "Adjustment = " & Adjustment, MsgBoxStyle.Information, "Entry " & HoursEntryID.ToString & " Cost Information")
                MsgBox("Labour Cost = " & Trim(xmlread.GetAttribute("LabourCost")) & " " & Trim(xmlread.GetAttribute("RatePayCurrencyID")) & vbCrLf & "OtherOfficeSpace Cost = " & Trim(xmlread.GetAttribute("OtherOfficeSpace")) & " " & Trim(xmlread.GetAttribute("RateAssignCurrencyID")) & vbCrLf & "OtherOfficeExpenses Cost = " & Trim(xmlread.GetAttribute("OtherOfficeExpenses")) & " " & Trim(xmlread.GetAttribute("RateAssignCurrencyID")) & vbCrLf & "OtherIT Cost = " & Trim(xmlread.GetAttribute("OtherIT")) & " " & Trim(xmlread.GetAttribute("RateAssignCurrencyID")) & vbCrLf & "OtherITCore Cost = " & Trim(xmlread.GetAttribute("OtherITCore")) & " " & Trim(xmlread.GetAttribute("RateAssignCurrencyID")) & vbCrLf & "Adjustment = " & Adjustment, MsgBoxStyle.Information, "Entry " & HoursEntryID.ToString & " Cost Information")

                xmlread.Close()
                conn.Close()
                conn.Dispose()
            End If
        End Sub

        Public Overrides Function ButtonText(ByRef source As System.Windows.Forms.CurrencyManager, ByVal rowNum As Integer) As String
            Return "Details"
        End Function
        Public Sub New(ByRef MyDataGrid As DataGrid, ByVal ColumnID As ColumnOrder, Optional ByVal [ReadOnly] As Boolean = True)
            MyBase.New(MyDataGrid, ColumnOrder.DetailButton)
            l_ColumnID = ColumnID
            l_FilterActive = False
            l_FilterString = ""
            l_ReadOnly = [ReadOnly]
            l_Sorted = False
        End Sub
    End Class


    Public Sub SetFilter(ByVal ColumnNumber As Integer, ByVal CurrentValue As String) Implements FilterableForm.SetFilter
        Dim c As ColumnStyleInterface
        c = CType(Me.HoursDataGrid.TableStyles(0).GridColumnStyles(ColumnNumber), ColumnStyleInterface)
        If CurrentValue = "" Then
            c.FilterString = ""
        Else
            c.FilterString = Me.HoursDataGrid.TableStyles(0).GridColumnStyles(ColumnNumber).MappingName & " like '%" & Trim(CurrentValue) & "%'"
        End If
        ' for the update of the filter icon on top of the column header
        Dim r As Rectangle = Me.HoursDataGrid.GetCellBounds(Me.HoursDataGrid.CurrentCell.RowNumber, ColumnNumber)
        r.Offset(0, -r.Top)
        Me.HoursDataGrid.Invalidate(New Region(r))

        ' move cells to refresh the selection... there must be a more elegant way...
        If CurrentCell.ColumnNumber > Me.HoursDataGrid.FirstVisibleColumn + 1 Then ' at least one complete column on the left
            Me.HoursDataGrid.CurrentCell = New DataGridCell(CurrentCell.RowNumber, CurrentCell.ColumnNumber - 1)
        Else
            Me.HoursDataGrid.CurrentCell = New DataGridCell(CurrentCell.RowNumber, CurrentCell.ColumnNumber + 1)
        End If
        Me.HoursDataGrid.CurrentCell = CurrentCell

        ' before changing the display dataset, we must keep the changes if any
        ApplyFilter()
        Me.HoursDataGrid.Invalidate()
    End Sub
    Private Sub ApplyFilter()
        DataView.RowFilter = GetCurrentFilter()
        Me.RecordTracker(Nothing, Nothing)
    End Sub
    Private Sub ClearFilter()
        Dim c As ColumnStyleInterface
        For Each c In Me.HoursDataGrid.TableStyles(0).GridColumnStyles
            If c.FilterActive Then
                c.ClearFilter()
            End If
        Next
        ApplyFilter()
    End Sub
    Private Function GetCurrentFilter() As String
        Dim c As ColumnStyleInterface
        GetCurrentFilter = Nothing
        For Each c In Me.HoursDataGrid.TableStyles(0).GridColumnStyles
            If c.FilterActive Then
                If IsNothing(GetCurrentFilter) Then
                    GetCurrentFilter = c.FilterString
                Else
                    GetCurrentFilter = GetCurrentFilter & " AND " & c.FilterString
                End If
            End If
        Next
    End Function
    Private Sub InitializeDataset()
        Me.InvoicedHoursDataset.InvoiceHours.Columns.Add(New DataColumn("DisplayDetailsButton"))
    End Sub

    Friend WithEvents HoursEntryIDTextColumn As MyTextColumnClass
    Friend WithEvents ContractTextColumn As MyTextColumnClass
    Friend WithEvents AssignedLocationTextBoxColumn As MyTextColumnClass
    Friend WithEvents PayCurrencyIDTextBox As MyTextColumnClass
    Friend WithEvents DisciplineCodeTextBox As MyTextColumnClass
    Friend WithEvents DisciplineDescriptionTextBox As MyTextColumnClass
    Friend WithEvents CompanyIDTextColumn As MyTextColumnClass
    Friend WithEvents EmployeeIDTextColumn As MyTextColumnClass
    Friend WithEvents EmployeeNameTextBoxColumn As MyTextColumnClass
    Friend WithEvents YearTextBoxColumn As MyTextColumnClass
    Friend WithEvents WeekTextBoxColumn As MyTextColumnClass
    Friend WithEvents PostedDateTextBoxColumn As MyTextColumnClass
    Friend WithEvents HoursTextBoxColumn As MyTextColumnClass
    Friend WithEvents InvoicedCheckBoxColumn As MyBooleanColumnClass
    Friend WithEvents InvoiceClosedCheckBoxColumn As MyBooleanColumnClass
    Friend WithEvents LabourCostTextBoxColumn As MyTextColumnClass
    Friend WithEvents OtherOfficeSpaceTextBoxColumn As MyTextColumnClass
    Friend WithEvents OtherOfficeExpensesTextBoxColumn As MyTextColumnClass
    Friend WithEvents OtherITTextBoxColumn As MyTextColumnClass
    Friend WithEvents OtherITCoreTextBoxColumn As MyTextColumnClass
    Friend WithEvents AdjustmentTextBoxColumn As MyTextColumnClass
    Private ShowDetailsButtonColumn As ShowDetailsButtonColumnClass

    Friend WithEvents RecordNoTextBox As System.Windows.Forms.TextBox
    Friend WithEvents MyHoursEntryID As ColumnListClass.ColumnDefinitionClass

    Friend WithEvents DataView As DataView
    Private Sub InitializeDataGrid()
        MyHoursEntryID = New ColumnListClass.ColumnDefinitionClass("HoursEntryID", "TEST", 50)
        Me.HoursEntryIDTextColumn = New MyTextColumnClass(ColumnOrder.HoursEntryID)
        Me.ContractTextColumn = New MyTextColumnClass(ColumnOrder.Contract)
        Me.AssignedLocationTextBoxColumn = New MyTextColumnClass(ColumnOrder.AssignedLocation)
        Me.PayCurrencyIDTextBox = New MyTextColumnClass(ColumnOrder.PayCurrencyID)
        Me.DisciplineCodeTextBox = New MyTextColumnClass(ColumnOrder.DisciplineCode)
        Me.DisciplineDescriptionTextBox = New MyTextColumnClass(ColumnOrder.DisciplineDescription)
        Me.CompanyIDTextColumn = New MyTextColumnClass(ColumnOrder.CompanyID)
        Me.EmployeeIDTextColumn = New MyTextColumnClass(ColumnOrder.EmployeeID)
        Me.EmployeeNameTextBoxColumn = New MyTextColumnClass(ColumnOrder.EmployeeName)
        Me.YearTextBoxColumn = New MyTextColumnClass(ColumnOrder.Year)
        Me.WeekTextBoxColumn = New MyTextColumnClass(ColumnOrder.Week)
        Me.PostedDateTextBoxColumn = New MyTextColumnClass(ColumnOrder.PostedDate)
        Me.HoursTextBoxColumn = New MyTextColumnClass(ColumnOrder.Hours)
        Me.InvoicedCheckBoxColumn = New MyBooleanColumnClass(ColumnOrder.Invoiced, False)
        Me.InvoiceClosedCheckBoxColumn = New MyBooleanColumnClass(ColumnOrder.InvoiceClosed, True)
        Me.LabourCostTextBoxColumn = New MyTextColumnClass(ColumnOrder.LabourCost)
        Me.OtherOfficeSpaceTextBoxColumn = New MyTextColumnClass(ColumnOrder.OtherOfficeSpace)
        Me.OtherOfficeExpensesTextBoxColumn = New MyTextColumnClass(ColumnOrder.OtherOfficeExpenses)
        Me.OtherITTextBoxColumn = New MyTextColumnClass(ColumnOrder.OtherIT)
        Me.OtherITCoreTextBoxColumn = New MyTextColumnClass(ColumnOrder.OtherITCore)
        Me.AdjustmentTextBoxColumn = New MyTextColumnClass(ColumnOrder.Adjustment)
        Me.ShowDetailsButtonColumn = New ShowDetailsButtonColumnClass(Me.HoursDataGrid, ColumnOrder.DetailButton)

        CType(Me.HoursDataGrid, System.ComponentModel.ISupportInitialize).BeginInit()
        '
        'EntryIDTextBoxColumn
        '
        Me.HoursEntryIDTextColumn.Format = ""
        Me.HoursEntryIDTextColumn.FormatInfo = Nothing
        Me.HoursEntryIDTextColumn.HeaderText = "EntryID"
        Me.HoursEntryIDTextColumn.MappingName = Me.InvoicedHoursDataset.InvoiceHours.HoursEntryIDColumn.ColumnName
        Me.HoursEntryIDTextColumn.ReadOnly = True
        'Me.HoursEntryIDTextColumn.Width = 40
        '
        'ContractTextBoxColumn
        '
        Me.ContractTextColumn.Format = ""
        Me.ContractTextColumn.FormatInfo = Nothing
        Me.ContractTextColumn.HeaderText = "Contract"
        Me.ContractTextColumn.MappingName = Me.InvoicedHoursDataset.InvoiceHours.ContractColumn.ColumnName
        Me.ContractTextColumn.ReadOnly = True
        'Me.ContractTextColumn.Width = 20       
        '
        'AssignedLocationTextBoxColumn
        '
        Me.AssignedLocationTextBoxColumn.Format = ""
        Me.AssignedLocationTextBoxColumn.FormatInfo = Nothing
        Me.AssignedLocationTextBoxColumn.HeaderText = "Location"
        Me.AssignedLocationTextBoxColumn.MappingName = Me.InvoicedHoursDataset.InvoiceHours.AssignedLocationColumn.ColumnName
        Me.AssignedLocationTextBoxColumn.ReadOnly = True
        'Me.AssignedLocationTextBoxColumn.Width = 40
        '
        'PayCurrencyIDTextBoxColumn
        '
        Me.PayCurrencyIDTextBox.Format = ""
        Me.PayCurrencyIDTextBox.FormatInfo = Nothing
        Me.PayCurrencyIDTextBox.HeaderText = "Pay Currency"
        Me.PayCurrencyIDTextBox.MappingName = Me.InvoicedHoursDataset.InvoiceHours.PayCurrencyIDColumn.ColumnName
        Me.PayCurrencyIDTextBox.ReadOnly = True
        'Me.PayCurrencyIDTextBox.Width = 30
        '
        'DisciplineCodeTextBoxColumn
        '
        Me.DisciplineCodeTextBox.Format = ""
        Me.DisciplineCodeTextBox.FormatInfo = Nothing
        Me.DisciplineCodeTextBox.HeaderText = "DisciplineCode"
        Me.DisciplineCodeTextBox.MappingName = Me.InvoicedHoursDataset.InvoiceHours.DisciplineCodeColumn.ColumnName
        Me.DisciplineCodeTextBox.ReadOnly = True
        'Me.DisciplineCodeTextBox.Width = 30
        '
        'DisciplineDescriptionTextBoxColumn
        '
        Me.DisciplineDescriptionTextBox.Format = ""
        Me.DisciplineDescriptionTextBox.FormatInfo = Nothing
        Me.DisciplineDescriptionTextBox.HeaderText = "Discipline"
        Me.DisciplineDescriptionTextBox.MappingName = Me.InvoicedHoursDataset.InvoiceHours.DisciplineDescriptionColumn.ColumnName
        Me.DisciplineDescriptionTextBox.ReadOnly = True
        'Me.DisciplineDescriptionTextBox.Width = 60
        '
        'CompanyIDTextColumn
        '
        Me.CompanyIDTextColumn.Alignment = System.Windows.Forms.HorizontalAlignment.Center
        Me.CompanyIDTextColumn.Format = ""
        Me.CompanyIDTextColumn.FormatInfo = Nothing
        Me.CompanyIDTextColumn.HeaderText = "Company"
        Me.CompanyIDTextColumn.MappingName = Me.InvoicedHoursDataset.InvoiceHours.CompanyIDColumn.ColumnName
        Me.CompanyIDTextColumn.ReadOnly = True
        'Me.CompanyIDTextColumn.Width = 60
        '
        'EmployeeIDTextColumn
        '
        Me.EmployeeIDTextColumn.Alignment = System.Windows.Forms.HorizontalAlignment.Center
        Me.EmployeeIDTextColumn.Format = ""
        Me.EmployeeIDTextColumn.FormatInfo = Nothing
        Me.EmployeeIDTextColumn.HeaderText = "Employee ID"
        Me.EmployeeIDTextColumn.MappingName = Me.InvoicedHoursDataset.InvoiceHours.EmployeeIDColumn.ColumnName
        Me.EmployeeIDTextColumn.ReadOnly = True
        'Me.EmployeeIDTextColumn.Width = 60
        '
        'EmployeeNameTextBoxColumn
        '
        Me.EmployeeNameTextBoxColumn.Format = ""
        Me.EmployeeNameTextBoxColumn.FormatInfo = Nothing
        Me.EmployeeNameTextBoxColumn.HeaderText = "Employee Name"
        Me.EmployeeNameTextBoxColumn.MappingName = Me.InvoicedHoursDataset.InvoiceHours.EmployeeNameColumn.ColumnName
        Me.EmployeeNameTextBoxColumn.ReadOnly = True
        'Me.EmployeeNameTextBoxColumn.Width = 75
        '
        'YearTextBoxColumn
        '
        Me.YearTextBoxColumn.Format = "00"
        Me.YearTextBoxColumn.FormatInfo = Nothing
        Me.YearTextBoxColumn.HeaderText = "Year"
        Me.YearTextBoxColumn.MappingName = Me.InvoicedHoursDataset.InvoiceHours.YearColumn.ColumnName
        Me.YearTextBoxColumn.ReadOnly = True
        '
        'WeekTextBoxColumn
        '
        Me.WeekTextBoxColumn.Format = "00"
        Me.WeekTextBoxColumn.FormatInfo = Nothing
        Me.WeekTextBoxColumn.HeaderText = "Week"
        Me.WeekTextBoxColumn.MappingName = Me.InvoicedHoursDataset.InvoiceHours.WeekColumn.ColumnName
        Me.WeekTextBoxColumn.ReadOnly = True
        '
        'PostedDateTextBoxColumn
        '
        Me.PostedDateTextBoxColumn.Format = "yyyy-MM-dd"
        Me.PostedDateTextBoxColumn.FormatInfo = Nothing
        Me.PostedDateTextBoxColumn.HeaderText = "Posted Date"
        Me.PostedDateTextBoxColumn.MappingName = Me.InvoicedHoursDataset.InvoiceHours.PostedDateColumn.ColumnName
        Me.PostedDateTextBoxColumn.ReadOnly = True
        'Me.PostedDateTextBoxColumn.Width = 75
        '
        'HoursTextBoxColumn
        '
        Me.HoursTextBoxColumn.Format = ""
        Me.HoursTextBoxColumn.FormatInfo = Nothing
        Me.HoursTextBoxColumn.HeaderText = "Hours"
        Me.HoursTextBoxColumn.MappingName = Me.InvoicedHoursDataset.InvoiceHours.HoursColumn.ColumnName
        Me.HoursTextBoxColumn.ReadOnly = True
        'Me.HoursTextBoxColumn.Width = 75
        '
        'LabourCostTextBoxColumn
        '
        Me.LabourCostTextBoxColumn.Format = ""
        Me.LabourCostTextBoxColumn.FormatInfo = Nothing
        Me.LabourCostTextBoxColumn.HeaderText = "LabourCost"
        Me.LabourCostTextBoxColumn.MappingName = Me.InvoicedHoursDataset.InvoiceHours.LabourCostColumn.ColumnName
        Me.LabourCostTextBoxColumn.ReadOnly = True
        '
        'OtherOfficeSpaceTextBoxColumn
        '
        Me.OtherOfficeSpaceTextBoxColumn.Format = ""
        Me.OtherOfficeSpaceTextBoxColumn.FormatInfo = Nothing
        Me.OtherOfficeSpaceTextBoxColumn.HeaderText = "OtherOfficeSpace"
        Me.OtherOfficeSpaceTextBoxColumn.MappingName = Me.InvoicedHoursDataset.InvoiceHours.OtherOfficeSpaceColumn.ColumnName
        Me.OtherOfficeSpaceTextBoxColumn.ReadOnly = True
        '
        'OtherOfficeExpensesTextBoxColumn
        '
        Me.OtherOfficeExpensesTextBoxColumn.Format = ""
        Me.OtherOfficeExpensesTextBoxColumn.FormatInfo = Nothing
        Me.OtherOfficeExpensesTextBoxColumn.HeaderText = "OtherOfficeExpenses"
        Me.OtherOfficeExpensesTextBoxColumn.MappingName = Me.InvoicedHoursDataset.InvoiceHours.OtherOfficeExpensesColumn.ColumnName
        Me.OtherOfficeExpensesTextBoxColumn.ReadOnly = True
        '
        'OtherITTextBoxColumn
        '
        Me.OtherITTextBoxColumn.Format = ""
        Me.OtherITTextBoxColumn.FormatInfo = Nothing
        Me.OtherITTextBoxColumn.HeaderText = "OtherIT"
        Me.OtherITTextBoxColumn.MappingName = Me.InvoicedHoursDataset.InvoiceHours.OtherITColumn.ColumnName
        Me.OtherITTextBoxColumn.ReadOnly = True
        '
        'OtherITCoreTextBoxColumn
        '
        Me.OtherITCoreTextBoxColumn.Format = ""
        Me.OtherITCoreTextBoxColumn.FormatInfo = Nothing
        Me.OtherITCoreTextBoxColumn.HeaderText = "OtherITCore"
        Me.OtherITCoreTextBoxColumn.MappingName = Me.InvoicedHoursDataset.InvoiceHours.OtherITCoreColumn.ColumnName
        Me.OtherITCoreTextBoxColumn.ReadOnly = True
        '
        'AdjustmentTextBoxColumn
        '
        Me.AdjustmentTextBoxColumn.Format = ""
        Me.AdjustmentTextBoxColumn.FormatInfo = Nothing
        Me.AdjustmentTextBoxColumn.HeaderText = "Adjustment"
        Me.AdjustmentTextBoxColumn.MappingName = Me.InvoicedHoursDataset.InvoiceHours.AdjustmentColumn.ColumnName
        Me.AdjustmentTextBoxColumn.ReadOnly = True
        '
        'InvoicedCheckBoxColumn
        '
        Me.InvoicedCheckBoxColumn.HeaderText = "Included"
        Me.InvoicedCheckBoxColumn.MappingName = Me.InvoicedHoursDataset.InvoiceHours.InvoicedColumn.ColumnName
        Me.InvoicedCheckBoxColumn.Width = 20
        Me.InvoicedCheckBoxColumn.AllowNull = False
        '
        'InvoiceClosedCheckBoxColumn
        '
        Me.InvoiceClosedCheckBoxColumn.HeaderText = "Closed"
        Me.InvoiceClosedCheckBoxColumn.MappingName = Me.InvoicedHoursDataset.InvoiceHours.InvoiceClosedColumn.ColumnName
        Me.InvoiceClosedCheckBoxColumn.Width = 0
        '
        Me.ShowDetailsButtonColumn.HeaderText = "Details"
        Me.ShowDetailsButtonColumn.MappingName = "DisplayDetailsButton"
        Me.ShowDetailsButtonColumn.Width = 75
        Me.DataGridTableStyle1.GridColumnStyles.AddRange(New System.Windows.Forms.DataGridColumnStyle() _
             {Me.HoursEntryIDTextColumn, _
              Me.ContractTextColumn, _
              Me.AssignedLocationTextBoxColumn, _
              Me.PayCurrencyIDTextBox, _
              Me.DisciplineCodeTextBox, _
              Me.DisciplineDescriptionTextBox, _
              Me.CompanyIDTextColumn, _
              Me.EmployeeIDTextColumn, _
              Me.EmployeeNameTextBoxColumn, _
              Me.YearTextBoxColumn, _
              Me.WeekTextBoxColumn, _
              Me.PostedDateTextBoxColumn, _
              Me.HoursTextBoxColumn, _
              Me.LabourCostTextBoxColumn, _
              Me.OtherOfficeSpaceTextBoxColumn, _
              Me.OtherOfficeExpensesTextBoxColumn, _
              Me.OtherITTextBoxColumn, _
              Me.OtherITCoreTextBoxColumn, _
              Me.AdjustmentTextBoxColumn, _
              Me.InvoicedCheckBoxColumn, _
              Me.InvoiceClosedCheckBoxColumn, _
              Me.ShowDetailsButtonColumn})

        Me.HoursDataGrid.TableStyles.AddRange(New System.Windows.Forms.DataGridTableStyle() {Me.DataGridTableStyle1})

        DataView = New DataView(Me.InvoicedHoursDataset.InvoiceHours)
        DataView.AllowDelete = False
        DataView.AllowEdit = True
        DataView.AllowNew = False
        DataView.RowStateFilter = DataViewRowState.CurrentRows

        Me.HoursDataGrid.DataSource = DataView
        Me.HoursDataGrid.TableStyles.Item(0).MappingName = Me.InvoicedHoursDataset.InvoiceHours.TableName
        CType(Me.HoursDataGrid, System.ComponentModel.ISupportInitialize).EndInit()
        Me.YearWeekTableAdapter = New NewLookupDatasetTableAdapters.YearWeekTableAdapter()
    End Sub
#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()
        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'Form overrides dispose to clean up the component list.
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
    Friend Shadows WithEvents CancelButton As System.Windows.Forms.Button
    Friend WithEvents SaveButton As System.Windows.Forms.Button
    Friend WithEvents SqlSelectCommand1 As System.Data.SqlClient.SqlCommand
    Friend WithEvents DataGridTableStyle1 As System.Windows.Forms.DataGridTableStyle
    Friend WithEvents HoursDataGrid As System.Windows.Forms.DataGrid
    Friend WithEvents BillingMonthComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents SelectAllButton As System.Windows.Forms.Button
    Friend WithEvents SelectNoneButton As System.Windows.Forms.Button
    Friend WithEvents RecalcButton As System.Windows.Forms.Button
    Friend WithEvents CloseInvoiceButton As System.Windows.Forms.Button
    Friend WithEvents InvoicedHoursDataset As PAF.InvoicedHours
    Friend WithEvents InvoicedHoursCostsDataset As PAF.InvoicedHoursCosts
    Friend WithEvents MyContextualMenu As myContextualMenu
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.CancelButton = New System.Windows.Forms.Button()
        Me.SaveButton = New System.Windows.Forms.Button()
        Me.HoursDataGrid = New System.Windows.Forms.DataGrid()
        Me.InvoicedHoursDataset = New PAF.InvoicedHours()
        Me.DataGridTableStyle1 = New System.Windows.Forms.DataGridTableStyle()
        Me.SqlSelectCommand1 = New System.Data.SqlClient.SqlCommand()
        Me.RecordNoTextBox = New System.Windows.Forms.TextBox()
        Me.BillingMonthComboBox = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.SelectAllButton = New System.Windows.Forms.Button()
        Me.SelectNoneButton = New System.Windows.Forms.Button()
        Me.RecalcButton = New System.Windows.Forms.Button()
        Me.CloseInvoiceButton = New System.Windows.Forms.Button()
        Me.InvoicedHoursCostsDataset = New PAF.InvoicedHoursCosts()
        Me.ClearFilterButton = New System.Windows.Forms.Button()
        Me.InvoiceHoursTableAdapter = New PAF.InvoicedHoursTableAdapters.InvoiceHoursTableAdapter()
        CType(Me.HoursDataGrid, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.InvoicedHoursDataset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.InvoicedHoursCostsDataset, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'CancelButton
        '
        Me.CancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.CancelButton.Location = New System.Drawing.Point(504, 280)
        Me.CancelButton.Name = "CancelButton"
        Me.CancelButton.Size = New System.Drawing.Size(48, 24)
        Me.CancelButton.TabIndex = 34
        Me.CancelButton.Text = "Cancel"
        '
        'SaveButton
        '
        Me.SaveButton.Location = New System.Drawing.Point(448, 280)
        Me.SaveButton.Name = "SaveButton"
        Me.SaveButton.Size = New System.Drawing.Size(48, 24)
        Me.SaveButton.TabIndex = 33
        Me.SaveButton.Text = "Save"
        '
        'HoursDataGrid
        '
        Me.HoursDataGrid.CaptionVisible = False
        Me.HoursDataGrid.DataMember = "InvoiceHours"
        Me.HoursDataGrid.DataSource = Me.InvoicedHoursDataset
        Me.HoursDataGrid.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.HoursDataGrid.Location = New System.Drawing.Point(12, 35)
        Me.HoursDataGrid.Name = "HoursDataGrid"
        Me.HoursDataGrid.Size = New System.Drawing.Size(540, 200)
        Me.HoursDataGrid.TabIndex = 38
        '
        'InvoicedHoursDataset
        '
        Me.InvoicedHoursDataset.DataSetName = "InvoicedHours"
        Me.InvoicedHoursDataset.Locale = New System.Globalization.CultureInfo("en-US")
        Me.InvoicedHoursDataset.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'DataGridTableStyle1
        '
        Me.DataGridTableStyle1.DataGrid = Me.HoursDataGrid
        Me.DataGridTableStyle1.HeaderForeColor = System.Drawing.SystemColors.ControlText
        '
        'RecordNoTextBox
        '
        Me.RecordNoTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.RecordNoTextBox.Location = New System.Drawing.Point(408, 248)
        Me.RecordNoTextBox.Name = "RecordNoTextBox"
        Me.RecordNoTextBox.ReadOnly = True
        Me.RecordNoTextBox.Size = New System.Drawing.Size(144, 20)
        Me.RecordNoTextBox.TabIndex = 39
        Me.RecordNoTextBox.TabStop = False
        Me.RecordNoTextBox.Text = "RecordNo"
        '
        'BillingMonthComboBox
        '
        Me.BillingMonthComboBox.Location = New System.Drawing.Point(88, 8)
        Me.BillingMonthComboBox.Name = "BillingMonthComboBox"
        Me.BillingMonthComboBox.Size = New System.Drawing.Size(128, 21)
        Me.BillingMonthComboBox.Sorted = True
        Me.BillingMonthComboBox.TabIndex = 40
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(16, 11)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(72, 16)
        Me.Label1.TabIndex = 41
        Me.Label1.Text = "Billing Month"
        '
        'SelectAllButton
        '
        Me.SelectAllButton.Location = New System.Drawing.Point(320, 248)
        Me.SelectAllButton.Name = "SelectAllButton"
        Me.SelectAllButton.Size = New System.Drawing.Size(80, 25)
        Me.SelectAllButton.TabIndex = 42
        Me.SelectAllButton.Text = "Select ALL"
        '
        'SelectNoneButton
        '
        Me.SelectNoneButton.Location = New System.Drawing.Point(240, 248)
        Me.SelectNoneButton.Name = "SelectNoneButton"
        Me.SelectNoneButton.Size = New System.Drawing.Size(80, 25)
        Me.SelectNoneButton.TabIndex = 43
        Me.SelectNoneButton.Text = "Unselect ALL"
        '
        'RecalcButton
        '
        Me.RecalcButton.Location = New System.Drawing.Point(224, 8)
        Me.RecalcButton.Name = "RecalcButton"
        Me.RecalcButton.Size = New System.Drawing.Size(128, 24)
        Me.RecalcButton.TabIndex = 44
        Me.RecalcButton.Text = "Recalculate Invoice"
        '
        'CloseInvoiceButton
        '
        Me.CloseInvoiceButton.Location = New System.Drawing.Point(360, 8)
        Me.CloseInvoiceButton.Name = "CloseInvoiceButton"
        Me.CloseInvoiceButton.Size = New System.Drawing.Size(128, 24)
        Me.CloseInvoiceButton.TabIndex = 45
        Me.CloseInvoiceButton.Text = "Close Invoice"
        '
        'InvoicedHoursCostsDataset
        '
        Me.InvoicedHoursCostsDataset.DataSetName = "InvoicedHoursCosts"
        Me.InvoicedHoursCostsDataset.Locale = New System.Globalization.CultureInfo("en-US")
        Me.InvoicedHoursCostsDataset.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'ClearFilterButton
        '
        Me.ClearFilterButton.Location = New System.Drawing.Point(8, 248)
        Me.ClearFilterButton.Name = "ClearFilterButton"
        Me.ClearFilterButton.Size = New System.Drawing.Size(80, 25)
        Me.ClearFilterButton.TabIndex = 46
        Me.ClearFilterButton.Text = "Clear Filter"
        '
        'InvoiceHoursTableAdapter
        '
        Me.InvoiceHoursTableAdapter.ClearBeforeFill = True
        '
        'InvoiceEditionForm
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(572, 324)
        Me.Controls.Add(Me.ClearFilterButton)
        Me.Controls.Add(Me.RecordNoTextBox)
        Me.Controls.Add(Me.CloseInvoiceButton)
        Me.Controls.Add(Me.RecalcButton)
        Me.Controls.Add(Me.SelectNoneButton)
        Me.Controls.Add(Me.SelectAllButton)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.BillingMonthComboBox)
        Me.Controls.Add(Me.HoursDataGrid)
        Me.Controls.Add(Me.CancelButton)
        Me.Controls.Add(Me.SaveButton)
        Me.MinimumSize = New System.Drawing.Size(568, 340)
        Me.Name = "InvoiceEditionForm"
        Me.Text = "Invoice content selection"
        CType(Me.HoursDataGrid, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.InvoicedHoursDataset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.InvoicedHoursCostsDataset, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region
    Private Sub RightClickHandler(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles HoursDataGrid.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Right Then
            Dim hit As DataGrid.HitTestInfo = HoursDataGrid.HitTest(e.X, e.Y)
            If hit.Type <> DataGrid.HitTestType.Cell Then
                Return
            End If
            If HoursDataGrid.CurrentCell.RowNumber <> hit.Row Or _
               HoursDataGrid.CurrentCell.ColumnNumber <> hit.Column Then
                HoursDataGrid.CurrentCell = New DataGridCell(hit.Row, hit.Column)
                MyContextualMenu.Show(Me.HoursDataGrid, Me.MousePosition)
            End If
            'Diagnostics.Debugger.Break()
        End If
    End Sub
    Private Sub SortClickHandler(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles HoursDataGrid.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Left Then
            Dim hit As DataGrid.HitTestInfo = HoursDataGrid.HitTest(e.X, e.Y)
            If hit.Type <> DataGrid.HitTestType.ColumnHeader Then
                Return
            End If
            CType(Me.HoursDataGrid.TableStyles(0).GridColumnStyles(hit.Column), ColumnStyleInterface).Sorted = True
        End If
    End Sub
    Private Sub PaintEventhandler(ByVal sender As System.Object, ByVal p As PaintEventArgs) 'Handles HoursDataGrid.Paint
        ' to keep if we need to display an icon when a filter is active in a specific column
        If p.ClipRectangle.Top < 10 And p.ClipRectangle.Bottom > 10 Then
            'Diagnostics.Debugger.Break()
            Dim b As Brush = Brushes.Red
            Dim c As Windows.Forms.DataGridColumnStyle
            Dim w As Integer = 0
            Dim MyIcon As Drawing.Icon
            MyIcon = New Icon("..\FilterActive.ico")
            Dim index As ColumnOrder
            index = ColumnOrder.HoursEntryID
            Dim Bounds As Rectangle
            For Each c In HoursDataGrid.TableStyles.Item(0).GridColumnStyles
                If CType(c, ColumnStyleInterface).FilterActive Then
                    Bounds = Me.HoursDataGrid.GetCellBounds(Me.HoursDataGrid.CurrentCell.RowNumber, index)
                    w = Bounds.Right - MyIcon.Width
                    If CType(c, ColumnStyleInterface).Sorted Then
                        w -= 12
                    End If
                    If w > Bounds.Left Then
                        p.Graphics.DrawIcon(MyIcon, w, 6)
                    End If
                End If
                index = CType(index + 1, ColumnOrder)
            Next
        End If
    End Sub
    Private Sub FillBillingMonth()
        Me.BillingMonthComboBox.DataSource = Me.YearWeekTableAdapter.GetData()
        Me.BillingMonthComboBox.DisplayMember = "StartMonth"
        Me.BillingMonthComboBox.ValueMember = "StartMonth"
        Me.BillingMonthComboBox.SelectedIndex = Me.BillingMonthComboBox.Items.Count - 1
    End Sub
    Private Sub InvoiceEditionForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        PrivilegedSQLConnection = GetSqlConnection()
        EnableRole(PrivilegedSQLConnection)

        InitializeDataset()
        InitializeDataGrid()
        Me.SqlSelectCommand1.Connection = PrivilegedSQLConnection ' patch for the init component before we have a privileged connection

        FillBillingMonth()
        Try
            Me.BillingMonth_SelectionChange(Nothing, Nothing)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Application Error")
            Me.Close()
        End Try
        ApplyFilter()

        Me.HoursDataGrid.CurrentCell = New DataGridCell(0, ColumnOrder.HoursEntryID)   ' position to the first row/wp

        'Me.InvoicedHoursDataset.
        'Me.HoursDataGrid.DataSource = Me.InvoicesHoursTableAdapter

        MyContextualMenu = New myContextualMenu(Me.HoursDataGrid, Me)
        Me.CompanyIDTextColumn.TextBox.ContextMenu = MyContextualMenu
        Me.ContractTextColumn.TextBox.ContextMenu = MyContextualMenu
        Me.DisciplineCodeTextBox.TextBox.ContextMenu = MyContextualMenu
        Me.DisciplineDescriptionTextBox.TextBox.ContextMenu = MyContextualMenu
        Me.EmployeeIDTextColumn.TextBox.ContextMenu = MyContextualMenu
        Me.EmployeeNameTextBoxColumn.TextBox.ContextMenu = MyContextualMenu
        Me.PostedDateTextBoxColumn.TextBox.ContextMenu = MyContextualMenu
        Me.YearTextBoxColumn.TextBox.ContextMenu = MyContextualMenu
        Me.WeekTextBoxColumn.TextBox.ContextMenu = MyContextualMenu
        Me.HoursTextBoxColumn.TextBox.ContextMenu = MyContextualMenu
        Me.LabourCostTextBoxColumn.TextBox.ContextMenu = MyContextualMenu
        Me.OtherOfficeSpaceTextBoxColumn.TextBox.ContextMenu = MyContextualMenu
        Me.OtherOfficeExpensesTextBoxColumn.TextBox.ContextMenu = MyContextualMenu
        Me.OtherITTextBoxColumn.TextBox.ContextMenu = MyContextualMenu
        Me.OtherITCoreTextBoxColumn.TextBox.ContextMenu = MyContextualMenu
        Me.AdjustmentTextBoxColumn.TextBox.ContextMenu = MyContextualMenu
        Me.PayCurrencyIDTextBox.TextBox.ContextMenu = MyContextualMenu
        Me.AssignedLocationTextBoxColumn.TextBox.ContextMenu = MyContextualMenu

        AutoSizeTable()
        InitializeStatusBar()
    End Sub
    Private Sub InvoiceEditionForm_Close(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Closed
        MyInvoiceEditionForm = Nothing
    End Sub
    Private PreviousCell As DataGridCell
    Private Sub RecordTracker(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HoursDataGrid.CurrentCellChanged
        Dim p As Integer = Me.HoursDataGrid.CurrentCell().RowNumber
        Dim c As Integer = DataView.Count
        If Not PreviousCell.Equals(Me.HoursDataGrid.CurrentCell()) Then
            PreviousCell = Me.HoursDataGrid.CurrentCell()
        End If
        If IsNothing(GetCurrentFilter) Then
            Me.RecordNoTextBox.Text = "Record " & p + 1 & " of " & c
            ' done in Enable buttons below, to handle the case when there is a change in the invoice
            'Me.CloseInvoiceButton.Enabled = Not CType(InvoicedHoursDataset.InvoiceHours.Rows(0), InvoicedHours.InvoiceHoursRow).InvoiceClosed
            'Me.RecalcButton.Enabled = Not CType(InvoicedHoursDataset.InvoiceHours.Rows(0), InvoicedHours.InvoiceHoursRow).InvoiceClosed
        Else
            Me.RecordNoTextBox.Text = "Record " & p + 1 & " of " & c & " (Filtered)"
            'Me.CloseInvoiceButton.Enabled = False
            'Me.RecalcButton.Enabled = False
        End If

        EnableButtons(IsNothing(InvoicedHoursDataset.InvoiceHours.GetChanges(DataRowState.Modified)))
    End Sub
    Private Sub ResizeHandler(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.SizeChanged
        Me.SaveButton.Location = New Point(Me.ClientSize.Width - Me.SaveButton.Width - Me.CancelButton.Width - 10, (Me.ClientSize.Height - Me.SaveButton.Height - 5) - 15)
        Me.CancelButton.Location = New Point(Me.ClientSize.Width - Me.CancelButton.Width - 5, (Me.ClientSize.Height - Me.CancelButton.Height - 5) - 15)
        Me.HoursDataGrid.Height = Me.ClientSize.Height - 120 ' 8 * 3 = height at top + before button + after the button
        Me.HoursDataGrid.Width = Me.ClientSize.Width - 16
        Me.RecordNoTextBox.Location = New Point(Me.ClientSize.Width - Me.RecordNoTextBox.Width - 8, Me.HoursDataGrid.Top + Me.HoursDataGrid.Height + 5)
        Me.SelectAllButton.Top = Me.RecordNoTextBox.Top
        Me.SelectAllButton.Left = Me.RecordNoTextBox.Left - Me.SelectAllButton.Width - 10
        Me.SelectNoneButton.Top = Me.RecordNoTextBox.Top
        Me.SelectNoneButton.Left = Me.RecordNoTextBox.Left - Me.SelectAllButton.Width - Me.SelectNoneButton.Width - 10
        Me.ClearFilterButton.Top = Me.RecordNoTextBox.Top
    End Sub

    Private Sub EnableRole(ByRef C As SqlClient.SqlConnection)
        Try
            If C.State <> ConnectionState.Open Then
                C.Open()
                Dim cmd As New SqlClient.SqlCommand("sp_setapprole PAF_INVOICE_EDITION_APPLICATION, 'paf'", C)
                cmd.ExecuteNonQuery()
            Else
                Dim cmd As New SqlClient.SqlCommand("sp_setapprole PAF_INVOICE_EDITION_APPLICATION, 'paf'", C)
                cmd.ExecuteNonQuery()
            End If

        Catch ex As SqlClient.SqlException
            If ex.Number <> 2762 Then ' already enabled
                MsgBox(ex.Message, MsgBoxStyle.Critical, "SQL Error")
            End If
        End Try
    End Sub
    Private Sub SaveButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveButton.Click
        If Me.InvoicedHoursDataset.HasChanges(DataRowState.Modified) Then
            Dim i As Integer
            Dim row As InvoicedHours.InvoiceHoursRow
            Dim AddToInvoiceSQLCommand As New SqlClient.SqlCommand
            Dim RemoveFromInvoiceSQLCommand As New SqlClient.SqlCommand
            AddToInvoiceSQLCommand.Connection = SQLConnection
            AddToInvoiceSQLCommand.CommandType = CommandType.StoredProcedure
            AddToInvoiceSQLCommand.CommandText = "AddHoursToInvoice"
            AddToInvoiceSQLCommand.Parameters.AddWithValue("@BillingMonth", CurrentBillingMonthSelection)
            AddToInvoiceSQLCommand.Parameters.AddWithValue("@HoursEntryID", "")

            RemoveFromInvoiceSQLCommand.Connection = SQLConnection
            RemoveFromInvoiceSQLCommand.CommandType = CommandType.StoredProcedure
            RemoveFromInvoiceSQLCommand.CommandText = "RemoveHoursFromInvoice"
            RemoveFromInvoiceSQLCommand.Parameters.AddWithValue("@BillingMonth", CurrentBillingMonthSelection)
            RemoveFromInvoiceSQLCommand.Parameters.AddWithValue("@HoursEntryID", "")

            i = 1

            Me.Cursor = Cursors.WaitCursor
            Try
                For Each row In InvoicedHoursDataset.InvoiceHours.GetChanges(DataRowState.Modified).Rows
                    info.Text = "Processing"
                    If row.Invoiced Then
                        AddToInvoiceSQLCommand.Parameters.Item("@HoursEntryID").Value = row.HoursEntryID
                        AddToInvoiceSQLCommand.ExecuteNonQuery()
                    Else
                        RemoveFromInvoiceSQLCommand.Parameters.Item("@HoursEntryID").Value = row.HoursEntryID
                        RemoveFromInvoiceSQLCommand.ExecuteNonQuery()
                    End If
                    StatusBar.progressBar.Value = (i / InvoicedHoursDataset.InvoiceHours.GetChanges(DataRowState.Modified).Rows.Count) * 100
                    i = i + 1
                Next
                InvoicedHoursDataset.InvoiceHours.AcceptChanges()
                Me.EnableButtons(True)
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "Error!")
                info.Text = "Ready"
            End Try
            Me.Cursor = Cursors.Arrow
            Thread.Sleep(300)
            StatusBar.progressBar.Value = 0
            info.Text = "Ready"
        End If
    End Sub
    Private Sub CancelButton_Click(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        If Me.InvoicedHoursDataset.HasChanges(DataRowState.Modified) Then
            If MsgBox("Do you want to cancel your changes?", MsgBoxStyle.YesNo, "Please confirm") = MsgBoxResult.Yes Then
                Me.InvoicedHoursDataset.RejectChanges()
                e.Cancel = False
            Else
                e.Cancel = True
            End If
        End If
    End Sub
    Private CurrentBillingMonthSelection As String
    Private Sub BillingMonth_SelectionChange(ByVal sender As Object, ByVal e As EventArgs) Handles BillingMonthComboBox.SelectionChangeCommitted
        Dim i As Integer
        Dim Year As Integer
        Dim Month As Integer
        Dim Day As Integer
        Dim s As String = CType(Me.BillingMonthComboBox.SelectedValue, String)
        If IsNothing(CurrentBillingMonthSelection) Then
            CurrentBillingMonthSelection = s
        Else
            If s = CurrentBillingMonthSelection Then
                Exit Sub
            End If
        End If
        ' we are changing selection: verify if there is changes to save
        If Not IsNothing(InvoicedHoursDataset.InvoiceHours.GetChanges(DataRowState.Modified)) Then
            Dim a As MsgBoxResult
            a = MsgBox("Do you want to save changes before selecting another billing month?", MsgBoxStyle.YesNoCancel, "Warning")
            If a = MsgBoxResult.Cancel Then
                Me.BillingMonthComboBox.SelectedItem = CurrentBillingMonthSelection
                Exit Sub
            End If
            If a = MsgBoxResult.Yes Then
                Me.SaveButton_Click(Nothing, Nothing)
            End If
        End If
        CurrentBillingMonthSelection = s
        Dim sArray As String() = s.Split(CType("-", Char))
        Year = Integer.Parse(sArray(0))
        Month = Integer.Parse(sArray(1))
        Day = Integer.Parse(sArray(2))

        Me.InvoicesHoursTableAdapter = New InvoicedHoursTableAdapters.InvoiceHoursTableAdapter()
        Me.HoursDataGrid.SuspendLayout()
        InvoicedHoursDataset.InvoiceHours.Clear()
        i = InvoicesHoursTableAdapter.Fill(InvoicedHoursDataset.InvoiceHours, New Date(Year, Month, Day))
        If i > 0 Then
            Me.HoursDataGrid.CurrentCell = New DataGridCell(1, Me.HoursDataGrid.CurrentCell.ColumnNumber)
            Me.RecalcButton.Enabled = Not CType(InvoicedHoursDataset.InvoiceHours.Rows(0), InvoicedHours.InvoiceHoursRow).InvoiceClosed
            Me.SaveButton.Enabled = Not CType(InvoicedHoursDataset.InvoiceHours.Rows(0), InvoicedHours.InvoiceHoursRow).InvoiceClosed
        End If
        Me.HoursDataGrid.ResumeLayout()

        AutoSizeTable()
    End Sub ' BillingMonth_SelectionChange

    Private Sub SelectNoneButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SelectNoneButton.Click
        Dim row As DataRowView
        For Each row In CType(HoursDataGrid.DataSource, DataView)
            CType(row.Row, InvoicedHours.InvoiceHoursRow).Invoiced = 0
        Next
        EnableButtons(IsNothing(InvoicedHoursDataset.InvoiceHours.GetChanges(DataRowState.Modified)))
    End Sub
    Private Sub SelectAllButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SelectAllButton.Click
        Dim row As DataRowView
        For Each row In CType(HoursDataGrid.DataSource, DataView)
            CType(row.Row, InvoicedHours.InvoiceHoursRow).Invoiced = 1
        Next
        EnableButtons(IsNothing(InvoicedHoursDataset.InvoiceHours.GetChanges(DataRowState.Modified)))
    End Sub
    Private Sub RecalcButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RecalcButton.Click
        Dim i As Integer
        Dim row As InvoicedHours.InvoiceHoursRow
        Dim EntryID As String = "Before corsor"
        Dim sqlcommand As New SqlClient.SqlCommand
        sqlcommand.Connection = PrivilegedSQLConnection
        sqlcommand.CommandType = CommandType.StoredProcedure
        sqlcommand.CommandText = "RecalcHours"
        sqlcommand.Parameters.AddWithValue("@BillingMonth", Me.BillingMonthComboBox.SelectedValue)
        sqlcommand.Parameters.AddWithValue("@HoursEntryID", "")
        Me.Cursor = Cursors.WaitCursor
        i = 1
        Try
            For Each row In InvoicedHoursDataset.InvoiceHours
                info.Text = "Processing"
                EntryID = row.HoursEntryID
                If row.Invoiced Then
                    ' call the stored procedure to recalculate
                    sqlcommand.Parameters.Item("@HoursEntryID").Value = row.HoursEntryID
                    sqlcommand.ExecuteNonQuery()
                End If
                StatusBar.progressBar.Value = (i / InvoicedHoursDataset.InvoiceHours.Count) * 100
                i = i + 1
            Next
        Catch ex As Exception
            MsgBox(ex.Message & vbCrLf & " Entry ID = " & EntryID, MsgBoxStyle.Critical, "Error!")
            info.Text = "Ready"
        End Try
        Me.Cursor = Cursors.Arrow
        Thread.Sleep(300)
        StatusBar.progressBar.Value = 0
        info.Text = "Ready"
    End Sub
    Private Sub CloseInvoiceButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CloseInvoiceButton.Click
        RecalcButton_Click(sender, e)
        Dim i As Integer
        Dim sqlcommand As New SqlClient.SqlCommand

        sqlcommand.Connection = PrivilegedSQLConnection
        sqlcommand.CommandType = CommandType.StoredProcedure
        sqlcommand.CommandText = "CloseInvoice"
        sqlcommand.Parameters.AddWithValue("@BillingMonth", Me.BillingMonthComboBox.SelectedValue)
        Try
            sqlcommand.ExecuteNonQuery()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error!")
        End Try
        Me.HoursDataGrid.SuspendLayout()
        InvoicedHoursDataset.InvoiceHours.Clear()
        i = InvoicesHoursTableAdapter.Fill(InvoicedHoursDataset.InvoiceHours, CDate(Me.BillingMonthComboBox.SelectedValue))
        Me.HoursDataGrid.ReadOnly = True
        Me.HoursDataGrid.ResumeLayout()
    End Sub
    Private Sub EnableButtons(ByVal NoChanges As Boolean)
        If InvoicedHoursDataset.InvoiceHours.Rows.Count = 0 Then
            Me.CloseInvoiceButton.Enabled = False
            Me.RecalcButton.Enabled = False
            Me.SaveButton.Enabled = False
            Me.SelectNoneButton.Enabled = False
            Me.SelectAllButton.Enabled = False
            Exit Sub
        End If
        If Not CType(InvoicedHoursDataset.InvoiceHours.Rows(0), InvoicedHours.InvoiceHoursRow).InvoiceClosed Then
            Me.CloseInvoiceButton.Enabled = NoChanges
            Me.RecalcButton.Enabled = NoChanges
            Me.SelectNoneButton.Enabled = True
            Me.SelectAllButton.Enabled = True
            Me.HoursDataGrid.ReadOnly = False
        Else
            Me.CloseInvoiceButton.Enabled = False
            Me.RecalcButton.Enabled = False
            Me.SelectNoneButton.Enabled = False
            Me.SelectAllButton.Enabled = False
            Me.HoursDataGrid.ReadOnly = True
        End If
    End Sub
    Private Sub MouseClickHandler(ByVal Sender As Object, ByVal e As System.EventArgs) Handles HoursDataGrid.Click       
        Dim HitTestInfo As DataGrid.HitTestInfo = HoursDataGrid.HitTest(Me.HoursDataGrid.PointToClient(Control.MousePosition))
        If Me.HoursDataGrid.ReadOnly Then
            Exit Sub
        End If        
        If HitTestInfo.Type = DataGrid.HitTestType.Cell AndAlso HitTestInfo.Column = ColumnOrder.Invoiced AndAlso HitTestInfo.Row < CType(HoursDataGrid.DataSource, DataView).Count Then
            Me.HoursDataGrid(HitTestInfo.Row, HitTestInfo.Column) = Not CType(Me.HoursDataGrid(HitTestInfo.Row, HitTestInfo.Column), Boolean)
        End If
    End Sub

    Public Sub AutoSizeCol(ByVal col As Integer)
        Dim width As Single
        width = 0
        Dim numRows As Integer
        numRows = CType(HoursDataGrid.DataSource, DataView).Count
        Dim g As Graphics
        g = Graphics.FromHwnd(Me.HoursDataGrid.Handle)
        Dim sf As StringFormat
        sf = New StringFormat(StringFormat.GenericTypographic)
        Dim size As SizeF
        Dim OriginalWidth As SizeF
        Dim i As Integer
        i = 0

        OriginalWidth = g.MeasureString(Me.HoursDataGrid.TableStyles(0).GridColumnStyles(col).HeaderText.ToString, Me.HoursDataGrid.Font, 500, sf)

        Do While (i < numRows)
            size = g.MeasureString(Me.HoursDataGrid(i, col).ToString, Me.HoursDataGrid.Font, 500, sf)
            If OriginalWidth.Width < size.Width Then
                If (size.Width > width) Then
                    width = size.Width
                End If
            End If
            i = (i + 1)
        Loop

        If width = 0 Then
            width = OriginalWidth.Width
        End If
        g.Dispose()
        width = width + 10
        Me.HoursDataGrid.TableStyles(0).GridColumnStyles(col).Width = CType(width, Integer)
    End Sub

    Public Sub AutoSizeTable()
        Dim numCols As Integer
        numCols = CType(HoursDataGrid.DataSource, DataView).Table.Columns.Count

        Dim i As Integer
        i = 0

        Do While (i < numCols)
            AutoSizeCol(i)
            i = (i + 1)
        Loop
    End Sub

    Private Sub InitializeStatusBar()
        Dim progress As New System.Windows.Forms.StatusBarPanel

        info.Text = "Ready"
        info.Width = 100

        progress.AutoSize = _
          System.Windows.Forms.StatusBarPanelAutoSize.Spring

        With StatusBar
            .Panels.Add(info)
            .Panels.Add(progress)
            .ShowPanels = True
            .setProgressBar = 1
            .progressBar.Minimum = 0
            .progressBar.Maximum = 100
            .Height = 18
        End With

        Me.Controls.Add(StatusBar)
    End Sub

    Private Sub ClearFilterButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ClearFilterButton.Click
        Me.ClearFilter()
    End Sub
End Class
Public Class DataGridButtonColumnClass
    Inherits System.Windows.Forms.DataGridColumnStyle
    Public Delegate Function ButtonTextFunction(ByRef source As System.Windows.Forms.CurrencyManager, ByVal rowNum As Integer) As String

    Private ThisRowIsDown As Integer = -1
    Private ThisColumnIsDown As Integer = -1
    Friend MyDataGrid As DataGrid
    Private OnClick As EventHandler
    Private ButtonText As ButtonTextFunction
    Public Sub New(ByRef pMyDataGrid As DataGrid, ByRef pOnClick As EventHandler, ByRef pButtonText As ButtonTextFunction)
        MyBase.New()
        MyDataGrid = pMyDataGrid
        OnClick = pOnClick
        ButtonText = pButtonText
    End Sub

    Protected Overrides Sub SetDataGridInColumn(ByVal Value As DataGrid)
        MyBase.SetDataGridInColumn(Value)
        AddHandler Me.DataGridTableStyle.DataGrid.MouseDown, AddressOf MouseDown
        AddHandler Me.DataGridTableStyle.DataGrid.MouseUp, AddressOf MouseUp
        MyDataGrid = Value
    End Sub
    Private Sub MouseUp(ByVal Sender As Object, ByVal e As MouseEventArgs)
        Dim HitTestInfo As DataGrid.HitTestInfo = Me.DataGridTableStyle.DataGrid.HitTest(New Point(e.X, e.Y))
        Dim BindingContext As BindingManagerBase = Me.DataGridTableStyle.DataGrid.BindingContext(DataGridTableStyle.DataGrid.DataSource, DataGridTableStyle.DataGrid.DataMember)
        If HitTestInfo.Type = DataGrid.HitTestType.Cell AndAlso HitTestInfo.Column = Me.MyDataGrid.CurrentCell.ColumnNumber AndAlso HitTestInfo.Row < BindingContext.Count Then
            ThisRowIsDown = -1
            ThisColumnIsDown = -1
            Me.Invalidate()
            OnClick(Sender, e)
        End If
    End Sub
    Private Sub MouseDown(ByVal Sender As Object, ByVal e As MouseEventArgs)
        Dim HitTestInfo As DataGrid.HitTestInfo = Me.DataGridTableStyle.DataGrid.HitTest(New Point(e.X, e.Y))
        ThisRowIsDown = HitTestInfo.Row
        ThisColumnIsDown = HitTestInfo.Column
        Dim BindingContext As BindingManagerBase = Me.DataGridTableStyle.DataGrid.BindingContext(DataGridTableStyle.DataGrid.DataSource, DataGridTableStyle.DataGrid.DataMember)
        If HitTestInfo.Type = DataGrid.HitTestType.Cell AndAlso HitTestInfo.Column = Me.MyDataGrid.CurrentCell.ColumnNumber AndAlso HitTestInfo.Row < BindingContext.Count Then
            Me.Invalidate()
        End If
    End Sub

    Protected Overrides Sub Abort(ByVal rowNum As Integer)
        Me.Invalidate()
    End Sub
    Protected Overrides Function Commit(ByVal dataSource As System.Windows.Forms.CurrencyManager, ByVal rowNum As Integer) As Boolean
        Me.Invalidate()
        Return True
    End Function
    Protected Overloads Overrides Sub Edit(ByVal source As System.Windows.Forms.CurrencyManager, ByVal rowNum As Integer, ByVal bounds As System.Drawing.Rectangle, ByVal [readOnly] As Boolean, ByVal instantText As String, ByVal cellIsVisible As Boolean)
        Me.Invalidate()
    End Sub
    Protected Overrides Function GetMinimumHeight() As Integer
        Return 0
    End Function
    Protected Overrides Function GetPreferredHeight(ByVal g As System.Drawing.Graphics, ByVal value As Object) As Integer
        Return 10
    End Function
    Protected Overrides Function GetPreferredSize(ByVal g As System.Drawing.Graphics, ByVal value As Object) As System.Drawing.Size
        GetPreferredSize.Height = GetPreferredHeight(g, value)
        GetPreferredSize.Width = 10
    End Function
    Protected Overloads Overrides Sub Paint(ByVal g As System.Drawing.Graphics, ByVal bounds As System.Drawing.Rectangle, ByVal source As System.Windows.Forms.CurrencyManager, ByVal rowNum As Integer)
        If rowNum = ThisRowIsDown And Me.MyDataGrid.CurrentCell.ColumnNumber = ThisColumnIsDown Then
            ControlPaint.DrawButton(g, bounds, ButtonState.Pushed)
        Else
            ControlPaint.DrawButton(g, bounds, ButtonState.Normal)
        End If
        g.DrawString(ButtonText(source, rowNum), Me.DataGridTableStyle.DataGrid.Font, Brushes.Black, bounds.Left, bounds.Top)
    End Sub
    Protected Overloads Overrides Sub Paint(ByVal g As System.Drawing.Graphics, ByVal bounds As System.Drawing.Rectangle, ByVal source As System.Windows.Forms.CurrencyManager, ByVal rowNum As Integer, ByVal alignToRight As Boolean)
        Paint(g, bounds, source, rowNum)
    End Sub
End Class

' maybe usefull, to store all column related information for the grid
Public Class ColumnListClass
    Public Enum ColumnTypeEnum
        Text
        CheckBox
        Button
    End Enum
    Public Enum FilterCriteria
        Equal
        HigherOrEqual
        LowerOrEqual
    End Enum
    Public Class ColumnDefinitionClass
        Private ColumnType As ColumnTypeEnum
        Private MappingName As String
        Private ColumnHeader As String
        Public Visible As Boolean
        Public [Readonly] As Boolean
        Private Width As Integer
        Private Format As String
        Private Sorted As Boolean
        Private Filtered As Boolean
        Private FilterString As String
        Private l_Datagrid As DataGrid
        Private ButtonText As DataGridButtonColumnClass.ButtonTextFunction
        Private OnClick As EventHandler
        Public Event mousemouve()

        Public Sub SetFilter(ByVal Value As String, ByVal criteria As FilterCriteria)
            Select Case criteria
                Case FilterCriteria.Equal
                    FilterString = MappingName & " = " & Value
                Case FilterCriteria.HigherOrEqual
                    FilterString = MappingName & " >= " & Value
                Case FilterCriteria.LowerOrEqual
                    FilterString = MappingName & " <= " & Value
            End Select
            Filtered = True
        End Sub
        Public Sub ClearFilter()
            Filtered = False
            FilterString = ""
        End Sub
        Public Sub New(ByVal pMappingName As String, _
                       ByVal pColumnHeader As String, _
                       ByVal pWidth As Integer, _
                       Optional ByVal pFormat As String = Nothing)
            MappingName = pMappingName
            ColumnHeader = pColumnHeader
            If IsNothing(pFormat) Then
                Format = ""
            Else
                Format = pFormat
            End If
            Width = pWidth
            ColumnType = ColumnTypeEnum.Text
            Visible = True
        End Sub
        Public Sub New(ByVal ColumnType As ColumnTypeEnum, _
                       ByVal pWidth As Integer, _
                       ByRef pDatagrid As DataGrid, _
                       ByRef pOnClick As EventHandler, _
                       ByRef pButtonText As DataGridButtonColumnClass.ButtonTextFunction)
            If Not ColumnType = ColumnTypeEnum.Button Then
                Throw (New Exception("ColumnDefinition: Button column type must be used for this type of parameters"))
            End If
            ColumnType = ColumnTypeEnum.Button
            Width = pWidth
            OnClick = pOnClick
            ButtonText = pButtonText
            l_Datagrid = pDatagrid
        End Sub
        Public Function GridColumn() As DataGridColumnStyle
            Select Case ColumnType
                Case ColumnTypeEnum.Button
                Case ColumnTypeEnum.CheckBox
                Case ColumnTypeEnum.Text
                    Return TextBoxColumn()
                Case Else
                    Return Nothing
            End Select
        End Function
        Private Function ButtonColumn() As DataGridButtonColumnClass
            ButtonColumn = New DataGridButtonColumnClass(l_Datagrid, OnClick, ButtonText)
        End Function
        Private Function TextBoxColumn() As DataGridTextBoxColumn
            TextBoxColumn = New DataGridTextBoxColumn
            TextBoxColumn.Format = Format
            TextBoxColumn.FormatInfo = Nothing
            TextBoxColumn.HeaderText = ColumnHeader
            TextBoxColumn.MappingName = MappingName
            If Not Visible Then
                TextBoxColumn.Width = 0
            Else
                TextBoxColumn.Width = Width
            End If
        End Function
    End Class
End Class

Public Class ProgressStatus : Inherits StatusBar
    Public progressBar As New ProgressBar
    Private _progressBar As Integer = -1

    Sub New()
        progressBar.Hide()
        Me.Controls.Add(progressBar)
    End Sub

    Public Property setProgressBar() As Integer
        Get
            Return _progressBar
        End Get
        Set(ByVal Value As Integer)
            _progressBar = Value
            Me.Panels(_progressBar).Style = StatusBarPanelStyle.OwnerDraw
        End Set
    End Property

    Private Sub Reposition(ByVal sender As Object, _
         ByVal sbdevent As System.Windows.Forms.StatusBarDrawItemEventArgs) _
         Handles MyBase.DrawItem
        progressBar.Location = New Point(sbdevent.Bounds.X, _
           sbdevent.Bounds.Y)
        progressBar.Size = New Size(sbdevent.Bounds.Width, _
           sbdevent.Bounds.Height)
        progressBar.Show()
    End Sub
End Class

Public Class MyTextColumnClass
    Inherits System.Windows.Forms.DataGridTextBoxColumn
    Implements ColumnStyleInterface
    Private l_ColumnID As Integer
    Private l_FilterActive As Boolean
    Private l_FilterString As String
    Private l_ReadOnly As Boolean
    Private l_Sorted As Boolean
    Public Sub ClearFilter() Implements ColumnStyleInterface.ClearFilter
        l_FilterActive = False
        l_FilterString = ""
    End Sub

    Public ReadOnly Property FilterActive() As Boolean Implements ColumnStyleInterface.FilterActive
        Get
            Return l_FilterActive
        End Get
    End Property
    Public Property FilterString() As String Implements ColumnStyleInterface.FilterString
        Get
            Return l_FilterString
        End Get
        Set(ByVal Value As String)
            If Value.Length > 0 Then
                l_FilterActive = True
            Else
                l_FilterActive = False
            End If
            l_FilterString = Value
        End Set
    End Property
    Public ReadOnly Property IsReadOnly() As Boolean Implements ColumnStyleInterface.IsReadOnly
        Get
            Return l_ReadOnly
        End Get
    End Property
    Public Property Sorted() As Boolean Implements ColumnStyleInterface.Sorted
        Get
            Return l_Sorted
        End Get
        Set(ByVal Value As Boolean)
            l_Sorted = Value
            If l_Sorted Then
                Dim c As ColumnStyleInterface
                For Each c In Me.DataGridTableStyle.GridColumnStyles
                    If c.ColumnID <> Me.ColumnID Then
                        c.Sorted = False
                    End If
                Next
            End If
        End Set
    End Property
    Public ReadOnly Property ColumnID() As Integer Implements ColumnStyleInterface.ColumnID
        Get
            Return l_ColumnID
        End Get
    End Property
    Public Sub New(ByVal ColumnID As Integer, Optional ByVal [ReadOnly] As Boolean = True)
        MyBase.New()
        l_ColumnID = ColumnID
        l_FilterActive = False
        l_FilterString = ""
        l_ReadOnly = [ReadOnly]
        l_Sorted = False
    End Sub

    Protected Overloads Overrides Sub Paint(ByVal g As System.Drawing.Graphics, ByVal bounds As System.Drawing.Rectangle, ByVal source As System.Windows.Forms.CurrencyManager, ByVal rowNum As Integer, ByVal BackgroundBrush As Brush, ByVal ForegroundBrush As Brush, ByVal alignToRight As Boolean)
        If Not MyBase.ReadOnly Then
            MyBase.Paint(g, bounds, source, rowNum, BackgroundBrush, ForegroundBrush, False)
        Else
            Dim forebrush As Brush = Brushes.Gray
            MyBase.Paint(g, bounds, source, rowNum, BackgroundBrush, forebrush, False)
        End If
    End Sub
End Class
Public Class MyBooleanColumnClass
    Inherits System.Windows.Forms.DataGridBoolColumn
    Implements ColumnStyleInterface
    Private l_ColumnID As Integer
    Private l_FilterActive As Boolean
    Private l_FilterString As String
    Private l_ReadOnly As Boolean
    Private l_Sorted As Boolean

    Public Sub ClearFilter() Implements ColumnStyleInterface.ClearFilter
        l_FilterActive = False
        l_FilterString = ""
    End Sub
    Public ReadOnly Property FilterActive() As Boolean Implements ColumnStyleInterface.FilterActive
        Get
            Return l_FilterActive
        End Get
    End Property
    Public Property FilterString() As String Implements ColumnStyleInterface.FilterString
        Get
            Return l_FilterString
        End Get
        Set(ByVal Value As String)
            If Value.Length > 0 Then
                l_FilterActive = True
            Else
                l_FilterActive = False
            End If
            l_FilterString = Value
        End Set
    End Property
    Public ReadOnly Property IsReadOnly() As Boolean Implements ColumnStyleInterface.IsReadOnly
        Get
            Return l_ReadOnly
        End Get
    End Property
    Public Property Sorted() As Boolean Implements ColumnStyleInterface.Sorted
        Get
            Return l_Sorted
        End Get
        Set(ByVal Value As Boolean)
            l_Sorted = Value
            If l_Sorted Then
                Dim c As ColumnStyleInterface
                For Each c In Me.DataGridTableStyle.GridColumnStyles
                    If c.ColumnID <> Me.ColumnID Then
                        c.Sorted = False
                    End If
                Next
            End If
        End Set
    End Property
    Public ReadOnly Property ColumnID() As Integer Implements ColumnStyleInterface.ColumnID
        Get
            Return l_ColumnID
        End Get
    End Property
    Public Sub New(ByVal ColumnID As Integer, ByVal [ReadOnly] As Boolean)
        MyBase.New()
        l_ColumnID = ColumnID
        l_FilterActive = False
        l_FilterString = ""
        l_ReadOnly = [ReadOnly]
        l_Sorted = False
    End Sub

    Protected Overloads Overrides Sub Paint(ByVal g As System.Drawing.Graphics, ByVal bounds As System.Drawing.Rectangle, ByVal source As System.Windows.Forms.CurrencyManager, ByVal rowNum As Integer, ByVal BackgroundBrush As Brush, ByVal ForegroundBrush As Brush, ByVal alignToRight As Boolean)
        If Not MyBase.ReadOnly Then
            MyBase.Paint(g, bounds, source, rowNum, BackgroundBrush, ForegroundBrush, False)
        Else
            Dim forebrush As Brush = Brushes.Gray
            MyBase.Paint(g, bounds, source, rowNum, BackgroundBrush, forebrush, False)
        End If
    End Sub
End Class