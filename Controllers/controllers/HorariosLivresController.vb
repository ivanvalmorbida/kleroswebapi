Imports System.Data.SqlClient
Imports System.Web.Http
Imports System.Web.Mvc

Namespace Controllers
    Public Class cHorariosLivres
        Public Property ID As Integer
        Public Property AgendaDataConsulta As String
        Public Property AgendaDiaSemana As String
        Public Property AgendaPeriodo As String
        Public Property AgendaHora As String
        Public Property AgendaObservacao As String

    End Class
    Public Class HorariosLivresController
        Inherits ApiController
        ' GET: api/HorariosLivres
        Public Function GetValues(ByVal id As Integer, ByVal conv As Integer, dias As Integer) As List(Of cHorariosLivres)
            Dim sqlReader As SqlDataReader, strSQL As String, cn As New Conexao
            Dim r As New List(Of cHorariosLivres)
            Dim sqlPar As New SqlParameter, colPar As New Collection

            sqlPar.DbType = DbType.Int32
            sqlPar.Value = id
            sqlPar.ParameterName = "@med"
            colPar.Add(sqlPar)

            sqlPar = New SqlParameter
            sqlPar.DbType = DbType.Int32
            sqlPar.Value = conv
            sqlPar.ParameterName = "@conv"
            colPar.Add(sqlPar)

            sqlPar = New SqlParameter
            sqlPar.DbType = DbType.Int32
            sqlPar.Value = dias
            sqlPar.ParameterName = "@d"
            colPar.Add(sqlPar)

            strSQL = strSQLHorariosLivres

            sqlReader = cn.OpenReaderWithParam(strSQL, colPar)
            While sqlReader.Read
                r.Add(New cHorariosLivres() With {
                    .ID = sqlReader("id").ToString(),
                    .AgendaDataConsulta = sqlReader("AgendaDataConsulta").ToString(),
                    .AgendaDiaSemana = sqlReader("AgendaDiaSemana").ToString(),
                    .AgendaHora = sqlReader("AgendaHora").ToString(),
                    .AgendaObservacao = sqlReader("AgendaObservacao").ToString(),
                    .AgendaPeriodo = sqlReader("AgendaPeriodo").ToString()
                })
            End While
            sqlReader.Close()
            cn.CloseConection()
            Return r
        End Function
    End Class
End Namespace