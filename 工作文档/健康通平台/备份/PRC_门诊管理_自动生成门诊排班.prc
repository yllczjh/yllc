CREATE OR REPLACE PROCEDURE PRC_�������_�Զ����������Ű� IS

  STR_��������         VARCHAR2(20) := '522633020000001';
  STR_�Ƿ������Ű�     VARCHAR2(10);
  INT_���ԤԼ����     INTEGER;
  INT_����             INTEGER;
  STR_����             VARCHAR2(10);
  STR_�Ű�����         VARCHAR2(20);
  INT_��ǰ�Ѵ����Ű��� INTEGER;

  STR_��¼ID VARCHAR2(50);

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
      
        IF INT_��ǰ�Ѵ����Ű��� = 0 THEN
          --��ȡ��¼ID
          SELECT SYS_GUID() INTO STR_��¼ID FROM DUAL;
        
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
             �Һ����ͱ���)
            SELECT A.��������,
                   SYS_GUID(),
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
                   A.�Һ����ͱ���
              FROM �������_����һ���Ű�� A
             WHERE A.�������� = STR_��������
               AND A.���� = STR_����
               AND A.�Ű���� IN (SELECT A.�Ű����
                                FROM �������_����һ���Ű�� A
                               WHERE A.�������� = STR_��������
                                 AND A.���� = STR_����);
        
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
                   (SELECT C.��¼ID
                      FROM �������_�����Ű��¼ C
                     WHERE C.�������� = A.��������
                       AND C.�Ű���� = A.�Ű����) AS ��¼ID,
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
               AND A.�Ű���� IN (SELECT A.�Ű����
                                FROM �������_����һ���Ű�� A
                               WHERE A.�������� = STR_��������
                                 AND A.���� = STR_����);
        
        END IF;
        INT_���� := INT_���� + 1;
      END LOOP;
      COMMIT;
    END IF;
  EXCEPTION
    WHEN OTHERS THEN
      ROLLBACK;
  END;
END PRC_�������_�Զ����������Ű�;
/
