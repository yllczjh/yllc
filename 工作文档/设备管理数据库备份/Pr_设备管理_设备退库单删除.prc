create or replace procedure Pr_�豸����_�豸�˿ⵥɾ��(str_�������� in varchar2,
                                            str_�˿ⵥ�� in varchar2,
                                            str_�豸���� in varchar2,
                                            str_�ʲ����� in varchar2,
                                            i_����ֵ     out integer,
                                            str_������Ϣ out varchar2) AS

  i_�˿���ϸ���� number(10);
  str_���˱�־   varchar2(50);

BEGIN

  BEGIN
  
    str_������Ϣ := '��ѯ���˱�־ʧ��! ';
    select min(���˱�־)
      into str_���˱�־
      from �豸����_�˿ⵥ
     where �������� = str_��������
       and �˿ⵥ�� = str_�˿ⵥ��;
  
    if Str_���˱�־ = '�ѽ���' then
      str_������Ϣ := '�õ����Ѿ�����!';
      GOTO �˳�;
    end if;
  
    str_������Ϣ := '��ѯ�˿ⵥ��ϸ����ʧ��! ';
    select count(0)
      into i_�˿���ϸ����
      from �豸����_�˿ⵥ��ϸ
     where �������� = str_��������
       and �˿ⵥ�� = str_�˿ⵥ��;
  
    If i_�˿���ϸ���� = 1 then
    
      str_������Ϣ := 'ɾ���豸��ⵥ��ͷʧ��! ';
      delete �豸����_�˿ⵥ
       where �������� = str_��������
         and �˿ⵥ�� = str_�˿ⵥ��;
    
    end if;
  
    str_������Ϣ := 'ɾ���豸��ⵥ��ϸʧ��! ';
    delete �豸����_�˿ⵥ��ϸ
     where �������� = str_��������
       and �˿ⵥ�� = str_�˿ⵥ��
       and �豸���� = str_�豸����
       and �ʲ����� = str_�ʲ�����;
  
  EXCEPTION
    WHEN OTHERS THEN
      GOTO �˳�;
  END;

  i_����ֵ     := 1;
  str_������Ϣ := 'ɾ���ɹ�! ';
  COMMIT;
  RETURN;

  <<�˳�>>
  i_����ֵ     := 0;
  str_������Ϣ := str_������Ϣ || SQLERRM;
  ROLLBACK;
  RETURN;

end Pr_�豸����_�豸�˿ⵥɾ��;
/
