Imports System.Data.SqlClient
Imports System.Web.Http
Imports System.Web.Mvc

Namespace Controllers
    Public Class HorariosLivres15Controller
        Inherits ApiController
        ' GET: api/ConvenioWeb
        Public Function GetValues(ByVal id As Integer) As List(Of cHorariosLivres)
            Dim sqlReader As SqlDataReader, strSQL As String, cn As New Conexao
            Dim r As New List(Of cHorariosLivres)
            Dim sqlPar As New SqlParameter, colPar As New Collection

            sqlPar.DbType = DbType.Int32
            sqlPar.Value = id
            sqlPar.ParameterName = "@med"
            colPar.Add(sqlPar)

            strSQL = strSQLHorariosLivres.Replace("{d}", "15")

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