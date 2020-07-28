create or replace procedure PR_�豸����_�������豸����(str_��������     in varchar2,
                                            str_�ʲ�����     in varchar2,
                                            str_����         in varchar2,
                                            str_��������     in varchar2,
                                            str_���֤��     in varchar2,
                                            str_���ڱ�־     in varchar2,
                                            str_״̬         in varchar2,
                                            str_��;         in varchar2,
                                            str_ע���       in varchar2,
                                            str_����λ��     in varchar2,
                                            str_ʹ�ò���     in varchar2,
                                            str_����Ա       in varchar2,
                                            str_��ϵ��       in varchar2,
                                            str_���򵥼�     in varchar2,
                                            str_��ֵ         in varchar2,
                                            str_���ñ�־     in varchar2,
                                            dat_��������     in date,
                                            dat_��������     in date,
                                            str_����ֵ       in varchar2,
                                            str_��ע         in varchar2,
                                            str_���û������� in varchar2,
                                            str_����         in varchar2,
                                            str_��ⵥ��     in varchar2,
                                            str_v_��������   in varchar2,
                                            str_v_����       in varchar2,
                                            str_v_Ʒ���ͺ�   in varchar2,
                                            str_v_���       in varchar2,
                                            str_v_����       in varchar2,
                                            str_v_����       in varchar2,
                                            str_v_��ע       in varchar2,
                                            i_����ֵ         out integer,
                                            str_������Ϣ     out varchar2) is

  arr_�������� PP_ȫ�ֱ���.array_����;
  arr_����     PP_ȫ�ֱ���.array_����;
  arr_Ʒ���ͺ� PP_ȫ�ֱ���.array_����;
  arr_���     PP_ȫ�ֱ���.array_����;
  arr_����     PP_ȫ�ֱ���.array_����;
  arr_����     PP_ȫ�ֱ���.array_����;
  arr_��ע     PP_ȫ�ֱ���.array_����;

  i_��������   number(18);
  str_��¼���� �豸����_���豸������.��¼����%type;

begin

  BEGIN
  
    str_������Ϣ := '�������豸��ʧ��! ';
    update �豸����_���豸��
       set ����         = str_����,
           ��������     = str_��������,
           ���֤��     = str_���֤��,
           ���ڱ�־     = str_���ڱ�־,
           ״̬         = str_״̬,
           ��;         = str_��;,
           ע���       = str_ע���,
           ����λ��     = str_����λ��,
           ʹ�ò���     = str_ʹ�ò���,
           ����Ա       = str_����Ա,
           ��ϵ��       = str_��ϵ��,
           ���򵥼�     = str_���򵥼�,
           ��ֵ         = str_��ֵ,
           ���ñ�־     = str_���ñ�־,
           ��������     = dat_��������,
           ��������     = dat_��������,
           ����ֵ       = str_����ֵ,
           ��ע         = str_��ע,
           ���û������� = str_���û�������,
           ����         = str_����
     where �������� = str_��������
       and �ʲ����� = str_�ʲ�����
       and ��ⵥ�� = str_��ⵥ��;
  
    if length(str_v_��������) > 0 then
      arr_�������� := FU_Strsplit(str_�����ַ��� => str_v_��������,
                              str_�ָ���     => ',');
      arr_����     := FU_Strsplit(str_�����ַ��� => str_v_����,
                                str_�ָ���     => ',');
      arr_Ʒ���ͺ� := FU_Strsplit(str_�����ַ��� => str_v_Ʒ���ͺ�,
                              str_�ָ���     => ',');
      arr_���     := FU_Strsplit(str_�����ַ��� => str_v_���,
                                str_�ָ���     => ',');
      arr_����     := FU_Strsplit(str_�����ַ��� => str_v_����,
                                str_�ָ���     => ',');
      arr_����     := FU_Strsplit(str_�����ַ��� => str_v_����,
                                str_�ָ���     => ',');
      arr_��ע     := FU_Strsplit(str_�����ַ��� => str_v_��ע,
                                str_�ָ���     => ',');
    
      for i in 1 .. arr_��������.count loop
      
        select count(1)
          into i_��������
          from �豸����_���豸������
         where �������� = str_��������
           and �ʲ����� = str_�ʲ�����
           and �������� = arr_��������(i)
           and ������� = '��';
      
      --�����޸�
        select NVL(max(to_number(��¼����)),0) + 1
          into str_��¼����
          from �豸����_���豸������;
      
        if i_�������� > 0 then
        
          str_������Ϣ := '�������豸��������ʧ��! ';
          update �豸����_���豸������
             set ���� = ���� + to_number(arr_����(i))
           where �������� = str_��������
             and �ʲ����� = str_�ʲ�����
             and �������� = arr_��������(i)
             and ������� = '��';
        
        else
        
          str_������Ϣ := '�������豸����ʧ��! ';
          insert into �豸����_���豸������
            (��������,
             �ʲ�����,
             ��������,
             ��¼����,
             ����,
             Ʒ���ͺ�,
             ���,
             ����,
             ����,
             �������,
             ��ע)
          values
            (str_��������,
             str_�ʲ�����,
             arr_��������(i),
             str_��¼����,
             arr_����(i),
             arr_Ʒ���ͺ�(i),
             arr_���(i),
             arr_����(i),
             arr_����(i),
             '��',
             arr_��ע(i));
        
        end if;
      
      end loop;
    
    end if;
  
  EXCEPTION
    WHEN OTHERS THEN
      GOTO �˳�;
  END;

  i_����ֵ     := 1;
  str_������Ϣ := '��ӳɹ�! ';
  RETURN;

  <<�˳�>>
  i_����ֵ     := 0;
  str_������Ϣ := str_������Ϣ || SQLERRM;
  RETURN;

end PR_�豸����_�������豸����;
/
