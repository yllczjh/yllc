create or replace procedure PR_�豸��ϸͳ��(str_��������     in varchar2,
                                      cur_��ѯ���     out sys_refcursor,
                                      i_����ֵ         out integer,
                                      str_������Ϣ     out varchar2) is
  Str_��ͷ varchar2(50);
begin
    open cur_��ѯ��� for
      select Fu_ȡ��_��������(a.��������) as ��������,
             a.��������,
             b.���� as �豸����,
             (select ����
                from ������Ŀ_�ֵ���ϸ
               where ������� = 'GB_009066'
                 and ���� = b.�豸���) as �豸���,
             b.Ʒ���ͺ�,
             b.���,
             a.���� as �����,
             a.����,
             a.��׼��,
             (select С������
                from ������Ŀ_С���ֵ�
               where �������� = a.��������
                 and ������� = '4'
                 and С����� = b.Ŀ¼����
                 and ��Ч״̬ = '��Ч') as Ŀ¼����,
             (a.���� * a.����) as �����ܶ�,
             (a.��׼�� * a.����) as ��׼���ܶ�
        from �豸����_����       a,
             �豸����_�豸Ŀ¼     b,
             ������Ŀ_������������ c
       where a.�������� = b.��������
         and a.�豸���� = b.�豸����
         and a.�������� = c.��Ͻ����
         and b.��Ч״̬ = '��Ч'
         and c.ɾ����־='0'
         --2020-04-03 �����޸�
         and a.��������=str_��������;
         --and c.�������� =
           -- (case when str_�������� = 'ȫ��' or str_�������� is null or
             -- str_�������� = ' ' then b.�������� else str_�������� end);

  I_����ֵ     := 1;
  Str_������Ϣ := 'OK';
end PR_�豸��ϸͳ��;


 
 
 
/
