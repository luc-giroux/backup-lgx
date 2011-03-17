Public Class SelectWPForm
    Inherits System.Windows.Forms.Form

#Region " Lists for ComboBox - Generic list definition"
    Private MustInherit Class ListOfListClass
        Private MyList As Collections.SortedList
        Public Class GenericListClass
            Inherits CollectionBase
            Public Class GenericItem
                Private l_Key As String
                Private l_DisplayValue As String
                Public ReadOnly Property Key() As String
                    Get
                        Return l_Key
                    End Get
                End Property
                Public ReadOnly Property DisplayValue() As String
                    Get
                        Return l_DisplayValue
                    End Get
                End Property
                Public Sub New(ByVal Key As String, ByVal DisplayValue As String)
                    l_Key = Key
                    l_DisplayValue = DisplayValue
                End Sub
            End Class
            Public Function Add(ByVal Item As GenericItem) As Integer
                Return MyBase.List.Add(Item)
            End Function
            Public Function GetKeyForIndex(ByVal IndexNo As Integer) As String
                Return CType(MyBase.List.Item(IndexNo), GenericItem).Key
            End Function
            Public Function GetIndexForKey(ByVal Key As String) As Integer
                Dim i As Integer
                For i = 0 To MyBase.List.Count - 1
                    If CType(MyBase.List.Item(i), GenericItem).Key = Key Then
                        Return i
                    End If
                Next
                Return -1
            End Function
        End Class
        Public MustOverride Function BuildList(ByVal Key As String) As GenericListClass

        Default Public ReadOnly Property Item(ByVal Key As String) As GenericListClass
            Get
                If IsNothing(Key) Then
                    l_Current = Nothing
                End If
                If MyList.ContainsKey(Key) Then
                    l_Current = CType(MyList.Item(Key), GenericListClass)
                Else ' we need to build it
                    l_Current = BuildList(Key)
                    MyList.Add(Key, l_Current)
                End If
                Return l_Current
            End Get
        End Property
        Dim l_Current As GenericListClass
        Public ReadOnly Property Current() As GenericListClass
            Get
                Return l_Current
            End Get
        End Property
        Protected MyReferenceToWorkPackageList As WorkPackage
        Public Sub New(ByRef WorkPackageList As WorkPackage)
            MyList = New Collections.SortedList
            MyReferenceToWorkPackageList = WorkPackageList
        End Sub
    End Class

#End Region

#Region " Lists for ComboBox - Implementations"
    Private Class PhaseListsClass
        Inherits ListOfListClass
        Public Sub New(ByRef WorkPackageList As WorkPackage)
            MyBase.New(WorkPackageList)
        End Sub

        Public Overrides Function BuildList(ByVal Key As String) As ListOfListClass.GenericListClass
            ' we don't care about the key for this one
            BuildList = New GenericListClass
            Dim row As WorkPackage.WorkPackageListRow
            Dim CurrentKey As String = ""
            For Each row In MyReferenceToWorkPackageList.WorkPackageList.Select(Nothing, "PhaseDescription")
                If row.PhaseID.ToString <> CurrentKey Then
                    BuildList.Add(New GenericListClass.GenericItem(row.PhaseID.ToString, row.PhaseDescription))
                    CurrentKey = row.PhaseID.ToString
                End If
            Next
        End Function
    End Class
    Private Class LocationListsClass
        Inherits ListOfListClass
        Public Sub New(ByRef WorkPackageList As WorkPackage)
            MyBase.New(WorkPackageList)
        End Sub

        Public Overrides Function BuildList(ByVal Key As String) As ListOfListClass.GenericListClass
            BuildList = New GenericListClass
            Dim row As WorkPackage.WorkPackageListRow
            Dim CurrentKey As String = ""
            Dim SortOrder As String
            SortOrder = MyReferenceToWorkPackageList.WorkPackageList.LocationSortOrderColumn.ColumnName & "," & _
                        MyReferenceToWorkPackageList.WorkPackageList.LocationIDColumn.ColumnName
            For Each row In MyReferenceToWorkPackageList.WorkPackageList.Select(Key, SortOrder)
                If row.LocationID <> CurrentKey Then
                    BuildList.Add(New GenericListClass.GenericItem(row.LocationID, row.LocationDescription))
                    CurrentKey = row.LocationID
                End If
            Next
        End Function
    End Class
    Private Class AreaListsClass
        Inherits ListOfListClass
        Public Sub New(ByRef WorkPackageList As WorkPackage)
            MyBase.New(WorkPackageList)
        End Sub

        Public Overrides Function BuildList(ByVal Key As String) As ListOfListClass.GenericListClass
            BuildList = New GenericListClass
            Dim row As WorkPackage.WorkPackageListRow
            Dim CurrentKey As String = ""
            Dim SortOrder As String
            SortOrder = MyReferenceToWorkPackageList.WorkPackageList.AreaSortOrderColumn.ColumnName & "," & _
                        MyReferenceToWorkPackageList.WorkPackageList.AreaIDColumn.ColumnName
            For Each row In MyReferenceToWorkPackageList.WorkPackageList.Select(Key, SortOrder)
                If row.AreaID.ToString <> CurrentKey Then
                    BuildList.Add(New GenericListClass.GenericItem(row.AreaID.ToString, row.AreaDescription))
                    CurrentKey = row.AreaID.ToString
                End If
            Next
        End Function
    End Class
    Private Class SubAreaListsClass
        Inherits ListOfListClass
        Public Sub New(ByRef WorkPackageList As WorkPackage)
            MyBase.New(WorkPackageList)
        End Sub

        Public Overrides Function BuildList(ByVal Key As String) As ListOfListClass.GenericListClass
            BuildList = New GenericListClass
            Dim row As WorkPackage.WorkPackageListRow
            Dim CurrentKey As String = ""
            Dim SortOrder As String
            SortOrder = MyReferenceToWorkPackageList.WorkPackageList.UnitSortOrderColumn.ColumnName & "," & _
                        MyReferenceToWorkPackageList.WorkPackageList.UnitIDColumn.ColumnName
            For Each row In MyReferenceToWorkPackageList.WorkPackageList.Select(Key, SortOrder)
                If row.UnitID.ToString <> CurrentKey Then
                    BuildList.Add(New GenericListClass.GenericItem(row.UnitID, row.UnitID & " " & row.UnitDescription))
                    CurrentKey = row.UnitID
                End If
            Next
        End Function
    End Class
    Private Class GroupListsClass
        Inherits ListOfListClass
        Public Sub New(ByRef WorkPackageList As WorkPackage)
            MyBase.New(WorkPackageList)
        End Sub

        Public Overrides Function BuildList(ByVal Key As String) As ListOfListClass.GenericListClass
            BuildList = New GenericListClass
            Dim row As WorkPackage.WorkPackageListRow
            Dim CurrentKey As String = ""
            Dim SortOrder As String
            SortOrder = MyReferenceToWorkPackageList.WorkPackageList.GroupSortOrderColumn.ColumnName & "," & _
                        MyReferenceToWorkPackageList.WorkPackageList.GroupIDColumn.ColumnName
            For Each row In MyReferenceToWorkPackageList.WorkPackageList.Select(Key, SortOrder)
                If row.GroupID.ToString <> CurrentKey Then
                    BuildList.Add(New GenericListClass.GenericItem(row.GroupID.ToString, row.GroupDescription))
                    CurrentKey = row.GroupID.ToString
                End If
            Next
        End Function
    End Class
    Private Class DisciplineListsClass
        Inherits ListOfListClass
        Public Sub New(ByRef WorkPackageList As WorkPackage)
            MyBase.New(WorkPackageList)
        End Sub

        Public Overrides Function BuildList(ByVal Key As String) As ListOfListClass.GenericListClass
            BuildList = New GenericListClass
            Dim row As WorkPackage.WorkPackageListRow
            Dim CurrentKey As String = ""
            Dim SortOrder As String
            SortOrder = MyReferenceToWorkPackageList.WorkPackageList.DisciplineLevel3SortOrderColumn.ColumnName & "," & _
                        MyReferenceToWorkPackageList.WorkPackageList.DisciplineIDColumn.ColumnName
            For Each row In MyReferenceToWorkPackageList.WorkPackageList.Select(Key, SortOrder)
                If row.DisciplineID.ToString <> CurrentKey Then
                    BuildList.Add(New GenericListClass.GenericItem(row.DisciplineID.ToString, row.DisciplineNo & " " & row.DisciplineDescription))
                    CurrentKey = row.DisciplineID.ToString
                End If
            Next
        End Function
    End Class
    Private Class SectionListsClass
        Inherits ListOfListClass
        Public Sub New(ByRef WorkPackageList As WorkPackage)
            MyBase.New(WorkPackageList)
        End Sub

        Public Overrides Function BuildList(ByVal Key As String) As ListOfListClass.GenericListClass
            BuildList = New GenericListClass
            Dim row As WorkPackage.WorkPackageListRow
            Dim CurrentKey As String = ""
            Dim SortOrder As String
            SortOrder = MyReferenceToWorkPackageList.WorkPackageList.SectionNoColumn.ColumnName & "," & _
                        MyReferenceToWorkPackageList.WorkPackageList.SectionIDColumn.ColumnName
            For Each row In MyReferenceToWorkPackageList.WorkPackageList.Select(Key, SortOrder)
                If row.SectionID.ToString <> CurrentKey Then
                    BuildList.Add(New GenericListClass.GenericItem(row.SectionID.ToString, row.SectionNo & " " & row.SectionDescription))
                    CurrentKey = row.SectionID.ToString
                End If
            Next
        End Function
    End Class

#End Region

    Private PhaseLists As PhaseListsClass
    Private LocationLists As LocationListsClass
    Private AreaLists As AreaListsClass
    Private SubAreaLists As SubAreaListsClass
    Private GroupLists As GroupListsClass
    Private DisciplineLists As DisciplineListsClass
    Private SectionLists As SectionListsClass
    Private RRHourlyRateCriteria As Boolean
    Public Sub New(ByVal pRRHourlyRateCriteria As Boolean)
        Me.New()
        RRHourlyRateCriteria = pRRHourlyRateCriteria
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
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents ContractComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents LocationComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents AreaComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents SubAreaComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents DisciplineL1ComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents DisciplineL3ComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents SectionComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents OKButton As System.Windows.Forms.Button
    Friend Shadows WithEvents CancelButton As System.Windows.Forms.Button
    'Friend WithEvents SqlConnection1 As System.Data.SqlClient.SqlConnection
    Friend WithEvents LocalSqlConnection As System.Data.SqlClient.SqlConnection
    Friend WithEvents SqlSelectCommand As System.Data.SqlClient.SqlCommand
    Friend WithEvents LocalSqlDataAdapter As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents WorkPackage As PAF_Adjustments2.WorkPackage
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.ContractComboBox = New System.Windows.Forms.ComboBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.LocationComboBox = New System.Windows.Forms.ComboBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.AreaComboBox = New System.Windows.Forms.ComboBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.SubAreaComboBox = New System.Windows.Forms.ComboBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.DisciplineL1ComboBox = New System.Windows.Forms.ComboBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.DisciplineL3ComboBox = New System.Windows.Forms.ComboBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.SectionComboBox = New System.Windows.Forms.ComboBox
        Me.OKButton = New System.Windows.Forms.Button
        Me.CancelButton = New System.Windows.Forms.Button
        Me.LocalSqlConnection = New System.Data.SqlClient.SqlConnection
        Me.SqlSelectCommand = New System.Data.SqlClient.SqlCommand
        Me.LocalSqlDataAdapter = New System.Data.SqlClient.SqlDataAdapter
        Me.WorkPackage = New PAF_Adjustments2.WorkPackage
        CType(Me.WorkPackage, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ContractComboBox
        '
        Me.ContractComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ContractComboBox.Location = New System.Drawing.Point(120, 16)
        Me.ContractComboBox.Name = "ContractComboBox"
        Me.ContractComboBox.Size = New System.Drawing.Size(160, 21)
        Me.ContractComboBox.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Label1.Location = New System.Drawing.Point(8, 16)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(104, 24)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Contract"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label2
        '
        Me.Label2.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Label2.Location = New System.Drawing.Point(8, 48)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(104, 24)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Location"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'LocationComboBox
        '
        Me.LocationComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.LocationComboBox.Location = New System.Drawing.Point(120, 48)
        Me.LocationComboBox.Name = "LocationComboBox"
        Me.LocationComboBox.Size = New System.Drawing.Size(160, 21)
        Me.LocationComboBox.TabIndex = 2
        '
        'Label3
        '
        Me.Label3.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Label3.Location = New System.Drawing.Point(10, 80)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(104, 24)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "Area"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'AreaComboBox
        '
        Me.AreaComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.AreaComboBox.Location = New System.Drawing.Point(122, 80)
        Me.AreaComboBox.Name = "AreaComboBox"
        Me.AreaComboBox.Size = New System.Drawing.Size(160, 21)
        Me.AreaComboBox.TabIndex = 4
        '
        'Label4
        '
        Me.Label4.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Label4.Location = New System.Drawing.Point(10, 112)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(104, 24)
        Me.Label4.TabIndex = 7
        Me.Label4.Text = "Sub Area"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'SubAreaComboBox
        '
        Me.SubAreaComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.SubAreaComboBox.Location = New System.Drawing.Point(122, 112)
        Me.SubAreaComboBox.Name = "SubAreaComboBox"
        Me.SubAreaComboBox.Size = New System.Drawing.Size(160, 21)
        Me.SubAreaComboBox.TabIndex = 6
        '
        'Label5
        '
        Me.Label5.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Label5.Location = New System.Drawing.Point(10, 144)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(104, 24)
        Me.Label5.TabIndex = 9
        Me.Label5.Text = "Discipline"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'DisciplineL1ComboBox
        '
        Me.DisciplineL1ComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.DisciplineL1ComboBox.Location = New System.Drawing.Point(122, 144)
        Me.DisciplineL1ComboBox.Name = "DisciplineL1ComboBox"
        Me.DisciplineL1ComboBox.Size = New System.Drawing.Size(160, 21)
        Me.DisciplineL1ComboBox.TabIndex = 8
        '
        'Label6
        '
        Me.Label6.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Label6.Location = New System.Drawing.Point(10, 176)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(104, 24)
        Me.Label6.TabIndex = 11
        Me.Label6.Text = "Sub Discipline"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'DisciplineL3ComboBox
        '
        Me.DisciplineL3ComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.DisciplineL3ComboBox.Location = New System.Drawing.Point(122, 176)
        Me.DisciplineL3ComboBox.Name = "DisciplineL3ComboBox"
        Me.DisciplineL3ComboBox.Size = New System.Drawing.Size(160, 21)
        Me.DisciplineL3ComboBox.TabIndex = 10
        '
        'Label7
        '
        Me.Label7.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Label7.Location = New System.Drawing.Point(10, 208)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(104, 24)
        Me.Label7.TabIndex = 13
        Me.Label7.Text = "Section"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'SectionComboBox
        '
        Me.SectionComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.SectionComboBox.Location = New System.Drawing.Point(122, 208)
        Me.SectionComboBox.Name = "SectionComboBox"
        Me.SectionComboBox.Size = New System.Drawing.Size(160, 21)
        Me.SectionComboBox.TabIndex = 12
        '
        'OKButton
        '
        Me.OKButton.Location = New System.Drawing.Point(72, 240)
        Me.OKButton.Name = "OKButton"
        Me.OKButton.Size = New System.Drawing.Size(64, 24)
        Me.OKButton.TabIndex = 14
        Me.OKButton.Text = "OK"
        '
        'CancelButton
        '
        Me.CancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.CancelButton.Location = New System.Drawing.Point(160, 240)
        Me.CancelButton.Name = "CancelButton"
        Me.CancelButton.Size = New System.Drawing.Size(64, 24)
        Me.CancelButton.TabIndex = 15
        Me.CancelButton.Text = "Cancel"
        '
        'LocalSqlConnection
        '
        Me.LocalSqlConnection.ConnectionString = "workstation id=MTL0737;packet size=4096;integrated security=SSPI;data source=""(lo" & _
        "cal)"";persist security info=False;initial catalog=PAF_TEST"
        '
        'SqlSelectCommand
        '
        Me.SqlSelectCommand.CommandText = _
            "SELECT  W.WorkPackageID, P.PhaseID AS PhaseID, P.Description AS PhaseDescription, " & _
                    "L.LocationID AS LocationID, L.LocationName AS LocationDescription, L.SortOrder A" & _
                    "S LocationSortOrder, A.AreaID AS AreaID, A.Description AS AreaDescription, " & _
                    "A.SortOrder AS AreaSortOrder, U.UnitID AS UnitID, U.Description AS UnitDescription," & _
                    "U.SortOrder AS UnitSortOrder, G.GroupID AS GroupID, " & _
                    "G.Description AS GroupDescription, G.SortOrder AS GroupSortOrder, D.DisciplineID AS DisciplineID, " & _
                    "D.DisciplineNo AS DisciplineNo, D.Description AS DisciplineDescription, " & _
                    "D.SortOrder AS DisciplineLevel3SortOrder, S.SectionID AS SectionID, S.SectionNo AS SectionNo, " & _
                    "S.Description AS SectionDescription FROM WorkPackage W " & _
                    "INNER JOIN Location L ON L.LocationID = W.LocationID " & _
                    "INNER JOIN Phase P ON P.PhaseID = W.PhaseID " & _
                    "INNER JOIN Unit U ON U.UnitID = W.UnitID " & _
                    "INNER JOIN SubArea SA ON SA.SubAreaID = U.SubAreaID " & _
                    "INNER JOIN Area A ON A.AreaID = SA.AreaID " & _
                    "INNER JOIN Section S ON S.SectionID = W.SectionID " & _
                    "INNER JOIN Discipline D ON D.DisciplineID = S.DisciplineID " & _
                    "INNER JOIN [Function] F ON F.FunctionID = D.FunctionID " & _
                    "INNER JOIN [Group] G ON G.GroupID = F.GroupID"
        Me.SqlSelectCommand.Connection = Me.LocalSqlConnection
        '
        'LocalSqlDataAdapter
        '
        Me.LocalSqlDataAdapter.SelectCommand = Me.SqlSelectCommand
        '
        'WorkPackage
        '
        Me.WorkPackage.DataSetName = "WorkPackage"
        Me.WorkPackage.Locale = New System.Globalization.CultureInfo("en-US")
        '
        'SelectWPForm
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.CancelButton = Me.CancelButton
        Me.ClientSize = New System.Drawing.Size(292, 273)
        Me.Controls.Add(Me.CancelButton)
        Me.Controls.Add(Me.OKButton)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.SectionComboBox)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.DisciplineL3ComboBox)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.DisciplineL1ComboBox)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.SubAreaComboBox)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.AreaComboBox)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.LocationComboBox)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.ContractComboBox)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "SelectWPForm"
        Me.Text = "Select WorkPackage"
        CType(Me.WorkPackage, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub SelectWPForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim i As Integer
        If LocalSqlConnection.State = ConnectionState.Open Then
            LocalSqlConnection.Close()
        End If
        LocalSqlConnection.ConnectionString = Config.SQLConnectionString
        If Not IsNothing(RRHourlyRateCriteria) Then
            Me.SqlSelectCommand.CommandText = Me.SqlSelectCommand.CommandText & " WHERE [UseR&RHourlyRate] = "
            If RRHourlyRateCriteria Then
                Me.SqlSelectCommand.CommandText = Me.SqlSelectCommand.CommandText & "1"
            Else
                Me.SqlSelectCommand.CommandText = Me.SqlSelectCommand.CommandText & "0"
            End If
        End If
        i = LocalSqlDataAdapter.Fill(WorkPackage.WorkPackageList)

        PhaseLists = New PhaseListsClass(WorkPackage)
        LocationLists = New LocationListsClass(WorkPackage)
        AreaLists = New AreaListsClass(WorkPackage)
        SubAreaLists = New SubAreaListsClass(WorkPackage)
        GroupLists = New GroupListsClass(WorkPackage)
        DisciplineLists = New DisciplineListsClass(WorkPackage)
        SectionLists = New SectionListsClass(WorkPackage)
        Dim row As ListOfListClass.GenericListClass.GenericItem
        For Each row In PhaseLists("")
            Me.ContractComboBox.Items.Add(row.DisplayValue)
        Next
        If Not IsNothing(SelectedWorkPackageID) Then
            Dim WPListrow As WorkPackage.WorkPackageListRow
            Dim WhereClause As String
            WhereClause = "WorkPackageID = '" & SelectedWorkPackageID & "'"
            For Each WPListrow In WorkPackage.WorkPackageList.Select(WhereClause)
                Me.ContractComboBox.SelectedIndex = Me.ContractComboBox.Items.IndexOf(WPListrow.PhaseDescription)
                Me.LocationComboBox.SelectedIndex = Me.LocationLists.Current.GetIndexForKey(WPListrow.LocationID)
                Me.AreaComboBox.SelectedIndex = Me.AreaLists.Current.GetIndexForKey(WPListrow.AreaID.ToString)
                Me.SubAreaComboBox.SelectedIndex = Me.SubAreaLists.Current.GetIndexForKey(WPListrow.UnitID)
                Me.DisciplineL1ComboBox.SelectedIndex = Me.GroupLists.Current.GetIndexForKey(WPListrow.GroupID.ToString)
                Me.DisciplineL3ComboBox.SelectedIndex = Me.DisciplineLists.Current.GetIndexForKey(WPListrow.DisciplineID.ToString)
                Me.SectionComboBox.SelectedIndex = Me.SectionLists.Current.GetIndexForKey(WPListrow.SectionID.ToString)
            Next
        Else
            Me.ContractComboBox.SelectedIndex = -1
            Me.ContractComboBox.Tag = Nothing
            Me.OKButton.Enabled = False
        End If

    End Sub
    Private Sub ContractComboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ContractComboBox.SelectedIndexChanged
        If Me.ContractComboBox.SelectedIndex <> -1 Then
            Dim row As ListOfListClass.GenericListClass.GenericItem
            Dim WhereClause As String
            Me.ContractComboBox.Tag = PhaseLists("").GetKeyForIndex(Me.ContractComboBox.SelectedIndex)
            WhereClause = "PhaseID = '" & CType(Me.ContractComboBox.Tag, String) & "'"
            Me.LocationComboBox.Items.Clear()
            For Each row In LocationLists(WhereClause)
                Me.LocationComboBox.Items.Add(row.DisplayValue)
            Next
        Else
            Me.LocationComboBox.Items.Clear()
        End If
        If Me.LocationComboBox.Items.Count = 1 Then
            Me.LocationComboBox.SelectedIndex = 0
        Else
            Me.LocationComboBox.SelectedIndex = -1
            Me.LocationComboBox.Tag = Nothing

            Me.AreaComboBox.Items.Clear()
            Me.AreaComboBox.Tag = Nothing

            Me.SubAreaComboBox.Items.Clear()
            Me.SubAreaComboBox.Tag = Nothing

            Me.DisciplineL1ComboBox.Items.Clear()
            Me.DisciplineL1ComboBox.Tag = Nothing

            Me.DisciplineL3ComboBox.Items.Clear()
            Me.DisciplineL3ComboBox.Tag = Nothing

            Me.SectionComboBox.Items.Clear()
            Me.SectionComboBox.Tag = Nothing

            Me.OKButton.Enabled = False
        End If
    End Sub
    Private Sub LocationComboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LocationComboBox.SelectedIndexChanged
        If Me.LocationComboBox.SelectedIndex <> -1 Then
            Dim row As ListOfListClass.GenericListClass.GenericItem
            Dim WhereClause As String
            Me.LocationComboBox.Tag = LocationLists.Current.GetKeyForIndex(Me.LocationComboBox.SelectedIndex)

            WhereClause = "PhaseID = '" & PhaseLists("").GetKeyForIndex(Me.ContractComboBox.SelectedIndex) & "'" & _
                    " AND LocationID = '" & CType(Me.LocationComboBox.Tag, String) & "'"
            Me.AreaComboBox.Items.Clear()
            For Each row In AreaLists(WhereClause)
                Me.AreaComboBox.Items.Add(row.DisplayValue)
            Next
        Else
            Me.AreaComboBox.Items.Clear()
        End If
        If Me.AreaComboBox.Items.Count = 1 Then
            Me.AreaComboBox.SelectedIndex = 0
        Else
            Me.AreaComboBox.SelectedIndex = -1
            Me.AreaComboBox.Tag = Nothing

            Me.SubAreaComboBox.Items.Clear()
            Me.SubAreaComboBox.Tag = Nothing

            Me.DisciplineL1ComboBox.Items.Clear()
            Me.DisciplineL1ComboBox.Tag = Nothing

            Me.DisciplineL3ComboBox.Items.Clear()
            Me.DisciplineL3ComboBox.Tag = Nothing

            Me.SectionComboBox.Items.Clear()
            Me.SectionComboBox.Tag = Nothing

            Me.OKButton.Enabled = False
        End If
    End Sub
    Private Sub AreaComboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AreaComboBox.SelectedIndexChanged
        If Me.AreaComboBox.SelectedIndex <> -1 Then
            Dim row As ListOfListClass.GenericListClass.GenericItem
            Dim WhereClause As String
            Me.AreaComboBox.Tag = AreaLists.Current.GetKeyForIndex(Me.AreaComboBox.SelectedIndex)

            WhereClause = "PhaseID = '" & PhaseLists("").GetKeyForIndex(Me.ContractComboBox.SelectedIndex) & "'" & _
                    " AND LocationID = '" & CType(Me.LocationComboBox.Tag, String) & "'" & _
                    " AND AreaID = '" & CType(Me.AreaComboBox.Tag, String) & "'"
            Me.SubAreaComboBox.Items.Clear()
            For Each row In SubAreaLists(WhereClause)
                Me.SubAreaComboBox.Items.Add(row.DisplayValue)
            Next
        Else
            Me.SubAreaComboBox.Items.Clear()
        End If
        If Me.SubAreaComboBox.Items.Count = 1 Then
            Me.SubAreaComboBox.SelectedIndex = 0
        Else
            Me.SubAreaComboBox.SelectedIndex = -1
            Me.SubAreaComboBox.Tag = Nothing

            Me.DisciplineL1ComboBox.Items.Clear()
            Me.DisciplineL1ComboBox.Tag = Nothing

            Me.DisciplineL3ComboBox.Items.Clear()
            Me.DisciplineL3ComboBox.Tag = Nothing

            Me.SectionComboBox.Items.Clear()
            Me.SectionComboBox.Tag = Nothing

            Me.OKButton.Enabled = False
        End If
    End Sub
    Private Sub SubAreaComboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SubAreaComboBox.SelectedIndexChanged
        If Me.SubAreaComboBox.SelectedIndex <> -1 Then
            Dim row As ListOfListClass.GenericListClass.GenericItem
            Dim WhereClause As String
            Me.SubAreaComboBox.Tag = SubAreaLists.Current.GetKeyForIndex(Me.SubAreaComboBox.SelectedIndex)

            WhereClause = "PhaseID = '" & PhaseLists("").GetKeyForIndex(Me.ContractComboBox.SelectedIndex) & "'" & _
                    " AND LocationID = '" & CType(Me.LocationComboBox.Tag, String) & "'" & _
                    " AND AreaID = '" & CType(Me.AreaComboBox.Tag, String) & "'" & _
                    " AND UnitID = '" & CType(Me.SubAreaComboBox.Tag, String) & "'"
            Me.DisciplineL1ComboBox.Items.Clear()
            For Each row In GroupLists(WhereClause)
                Me.DisciplineL1ComboBox.Items.Add(row.DisplayValue)
            Next
        Else
            Me.DisciplineL1ComboBox.Items.Clear()
        End If
        If Me.DisciplineL1ComboBox.Items.Count = 1 Then
            Me.DisciplineL1ComboBox.SelectedIndex = 0
        Else
            Me.DisciplineL1ComboBox.SelectedIndex = -1
            Me.DisciplineL1ComboBox.Tag = Nothing

            Me.DisciplineL3ComboBox.Items.Clear()
            Me.DisciplineL3ComboBox.Tag = Nothing

            Me.SectionComboBox.Items.Clear()
            Me.SectionComboBox.Tag = Nothing

            Me.OKButton.Enabled = False
        End If
    End Sub
    Private Sub DisciplineL1ComboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DisciplineL1ComboBox.SelectedIndexChanged
        If Me.DisciplineL1ComboBox.SelectedIndex <> -1 Then
            Dim row As ListOfListClass.GenericListClass.GenericItem
            Dim WhereClause As String
            Me.DisciplineL1ComboBox.Tag = GroupLists.Current.GetKeyForIndex(Me.DisciplineL1ComboBox.SelectedIndex)

            WhereClause = "PhaseID = '" & PhaseLists("").GetKeyForIndex(Me.ContractComboBox.SelectedIndex) & "'" & _
                    " AND LocationID = '" & CType(Me.LocationComboBox.Tag, String) & "'" & _
                    " AND AreaID = '" & CType(Me.AreaComboBox.Tag, String) & "'" & _
                    " AND UnitID = '" & CType(Me.SubAreaComboBox.Tag, String) & "'" & _
                    " AND GroupID = '" & CType(Me.DisciplineL1ComboBox.Tag, String) & "'"
            Me.DisciplineL3ComboBox.Items.Clear()
            For Each row In DisciplineLists(WhereClause)
                Me.DisciplineL3ComboBox.Items.Add(row.DisplayValue)
            Next
        Else
            Me.DisciplineL3ComboBox.Items.Clear()
        End If
        If Me.DisciplineL3ComboBox.Items.Count = 1 Then
            Me.DisciplineL3ComboBox.SelectedIndex = 0
        Else
            Me.DisciplineL3ComboBox.SelectedIndex = -1
            Me.DisciplineL3ComboBox.Tag = Nothing

            Me.SectionComboBox.Items.Clear()
            Me.SectionComboBox.Tag = Nothing

            Me.OKButton.Enabled = False
        End If
    End Sub
    Private Sub DisciplineL3ComboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DisciplineL3ComboBox.SelectedIndexChanged
        If Me.DisciplineL3ComboBox.SelectedIndex <> -1 Then
            Dim row As ListOfListClass.GenericListClass.GenericItem
            Dim WhereClause As String
            Me.DisciplineL3ComboBox.Tag = DisciplineLists.Current.GetKeyForIndex(Me.DisciplineL3ComboBox.SelectedIndex)

            WhereClause = "PhaseID = '" & PhaseLists("").GetKeyForIndex(Me.ContractComboBox.SelectedIndex) & "'" & _
                    " AND LocationID = '" & CType(Me.LocationComboBox.Tag, String) & "'" & _
                    " AND AreaID = '" & CType(Me.AreaComboBox.Tag, String) & "'" & _
                    " AND UnitID = '" & CType(Me.SubAreaComboBox.Tag, String) & "'" & _
                    " AND GroupID = '" & CType(Me.DisciplineL1ComboBox.Tag, String) & "'" & _
                    " AND DisciplineID = '" & CType(Me.DisciplineL3ComboBox.Tag, String) & "'"
            Me.SectionComboBox.Items.Clear()
            For Each row In SectionLists(WhereClause)
                Me.SectionComboBox.Items.Add(row.DisplayValue)
            Next
        Else
            Me.SectionComboBox.Items.Clear()
        End If
        If Me.SectionComboBox.Items.Count = 1 Then
            Me.SectionComboBox.SelectedIndex = 0
        Else
            Me.SectionComboBox.SelectedIndex = -1
            Me.SectionComboBox.Tag = Nothing
            Me.OKButton.Enabled = False
        End If
    End Sub
    Private Sub SectionComboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SectionComboBox.SelectedIndexChanged
        If Me.DisciplineL3ComboBox.SelectedIndex <> -1 Then
            Me.SectionComboBox.Tag = SectionLists.Current.GetKeyForIndex(Me.SectionComboBox.SelectedIndex)
            Me.OKButton.Enabled = True
        Else
            Me.SectionComboBox.Tag = Nothing
        End If
    End Sub

    Private Sub CancelButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CancelButton.Click
        Me.Close()
    End Sub
    Private Sub OKButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OKButton.Click
        Dim row As WorkPackage.WorkPackageListRow
        Dim WhereClause As String
        WhereClause = "PhaseID = '" & PhaseLists("").GetKeyForIndex(Me.ContractComboBox.SelectedIndex) & "'" & _
                " AND LocationID = '" & CType(Me.LocationComboBox.Tag, String) & "'" & _
                " AND AreaID = '" & CType(Me.AreaComboBox.Tag, String) & "'" & _
                " AND UnitID = '" & CType(Me.SubAreaComboBox.Tag, String) & "'" & _
                " AND GroupID = '" & CType(Me.DisciplineL1ComboBox.Tag, String) & "'" & _
                " AND DisciplineID = '" & CType(Me.DisciplineL3ComboBox.Tag, String) & "'" & _
                " AND SectionID = '" & CType(Me.SectionComboBox.Tag, String) & "'"
        For Each row In WorkPackage.WorkPackageList.Select(WhereClause)
            SelectedWorkPackageID = row.WorkPackageID
        Next
        Me.DialogResult = DialogResult.OK
    End Sub
End Class