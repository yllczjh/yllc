create or replace procedure PRC_�������_���������Ű��¼(str_�������� in varchar2,
                                              str_����     in varchar2,
                                              str_����     in varchar2,
                                              int_����ֵ   out integer,
                                              str_������Ϣ out varchar2) is
begin

  insert into �������_�����Ű��¼
    (��������,
     ��¼ID,
     �Ű�����,
     �Ű����,
     �Һ���������,
     ����,
     ���ұ���,
     ��������,
     ҽ������,
     ҽ������,
     �����޺�,
     ��������޺�,
     �����ѹҺ���,
     �����޺�,
     ��������޺�,
     �����ѹҺ���,
     ��������,
     ����λ��,
     ����ʱ��,
     ���ɷ�ʽ,
     �Һ����ͱ���)
    select a.��������,
           SYS_GUID(),
           to_date(str_����, 'yyyy-MM-dd'),
           a.�Ű����,
           a.�Һ���������,
           a.����,
           a.���ұ���,
           a.��������,
           a.ҽ������,
           a.ҽ������,
           a.�����޺�,
           a.��������޺�,
           0,
           a.�����޺�,
           a.��������޺�,
           0,
           a.��������,
           a.����λ��,
           sysdate,
           '�ֶ�����',
           a.�Һ����ͱ���
      from �������_����һ���Ű�� a
     where a.�������� = str_��������
       and a.���� = str_����;

  int_����ֵ   := 1;
  str_������Ϣ := 'ok';
  commit;
exception
  when others then
    int_����ֵ   := 0;
    str_������Ϣ := sqlerrm;
    rollback;

end PRC_�������_���������Ű��¼;

 
 
 
/
