
Imports System.Data.SqlClient
Imports System.Web.Http

Namespace Controllers
    Public Class cUltimaConsulta
        Public Property DataUltimaConsulta As String
        Public Property MedicoNome As String
        Public Property MedicoCodigo As Integer
    End Class

    Public Class UltimaConsultaController
        Inherits ApiController

        ' GET: api/UltimaConsulta/id
        Public Function GetValues(ByVal id As Integer) As cUltimaConsulta
            Dim sqlReader As SqlDataReader, strSQL As String, cn As New Conexao
            Dim sqlPar As New SqlParameter, colPar As New Collection
            Dim r As New cUltimaConsulta

            sqlPar.DbType = DbType.Int32
            sqlPar.Value = id
            sqlPar.ParameterName = "@id"
            colPar.Add(sqlPar)

            strSQL = "select top 1 data_consulta as DataUltimaConsulta, 
                m.NOME as MedicoNome, m.CODIGO as MedicoCodigo
                from CONSULTA_GERAL  cg
                inner join medico m on m.codigo = cg.MEDICO
                where paciente= @id
                order by cg.DATA_CONSULTA desc"

            sqlReader = cn.OpenReaderWithParam(strSQL, colPar)
            If sqlReader.Read Then
                r.DataUltimaConsulta = sqlReader("DataUltimaConsulta").ToString()
                r.MedicoNome = sqlReader("MedicoNome").ToString()
                r.MedicoCodigo = sqlReader("MedicoCodigo").ToString()
            End If
            sqlReader.Close()
            cn.CloseConection()
            Return r
        End Function
    End Class
End Namespace