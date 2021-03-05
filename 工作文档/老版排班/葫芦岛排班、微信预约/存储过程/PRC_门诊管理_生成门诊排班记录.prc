CREATE OR REPLACE PROCEDURE PRC_�������_���������Ű��¼(STR_�������� IN VARCHAR2,
                                              STR_����     IN VARCHAR2,
                                              STR_����     IN VARCHAR2,
                                              INT_����ֵ   OUT INTEGER,
                                              STR_������Ϣ OUT VARCHAR2) IS
  INT_����         INTEGER;
  STR_��������     VARCHAR2(50);
  STR_ҽ������     VARCHAR2(50);
  STR_�Һ��������� VARCHAR2(50);
  STR_���ұ���     VARCHAR2(50);
  STR_ҽ������     VARCHAR2(50);
  STR_�Һ����ͱ��� VARCHAR2(50);

  CURSOR CUR_�Ű��¼ IS
    SELECT A.�Ű����, SYS_GUID() AS ��¼ID
      FROM �������_����һ���Ű�� A
     WHERE A.�������� = STR_��������
       AND A.���� = STR_����
       AND A.�Ű���� NOT IN
           (SELECT B.�Ű����
              FROM �������_�����Ű��¼ B
             WHERE B.�������� = STR_��������
               AND B.���� = STR_����
               AND B.�Ű����� = TO_DATE(STR_����, 'yyyy-MM-dd'));

BEGIN

  --�ж��Ƿ���ڵ������ڵ��Ű�
  SELECT COUNT(*)
    INTO INT_����ֵ
    FROM �������_����һ���Ű��
   WHERE �������� = STR_��������
     AND ���� = STR_����;
  IF INT_����ֵ = 0 THEN
    INT_����ֵ   := 0;
    STR_������Ϣ := '�������Ű�����ӡ�' || STR_���� || '�����Ű��¼��';
    RETURN;
  END IF;

  INT_����ֵ := 0;
  --��ȡ��Ҫ���ɵ��Ű����
  FOR CUR_RESULT IN CUR_�Ű��¼ LOOP
  
    SELECT ��������,
           ���ұ���,
           ҽ������,
           ҽ������,
           �Һ���������,
           �Һ����ͱ���
      INTO STR_��������,
           STR_���ұ���,
           STR_ҽ������,
           STR_ҽ������,
           STR_�Һ���������,
           STR_�Һ����ͱ���
      FROM �������_����һ���Ű��
     WHERE �������� = STR_��������
       AND ���� = STR_����
       AND �Ű���� = CUR_RESULT.�Ű����;
  
    SELECT COUNT(1)
      INTO INT_����
      FROM �������_�����Ű��¼
     WHERE �������� = STR_��������
       AND ���� = STR_����
       AND �Ű����� = TO_DATE(STR_����, 'yyyy-MM-dd')
       AND ���ұ��� = STR_���ұ���
       AND ҽ������ = STR_ҽ������
       AND �Һ����ͱ��� = STR_�Һ����ͱ���;
    IF INT_���� > 0 THEN
      INT_����ֵ   := 0;
      STR_������Ϣ := '�����Ű��Ѵ����ֶ���ӵ�[' || STR_�������� || ']��[' || STR_ҽ������ ||']�Һ�����Ϊ['|| STR_�Һ��������� ||
                  ']���Ű�,�����Ű��м�¼��ͻ�����ȴ���';
      ROLLBACK;
      RETURN;
    END IF;
  
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
             TO_DATE(STR_����, 'yyyy-MM-dd'),
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
             '�ֶ�����',
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
  
    INT_����ֵ := INT_����ֵ + 1;
  END LOOP;
  IF INT_����ֵ = 0 THEN
    STR_������Ϣ := '����û����Ҫ���ɵ��Ű��¼';
    RETURN;
  END IF;
  INT_����ֵ   := 1;
  STR_������Ϣ := 'ok';
  COMMIT;
EXCEPTION
  WHEN OTHERS THEN
    INT_����ֵ   := 0;
    STR_������Ϣ := SQLERRM;
    ROLLBACK;
  
END PRC_�������_���������Ű��¼;
/
