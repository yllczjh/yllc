CREATE OR REPLACE PROCEDURE Pr_�豸����_�豸ɾ��(str_�������� in varchar2,
                                         str_��ⵥ�� in varchar2,
                                         str_�豸���� in varchar2,
                                         i_����ֵ     out integer,
                                         str_������Ϣ out varchar2) AS

 -- i_����               number(10);
  i_�����ϸ����       number(10);
  --str_�豸���������ʶ varchar2(10);

BEGIN

  BEGIN

    /*str_������Ϣ := '��ѯ�豸�����ϸ����ʧ��! ';
    select ����, �豸���������־
      into i_����, str_�豸���������ʶ
      from �豸����_��ⵥ��ϸ
     where �������� = str_��������
       and ��ⵥ�� = str_��ⵥ��
       and �豸���� = str_�豸����;*/

/*    str_������Ϣ := '�޸��豸���ʧ��! ';
    update �豸����_����
       set ���� =
           (���� - i_����)
     where �������� = str_��������
       and �豸���� = str_�豸����;*/

    select count(0)
      into i_�����ϸ����
      from �豸����_��ⵥ��ϸ
     where �������� = str_��������
       and ��ⵥ�� = str_��ⵥ��;

    If i_�����ϸ���� = 1 then

      str_������Ϣ := 'ɾ���豸��ⵥ��ͷʧ��! ';
      delete �豸����_��ⵥ
       where �������� = str_��������
         and ��ⵥ�� = str_��ⵥ��;

    end if;

    str_������Ϣ := 'ɾ���豸��ⵥ��ϸʧ��! ';
    delete �豸����_��ⵥ��ϸ
     where �������� = str_��������
       and ��ⵥ�� = str_��ⵥ��
       and �豸���� = str_�豸����;

/*    str_������Ϣ := 'ɾ�����豸��ʧ��! ';
    delete �豸����_���豸��
     where �������� = str_��������
       and ��ⵥ�� = str_��ⵥ��
       and �豸���� = str_�豸����;*/

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

END;


 
 
 
/
