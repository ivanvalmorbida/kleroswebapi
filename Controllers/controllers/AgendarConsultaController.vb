Imports System.Data.SqlClient
Imports System.Web.Http
Imports System.Web.Mvc

Namespace Controllers
    Public Class AgendarConsultaController
        Inherits ApiController

        Public Class cAgendarConsulta
            Public Property IdAgenda As Integer
            Public Property IdPaciente As Integer
            Public Property IdConvenio As Integer
            Public Property NomePaciente As String
            Public Property Celular As String
            Public Property CPFPaciente As String
            Public Property DataNascim As Date
        End Class

        Public Class cAgendarConsultaRet
            Public Property IdAgenda As Integer
            Public Property MedicoNome As String
            Public Property Convenio As String
            Public Property NomePaciente As String
            Public Property Celular As String
            Public Property CPFPaciente As String
            Public Property DataConsulta As Date
        End Class

        Public Function GetValues(IdAgenda As Integer) As cAgendarConsultaRet
            Dim sqlReader As SqlDataReader, strSQL As String, cn As New Conexao
            Dim r As New cAgendarConsultaRet

            strSQL = $"select ID, convert(char, DATA_CONSULTA, 103) as DataConsulta, Hora, NOMEPACI, '55' + Celular as Celular
                ,m.NOME as MedicoNome, co.APELIDO as Convenio, ac.CNPJCPF
                from Agenda_clinica ac
                inner join medico m on m.codigo = ac.medico	
                inner join convenio co on co.CODIGO=CONVENIO
                where id={IdAgenda}"

            sqlReader = cn.OpenReader(strSQL)
            If sqlReader.Read Then
                r.IdAgenda = sqlReader("ID").ToString()
                r.Celular = sqlReader("Celular").ToString()
                r.DataConsulta = sqlReader("DataConsulta").ToString()
                r.NomePaciente = sqlReader("NOMEPACI").ToString()
                r.MedicoNome = sqlReader("MedicoNome").ToString()
                r.Convenio = sqlReader("Convenio").ToString()
                r.CPFPaciente = sqlReader("CNPJCPF").ToString()
            End If
            sqlReader.Close()
            cn.CloseConection()
            Return r
        End Function

        Public Function PostValue(ByVal obj As cAgendarConsulta) As IHttpActionResult
            Dim sqlReader As SqlDataReader, strSQL As String, cn As New Conexao
            Dim sqlPar As New SqlParameter, colPar As New Collection
            Dim r As New cPacienteCPF

            sqlPar.DbType = DbType.String
            sqlPar.Value = obj.IdAgenda
            sqlPar.ParameterName = "@id"
            colPar.Add(sqlPar)

            strSQL = "Select id from Agenda_Clinica where id=@id And isnull(NomePaci, '') = ''"

            sqlReader = cn.OpenReaderWithParam(strSQL, colPar)
            If sqlReader.Read Then
                sqlReader.Close()

                sqlPar = New SqlParameter
                sqlPar.DbType = DbType.Int32
                sqlPar.Value = obj.IdPaciente
                sqlPar.ParameterName = "@paciente"
                colPar.Add(sqlPar)
                sqlPar = New SqlParameter
                sqlPar.DbType = DbType.String
                sqlPar.Value = obj.NomePaciente
                sqlPar.ParameterName = "@Nome"
                colPar.Add(sqlPar)
                sqlPar = New SqlParameter
                sqlPar.DbType = DbType.Int32
                sqlPar.Value = obj.IdConvenio
                sqlPar.ParameterName = "@Convenio"
                colPar.Add(sqlPar)
                sqlPar = New SqlParameter
                sqlPar.DbType = DbType.String
                sqlPar.Value = obj.Celular
                sqlPar.ParameterName = "@Celular"
                colPar.Add(sqlPar)
                sqlPar = New SqlParameter
                sqlPar.DbType = DbType.Date
                sqlPar.Value = obj.DataNascim
                sqlPar.ParameterName = "@DataNascim"
                colPar.Add(sqlPar)
                sqlPar = New SqlParameter
                sqlPar.DbType = DbType.String
                sqlPar.Value = obj.CPFPaciente
                sqlPar.ParameterName = "@CPF"
                colPar.Add(sqlPar)

                cn.ExecuteWithParam("update AGENDA_CLINICA Set NOMEPACI=@Nome, CONVENIO=@Convenio,
                Celular=@Celular, PACIENTE=@paciente, 
                OBSERVACAO=@DataNascim, STATUS='WA3', TIPO_ATENDIMENTO=2, SECRETARIA=0, 
                TIPO_ATENDIMENTO_ABRANGE=4, Nacionalidade=0, CNPJCPF=@CPF where id=@id", colPar)

                If cn.MSG <> "" Then
                    Return BadRequest(cn.MSG)
                Else
                    cn.Execute("insert into TRILHA_AGENDA (MEDICO, Data, PERIODO, HORA, 
                        EVENTO, DATA_ALTERACAO, FUNCIONARIO, HISTORICO, TipoAgenda)
                        select MEDICO, DATA_CONSULTA, PERIODO, HORA, 4 evento, getdate() alterado, 
                        0 funcionario, 'Paciente: '+NOMEPACI+' Motivo: Novo -> WhatsAPP Agenda Consulta' historico, 1 tipo from AGENDA_CLINICA
                        where id=" & obj.IdAgenda)
                End If
            Else
                Return BadRequest("Horario já utilizado")
                sqlReader.Close()
            End If

            cn.CloseConection()
            Return Ok("Consulta agendada com sucesso")
        End Function
    End Class
End Namespace