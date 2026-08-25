Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Xml

Public NotInheritable Class Conexao
    Private DBCon As New SqlConnection
    Private strConection As String = "Server=<Server>;Database=<DB>;User ID=SA;Password=CTInfo&Kleros4002;Trusted_Connection=False;Connection Timeout=30;Pooling=False;"
    Private _MSG As String

    Public ReadOnly Property MSG() As String
        Get
            Return _MSG
        End Get
    End Property

    Public ReadOnly Property Connection() As SqlConnection
        Get
            Return DBCon
        End Get
    End Property

    Public Property StringConection() As String
        Get
            Return strConection
        End Get
        Set(ByVal value As String)
            strConection = value
        End Set
    End Property

    Public Sub OpenConection()
        If DBCon.State = ConnectionState.Closed Then
            Dim fluxoTexto As IO.StreamReader
            Dim winDir, strX As String

            winDir = System.Environment.GetEnvironmentVariable("windir")
            strX = ""
            If IO.File.Exists(winDir & "\Kleros_Clinica.ini") Then
                fluxoTexto = New IO.StreamReader(winDir & "\Kleros_Clinica.ini")
                strX = fluxoTexto.ReadLine
                While True
                    If Left(strX, 4) = "PATH" Then
                        Exit While
                    End If
                    Try
                        strX = fluxoTexto.ReadLine
                    Catch ex As Exception
                        Exit While
                    End Try
                End While
                fluxoTexto.Close()
            End If

            strX = Right(strX, Len(strX) - 7)
            strX = strX & "Config.xml"

            Dim objXml As New XmlDocument
            objXml.Load(strX)

            strX = objXml.SelectSingleNode("config").ChildNodes(0).ChildNodes(0).InnerText

            Dim matriz() As String = strX.Split(";")
            strX = matriz(1)
            strX = Right(strX, Len(strX) - 12)
            strConection = strConection.Replace("<Server>", strX)

            strX = matriz(2)
            strX = Right(strX, Len(strX) - 16)
            strConection = strConection.Replace("<DB>", strX)

            DBCon = New SqlConnection(strConection)
            DBCon.Open()
        End If
    End Sub

    Public Sub CloseConection()
        If DBCon.State = ConnectionState.Open Then
            DBCon.Close()
        End If
    End Sub

    Public Function OpenReader(ByVal strSQL As String) As SqlDataReader
        If DBCon.State = ConnectionState.Closed Then
            OpenConection()
        End If

        Dim SQLCommand As New SqlCommand(strSQL, DBCon)
        Dim SQLReader As SqlDataReader = SQLCommand.ExecuteReader()
        Return SQLReader
    End Function

    Public Function OpenDataSet(ByVal strSQL As String, ByVal strTabela As String) As DataSet
        If DBCon.State = ConnectionState.Closed Then
            OpenConection()
        End If

        Dim SQLDataAd As New SqlDataAdapter(strSQL, DBCon)
        Dim myDataSet As New DataSet()
        SQLDataAd.Fill(myDataSet, strTabela)
        Return myDataSet

        DBCon.Dispose()
    End Function

    Public Function DataAdapter(ByVal strSQL As String) As SqlDataAdapter
        If DBCon.State = ConnectionState.Closed Then
            OpenConection()
        End If
        Dim SQLDataAd As New SqlDataAdapter(strSQL, DBCon)
        Return SQLDataAd
    End Function

    Public Sub Execute(ByVal strSQL As String)
        If DBCon.State = ConnectionState.Closed Then
            OpenConection()
        End If

        Dim SQLComand As New SqlCommand(strSQL, DBCon)
        Try
            _MSG = ""
            SQLComand.ExecuteNonQuery()
        Catch e As Exception
            _MSG = e.Message
        End Try
    End Sub

    Public Sub ExecuteWithParam(ByVal strSQL As String, ByVal SQLPar As Collection)
        If DBCon.State = ConnectionState.Closed Then
            OpenConection()
        End If

        Dim SQLCommandWithPar As New SqlCommand()
        Dim xPar As SqlParameter

        SQLCommandWithPar.Connection = DBCon
        SQLCommandWithPar.CommandText = strSQL
        For i As Integer = 1 To SQLPar.Count
            xPar = SQLPar(i)
            SQLCommandWithPar.Parameters.AddWithValue(xPar.ParameterName, xPar.Value)
        Next
        Try
            _MSG = ""
            SQLCommandWithPar.ExecuteNonQuery()
        Catch e As Exception
            _MSG = e.Message
        End Try
    End Sub

    Public Function OpenReaderWithParam(ByVal strSQL As String, ByVal SQLPar As Collection) As SqlDataReader
        Dim rd As SqlDataReader

        If DBCon.State = ConnectionState.Closed Then
            OpenConection()
        End If

        Dim SQLCommandWithPar As New SqlCommand()
        Dim xPar As SqlParameter

        SQLCommandWithPar.Connection = DBCon
        SQLCommandWithPar.CommandText = strSQL
        For i As Integer = 1 To SQLPar.Count
            xPar = SQLPar(i)
            SQLCommandWithPar.Parameters.AddWithValue(xPar.ParameterName, xPar.Value)
        Next
        rd = SQLCommandWithPar.ExecuteReader()
        Return rd
        SQLCommandWithPar.Dispose()
    End Function

    Public Function OpenDataSetWithParam(ByVal strSQL As String, ByVal strTabela As String, ByVal SQLPar As Collection) As DataSet
        If DBCon.State = ConnectionState.Closed Then
            OpenConection()
        End If

        Dim SQLCommandWithPar As New SqlCommand()
        Dim xPar As SqlParameter

        SQLCommandWithPar.Connection = DBCon
        SQLCommandWithPar.CommandText = strSQL
        For i As Integer = 1 To SQLPar.Count
            xPar = SQLPar(i)
            SQLCommandWithPar.Parameters.AddWithValue(xPar.ParameterName, xPar.Value)
        Next

        Dim SQLDataAd As New SqlDataAdapter(SQLCommandWithPar)
        Dim myDataSet As New DataSet()

        SQLDataAd.Fill(myDataSet, strTabela)
        Return myDataSet
        DBCon.Dispose()
    End Function
End Class








