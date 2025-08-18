Imports System.Web.Http
Imports System.Data.SqlClient

Namespace Controllers
    Public Class cConfirmaConsulta
        Public Property ID As Integer
        Public Property DATA_CONSULTA As Date
        Public Property HORA As String
        Public Property NOMEPACI As String
        Public Property CELULAR As String
        Public Property TIPOATENDIMENTO As String
        Public Property MEDICONOME As String
        Public Property ENDERECO As String
        Public Property CLINICA As String
    End Class
    Public Class ConfirmaConsulta48hController
        Inherits ApiController

        ' GET: api/ConfirmaConsulta48h
        Public Function GetValues() As List(Of cConfirmaConsulta)
            Dim sqlReader As SqlDataReader, strSQL As String, cn As New Conexao
            Dim r As New List(Of cConfirmaConsulta)()

            strSQL = "select ID, DATA_CONSULTA, Hora, NOMEPACI, '55' + Celular as Celular
                        , case when ac.TIPO_ATENDIMENTO= 1 then 'Prim.Consulta' when ac.TIPO_ATENDIMENTO= 2 then 'Consulta' when ac.TIPO_ATENDIMENTO=3 then 'Retorno' when ac.TIPO_ATENDIMENTO=6 then 'Puericultura' when ac.TIPO_ATENDIMENTO=7 then 'Emergencia' when ac.TIPO_ATENDIMENTO=8 then 'Recado' when ac.TIPO_ATENDIMENTO=9 then 'Cirurgia' end as TipoAtendimento
                        , DATA_CONSULTA, Hora as HoraConsulta, m.NOME as MedicoNome, rtrim(c.ENDERECO) + ' ' + rtrim(str(c.NUMERO)) as ENDERECO, c.NOME as CLINICA
                        from Agenda_clinica ac
                        inner join medico m on m.codigo = ac.medico	
                        inner join CLIENTE c on c.codigo = 3
                        where year(DATA_CONSULTA) = YEAR(GETdate()) and month(data_consulta)= month(getdate()) and DAY(data_consulta) = day(getdate()) + 2
                        and len(rtrim(celular))>0
                        and isnull(nomepaci, '') > ''
                        and TIPO_ATENDIMENTO in (1,2,3,6,7)
                        and status <> 'CON'
                        order by HORA"

            sqlReader = cn.OpenReader(strSQL)
            While sqlReader.Read
                r.Add(New cConfirmaConsulta() With {
                    .CELULAR = sqlReader("CELULAR").ToString(),
                    .CLINICA = sqlReader("CLINICA").ToString(),
                    .DATA_CONSULTA = sqlReader("DATA_CONSULTA").ToString(),
                    .ENDERECO = sqlReader("ENDERECO").ToString(),
                    .HORA = sqlReader("HORA").ToString(),
                    .ID = sqlReader("ID").ToString(),
                    .MEDICONOME = sqlReader("MEDICONOME").ToString(),
                    .NOMEPACI = sqlReader("NOMEPACI").ToString(),
                    .TIPOATENDIMENTO = sqlReader("TIPOATENDIMENTO").ToString()
                })
            End While
            sqlReader.Close()
            cn.CloseConection()
            Return r
        End Function

        ' GET: api/ConfirmaConsulta48h/5
        Public Function GetValue(ByVal id As Integer) As String
            Return "value"
        End Function

        ' POST: api/ConfirmaConsulta48h
        Public Sub PostValue(<FromBody()> ByVal value As String)

        End Sub

        ' PUT: api/ConfirmaConsulta48h/5
        Public Sub PutValue(ByVal id As Integer, <FromBody()> ByVal value As String)

        End Sub

        ' DELETE: api/ConfirmaConsulta48h/5
        Public Sub DeleteValue(ByVal id As Integer)

        End Sub
    End Class
End Namespace