CREATE OR REPLACE PROCEDURE PR_������ͨ_����������ɷ��嵥(STR_��������   IN VARCHAR2,
                                              STR_����ID     IN VARCHAR2,
                                              STR_���ﲡ���� IN VARCHAR2,
                                              INT_����ֵ     OUT INTEGER,
                                              STR_������Ϣ   OUT VARCHAR2) IS

  -- �������
  STR_�Ƿ��������շ� VARCHAR2(50);
  I_����               INTEGER;
  STR_�շ����         VARCHAR2(50);
  STR_ҽ����           VARCHAR2(50);
  STR_�Һ����         VARCHAR2(50);
  NUM_Ӧ�����         NUMBER(18, 3);
  STR_������           VARCHAR2(50);
  DAT_ϵͳʱ��         DATE;
  STR_ƽ̨��ʶ         VARCHAR2(50);
  STR_״̬             VARCHAR2(50);

  TYPE REF_CURSOR_TYPE IS REF CURSOR;
  CUR_���ɷ���ϸ REF_CURSOR_TYPE;
  ROW_���ɷ���ϸ �������_����ҽ����ϸ%ROWTYPE;
  STR_SQL        VARCHAR2(1000);
  NUM_�������   NUMBER(10, 3);
BEGIN

  -- �����ݳ�ʼ����
  SELECT SYSDATE INTO DAT_ϵͳʱ�� FROM DUAL;
  NUM_Ӧ����� := 0;
  STR_ƽ̨��ʶ := '12320';

  BEGIN
    SELECT ״̬
      INTO STR_״̬
      FROM ������ͨ_ƽ̨��������
     WHERE ƽ̨��ʶ = STR_ƽ̨��ʶ
       AND ���ܱ��� = '3003';
  EXCEPTION
    WHEN OTHERS THEN
      STR_״̬ := '0';
  END;
  IF STR_״̬ = '0' THEN
    INT_����ֵ := 0;
    RETURN;
  END IF;

  BEGIN
    SELECT COUNT(1)
      INTO INT_����ֵ
      FROM ������ͨ_����
     WHERE ƽ̨��ʶ = STR_ƽ̨��ʶ
       AND ����ID = STR_����ID
       AND ���ﲡ���� = STR_���ﲡ����
       AND �������� = 'ԤԼ�Һ�';
  EXCEPTION
    WHEN OTHERS THEN
      INT_����ֵ := 0;
  END;
  IF INT_����ֵ = 0 THEN
    RETURN;
  END IF;

  -- ��ȡϵͳ����
  BEGIN
    SELECT TO_NUMBER(ֵ)
      INTO I_����
      FROM ������Ŀ_���������б�
     WHERE �������� = '48'
       AND �������� = STR_��������;
  EXCEPTION
    WHEN OTHERS THEN
      I_���� := 15;
  END;

  BEGIN
    SELECT ֵ
      INTO STR_�Ƿ��������շ�
      FROM ������Ŀ_���������б�
     WHERE �������� = '311'
       AND �������� = STR_��������;
  EXCEPTION
    WHEN OTHERS THEN
      STR_�Ƿ��������շ� := '��';
  END;

  --���������ݡ�
  BEGIN
    SELECT �������
      INTO NUM_�������
      FROM ������ͨ_ƽ̨��������
     WHERE ƽ̨��ʶ = STR_ƽ̨��ʶ
       AND ��Ч״̬ = '1';
  EXCEPTION
    WHEN OTHERS THEN
      NUM_������� := 100;
  END;

  BEGIN
    SELECT DISTINCT A.ҽ����, A.�Һ����
      INTO STR_ҽ����, STR_�Һ����
      FROM �������_����ҽ�� A, �������_�ҺŵǼ� C
     WHERE A.�������� = C.��������
       AND A.����ID = C.����ID
       AND A.�Һ���� = C.�Һ����
       AND C.����״̬ = (CASE
             WHEN STR_�Ƿ��������շ� = '��' THEN
              '��ɽ���'
             ELSE
              C.����״̬
           END)
       AND (C.����״̬ <> '��ɽ���' OR EXISTS
            (SELECT 1
               FROM �������_����ҽ�� P
              WHERE P.�������� = A.��������
                AND P.�Һ���� = A.�Һ����
                AND P.����ID = A.����ID
                AND P.�շ�״̬ = '����δ�շ�'))
       AND C.�˺ű�־ = '��'
       AND (A.������� = '1' OR A.������� = '2' OR A.������� = '6')
       AND A.�������� = STR_��������
       AND A.���ﲡ���� = STR_���ﲡ����
       AND A.����ID = STR_����ID
       AND C.�Һ�ʱ�� > TRUNC(SYSDATE) - I_���� + 1
       AND A.¼��ʱ�� > TRUNC(SYSDATE) - I_���� + 1
       AND A.�շ�״̬ = '����δ�շ�'
       AND A.���۷�ʽ <> '�˷��Զ�����'
       AND C.�������ͱ��� = '1';
  EXCEPTION
    WHEN NO_DATA_FOUND THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := 'δ�ҵ���Ч�Ĵ��ɷ���Ϣ��';
      GOTO �˳�;
    WHEN OTHERS THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := 'ϵͳ�쳣��' || SQLERRM;
      GOTO �˳�;
  END;

  -- ����ҽ����ϸ
  BEGIN
  
    PR_������ͨ_����ҽ����ϸ(STR_�������� => STR_��������,
                   STR_�Һ���� => STR_�Һ����,
                   STR_ҽ����   => STR_ҽ����,
                   STR_�շ���� => STR_�շ����,
                   INT_����ֵ   => INT_����ֵ,
                   STR_������Ϣ => STR_������Ϣ);
  
    IF INT_����ֵ = -1 THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := '���ɴ��ɷ���Ϣʧ�ܣ�';
      GOTO �˳�;
    END IF;
  
  EXCEPTION
    WHEN OTHERS THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := 'ϵͳ�쳣��' || SQLERRM;
      GOTO �˳�;
  END;

  BEGIN
  
    BEGIN
      SELECT ҽԺ������
        INTO STR_������
        FROM ������ͨ_����
       WHERE ƽ̨��ʶ = STR_ƽ̨��ʶ
         AND ҽԺ���� = STR_��������
         AND ����ID = STR_����ID
         AND ���ﲡ���� = STR_���ﲡ����
         AND ����״̬ = '��֧��'
         AND �������� = '����ɷ�'
         AND ROWNUM = 1;
    EXCEPTION
      WHEN NO_DATA_FOUND THEN
        STR_������ := '';
      WHEN OTHERS THEN
        INT_����ֵ   := -1;
        STR_������Ϣ := '���Ҷ�����Ϣʧ��!';
        GOTO �˳�;
    END;
  
    IF STR_������ IS NULL THEN
      -- 1)���ɶ�����
      PR_��ȡ_ϵͳΨһ��(PRM_Ψһ�ű��� => '6002',
                  PRM_��������   => STR_��������,
                  PRM_��������   => '1',
                  PRM_����Ψһ�� => STR_������,
                  PRM_ִ�н��   => INT_����ֵ,
                  PRM_������Ϣ   => STR_������Ϣ);
      IF INT_����ֵ <> 0 THEN
        INT_����ֵ   := -1;
        STR_������Ϣ := '����������ʧ��!';
        GOTO �˳�;
      END IF;
    ELSE
      --ɾ��δ�ɷѵĶ�����ϸ��¼
      DELETE FROM ������ͨ_������ϸ WHERE ������ = STR_������;
      --ɾ��δ�ɷѵĶ�����¼
      DELETE FROM ������ͨ_���� WHERE ҽԺ������ = STR_������;
    END IF;
  
    STR_SQL := 'SELECT *
      FROM �������_����ҽ����ϸ
     WHERE �������� = ''' || STR_�������� || '''
       AND ����ID = ''' || STR_����ID || '''
       AND ���ﲡ���� = ''' || STR_���ﲡ���� || '''
       AND �շ���� = ''' || STR_�շ���� || '''';
    OPEN CUR_���ɷ���ϸ FOR STR_SQL;
    LOOP
      FETCH CUR_���ɷ���ϸ
        INTO ROW_���ɷ���ϸ;
      EXIT WHEN CUR_���ɷ���ϸ%NOTFOUND;
      INSERT INTO ������ͨ_������ϸ
        (��ˮ��,
         ������,
         Ψһ����,
         �������,
         С�����,
         ��Ŀ����,
         ��Ŀ����,
         ���,
         ���κ�,
         ����,
         ��λ,
         ����,
         �ܽ��,
         �������)
      VALUES
        (SEQ_������ͨ_������ϸ_��ˮ��.NEXTVAL,
         STR_������,
         ROW_���ɷ���ϸ.�Ƽ�ID,
         ROW_���ɷ���ϸ.�������,
         ROW_���ɷ���ϸ.С�����,
         ROW_���ɷ���ϸ.��Ŀ����,
         ROW_���ɷ���ϸ.��Ŀ����,
         ROW_���ɷ���ϸ.���,
         ROW_���ɷ���ϸ.���κ�,
         ROW_���ɷ���ϸ.����,
         ROW_���ɷ���ϸ.��λ����,
         ROW_���ɷ���ϸ.����,
         ROW_���ɷ���ϸ.�ܽ��,
         ROW_���ɷ���ϸ.�������);
    
      INT_����ֵ := SQL%ROWCOUNT;
      IF INT_����ֵ = 0 THEN
        INT_����ֵ   := -1;
        STR_������Ϣ := '���涩����ϸʧ�ܣ�';
        GOTO �˳�;
      END IF;
    
      NUM_Ӧ����� := NUM_Ӧ����� + ROW_���ɷ���ϸ.�ܽ��;
    
    END LOOP;
  
    INSERT INTO ������ͨ_����
      (��ˮ��,
       ƽ̨��ʶ,
       ҽԺ����,
       ����ID,
       ���ﲡ����,
       ��������,
       ��������,
       ����ʱ��,
       ҽԺ������,
       �ܽ��,
       Ӧ�����,
       ʵ�����,
       ����״̬,
       ������,
       ����ʱ��,
       ������,
       ����ʱ��)
    VALUES
      (SEQ_������ͨ_����_��ˮ��.NEXTVAL,
       STR_ƽ̨��ʶ,
       STR_��������,
       STR_����ID,
       STR_���ﲡ����,
       STR_�շ����,
       '����ɷ�',
       DAT_ϵͳʱ��,
       STR_������,
       NUM_Ӧ�����,
       NUM_Ӧ�����,
       0,
       '��֧��',
       STR_ƽ̨��ʶ,
       DAT_ϵͳʱ��,
       STR_ƽ̨��ʶ,
       DAT_ϵͳʱ��);
  
    INT_����ֵ := SQL%ROWCOUNT;
    IF INT_����ֵ = 0 THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := '���涩��ʧ�ܣ�';
      GOTO �˳�;
    END IF;
    CLOSE CUR_���ɷ���ϸ;
  
    INT_����ֵ := '0';
  
    COMMIT;
    RETURN;
  EXCEPTION
    WHEN OTHERS THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := 'ϵͳ�쳣��' || SQLERRM;
      GOTO �˳�;
  END;

  <<�˳�>>
  IF CUR_���ɷ���ϸ%ISOPEN THEN
    CLOSE CUR_���ɷ���ϸ;
  END IF;

  INT_����ֵ := '-1';

  ROLLBACK;
  RETURN;
END PR_������ͨ_����������ɷ��嵥;
/
