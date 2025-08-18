Imports System.Net
Imports System.Web.Http

Namespace Controllers
    Public Class cConfirmacaoConsulta
        Public Property id As Integer
        Public Property response As String  'CONFIRMAR ou DESMARCAR
        Public Property type As String
    End Class

    Public Class ConfirmaConsultaController
        Inherits ApiController

        ' GET: api/ConfirmaConsulta
        Public Function GetValues() As IEnumerable(Of String)
            Return New String() {"value1", "value2"}
        End Function

        ' GET: api/ConfirmaConsulta/5
        Public Function GetValue(ByVal id As Integer) As String
            Return "value"
        End Function

        ' POST: api/ConfirmaConsulta
        Public Sub PostValue(<FromBody()> ByVal obj As cConfirmacaoConsulta)
            Dim strSQL As String, cn As New Conexao

            If obj.response = "CONFIRMAR" Then
                '-- No retorno confirmando consulta
                'update AGENDA_CLINICA set STATUS = 'CON' where id = id

                'insert into TRILHA_AGENDA (MEDICO, Data, PERIODO, HORA, EVENTO, DATA_ALTERACAO, FUNCIONARIO, HISTORICO, TipoAgenda)
                'values(MEDICO, Data, PERIODO, HORA, 4, getdate(), 0, 'WhatsAPP Confirma', 1)

            Else

                '-- No retorno Cancelando consulta
                'Update AGENDA_CLINICA set NOMEPACI='',PACIENTE=0,TIPO_ATENDIMENTO=0,FONEPACI='',TONOMETRIA=0,MAPRETINA=0, ACUIDADE = 0,CONVENIO=0, STATUS='" & Trim(CONVENIO_HORARIO) & "', SECRETARIA=0,OBSERVACAO='',REQUISICAO=0,REQUISICAO_TONO=0,REQUISICAO_MAP=0, REQUISICAO_ACUIDADE = 0,UTILIZADO=0, TIPO_LENTE_USO=0, TIPO_ATENDIMENTO_ABRANGE = -1, AUTORIZACAO = '', RG = '',GUIA=0, NRCONVENIO='', GONIOSCOPIA=0, MOTILIDADE= 0, REQUISICAO_GONIOSCOPIA= 0, REQUISICAO_MOTILIDADE= 0, CodigoClube=0, EsteticistaAtividade=0 "     ', CONVENIO_PLANO=''
                'Where ID = ID

                'insert into TRILHA_AGENDA (MEDICO, Data, PERIODO, HORA, EVENTO, DATA_ALTERACAO, FUNCIONARIO, HISTORICO, TipoAgenda)
                'values(MEDICO, Data, PERIODO, HORA, 2, getdate(), 0, 'Paciente: ' & NomePaciente & ' Motivo: WhatsAPP Cancelou', 1)

            End If


        End Sub

        ' PUT: api/ConfirmaConsulta/5
        Public Sub PutValue(ByVal id As Integer, <FromBody()> ByVal value As String)

        End Sub

        ' DELETE: api/ConfirmaConsulta/5
        Public Sub DeleteValue(ByVal id As Integer)

        End Sub
    End Class
End Namespace