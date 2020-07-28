CREATE OR REPLACE PROCEDURE PR_������ͨ_ԤԼ�Һ�ȡ��(STR_������� IN VARCHAR2,
                                           STR_ƽ̨��ʶ IN VARCHAR2,
                                           STR_���ܱ��� IN VARCHAR2, --2011
                                           STR_���ñ�ʶ IN VARCHAR2, --0ƽ̨  1ҽԺ
                                           LOB_��Ӧ���� OUT CLOB,
                                           INT_����ֵ   OUT INTEGER,
                                           STR_������Ϣ OUT VARCHAR2) IS

  --�����������
  STR_ҽԺID     VARCHAR2(50);
  STR_ƽ̨������ VARCHAR2(50);

  --��ҵ�������
  STR_SQL      VARCHAR2(1000);
  DAT_ϵͳʱ�� DATE;
  STR_ԤԼ���� VARCHAR2(50);

  STR_����ID       VARCHAR2(50);
  STR_�Ű�ID       VARCHAR2(50);
  STR_�Һ����     VARCHAR2(50);
  STR_�Һŵ���     VARCHAR2(50);
  STR_���ﲡ����   VARCHAR2(50);
  NUM_�Һŷ�       NUMBER(10, 4);
  NUM_����       NUMBER(10, 4);
  NUM_�ܷ���       NUMBER(10, 4);
  STR_���ù������ VARCHAR2(50);

  STR_�Һſ��ұ��� VARCHAR2(50);
  STR_�Һſ���λ�� VARCHAR2(50);
  STR_�Һ�ҽ������ VARCHAR2(50);
  STR_�Һ����ͱ��� VARCHAR2(50);

  STR_�������ͱ��� VARCHAR2(50);
  STR_������������ VARCHAR2(50);
  STR_����״̬     VARCHAR2(50);
  STR_�Һ���Դ     VARCHAR2(50);
  STR_���ʽ     VARCHAR2(50);

  DAT_ԤԼ��ʼʱ�� DATE;
  DAT_ԤԼ����ʱ�� DATE;
  STR_�հ�α�ʶ   VARCHAR2(50);

  STR_ҽԺ����� VARCHAR2(50);
  STR_��������   VARCHAR2(50);
  STR_ƽ̨����   VARCHAR2(50);

BEGIN
  BEGIN
  
    --���������������
    STR_ҽԺID := FU_������ͨ_�ڵ�ֵ(STR_�������, 'HOS_ID');
    IF STR_���ñ�ʶ = '1' THEN
      --ҽԺ
      STR_ҽԺID := FU_������ͨ_ҽԺIDת��(STR_ƽ̨��ʶ, STR_ҽԺID, '2');
    END IF;
  
    STR_ƽ̨������ := FU_������ͨ_�ڵ�ֵ(STR_�������, 'ORDER_ID');
  
    STR_�������ͱ��� := '1';
    STR_������������ := '�ֽ�';
    STR_����״̬     := '�ȴ�����';
    STR_�Һ���Դ     := 'ԤԼ';
  
    -- ��������ʼ����
    SELECT SYSDATE INTO DAT_ϵͳʱ�� FROM DUAL;
  
    BEGIN
      SELECT ƽ̨����, ֧����ʽ
        INTO STR_ƽ̨����, STR_���ʽ
        FROM ������ͨ_ƽ̨��������
       WHERE ƽ̨��ʶ = STR_ƽ̨��ʶ
         AND ROWNUM = 1;
    EXCEPTION
      WHEN OTHERS THEN
        STR_���ʽ := '����֧��';
        STR_ƽ̨���� := STR_ƽ̨��ʶ;
    END;
  
    IF STR_ҽԺID IS NULL THEN
      INT_����ֵ   := 1;
      STR_������Ϣ := '�봫��ҽԺID';
      GOTO �˳�;
    END IF;
    IF STR_ƽ̨������ IS NULL THEN
      INT_����ֵ   := 1;
      STR_������Ϣ := '�봫��ƽ̨������';
      GOTO �˳�;
    END IF;
    STR_�������� := FU_������ͨ_ҽԺIDת��(STR_ƽ̨��ʶ, STR_ҽԺID, '1');
    IF STR_�������� IS NULL THEN
      INT_����ֵ   := 1;
      STR_������Ϣ := 'ҽԺID��Ч';
      GOTO �˳�;
    END IF;
  
    -- ����ȡԤԼ���š�
    BEGIN
      SELECT ��������
        INTO STR_ԤԼ����
        FROM ������ͨ_����
       WHERE ҽԺ���� = STR_��������
         AND ƽ̨��ʶ = STR_ƽ̨��ʶ
         AND ƽ̨������ = STR_ƽ̨������
         AND ����״̬ = '��֧��';
    EXCEPTION
      WHEN NO_DATA_FOUND THEN
        INT_����ֵ   := 201101;
        STR_������Ϣ := '�ҺŶ���������';
        GOTO �˳�;
      WHEN OTHERS THEN
        INT_����ֵ   := 99;
        STR_������Ϣ := 'ϵͳ����' || SQLERRM;
        GOTO �˳�;
    END;
  
    -- ��ȡԤԼ�Һ�����
    BEGIN
      SELECT �Һſ��ұ���,
             �Һſ���λ��,
             �Һ�ҽ������,
             �Һ����ͱ���,
             ����ID,
             �Ű�ID,
             �Һŷ�,
             ����,
             �Һŷ� + ����,
             �������,
             ԤԼʱ�ο�ʼ,
             ԤԼʱ�ν���,
             �հ�α�ʶ
        INTO STR_�Һſ��ұ���,
             STR_�Һſ���λ��,
             STR_�Һ�ҽ������,
             STR_�Һ����ͱ���,
             STR_����ID,
             STR_�Ű�ID,
             NUM_�Һŷ�,
             NUM_����,
             NUM_�ܷ���,
             STR_���ù������,
             DAT_ԤԼ��ʼʱ��,
             DAT_ԤԼ����ʱ��,
             STR_�հ�α�ʶ
        FROM �������_ԤԼ�Һ� G
       WHERE G.�������� = STR_��������
         AND G.����ID = STR_ԤԼ����
         AND G.ȥ���־ = 'ԤԼ'
         AND G.֧����־ = '��'
         AND TO_CHAR(G.ԤԼʱ��, 'yyyymmdd') = TO_CHAR(SYSDATE, 'yyyymmdd');
    EXCEPTION
      WHEN NO_DATA_FOUND THEN
        INT_����ֵ   := 201101;
        STR_������Ϣ := '�ҺŶ���������';
        GOTO �˳�;
      WHEN OTHERS THEN
        INT_����ֵ   := 99;
        STR_������Ϣ := 'ϵͳ�쳣��' || SQLERRM;
        GOTO �˳�;
    END;
  
    -- ���������ﲡ���š�
    PR_����_ȡ��ҵ������(STR_��������   => STR_��������,
                  STR_���������� => '���ﲡ����',
                  STR_���ز����� => STR_���ﲡ����,
                  INT_����ֵ     => INT_����ֵ,
                  STR_������Ϣ   => STR_������Ϣ);
    IF INT_����ֵ <> 1 THEN
      INT_����ֵ   := 99;
      STR_������Ϣ := '�������ﲡ����ʧ��,ԭ��:' + STR_������Ϣ;
      GOTO �˳�;
    END IF;
  
    -- �������Һ���š�
    PR_��ȡ_ϵͳΨһ��(PRM_Ψһ�ű��� => '26',
                PRM_��������   => STR_��������,
                PRM_��������   => '1',
                PRM_����Ψһ�� => STR_�Һ����,
                PRM_ִ�н��   => INT_����ֵ,
                PRM_������Ϣ   => STR_������Ϣ);
    IF INT_����ֵ <> 0 THEN
      INT_����ֵ   := 99;
      STR_������Ϣ := '�����Һ����ʧ��!';
      GOTO �˳�;
    END IF;
  
    -- �������Һŵ��š�
    SELECT FU_����_��ȡ��ǰƱ�ݺ�(STR_��������, STR_ƽ̨��ʶ, '4')
      INTO STR_�Һŵ���
      FROM DUAL;
  
    IF STR_�Һŵ��� = '�뵽����������Ʊ��' THEN
      INT_����ֵ   := 99;
      STR_������Ϣ := '�ò���Ա�޹Һŵ���,��֪ͨ����������Ʊ��!';
      GOTO �˳�;
    END IF;
  
    -- ���ɹҺż�¼ 
    BEGIN
      INSERT INTO �������_�ҺŵǼ�
        (��������,
         ����ID,
         ���ﲡ����,
         �Һ����,
         �Һŵ���,
         �Һſ��ұ���,
         �Һſ���λ��,
         �Һ�ҽ������,
         �Һ����ͱ���,
         ����Ա����,
         �Һ�ʱ��,
         �˺ű�־,
         �������,
         �Һŷ�,
         ������,
         ����,
         ������,
         �ܷ���,
         �Ƿ���,
         ���,
         ����״̬,
         �������ͱ���,
         �Һ���Դ,
         ������ұ���,
         ����ҽ������,
         �������,
         �Ը����,
         �Һ�������,
         ��֧�����,
         ԤԼ��ʼʱ��,
         ԤԼ����ʱ��,
         �հ�α�ʶ,
         �Ű�ID)
      VALUES
        (STR_��������,
         STR_����ID,
         STR_���ﲡ����,
         STR_�Һ����,
         STR_�Һŵ���,
         STR_�Һſ��ұ���,
         STR_�Һſ���λ��,
         STR_�Һ�ҽ������,
         STR_�Һ����ͱ���,
         STR_ƽ̨��ʶ,
         DAT_ϵͳʱ��,
         '��',
         STR_���ù������,
         NUM_�Һŷ�,
         0,
         NUM_����,
         0,
         NUM_�ܷ���,
         '��',
         '0',
         STR_����״̬,
         STR_�������ͱ���,
         STR_�Һ���Դ,
         STR_�Һſ��ұ���,
         STR_�Һ�ҽ������,
         0,
         NUM_�ܷ���,
         '-1',
         0,
         DAT_ԤԼ��ʼʱ��,
         DAT_ԤԼ����ʱ��,
         STR_�հ�α�ʶ,
         STR_�Ű�ID);
    
      INT_����ֵ := SQL%ROWCOUNT;
      IF INT_����ֵ = 0 THEN
        INT_����ֵ   := 99;
        STR_������Ϣ := '����Һ�����ʧ�ܣ�';
        GOTO �˳�;
      END IF;
    
      -- ������֧���¼
      INSERT INTO �������_��֧��
        (��������,
         ���ݺ�,
         �շѽ��,
         ���ʽ,
         ҵ������,
         ����Ա����,
         ����Ա����,
         �շ�ʱ��,
         �Һ����,
         ��Ʊ���,
         �Һ��շѱ�־,
         �������ͱ���,
         ������������)
      VALUES
        (STR_��������,
         STR_�Һŵ���,
         NUM_�ܷ���,
         STR_���ʽ,
         '�Һ�',
         STR_ƽ̨��ʶ,
         STR_ƽ̨����,
         SYSDATE,
         STR_�Һ����,
         STR_�Һ����,
         '�Һ�',
         STR_�������ͱ���,
         STR_������������);
    
      INT_����ֵ := SQL%ROWCOUNT;
      IF INT_����ֵ = 0 THEN
        INT_����ֵ   := 99;
        STR_������Ϣ := '������֧������ʧ�ܣ�';
        GOTO �˳�;
      END IF;
    
      -- ����ԤԼ״̬
      UPDATE �������_ԤԼ�Һ�
         SET ȥ���־ = '����',
             �Һ���� = STR_�Һ����,
             ȡ��ʱ�� = DAT_ϵͳʱ��
       WHERE �������� = STR_��������
         AND ����ID = STR_ԤԼ����;
    
      INT_����ֵ := SQL%ROWCOUNT;
      IF INT_����ֵ = 0 THEN
        INT_����ֵ   := -1;
        STR_������Ϣ := '����ԤԼ״̬ʧ�ܣ�';
        GOTO �˳�;
      END IF;
    
      -- ���¶���
      UPDATE ������ͨ_����
         SET ���ﲡ���� = STR_���ﲡ����
       WHERE ƽ̨��ʶ = STR_ƽ̨��ʶ
         AND ҽԺ���� = STR_��������
         AND ƽ̨������ = STR_ƽ̨������
         AND �������� = STR_ԤԼ����
         AND �������� = 'ԤԼ�Һ�';
    
      INT_����ֵ := SQL%ROWCOUNT;
      IF INT_����ֵ = 0 THEN
        INT_����ֵ   := 99;
        STR_������Ϣ := '���¶���״̬ʧ�ܣ�';
        GOTO �˳�;
      END IF;
    
      IF STR_���ñ�ʶ = '0' THEN
        STR_SQL      := 'SELECT ''' || STR_ҽԺ����� ||
                        ''' AS HOSP_SERIAL_NUM,      
                         ''�Һŵ���:' || STR_�Һŵ��� ||
                        ',������:' || STR_���ﲡ���� ||
                        ',����ݴ���Ϣ���ҺŴ��ڴ�ӡ�Һŵ�'' AS REMARK FROM DUAL';
        LOB_��Ӧ���� := FU_������ͨ_�õ���Ӧ����(STR_SQL, 'RES', '');
      ELSE
        LOB_��Ӧ���� := STR_�Һŵ���;
      END IF;
    
      INT_����ֵ   := 0;
      STR_������Ϣ := '���׳ɹ�';
    
      COMMIT;
    
    EXCEPTION
      WHEN OTHERS THEN
        INT_����ֵ   := 99;
        STR_������Ϣ := '��Ӧ���󱨴�:' || SQLERRM;
        GOTO �˳�;
    END;
  
  END;
  <<�˳�>>

  -- ��������־��
  PR_������ͨ_������־(STR_ƽ̨��ʶ => STR_ƽ̨��ʶ,
               STR_ҽԺ���� => STR_��������,
               STR_���ܱ��� => STR_���ܱ���,
               STR_������� => STR_�������,
               DAT_����ʱ�� => DAT_ϵͳʱ��,
               INT_����ֵ   => INT_����ֵ,
               STR_������Ϣ => STR_������Ϣ,
               DAT_ִ��ʱ�� => SYSDATE);
  ROLLBACK;
  RETURN;

END PR_������ͨ_ԤԼ�Һ�ȡ��;
/
