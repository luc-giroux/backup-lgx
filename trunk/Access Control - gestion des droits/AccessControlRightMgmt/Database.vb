Module Database
    Public Class DB
        Private Const ConnectionString As String = "Data Source=10.199.80.10;Initial Catalog=KNS_AccessControl;User Id=KNS_AC;Password=KNS_AC;"
        Private DBConnection As SqlClient.SqlConnection
        Public Sub New()
            DBConnection = New SqlClient.SqlConnection(ConnectionString)
            Try
                DBConnection.Open()
                ' validate the connection works
                Dim sqlcmd As New SqlClient.SqlCommand("SELECT GetDate()", DBConnection)
                Dim t As String = CType(sqlcmd.ExecuteScalar, String)
            Catch ex As Exception
                Throw (New Exception("Erreur lors de la connection à la base de données"))
            End Try
        End Sub
        Public Function CheckIsAuthorised() As Boolean
            Try
                Dim sqlStmt As String = "SELECT COUNT(*) FROM AuthorisedUsers WHERE Username = '" + System.Environment.UserName() & "' AND MachineName = '" & System.Environment.MachineName & "'"
                Dim sqlCmd As New SqlClient.SqlCommand(sqlStmt, DBConnection)
                Dim i As Integer
                i = CType(sqlCmd.ExecuteScalar(), Integer)
                If i > 0 Then
                    Return True
                Else
                    Return False
                End If
            Catch ex As Exception
                MsgBox("CheckIsAuthorised: " & ex.Message, MsgBoxStyle.Critical, "Erreur")
                Me.Log("Error", "in CheckIsAuthorised", ex.StackTrace)
                Return False
            End Try
            Return False
        End Function
        Public Function getIndivRecordType(ByVal IndivId As String) As String
            Dim recType As String
            Try
                Dim sqlStmt As String = "SELECT Status FROM [InetDb].[dbo].[Individuals] WHERE IndivId = '" + IndivId + "'"
                Dim sqlCmd As New SqlClient.SqlCommand(sqlStmt, DBConnection)
                recType = CStr(sqlCmd.ExecuteScalar())
            Catch ex As Exception
                MsgBox("getIndivRecordType: " & ex.Message, MsgBoxStyle.Critical, "Erreur")
                Me.Log("Error", "in getIndivRecordType", ex.StackTrace)
                Return ""
            End Try
            Return recType
        End Function
        Public Sub updateIndivRecordType(ByVal IndivId As String, ByVal NewStatus As Integer)
            Try
                Dim sqlStmt As String = "Update [InetDb].[dbo].[Individuals] SET Status = " + NewStatus.ToString + " WHERE IndivId = '" + IndivId + "'"
                Dim sqlCmd As New SqlClient.SqlCommand(sqlStmt, DBConnection)
                sqlCmd.ExecuteNonQuery()
            Catch ex As Exception
                MsgBox("updateIndivRecordType: " & ex.Message, MsgBoxStyle.Critical, "Erreur")
                Me.Log("Error", "in updateIndivRecordType", ex.StackTrace)
            End Try
        End Sub
        Public Function FindCards(ByVal CardNo As Integer, ByVal LastName As String) As DataSet1.CardListDataTable
            Dim sqlStmt As String
            Dim sqlCmd As SqlClient.SqlCommand
            Dim sqlReader As SqlClient.SqlDataReader = Nothing
            FindCards = Nothing
            If CardNo > 0 Then
                sqlStmt = "SELECT IndivID, Lastname, Firstname, Custom01 " & _
                            "FROM [InetDb].[dbo].[Individuals] " & _
                            "INNER JOIN [InetDb].[dbo].[IndivCustomData] ON IndivNDX = IndivID " & _
                            "WHERE IndivId = " & CardNo
            Else ' query with lastname
                sqlStmt = "SELECT IndivID, Lastname, Firstname, Custom01 " & _
                            "FROM [InetDb].[dbo].[Individuals] " & _
                            "INNER JOIN [InetDb].[dbo].[IndivCustomData] ON IndivNDX = IndivID " & _
                            "WHERE Lastname like '" & LastName & "%'"
            End If
            Try
                sqlCmd = New SqlClient.SqlCommand(sqlStmt, DBConnection)
                sqlReader = sqlCmd.ExecuteReader()
                If Not sqlReader.HasRows Then
                    FindCards = Nothing
                Else
                    FindCards = New DataSet1.CardListDataTable()
                    Dim row As DataSet1.CardListRow
                    While sqlReader.Read()
                        row = FindCards.NewCardListRow()
                        row.SetField(0, sqlReader.GetValue(0))
                        row.SetField(1, sqlReader.GetValue(1))
                        row.SetField(2, sqlReader.GetValue(2))
                        row.SetField(3, sqlReader.GetValue(3))
                        FindCards.AddCardListRow(row)
                    End While
                End If
                sqlReader.Close()
            Catch ex As Exception
                MsgBox("FindCards: " & ex.Message, MsgBoxStyle.Critical, "Erreur")
                Me.Log("Error", "in FindCards", ex.StackTrace)
                FindCards = Nothing
            End Try
            If Not IsNothing(sqlReader) Then
                sqlReader.Close()
            End If
        End Function ' FindCards
        Public Function GetCardDetails(ByVal CardNo As Integer) As DataSet1.CardListRow
            Dim sqlStmt As String
            Dim sqlCmd As SqlClient.SqlCommand
            Dim sqlReader As SqlClient.SqlDataReader = Nothing
            GetCardDetails = Nothing
            If CardNo = 0 Then
                MsgBox("GetCardDetails: Argument invalide!", MsgBoxStyle.Critical, "Erreur")
                Me.Log("Error", "in GetCardDetails", "CardNo = 0")
                Return Nothing
            End If
            sqlStmt = "SELECT IndivID, Lastname, Firstname, Custom01, Status " & _
                        "FROM [InetDb].[dbo].[Individuals] " & _
                        "INNER JOIN [InetDb].[dbo].[IndivCustomData] ON IndivNDX = IndivID " & _
                        "WHERE IndivId = " & CardNo
            Try
                sqlCmd = New SqlClient.SqlCommand(sqlStmt, DBConnection)
                sqlReader = sqlCmd.ExecuteReader()
                If Not sqlReader.HasRows Then
                    MsgBox("GetCardDetails: Aucune carte retrouvée pour la carte " + CardNo.ToString, MsgBoxStyle.Critical, "Erreur")
                    GetCardDetails = Nothing
                Else
                    GetCardDetails = New DataSet1.CardListDataTable().NewCardListRow
                    sqlReader.Read()
                    GetCardDetails.SetField(0, sqlReader.GetValue(0)) ' IndivID
                    GetCardDetails.SetField(1, sqlReader.GetValue(1)) ' Lastname
                    GetCardDetails.SetField(2, sqlReader.GetValue(2)) ' Firstname
                    GetCardDetails.SetField(3, sqlReader.GetValue(3)) ' Custom01 - Enterprise
                    GetCardDetails.SetField(4, sqlReader.GetValue(4)) ' status
                End If
                sqlReader.Close()
                Return GetCardDetails
            Catch ex As Exception
                MsgBox("GetCardDetails: " & ex.Message, MsgBoxStyle.Critical, "Erreur")
                Me.Log("Error", "in GetCardDetails", ex.StackTrace)
                If Not IsNothing(sqlReader) Then
                    sqlReader.Close()
                End If
                Return Nothing
            End Try
        End Function ' GetCardDetails
        Public Function GetPicture(ByVal CardNo As Integer) As Image
            Dim sqlStmt As String
            Dim sqlCmd As SqlClient.SqlCommand
            Dim Picture As Object
            If CardNo = 0 Then
                MsgBox("GetPicture: Argument invalide!", MsgBoxStyle.Critical, "Erreur")
                Return Nothing
            End If
            GetPicture = Nothing
            sqlStmt = "SELECT UserImage FROM [InetDb].[dbo].[IndivImages] WHERE IndivNdx = " & CardNo
            Try
                sqlCmd = New SqlClient.SqlCommand(sqlStmt, DBConnection)
                Picture = sqlCmd.ExecuteScalar()
                If IsNothing(Picture) Then
                    Return Nothing
                Else
                    GetPicture = CType(New ImageConverter().ConvertFrom(Picture), Image)
                End If
            Catch ex As Exception
                If ex.Message = "ImageConverter cannot convert from System.DBNull." Then
                    'MsgBox("Pas de d'image associée à l'individu", MsgBoxStyle.Information, "No Picture")
                Else
                    MsgBox("GetPicture: " & ex.Message, MsgBoxStyle.Critical, "Erreur")
                End If

                Return Nothing
            End Try
        End Function ' GetPicture
        Public Function GetAvailableRights() As DataSet1.CardGroupsDataTable
            GetAvailableRights = New DataSet1.CardGroupsDataTable
            Dim sqlStmt As String
            Dim sqlCmd As SqlClient.SqlCommand
            Dim sqlReader As SqlClient.SqlDataReader = Nothing
            Dim row As DataSet1.CardGroupsRow
            sqlStmt = "SELECT DoorGroupID, DoorGroupName " & _
                    "FROM [InetDB].[dbo].[groups] " & _
                    "INNER JOIN [InetDB].[dbo].[DoorsGroups] ON doorgroupid = doorgroupndx " & _
                    "ORDER BY DoorGroupName"
            Try
                sqlCmd = New SqlClient.SqlCommand(sqlStmt, DBConnection)

                sqlReader = sqlCmd.ExecuteReader
                While sqlReader.Read
                    row = GetAvailableRights.NewCardGroupsRow()
                    row.SetField(0, 1) ' TenantNdx
                    row.SetField(1, sqlReader.GetValue(0)) ' DoorGroupNdx
                    row.SetField(2, sqlReader.GetValue(1)) ' DoorGroupName
                    row.SetField(3, 0) ' PriorityOrder
                    row.SetField(4, 0) ' DoorSchedule
                    GetAvailableRights.AddCardGroupsRow(row)
                End While
                sqlReader.Close()
            Catch ex As Exception
                MsgBox("GetAvailableRights: " & ex.Message, MsgBoxStyle.Critical, "Erreur")
                Me.Log("Error", "in GetAvailableRights", ex.StackTrace)
                GetAvailableRights = Nothing
            End Try
            If Not IsNothing(sqlReader) Then
                sqlReader.Close()
            End If
            Return GetAvailableRights
        End Function ' GetAvailableRights
        Public Function GetCardRightAssignments(ByVal CardNo As Integer) As DataSet1.CardGroupsDataTable
            Dim sqlStmt As String
            Dim sqlCmd As SqlClient.SqlCommand
            Dim sqlReader As SqlClient.SqlDataReader = Nothing
            Dim row As DataSet1.CardGroupsRow
            GetCardRightAssignments = Nothing
            sqlStmt = "SELECT XRefIndivGroupDoor.TenantNdx, [XRefIndivGroupDoor].doorgroupndx, DoorGroupName, PriorityOrder, DoorSchedule " & _
                        "FROM [InetDB].[dbo].[XRefIndivGroupDoor] As XRefIndivGroupDoor " & _
                        "INNER JOIN [InetDB].[dbo].[DoorsGroups] As DoorsGroups ON [DoorsGroups].doorgroupid = [XRefIndivGroupDoor].doorgroupndx " & _
                        "INNER JOIN [InetDB].[dbo].[groups] As Groups ON [DoorsGroups].doorgroupid = [groups].doorgroupndx " & _
                        "WHERE IndivNdx = " & CardNo.ToString & _
                        " ORDER BY PriorityOrder"
            Try
                sqlCmd = New SqlClient.SqlCommand(sqlStmt, DBConnection)

                GetCardRightAssignments = New DataSet1.CardGroupsDataTable
                sqlReader = sqlCmd.ExecuteReader
                While sqlReader.Read
                    row = GetCardRightAssignments.NewCardGroupsRow()
                    row.SetField(0, sqlReader.GetValue(0)) ' TenantNdx
                    row.SetField(1, sqlReader.GetValue(1)) ' DoorGroupNdx
                    row.SetField(2, sqlReader.GetValue(2)) ' DoorGroupName
                    row.SetField(3, sqlReader.GetValue(3)) ' PriorityOrder
                    row.SetField(4, sqlReader.GetValue(4)) ' DoorSchedule
                    GetCardRightAssignments.AddCardGroupsRow(row)
                End While
                sqlReader.Close()
            Catch ex As Exception
                MsgBox("GetCardRightAssignments: " & ex.Message, MsgBoxStyle.Critical, "Erreur")
                Me.Log("Error", "in GetCardRightAssignments", ex.StackTrace)
                GetCardRightAssignments = Nothing
                If Not IsNothing(sqlReader) Then
                    sqlReader.Close()
                End If
            End Try
        End Function ' GetCardRightAssignments

        ' V1.1
        Private Function GetDoorsForGroup(ByVal GroupID As Integer, ByVal Transaction As SqlClient.SqlTransaction) As Collections.ArrayList
            Dim sqlStmt As String
            Dim sqlCmd As SqlClient.SqlCommand
            Dim sqlReader As SqlClient.SqlDataReader = Nothing
            GetDoorsForGroup = New Collections.ArrayList
            sqlStmt = "SELECT ChildGroupNdx FROM [InetDB].[dbo].[XRefGroupDoor] WHERE ParentGroupNdx = " + GroupID.ToString
            Try
                sqlCmd = New SqlClient.SqlCommand(sqlStmt, DBConnection)
                sqlCmd.Transaction = Transaction
                sqlReader = sqlCmd.ExecuteReader
                While sqlReader.Read
                    GetDoorsForGroup.Add(sqlReader.GetInt32(0))
                End While
                sqlReader.Close()
            Catch ex As Exception
                MsgBox("GetDoorsForGroup: " & ex.Message, MsgBoxStyle.Critical, "Erreur")
                Me.Log("Error", "in GetDoorsForGroup", ex.StackTrace)
                If Not IsNothing(sqlReader) Then
                    sqlReader.Close()
                End If
            End Try
        End Function
        
        Public Sub ApplyGroupChanges(ByRef Table As DataSet1.CardGroupsDataTable, ByVal CardNo As Integer)
            Dim sqlStmt As String
            Dim sqlCmd As New SqlClient.SqlCommand()
            Dim rowCount As Integer
            Dim NextPriorityOrder As Integer
            Dim Transaction As SqlClient.SqlTransaction = Nothing
            Dim DoorsToBeRemoved As ArrayList
            Try
                Transaction = DBConnection.BeginTransaction()
                sqlCmd.Connection = DBConnection
                sqlCmd.Transaction = Transaction
                DoorsToBeRemoved = New ArrayList
                If Not IsNothing(Table.GetChanges(DataRowState.Deleted)) Then
                    For Each row As DataSet1.CardGroupsRow In Table.GetChanges(DataRowState.Deleted).Rows

                        For Each Door As Integer In GetDoorsForGroup(CType(row.Item("DoorGroupNdx", DataRowVersion.Original), Integer), Transaction)
                            If Not DoorsToBeRemoved.Contains(Door) Then
                                DoorsToBeRemoved.Add(Door)
                            End If
                        Next

                        ' remove group in DB
                        sqlStmt = "DELETE FROM [InetDb].[dbo].[XRefIndivGroupDoor]" & _
                                    "WHERE IndivNdx = " & CardNo & " " & _
                                    "AND DoorGroupNdx = " & row.Item("DoorGroupNdx", DataRowVersion.Original).ToString
                        sqlCmd.CommandText = sqlStmt
                        Me.Log("Info", "Card " & CardNo.ToString & ": removing group " & row.Item("DoorGroupName", DataRowVersion.Original).ToString)
                        rowCount = sqlCmd.ExecuteNonQuery
                        ' Cannot validate because of trigger on the table
                        'If rowCount <> 1 Then
                        '    Throw (New Exception("ApplyGroupChanges: DELETE with rowcount = " & rowCount.ToString))
                        'End If
                    Next
                    ' reasign PriorityOrder in case there is gaps
                    NextPriorityOrder = ReassignedPriorityOrder(CardNo, Transaction)

                    ' check to prevent removal of doors covered by other groups
                    If Not IsNothing(Table.GetChanges(DataRowState.Unchanged)) Then
                        For Each row As DataSet1.CardGroupsRow In Table.GetChanges(DataRowState.Unchanged).Rows
                            For Each Door As Integer In GetDoorsForGroup(row.DoorGroupNdx, Transaction)
                                If DoorsToBeRemoved.Contains(Door) Then
                                    DoorsToBeRemoved.Remove(Door)
                                End If
                            Next
                        Next
                    End If

                    ' remove door in controllers
                    For Each Door As Integer In DoorsToBeRemoved
                        InsertDownloadPending(CardNo, Door, AddOrRemoveEnum.Remove, Transaction)
                    Next
                End If

                If Not IsNothing(Table.GetChanges(DataRowState.Added)) Then
                    For Each row As DataSet1.CardGroupsRow In Table.GetChanges(DataRowState.Added).Rows
                        sqlStmt = "INSERT INTO [InetDb].[dbo].[XRefIndivGroupDoor]" & _
                                    "(TenantNdx, IndivNdx, DoorGroupNdx, PriorityOrder, DoorSchedule)" & _
                                    "VALUES (" & _
                                    "1, " & CardNo.ToString & ", " & row.DoorGroupNdx.ToString & ", " & NextPriorityOrder.ToString & ", 0" & _
                                    ")"
                        Me.Log("Info", "Card " & CardNo.ToString & ": adding group " & row.DoorGroupName)
                        Debug.Print(sqlStmt)
                        sqlCmd.CommandText = sqlStmt
                        sqlCmd.ExecuteNonQuery()
                        NextPriorityOrder += 1
                        For Each door As Integer In GetDoorsForGroup(row.DoorGroupNdx, Transaction)
                            InsertDownloadPending(CardNo, door, AddOrRemoveEnum.Add, Transaction)
                        Next
                    Next
                End If

                Transaction.Commit()
            Catch ex As Exception
                MsgBox("ApplyGroupChanges: " & ex.Message, MsgBoxStyle.Critical, "Erreur")
                Me.Log("Error", "in ApplyGroupChanges", ex.StackTrace)
                If Not IsNothing(Transaction) Then
                    Transaction.Rollback()
                End If
            End Try
        End Sub ' ApplyGroupChanges
        Private Function ReassignedPriorityOrder(ByVal CardNo As Integer, ByVal Transaction As SqlClient.SqlTransaction) As Integer
            ' returns the next PriorityOrder
            ' Throws and exception in case of error as called in a Try/Catch context
            ' The calling context will rollback changes if there is an exception
            Dim sqlStmt As String
            Dim sqlCmd As SqlClient.SqlCommand
            Dim sqlReader As SqlClient.SqlDataReader = Nothing
            Dim SqlStmtToExecute As New Collections.Queue
            Dim t As String
            Try
                ReassignedPriorityOrder = 1
                sqlStmt = "SELECT DoorGroupNdx, PriorityOrder " & _
                            "FROM [InetDb].[dbo].[XRefIndivGroupDoor] " & _
                            "WHERE IndivNdx = " & CardNo.ToString & " " & _
                            "ORDER BY PriorityOrder"
                sqlCmd = New SqlClient.SqlCommand(sqlStmt, DBConnection)
                sqlCmd.Transaction = Transaction
                sqlReader = sqlCmd.ExecuteReader
                While sqlReader.Read
                    If sqlReader.GetByte(1) <> ReassignedPriorityOrder Then
                        t = "UPDATE [InetDb].[dbo].[XRefIndivGroupDoor] " & _
                            "SET PriorityOrder = " & ReassignedPriorityOrder.ToString & " " & _
                            "WHERE IndivNdx = " & CardNo.ToString & " " & _
                            "AND DoorGroupNdx = " & sqlReader.GetInt32(0).ToString
                        SqlStmtToExecute.Enqueue(t)
                    End If
                    ReassignedPriorityOrder += 1
                End While
                sqlReader.Close()
                While SqlStmtToExecute.Count > 0
                    t = CType(SqlStmtToExecute.Dequeue(), String)
                    Debug.WriteLine(t)
                    sqlCmd.CommandText = t
                    Me.Log("Info", t)
                    sqlCmd.ExecuteScalar()
                End While
            Catch ex As Exception
                If Not IsNothing(sqlReader) Then
                    sqlReader.Close()
                End If
                Me.Log("Error", "in ReassignedPriorityOrder", ex.StackTrace)
                Throw (New Exception("(in ReassignedPriorityOrder): " & ex.Message))
            End Try
        End Function
        Private Enum AddOrRemoveEnum
            Add
            Remove
        End Enum

        ' V 1.1
        Private Sub InsertDownloadPending(ByVal CardNo As Integer, ByVal DoorID As Integer, ByVal AddOrRemove As AddOrRemoveEnum, ByVal Transaction As SqlClient.SqlTransaction)
            Dim sqlStmt As String
            Dim sqlCmd As SqlClient.SqlCommand

            ' @P1,@P2,@P3,@P4)', N'@P1 int,@P2 varbinary(8),@P3 tinyint,@P4 varbinary(5)', 532613, 0x00000000000103FD, 5, 0x040103FD00"
            Try
                sqlStmt = "INSERT INTO [InetDb].[dbo].[DownloadPending] ([PointId],[BinId],[Action],[Data]) VALUES ("
                sqlStmt = sqlStmt & DoorID & ", "
                sqlStmt = sqlStmt & "0x" & CType(CardNo + 65536, Integer).ToString("X16") & ", "
                If AddOrRemove = AddOrRemoveEnum.Add Then
                    sqlStmt = sqlStmt & "2, " ' Action
                    '                         0x4 0000 0000
                    sqlStmt = sqlStmt & "0x" & CType(17179869184 + (CardNo + 65536) * 256 + 1, Long).ToString("X10")
                Else
                    sqlStmt = sqlStmt & "5, " ' Action
                    '                         0x4 0000 0000
                    sqlStmt = sqlStmt & "0x" & CType(17179869184 + (CardNo + 65536) * 256, Long).ToString("X10")
                End If
                sqlStmt = sqlStmt & ");"

                '' IdWorks_Notify 'IdWorks Update', @TenantNdx, @IndivId
                sqlStmt = "[InetDB].[dbo].[IdWorks_Notify] 'IdWorks Update', 1, " & CardNo.ToString
                sqlCmd = New SqlClient.SqlCommand(sqlStmt, DBConnection)
                sqlCmd.Transaction = Transaction
                Debug.WriteLine(sqlStmt)
                Log("INFO", "InsertDownloadPending", sqlStmt)
                sqlCmd.CommandText = sqlStmt
                sqlCmd.ExecuteNonQuery()

                Me.InsertEvent(CardNo, DoorID, Transaction)
            Catch ex As Exception
                MsgBox("InsertDownloadPending: " & ex.Message, MsgBoxStyle.Critical, "Erreur")
                Me.Log("Error", "in InsertDownloadPending", ex.StackTrace)
                Throw (New Exception("(in InsertDownloadPending): " & ex.Message))
            End Try
        End Sub
        Private Sub InsertEvent(ByVal CardNo As Integer, ByVal DoorID As Integer, ByVal Transaction As SqlClient.SqlTransaction)
            Dim sqlStmt As String
            Dim sqlCmd As SqlClient.SqlCommand

            ' Note: will create the [DPURestore] row via a trigger on the [Events] table

            Try
                sqlStmt = "[InetDb].[dbo].[InsertEvent]"
                ' Getdate(), '', 1, 524449, 255, 33, 0, 3, 0, 0, 128, 0.000000000000000e+000, '', '', 'CPAC', '001-01428', 0, 1, 0, 0, 0, 4, @P1 output, @P2 output, @P3 output, @P4 output"

                ' DPUSpecProc = 4 - must mean something
                sqlCmd = New SqlClient.SqlCommand(sqlStmt, DBConnection)
                sqlCmd.Transaction = Transaction
                sqlCmd.CommandType = CommandType.StoredProcedure
                ' Parameters:
                sqlCmd.Parameters.AddWithValue("@EventTime", Date.Now)
                sqlCmd.Parameters.AddWithValue("@MessageText", "AccessCtrlRightMgmt")
                sqlCmd.Parameters.AddWithValue("@HostNum", 1)
                sqlCmd.Parameters.AddWithValue("@PointAddr", DoorID) ' Doors as defined in [DoorGroups]
                sqlCmd.Parameters.AddWithValue("@SiteNum", 255) ' constant - for now...
                sqlCmd.Parameters.AddWithValue("@EventTypeNdx", 33) ' 33 is Edit-Individual
                sqlCmd.Parameters.AddWithValue("@StatusByte", 0) ' constant
                sqlCmd.Parameters.AddWithValue("@Priority", 1) ' constant
                sqlCmd.Parameters.AddWithValue("@DistGroup", 0)
                sqlCmd.Parameters.AddWithValue("@CellNdx", 0) ' constant
                sqlCmd.Parameters.AddWithValue("@DistMask", 255)
                sqlCmd.Parameters.AddWithValue("@FloatValue", 0.0)
                sqlCmd.Parameters.AddWithValue("@TextField", "")
                sqlCmd.Parameters.AddWithValue("@ControlDescr", "")
                sqlCmd.Parameters.AddWithValue("@ControlSource", "EK")
                sqlCmd.Parameters.AddWithValue("@DeviceName", "001-" & CardNo.ToString("D5"))
                sqlCmd.Parameters.AddWithValue("@AlarmType", 0)
                sqlCmd.Parameters.AddWithValue("@FormatType", 1)
                sqlCmd.Parameters.AddWithValue("@EventHasAlarm", 0)
                sqlCmd.Parameters.AddWithValue("@KeyCardNumber", 0)
                sqlCmd.Parameters.AddWithValue("@TenantNdx", 0)
                sqlCmd.Parameters.AddWithValue("@DpuSpecProc", 4) ' DPUSpecProc = 1 - must mean something
                sqlCmd.Parameters.AddWithValue("@AutoPage", 0)
                sqlCmd.Parameters.AddWithValue("@TextOutput", "")
                sqlCmd.Parameters.AddWithValue("@AlarmCamera", 0)
                sqlCmd.Parameters.AddWithValue("@AlarmCameraDur", 0)

                'sqlCmd.Parameters.Add(New SqlClient.SqlParameter("@AutoPage", SqlDbType.Int, 4))
                'sqlCmd.Parameters.Add(New SqlClient.SqlParameter("@TextOutput", SqlDbType.VarChar, 127))
                'sqlCmd.Parameters.Add(New SqlClient.SqlParameter("@AlarmCamera", SqlDbType.SmallInt, 2))
                'sqlCmd.Parameters.Add(New SqlClient.SqlParameter("@AlarmCameraDur", SqlDbType.SmallInt, 2))

                Debug.WriteLine(sqlStmt)
                Log("INFO", "InsertEvent", sqlStmt)
                sqlCmd.ExecuteNonQuery()
            Catch ex As Exception
                MsgBox("InsertEvent: " & ex.Message, MsgBoxStyle.Critical, "Erreur")
                Me.Log("Error", "in InsertEvent", ex.StackTrace)
                Throw (New Exception("(in InsertEvent): " & ex.Message))
            End Try
        End Sub
        Public Sub Log(ByVal LogType As String, ByVal Message As String, Optional ByVal ErrorMsg As String = "")
            Static LogDbConnection As SqlClient.SqlConnection
            If IsNothing(LogDbConnection) Then
                LogDbConnection = New SqlClient.SqlConnection(ConnectionString)
                LogDbConnection.Open()
            End If
            Dim sqlStmt As String
            Dim sqlCmd As SqlClient.SqlCommand
            Try
                sqlStmt = "INSERT INTO [KNS_AccessControl].[dbo].[ApplicationLog]" & _
                            "([Application], [LogType], " & _
                            "[Message], [User], [Workstation], [ErrorMsg])" & _
                            "VALUES ( 'AccessControlRightMgmt', " & _
                            "'" & LogType & "'," & _
                            "'" & Message.Replace("'", "''") & "'," & _
                            "'" & Environment.UserName & "'," & _
                            "'" & Environment.MachineName & "'," & _
                            "'" & Left(ErrorMsg.Replace("'", "''"), 1024) & "')"
                sqlCmd = New SqlClient.SqlCommand(sqlStmt, LogDbConnection)
                sqlCmd.ExecuteNonQuery()
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Exclamation, "Erreur")
            End Try
        End Sub

        'Permet que la sauvegarde soit impactée au niveau de tous les controleurs d'accès de manière immédiate
        Public Sub ApplyChangesToInetCache(ByRef Cards As DataSet1.CardsDataTable, ByVal IndivID As String)
            Dim sqlStmt As String
            Dim sqlCmd As SqlClient.SqlCommand
            Dim Transaction As SqlClient.SqlTransaction = Nothing
            Dim DoorsGroup As DataSet1.CardGroupsDataTable

            Try
                'on récupère les groupes dispo de l'utilisateur
                DoorsGroup = GetCardRightAssignments(Integer.Parse(IndivID))
                Transaction = DBConnection.BeginTransaction()
                For Each row As DataSet1.CardGroupsRow In DoorsGroup.Rows
                    For Each door As Integer In GetDoorsForGroup(row.DoorGroupNdx, Transaction)
                        'InsertDownloadPending(Integer.Parse(IndivID), door, AddOrRemoveEnum.Add, Transaction)
                        Me.InsertEvent(Integer.Parse(IndivID), door, Transaction)

                        'Désactivation de la Carte (card, not individual)
                        For Each card As DataSet1.CardsRow In Cards
                            sqlStmt = "INSERT INTO [InetDb].[dbo].[DownloadPending] ([PointId],[BinId],[Action],[Data]) VALUES ("
                            sqlStmt = sqlStmt & door & ", "
                            sqlStmt = sqlStmt & "0x" & card.MiFareCardNumber & ", 1,"
                            If card.Disabled Then
                                sqlStmt = sqlStmt & "0x0E" & card.MiFareCardNumber & "011ACBC01208)"
                            Else
                                sqlStmt = sqlStmt & "0x0E" & card.MiFareCardNumber & "011ACB401208)"
                            End If
                            sqlCmd = New SqlClient.SqlCommand(sqlStmt, DBConnection)
                            sqlCmd.Transaction = Transaction
                            sqlCmd.ExecuteNonQuery()
                        Next
                    Next
                Next

                

                '' IdWorks_Notify 'IdWorks Update', @TenantNdx, @IndivId
                sqlStmt = "[InetDB].[dbo].[IdWorks_Notify] 'IdWorks Update', 1, " & IndivID
                sqlCmd = New SqlClient.SqlCommand(sqlStmt, DBConnection)
                sqlCmd.Transaction = Transaction
                sqlCmd.ExecuteNonQuery()
                Transaction.Commit()

            Catch ex As Exception
                MsgBox("ApplyChangesToInetCache: " & ex.Message, MsgBoxStyle.Critical, "Erreur")
                Me.Log("Error", "in ApplyChangesToInetCache", ex.StackTrace)
                If Not IsNothing(Transaction) Then
                    Transaction.Rollback()
                End If
            End Try

        End Sub

        ' Save in database the type of modification made to an individual
        Public Sub SaveIndivActivationChangesReason(ByVal IndivId As String, ByVal changeType As String, ByVal Motif As String)
            Try
                Motif = Motif.Replace("'", "''")
                Dim sqlStmt As String = "INSERT INTO [InetDb].[dbo].[IndivActivationChanges] (ChangeDate, IndivID, ChangeType, Reason, ApplicationUser) " & _
                                        "VALUES (GETDATE()," + IndivId + ", '" + changeType + "', '" + Motif + "', '" + Environment.UserName + "')"
                Dim sqlCmd As New SqlClient.SqlCommand(sqlStmt, DBConnection)
                sqlCmd.ExecuteNonQuery()
            Catch ex As Exception
                MsgBox("SaveIndivActivationChangesReason: " & ex.Message, MsgBoxStyle.Critical, "Erreur")
                Me.Log("Error", "in SaveIndivActivationChangesReason", ex.StackTrace)
            End Try
        End Sub

        'Check if an individual is on site or off site
        Public Function IsIndividualOnSite(ByVal IndivId As String) As Boolean
            Try
                Dim sqlStmt As String = "SELECT	count(*) " & _
                                        "FROM [InetDb].[dbo].Events as B " & _
                                        "INNER JOIN [InetDb].[dbo].Individuals AS I ON I.IndivID = B.IndivNdx " & _
                                        "WHERE(B.FloatValue = 0) " & _
                                        "AND B.EventTypeNdx = 16 " & _
                                        "AND EventID IN (SELECT	MAX(EventID) " & _
                                        "		FROM [InetDb].[dbo].Events As A " & _
                                        "                WHERE(A.FloatValue = 0) " & _
                                        "		GROUP BY IndivNdx) " & _
                                        "AND B.IndivNdx = " + IndivId + ""
                Dim sqlCmd As New SqlClient.SqlCommand(sqlStmt, DBConnection)
                Dim i As Integer
                i = CType(sqlCmd.ExecuteScalar(), Integer)
                If i > 0 Then
                    Return True
                Else
                    Return False
                End If
            Catch ex As Exception
                MsgBox("IsIndividualOnSite: " & ex.Message, MsgBoxStyle.Critical, "Erreur")
                Me.Log("Error", "in IsIndividualOnSite", ex.StackTrace)
            End Try
        End Function
    End Class
End Module
