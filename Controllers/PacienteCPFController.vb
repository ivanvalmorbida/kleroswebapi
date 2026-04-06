Imports System.Data.SqlClient
Imports System.Web.Http
Imports System.Web.Mvc

Namespace Controllers
    Public Class cPacienteCPF
        Public Property ConvenioCodigo As Integer
        Public Property ConvenioNome As String
        Public Property PacienteCelular As String
        Public Property PacienteCodigo As Integer
        Public Property PacienteNome As String
        Public Property PacienteDataNascim As String

    End Class

    Public Class PacienteCPFController
        Inherits ApiController

        ' GET: api/PacienteCPF/123456789
        Public Function GetValue(ByVal id As String) As cPacienteCPF
            Dim sqlReader As SqlDataReader, strSQL As String, cn As New Conexao
            Dim sqlPar As New SqlParameter, colPar As New Collection
            Dim r As New cPacienteCPF

            sqlPar.DbType = DbType.String
            sqlPar.Value = id
            sqlPar.ParameterName = "@cpf"
            colPar.Add(sqlPar)

            strSQL = "select p.codigo as PacienteCodigo, p.NOME as PacienteNome, 
                DATA_NASCIM as PacienteDataNascim, celular as PacienteCelular, 
                c.codigo as ConvenioCodigo, c.nome as ConvenioNome 
                from paciente p
                inner join convenio c on c.codigo = CONVENIO
                where CNPJCPF = @cpf"

            sqlReader = cn.OpenReaderWithParam(strSQL, colPar)
            If sqlReader.Read Then
                r.ConvenioCodigo = sqlReader("ConvenioCodigo").ToString()
                r.ConvenioNome = sqlReader("ConvenioNome").ToString()
                r.PacienteCelular = sqlReader("PacienteCelular").ToString()
                r.PacienteCodigo = sqlReader("PacienteCodigo").ToString()
                r.PacienteNome = sqlReader("PacienteNome").ToString()
                r.PacienteDataNascim = sqlReader("PacienteDataNascim").ToString()
            End If
            sqlReader.Close()
            cn.CloseConection()
            Return r
        End Function
    End Class
End Namespace