create or replace procedure PR_�����ϱ�_���﷢�ȴ�����¼(STR_����          IN VARCHAR2,
                                             CUR_����_�б���Ϣ OUT SYS_REFCURSOR) IS

  STR_�������� VARCHAR2(50) := FU_ͨ��_��ȡ�ַ���ֵ(STR_����, '|', 1);
  /*DAT_��Ժʱ����ʼ DATE := TO_DATE(FU_ͨ��_��ȡ�ַ���ֵ(STR_����, '|', 2),
                             'yyyy-MM-dd hh24:mi:ss');
  DAT_��Ժʱ���ֹ DATE := TO_DATE(FU_ͨ��_��ȡ�ַ���ֵ(STR_����, '|', 3),
                             'yyyy-MM-dd hh24:mi:ss');
  STR_��������     VARCHAR2(50) := FU_ͨ��_��ȡ�ַ���ֵ(STR_����, '|', 4);*/
  STR_��Ŀ���� VARCHAR2(50) := FU_ͨ��_��ȡ�ַ���ֵ(STR_����, '|', 5);
  STR_��ˮ��   VARCHAR2(50) := FU_ͨ��_��ȡ�ַ���ֵ(STR_����, '|', 6);
BEGIN
  BEGIN
    OPEN CUR_����_�б���Ϣ FOR
      select 1 from dual;
             
  
  end;

end PR_�����ϱ�_���﷢�ȴ�����¼;
/
