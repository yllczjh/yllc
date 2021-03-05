CREATE OR REPLACE PROCEDURE PRC_�������_�Զ����������Ű� IS

  STR_��������         VARCHAR2(20) := '222403100001';
  STR_�Ƿ������Ű�     VARCHAR2(10);
  INT_���ԤԼ����     INTEGER;
  INT_����             INTEGER;
  STR_����             VARCHAR2(10);
  STR_�Ű�����         VARCHAR2(20);
  INT_��ǰ�Ѵ����Ű��� INTEGER;


  CURSOR CUR_�Ű��¼ IS
    SELECT A.�Ű����, sys_guid() as ��¼ID
      FROM �������_����һ���Ű�� A
     WHERE A.�������� = STR_��������
       AND A.���� = STR_����
       AND A.�Ű���� NOT IN
           (SELECT B.�Ű����
              FROM �������_�����Ű��¼ B
             WHERE B.�������� = STR_��������
               AND B.���� = STR_����
               AND B.�Ű����� = TO_DATE(STR_�Ű�����, 'yyyy-MM-dd'));

BEGIN

  --�����Ƿ������Ű����
  BEGIN
    SELECT ֵ
      INTO STR_�Ƿ������Ű�
      FROM ������Ŀ_���������б�
     WHERE �������룽 '910536'
       AND �������� = STR_��������;
  EXCEPTION
    WHEN OTHERS THEN
      STR_�Ƿ������Ű� := '��';
  END;
  --���︴���ԤԼ�������
  BEGIN
    SELECT ֵ
      INTO INT_���ԤԼ����
      FROM ������Ŀ_���������б�
     WHERE �������룽 '910540'
       AND �������� = STR_��������;
  EXCEPTION
    WHEN OTHERS THEN
      INT_���ԤԼ���� := 15;
  END;

  BEGIN
    IF STR_�Ƿ������Ű� = '��' THEN
      INT_���� := 0;
      LOOP
        EXIT WHEN INT_���� > INT_���ԤԼ����;
        INT_��ǰ�Ѵ����Ű��� := 0;
        SELECT COUNT(1),
               DECODE(TO_CHAR(SYSDATE + INT_����, 'D'),
                      '1',
                      '������',
                      '2',
                      '����һ',
                      '3',
                      '���ڶ�',
                      '4',
                      '������',
                      '5',
                      '������',
                      '6',
                      '������',
                      '7',
                      '������'),
               TO_CHAR(SYSDATE + INT_����, 'yyyy-mm-dd')
          INTO INT_��ǰ�Ѵ����Ű���, STR_����, STR_�Ű�����
          FROM �������_�����Ű��¼
         WHERE �������� = STR_��������
           AND �Ű����� = TRUNC(SYSDATE + INT_����);
      
        --IF INT_��ǰ�Ѵ����Ű��� = 0 THEN
      
        FOR CUR_RESULT IN CUR_�Ű��¼ LOOP
          --���� �������_�����Ű��¼
          INSERT INTO �������_�����Ű��¼
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
             ��������,
             ����λ��,
             ����ʱ��,
             ���ɷ�ʽ,
             ����״̬,
             �Һ����ͱ���)
            SELECT A.��������,
                   CUR_RESULT.��¼ID,
                   TO_DATE(STR_�Ű�����, 'yyyy-MM-dd'),
                   A.�Ű����,
                   A.�Һ���������,
                   A.����,
                   A.���ұ���,
                   A.��������,
                   A.ҽ������,
                   A.ҽ������,
                   A.��������,
                   A.����λ��,
                   SYSDATE,
                   '�Զ�����',
                   '1',
                   A.�Һ����ͱ���
              FROM �������_����һ���Ű�� A
             WHERE A.�������� = STR_��������
               AND A.���� = STR_����
               AND A.�Ű���� = CUR_RESULT.�Ű����;
        
          --���� �������_���Ű�ʱ�α�
          INSERT INTO �������_���Ű�ʱ�α�
            (�հ�α�ʶ,
             ��������,
             �Ű����,
             ��¼ID,
             �޺����ͱ���,
             ʱ�η������,
             ʱ�α���,
             ��ʼʱ��,
             ����ʱ��,
             �޺���,
             ˳���,
             ��Ч״̬,
             �ѹҺ���,
             ֧�ֹ���)
            SELECT SEQ_�������_���Ű�_�Ű��ʶ.NEXTVAL,
                   A.��������,
                   A.�Ű����,
                   CUR_RESULT.��¼ID,
                   A.�޺����ͱ���,
                   A.ʱ�η������,
                   A.ʱ�α���,
                   A.��ʼʱ��,
                   A.����ʱ��,
                   A.�޺���,
                   A.˳���,
                   A.��Ч״̬,
                   0,
                   '��'
              FROM �������_���Ű�ʱ�α� A
             WHERE A.�������� = STR_��������
               AND A.�Ű���� = CUR_RESULT.�Ű����;
        
        --END IF;
        END LOOP;
        INT_���� := INT_���� + 1;
      END LOOP;
      COMMIT;
    END IF;
  EXCEPTION
    WHEN OTHERS THEN
      dbms_output.put_line(sqlerrm);
      ROLLBACK;
  END;
END PRC_�������_�Զ����������Ű�;
/
