Imports System.Web.Http
Imports System.Web.Mvc

Namespace Controllers
    Public Class AgendarConsultaController
        Inherits ApiController
        'IdAgenda
        'IdPaciente
        'NomePaciente
        'IdConvenio
        'Celular
        'DataNascim
        'CPFPaciente

        Public Class cConfirmacaoConsulta
            Public Property id As Integer
            Public Property response As String  'CONFIRMAR ou DESMARCAR
            Public Property type As String
        End Class

        Public Sub PostValue(<FromBody()> ByVal obj As cConfirmacaoConsulta)
            Dim cn As New Conexao

            If obj.response = "CONFIRMAR" Then
                'No retorno confirmando consulta
                cn.Execute("update AGENDA_CLINICA set STATUS = 'CON' where id=" & obj.id)

                cn.Execute("insert into TRILHA_AGENDA (MEDICO, Data, PERIODO, HORA, 
                EVENTO, DATA_ALTERACAO, FUNCIONARIO, HISTORICO, TipoAgenda)
                select MEDICO, DATA_CONSULTA, PERIODO, HORA, 4 evento, getdate() alterado, 
                0 funcionario, 'Paciente: '+NOMEPACI+' Motivo: WhatsAPP Confirma' historico, 1 tipo from AGENDA_CLINICA
                where id=" & obj.id)

            Else
                'No retorno Cancelando consulta
                cn.Execute("Update AGENDA_CLINICA set NOMEPACI='',PACIENTE=0,TIPO_ATENDIMENTO=0,FONEPACI='',
                TONOMETRIA=0,MAPRETINA=0, ACUIDADE = 0,CONVENIO=0, STATUS='', 
                SECRETARIA=0,OBSERVACAO='',REQUISICAO=0,REQUISICAO_TONO=0,REQUISICAO_MAP=0, 
                REQUISICAO_ACUIDADE = 0,UTILIZADO=0, TIPO_LENTE_USO=0, TIPO_ATENDIMENTO_ABRANGE = -1, 
                AUTORIZACAO = '', RG = '',GUIA=0, NRCONVENIO='', GONIOSCOPIA=0, MOTILIDADE= 0, 
                REQUISICAO_GONIOSCOPIA= 0, REQUISICAO_MOTILIDADE= 0, CodigoClube=0, EsteticistaAtividade=0, 
                CONVENIO_PLANO='' Where STATUS not in('FCH','FAT','AGU','ESP','PRE','CON') and ID =" & obj.id)

                cn.Execute("insert into TRILHA_AGENDA (MEDICO, Data, PERIODO, HORA, 
                EVENTO, DATA_ALTERACAO, FUNCIONARIO, HISTORICO, TipoAgenda)
                select MEDICO, DATA_CONSULTA, PERIODO, HORA, 4 evento, getdate() alterado, 
                0 funcionario, 'Paciente: '+NOMEPACI+' Motivo: WhatsAPP Cancelou ' historico, 1 tipo from AGENDA_CLINICA
                where STATUS not in('FCH','FAT','AGU','ESP','PRE','CON') and id=" & obj.id)
            End If
        End Sub

    End Class
End Namespace

'


'FALTA FAZER
'* Se após a seleção do horário, este já tiver sido utilizado, informar o Paciente para selecionar novo horário.
'Select Case AgendaNomePaci from Agenda_Clinica
'where Medico = MedicoCodigoSelecionado And DataConsulta = DataSelecionada And Hora = HoraSelecionada And isnull(NomePaci, '') = ''
'\* Se já tiver registro, informar o Paciente que o horário já está utilizado. Direcionar para escolha de novo horário.

'* Update Agenda Clinica
'update AGENDA_CLINICA
'Set NOMEPACI = PacienteNome, CONVENIO = ConvenioCodigoSelecionado, Celular = PacienteCelular, OBSERVACAO = PacienteDataNascim, STATUS = 'WA1'
', TIPO_ATENDIMENTO = 2, SECRETARIA = 0, TIPO_ATENDIMENTO_ABRANGE = 4, CNPJCPF = PacienteCNPJCPF, Nacionalidade = 0, CNPJCPF = PacienteCPF
'where MEDICO = MedicoCodigoSelecionado And DATA_CONSULTA = DataSelecionada And HORA = HoraSelecionada

'* Insert Trilha
'insert into TRILHA_AGENDA (MEDICO, DATA, PERIODO, HORA, EVENTO, DATA_ALTERACAO, FUNCIONARIO, HISTORICO, TipoAgenda)
'values(MedicoCodigoSelecionado, DataSelecionada, Case when HoraSelecionada < 1200 then 1 else 2 end, HoraSelecionada, 1, getdate(), 0, 
''Anterior -> ' & StatusAnterior & 'Novo -> WhatsAPP Agenda Consulta, 1)

'* Identifica os horários livres do Médico para agendamento (15 dias)
' Select Case convert(Of Char, DATA_CONSULTA, 103) As AgendaDataConsulta
' , case when DATEpart(WEEKDAY, DATA_CONSULTA) = 2 then 'Segunda' else case when DATEpart(WEEKDAY, DATA_CONSULTA) = 3 then 'Terça'  else case when DATEpart(WEEKDAY, DATA_CONSULTA) = 4 then 'Quarta' 
'  Else Case When DATEpart(WEEKDAY, DATA_CONSULTA) = 5 Then 'Quinta' else case when DATEpart(WEEKDAY, DATA_CONSULTA) = 6 then 'Sexta' end end end end end as AgendaDiaSemana
' , Período as AgendaPeriodo, Hora as AgendaHora, case when STATUS = '$$$' then 'Horário para Convênio Particular' else '' end as AgendaObservacao
' --, STATUS, m.NOME as AgendaMedicoNome
' from AGENDA_CLINICA
' inner join medico m On m.CODIGO=MEDICO
' left outer join PARAMETROS_GERAIS pg On pg.COD_PARTICULAR > 0
' where medico = 17 And DATA_CONSULTA >= getdate()
'  And DATA_CONSULTA <= getdate() + 15
' And isnull(NOMEPACI, '') = ''
' order by DATA_CONSULTA, HORA