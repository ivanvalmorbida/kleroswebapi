Module Module1

    Public strSQLHorariosLivres As String = "select id, convert(char, DATA_CONSULTA, 103) as AgendaDataConsulta
            , case when DATEpart(WEEKDAY, DATA_CONSULTA) = 2 then 'Segunda' else case when DATEpart(WEEKDAY, DATA_CONSULTA) = 3 then 'Terça'  else case when DATEpart(WEEKDAY, DATA_CONSULTA) = 4 then 'Quarta' 
            else case when DATEpart(WEEKDAY, DATA_CONSULTA) = 5 then 'Quinta' else case when DATEpart(WEEKDAY, DATA_CONSULTA) = 6 then 'Sexta' end end end end end as AgendaDiaSemana
            , Período as AgendaPeriodo, Hora as AgendaHora, case when STATUS = '$$$' then 'Horário para Convênio Particular' else '' end as AgendaObservacao
            from AGENDA_CLINICA
            inner join medico m on m.CODIGO=MEDICO
            left outer join PARAMETROS_GERAIS pg on pg.COD_PARTICULAR > 0
            where medico = @med and DATA_CONSULTA >= getdate()
            and DATA_CONSULTA <= getdate() + {d}
            and isnull(NOMEPACI, '') = ''
            order by DATA_CONSULTA, HORA"

    Public strSQLConfirmaConsulta As String = "select ID, DATA_CONSULTA, Hora, NOMEPACI, '55' + Celular as Celular
            , case when ac.TIPO_ATENDIMENTO= 1 then 'Prim.Consulta' when ac.TIPO_ATENDIMENTO= 2 then 'Consulta' when ac.TIPO_ATENDIMENTO=3 then 'Retorno' when ac.TIPO_ATENDIMENTO=6 then 'Puericultura' when ac.TIPO_ATENDIMENTO=7 then 'Emergencia' when ac.TIPO_ATENDIMENTO=8 then 'Recado' when ac.TIPO_ATENDIMENTO=9 then 'Cirurgia' end as TipoAtendimento
            , DATA_CONSULTA, Hora as HoraConsulta, m.NOME as MedicoNome, rtrim(c.ENDERECO) + ' ' + rtrim(str(c.NUMERO)) as ENDERECO, c.NOME as CLINICA
            from Agenda_clinica ac
            inner join medico m on m.codigo = ac.medico	
            inner join CLIENTE c on c.codigo = 3
            where DATA_CONSULTA = dateadd(DD, +{d}, cast(getdate() as date))
            and PERIODO <> 3
            and len(rtrim(celular))>0
            and isnull(nomepaci, '') > ''
            and TIPO_ATENDIMENTO in (1,2,3,6,7)
            and status <> 'CON' and status <> 'CAN'
            order by HORA"

End Module
