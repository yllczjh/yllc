CREATE OR REPLACE PROCEDURE PR_������ͨ_�ɷѵ�֧��(STR_������� IN VARCHAR2,
                                          STR_ƽ̨��ʶ IN VARCHAR2,
                                          STR_���ܱ��� IN VARCHAR2, --3003
                                          LOB_��Ӧ���� OUT CLOB,
                                          INT_����ֵ   OUT INTEGER,
                                          STR_������Ϣ OUT VARCHAR2) IS

  STR_SQL VARCHAR2(1000);
  --���̶�������
  STR_ҽԺID           VARCHAR2(50);
  STR_ƽ̨������       VARCHAR2(50);
  STR_ҽԺ������       VARCHAR2(50);
  STR_��ˮ��           VARCHAR2(50);
  STR_��������         VARCHAR2(50);
  STR_����ʱ��         VARCHAR2(50);
  STR_֧������ID       VARCHAR2(50);
  STR_�ܽ��           VARCHAR2(50);
  STR_Ӧ�����         VARCHAR2(50);
  STR_�����Ը����     VARCHAR2(50);
  STR_ҽ��ͳ��֧����� VARCHAR2(50);
  STR_������Ӧ����     VARCHAR2(50);
  STR_������Ӧ����     VARCHAR2(50);
  STR_�̻���           VARCHAR2(50);
  STR_�ն˺�           VARCHAR2(50);
  STR_���п���         VARCHAR2(50);
  STR_������֧���ʺ�   VARCHAR2(50);
  STR_����ԱID         VARCHAR2(50);
  STR_�վݺ�           VARCHAR2(50);

  --��ҵ�������
  DAT_ϵͳʱ��     DATE;
  CUR_Ԥ����Ϣ     SYS_REFCURSOR;
  STR_Ԥ������ϸ VARCHAR2(4000);

  STR_ִ�п��ұ���   VARCHAR(50);
  NUM_�����ܶ�       NUMBER(18, 3);
  NUM_�Ը��ܶ�       NUMBER(18, 3);
  NUM_�Ż��ܶ�       NUMBER(18, 3);
  NUM_Ӧ���ܶ�       NUMBER(18, 3);
  NUM_�����ܶ�       NUMBER(18, 3);
  NUM_ʵ���ܶ�       NUMBER(18, 3);
  NUM_�����ܶ�       NUMBER(18, 3);
  NUM_������֧���ܶ� NUMBER(18, 3);

  INT_С��λ��       INTEGER;
  STR_���뷽ʽ       VARCHAR2(50);
  STR_�շ�ֱ�ӿۿ�� VARCHAR2(50);
  STR_��ִ�п��ҷ�Ʊ VARCHAR2(50);

  STR_�շ���� VARCHAR2(50);
  STR_����ID   VARCHAR2(50);
  STR_�Һ���� VARCHAR2(50);
  STR_ҽ����   VARCHAR2(50);
  STR_��Ʊ��   VARCHAR2(50);
  STR_��Ʊ��� VARCHAR2(50);

  STR_��������     VARCHAR2(50);
  STR_֧����ʽ     VARCHAR2(50);
  STR_�������ͱ��� VARCHAR2(50);
  STR_������������ VARCHAR2(50);
  STR_���ﲡ����   VARCHAR2(50);
  NUM_�������     NUMBER(10, 3);

  STR_�������� VARCHAR2(50);
  STR_����״̬ VARCHAR2(50);
BEGIN
  BEGIN
    --���̶�����������
    STR_ҽԺID           := FU_������ͨ_�ڵ�ֵ(STR_�������, 'HOS_ID');
    STR_ƽ̨������       := FU_������ͨ_�ڵ�ֵ(STR_�������, 'ORDER_ID');
    STR_ҽԺ������       := FU_������ͨ_�ڵ�ֵ(STR_�������, 'HOSP_SEQUENCE');
    STR_��ˮ��           := FU_������ͨ_�ڵ�ֵ(STR_�������, 'SERIAL_NUM');
    STR_��������         := FU_������ͨ_�ڵ�ֵ(STR_�������, 'PAY_DATE');
    STR_����ʱ��         := FU_������ͨ_�ڵ�ֵ(STR_�������, 'PAY_TIME');
    STR_֧������ID       := FU_������ͨ_�ڵ�ֵ(STR_�������, 'PAY_CHANNEL_ID');
    STR_�ܽ��           := FU_������ͨ_�ڵ�ֵ(STR_�������, 'PAY_TOTAL_FEE');
    STR_Ӧ�����         := FU_������ͨ_�ڵ�ֵ(STR_�������, 'PAY_BEHOOVE_FEE');
    STR_�����Ը����     := FU_������ͨ_�ڵ�ֵ(STR_�������, 'PAY_ACTUAL_FEE');
    STR_ҽ��ͳ��֧����� := FU_������ͨ_�ڵ�ֵ(STR_�������, 'PAY_MI_FEE');
    STR_������Ӧ����     := FU_������ͨ_�ڵ�ֵ(STR_�������, 'PAY_RES_CODE');
    STR_������Ӧ����     := FU_������ͨ_�ڵ�ֵ(STR_�������, 'PAY_RES_DESC');
    STR_�̻���           := FU_������ͨ_�ڵ�ֵ(STR_�������, 'MERCHANT_ID');
    STR_�ն˺�           := FU_������ͨ_�ڵ�ֵ(STR_�������, 'TERMINAL_ID');
    STR_���п���         := FU_������ͨ_�ڵ�ֵ(STR_�������, 'BANK_NO');
    STR_������֧���ʺ�   := FU_������ͨ_�ڵ�ֵ(STR_�������, 'PAY_ACCOUNT');
    STR_����ԱID         := FU_������ͨ_�ڵ�ֵ(STR_�������, 'OPERATOR_ID');
    STR_�վݺ�           := FU_������ͨ_�ڵ�ֵ(STR_�������, 'RECEIPT_ID');
  
    -- �����ݳ�ʼ����
    SELECT SYSDATE INTO DAT_ϵͳʱ�� FROM DUAL;
    STR_��������     := '����ɷ�';
    STR_�������ͱ��� := '1';
    STR_������������ := '�ֽ�';
  
    --��������֤��
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
    IF STR_ҽԺ������ IS NULL THEN
      INT_����ֵ   := 1;
      STR_������Ϣ := '�봫�����ǼǺ�';
      GOTO �˳�;
    END IF;
    IF STR_��ˮ�� IS NULL THEN
      INT_����ֵ   := 1;
      STR_������Ϣ := '�봫����ˮ��';
      GOTO �˳�;
    END IF;
    IF STR_�������� IS NULL THEN
      INT_����ֵ   := 1;
      STR_������Ϣ := '�봫�뽻������';
      GOTO �˳�;
    END IF;
    IF STR_����ʱ�� IS NULL THEN
      INT_����ֵ   := 1;
      STR_������Ϣ := '�봫�뽻��ʱ��';
      GOTO �˳�;
    END IF;
    IF STR_֧������ID IS NULL THEN
      INT_����ֵ   := 1;
      STR_������Ϣ := '�봫��֧������ID';
      GOTO �˳�;
    END IF;
    IF STR_�ܽ�� IS NULL THEN
      INT_����ֵ   := 1;
      STR_������Ϣ := '�봫���ܽ��';
      GOTO �˳�;
    END IF;
    IF STR_Ӧ����� IS NULL THEN
      INT_����ֵ   := 1;
      STR_������Ϣ := '�봫��Ӧ�����';
      GOTO �˳�;
    END IF;
    IF STR_�����Ը���� IS NULL THEN
      INT_����ֵ   := 1;
      STR_������Ϣ := '�봫������Ը����';
      GOTO �˳�;
    END IF;
    IF STR_ҽ��ͳ��֧����� IS NULL THEN
      INT_����ֵ   := 1;
      STR_������Ϣ := '�봫��ҽ��ͳ��֧�����';
      GOTO �˳�;
    END IF;
  
    STR_�������� := FU_������ͨ_ҽԺIDת��(STR_ƽ̨��ʶ, STR_ҽԺID, '1');
    IF STR_�������� IS NULL THEN
      INT_����ֵ   := 1;
      STR_������Ϣ := 'ҽԺID��Ч';
      GOTO �˳�;
    END IF;
  
    -- ��ϵͳ������
    BEGIN
      SELECT ֵ
        INTO STR_���뷽ʽ
        FROM ������Ŀ_���������б�
       WHERE �������� = '53'
         AND �������� = STR_��������;
    EXCEPTION
      WHEN OTHERS THEN
        STR_���뷽ʽ := '2';
    END;
  
    BEGIN
      SELECT TO_NUMBER(ֵ)
        INTO INT_С��λ��
        FROM ������Ŀ_���������б�
       WHERE �������� = '52'
         AND �������� = STR_��������;
    EXCEPTION
      WHEN OTHERS THEN
        INT_С��λ�� := 2;
    END;
  
    BEGIN
      SELECT ֵ
        INTO STR_�շ�ֱ�ӿۿ��
        FROM ������Ŀ_���������б�
       WHERE �������� = '164'
         AND �������� = STR_��������;
    EXCEPTION
      WHEN OTHERS THEN
        STR_�շ�ֱ�ӿۿ�� := '��';
    END;
  
    BEGIN
      SELECT ֵ
        INTO STR_��ִ�п��ҷ�Ʊ
        FROM ������Ŀ_���������б�
       WHERE �������� = '50'
         AND �������� = STR_��������;
    EXCEPTION
      WHEN OTHERS THEN
        STR_��ִ�п��ҷ�Ʊ := '0';
    END;
  
    BEGIN
      SELECT �������, ֧����ʽ
        INTO NUM_�������, STR_֧����ʽ
        FROM ������ͨ_ƽ̨��������
       WHERE ƽ̨��ʶ = STR_ƽ̨��ʶ
         AND ��Ч״̬ = '1';
    EXCEPTION
      WHEN OTHERS THEN
        NUM_������� := 100;
    END;
  
    --����֤������
    BEGIN
      SELECT ��������, ����ID, ���ﲡ����, ����״̬, Ӧ�����
        INTO STR_�շ����,
             STR_����ID,
             STR_���ﲡ����,
             STR_����״̬,
             NUM_Ӧ���ܶ�
        FROM ������ͨ_����
       WHERE ƽ̨��ʶ = STR_ƽ̨��ʶ
         AND ҽԺ���� = STR_��������
         AND ҽԺ������ = STR_ҽԺ������
         AND �������� = STR_��������;
    EXCEPTION
      WHEN NO_DATA_FOUND THEN
        INT_����ֵ   := 300301;
        STR_������Ϣ := '�ɷѶ���������';
        GOTO �˳�;
      WHEN OTHERS THEN
        INT_����ֵ   := -1;
        STR_������Ϣ := 'ϵͳ�쳣��' || SQLERRM;
        GOTO �˳�;
    END;
    IF STR_����״̬ = '��֧��' THEN
      INT_����ֵ   := 300302;
      STR_������Ϣ := '�ɷѶ�����֧��';
      GOTO �˳�;
    END IF;
    IF TO_NUMBER(STR_Ӧ�����) / NUM_������� <> NUM_Ӧ���ܶ� OR
       (TO_NUMBER(STR_�����Ը����) + TO_NUMBER(STR_ҽ��ͳ��֧�����)) / NUM_������� <>
       NUM_Ӧ���ܶ� THEN
      INT_����ֵ   := 300304;
      STR_������Ϣ := '�ɷѽ���ȷ';
      GOTO �˳�;
    END IF;
  
    -- ����֤ҽ��״̬��
    SELECT COUNT(1)
      INTO INT_����ֵ
      FROM �������_����ҽ����ϸ M, �������_����ҽ�� Y
     WHERE M.�������� = Y.��������
       AND M.����ID = Y.����ID
       AND M.���ﲡ���� = Y.���ﲡ����
       AND M.��� = Y.���
       AND M.ҽ���� = Y.ҽ����
       AND M.�������� = STR_��������
       AND M.����ID = STR_����ID
       AND M.�շ���� = STR_�շ����
       AND Y.�շ�״̬ = '����δ�շ�'
       AND Y.���۷�ʽ <> '�˷��Զ�����';
  
    IF INT_����ֵ <= 0 THEN
      INT_����ֵ   := 300303;
      STR_������Ϣ := '�ɷѶ����ѹر�';
      GOTO �˳�;
    END IF;
  
    -- ����֤����״̬��
    SELECT COUNT(1)
      INTO INT_����ֵ
      FROM �������_����ҽ����ϸ M, �������_���ﴦ�� C
     WHERE M.�������� = C.��������
       AND M.����ID = C.����ID
       AND M.���ﲡ���� = C.���ﲡ����
       AND M.��� = C.���
       AND M.ҽ���� = C.ҽ����
       AND M.��ˮ�� = C.ҽ����ˮ��
       AND M.�������� = STR_��������
       AND M.����ID = STR_����ID
       AND M.�շ���� = STR_�շ����;
  
    IF INT_����ֵ > 0 THEN
      INT_����ֵ   := 300303;
      STR_������Ϣ := '�ɷѶ����ѹر�';
      GOTO �˳�;
    END IF;
  
    -- ����֤ҽ����ϸ��
    BEGIN
      SELECT DISTINCT �Һ����, ҽ����
        INTO STR_�Һ����, STR_ҽ����
        FROM �������_����ҽ����ϸ
       WHERE �������� = STR_��������
         AND ����ID = STR_����ID
         AND �շ���� = STR_�շ����;
    EXCEPTION
      WHEN NO_DATA_FOUND THEN
        INT_����ֵ   := 300301;
        STR_������Ϣ := '�ɷѶ���������';
        GOTO �˳�;
      WHEN OTHERS THEN
        INT_����ֵ   := 99;
        STR_������Ϣ := 'ϵͳ�쳣��' || SQLERRM;
        GOTO �˳�;
    END;
  
    -- ���ɷ�Ʊ��
    SELECT FU_����_��ȡ��ǰƱ�ݺ�(STR_��������, STR_ƽ̨��ʶ, '1')
      INTO STR_��Ʊ��
      FROM DUAL;
  
    IF STR_��Ʊ�� = '�뵽����������Ʊ��' THEN
      INT_����ֵ   := 99;
      STR_������Ϣ := '�ò���Ա�޷�Ʊ��,��֪ͨ����������Ʊ��!';
      GOTO �˳�;
    END IF;
  
    -- ���ɷ�Ʊ���
    SELECT SEQ_�������_��Ʊ�Ǽ�_��Ʊ���.NEXTVAL
      INTO STR_��Ʊ���
      FROM DUAL;
  
    -- �����ܴ���
    BEGIN
    
      PR_�������_Ԥ����(STR_��������       => STR_��������,
                  STR_Ψһ����       => STR_�շ����,
                  STR_��Ա���ͱ���   => '-1',
                  DEC_�Ż�ֵ         => 0,
                  NUM_�����ܶ�       => 0,
                  STR_��ִ�п��ҷ�Ʊ => STR_��ִ�п��ҷ�Ʊ,
                  STR_���뷽ʽ       => STR_���뷽ʽ,
                  INT_����λ��       => INT_С��λ��,
                  CUR_Ԥ����Ϣ       => CUR_Ԥ����Ϣ,
                  INT_����ֵ         => INT_����ֵ,
                  STR_������Ϣ       => STR_������Ϣ);
    
      IF INT_����ֵ = 0 THEN
        INT_����ֵ   := 99;
        STR_������Ϣ := '����Ԥ�����¼ʧ��!';
        GOTO �˳�;
      END IF;
    
      LOOP
        FETCH CUR_Ԥ����Ϣ
          INTO STR_ִ�п��ұ���,
               NUM_�����ܶ�,
               NUM_�����ܶ�,
               NUM_�Ը��ܶ�,
               NUM_�Ż��ܶ�,
               NUM_Ӧ���ܶ�,
               NUM_�����ܶ�,
               NUM_ʵ���ܶ�,
               NUM_������֧���ܶ�;
        EXIT WHEN CUR_Ԥ����Ϣ%NOTFOUND;
      
        STR_Ԥ������ϸ := STR_Ԥ������ϸ || STR_��Ʊ�� || '~' || STR_ִ�п��ұ��� || '~' ||
                      NUM_�����ܶ� || '~' || NUM_�����ܶ� || '~' || NUM_�Ը��ܶ� || '~' ||
                      NUM_�Ż��ܶ� || '~' || NUM_Ӧ���ܶ� || '~' || NUM_�����ܶ� || '~' ||
                      NUM_ʵ���ܶ� || '~' || STR_��Ʊ��� || '~' || 0 || '~' ||
                      NUM_Ӧ���ܶ� || '~' || 0 || '~' || NUM_Ӧ���ܶ� || '~' ||
                      NUM_������֧���ܶ� || '~~|';
      END LOOP;
    
      CLOSE CUR_Ԥ����Ϣ;
    
      IF STR_Ԥ������ϸ IS NULL THEN
        INT_����ֵ   := 99;
        STR_������Ϣ := '����Ԥ�����¼ʧ��!';
        GOTO �˳�;
      END IF;
    
      STR_Ԥ������ϸ := '��Ʊ��,ִ�п��ұ���,�����ܶ�,�����ܶ�,�Ը��ܶ�,�Ż��ܶ�,Ӧ���ܶ�,�����ܶ�,ʵ���ܶ�,��Ʊ���,ԭ��Ʊҽ��֧ͨ�����,�����˷��ܶ�,���ο��˷��ܶ�,�����ֽ��˷��ܶ�,������֧���ܶ�##' ||
                    STR_Ԥ������ϸ;
    
      DBMS_OUTPUT.PUT_LINE(STR_Ԥ������ϸ);
    
      PR_�������_�����շ�(STR_��������       => STR_��������,
                   STR_����ID         => STR_����ID,
                   STR_���ﲡ����     => STR_���ﲡ����,
                   STR_�Һ����       => STR_�Һ����,
                   STR_�������ͱ���   => STR_�������ͱ���,
                   STR_������������   => STR_������������,
                   STR_�շ����       => STR_�շ����,
                   STR_Ԥ������ϸ   => STR_Ԥ������ϸ,
                   STR_���ʽ       => STR_֧����ʽ || '|' || NUM_Ӧ���ܶ� || '@',
                   STR_ԭ��Ʊ���     => '0',
                   INT_���۱���       => 0,
                   STR_���۷�ʽ       => '-1',
                   STR_����Ա����     => STR_ƽ̨��ʶ,
                   STR_����Ա����     => STR_ƽ̨��ʶ,
                   STR_�շ�ֱ�ӿۿ�� => STR_�շ�ֱ�ӿۿ��,
                   INT_С��λ��       => INT_С��λ��,
                   STR_���뷽ʽ       => STR_���뷽ʽ,
                   STR_������Ϣ       => '',
                   DAT_ϵͳʱ��       => DAT_ϵͳʱ��,
                   INT_����ֵ         => INT_����ֵ,
                   STR_������Ϣ       => STR_������Ϣ,
                   STR_һ��ͨ���׺�   => '',
                   STR_���㷽ʽ       => '');
    
      IF INT_����ֵ <> 1 THEN
        INT_����ֵ   := 99;
        STR_������Ϣ := '����ɷѼ�¼ʧ��!';
        GOTO �˳�;
      END IF;
    
      -- ���¶���״̬
      UPDATE ������ͨ_����
         SET ����״̬         = '��֧��',
             ƽ̨������       = STR_ƽ̨������,
             ʵ�����         = TO_NUMBER(STR_�����Ը����) / NUM_�������,
             ҽ��ͳ��֧����� = TO_NUMBER(STR_ҽ��ͳ��֧�����) / NUM_�������,
             ҽԺ֧����       = STR_��Ʊ���,
             ƽ̨������ˮ��   = STR_��ˮ��,
             ֧��ʱ��         = TO_DATE(STR_�������� || ' ' || STR_����ʱ��,
                                    'yyyy-MM-dd hh24:mi:ss'),
             ֧������         = STR_֧������ID,
             ������           = STR_ƽ̨��ʶ,
             ����ʱ��         = DAT_ϵͳʱ��
       WHERE ƽ̨��ʶ = STR_ƽ̨��ʶ
         AND ����ID = STR_����ID
         AND ҽԺ������ = STR_ҽԺ������
         AND �������� = STR_��������
         AND ����״̬ = '��֧��';
    
      STR_SQL := 'SELECT ''' || STR_��Ʊ��� || ''' AS HOSP_ORDER_ID,
    '''' AS RECEIPT_ID,''' || STR_���ﲡ���� ||
                 ''' AS HOSP_MEDICAL_NUM ,''��Ʊ��:' || STR_��Ʊ�� ||
                        ',������:' || STR_���ﲡ���� ||
                        ',����ݴ���Ϣ���ɷѴ��ڴ�ӡ�ɷѵ���'' AS  HOSP_REMARK FROM DUAL';
    
      LOB_��Ӧ���� := FU_������ͨ_�õ���Ӧ����(STR_SQL, 'RES', '');
    
      INT_����ֵ   := 0;
      STR_������Ϣ := '���׳ɹ�';
    
      COMMIT;
    
    EXCEPTION
      WHEN OTHERS THEN
        INT_����ֵ   := 99;
        STR_������Ϣ := '��Ӧ���󱨴�' || SQLERRM;
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
END PR_������ͨ_�ɷѵ�֧��;
/
