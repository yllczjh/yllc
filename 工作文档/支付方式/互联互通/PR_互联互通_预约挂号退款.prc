CREATE OR REPLACE PROCEDURE PR_������ͨ_ԤԼ�Һ��˿�(STR_������� IN VARCHAR2,
                                           STR_ƽ̨��ʶ IN VARCHAR2, --2010
                                           STR_���ܱ��� IN VARCHAR2,
                                           LOB_��Ӧ���� OUT CLOB,
                                           INT_����ֵ   OUT INTEGER,
                                           STR_������Ϣ OUT VARCHAR2) IS

  --�����������
  STR_ҽԺID       VARCHAR2(50);
  STR_ƽ̨������   VARCHAR2(50);
  STR_ҽԺ������   VARCHAR2(50);
  STR_ƽ̨�˿�� VARCHAR2(50);
  STR_�˿���ˮ��   VARCHAR2(50);
  STR_�ܽ��       VARCHAR2(50);
  STR_�˿���     VARCHAR2(50);
  STR_�˿�����     VARCHAR2(50);
  STR_�˿�ʱ��     VARCHAR2(50);
  STR_������Ӧ���� VARCHAR2(50);
  STR_������Ӧ���� VARCHAR2(50);
  STR_�˿�ԭ��     VARCHAR2(50);

  STR_ԤԼ����     VARCHAR2(50);
  STR_����״̬     VARCHAR2(50);
  STR_SQL          VARCHAR2(1000);
  STR_ҽԺ�˿�� VARCHAR2(50);
  NUM_ʵ�����     NUMBER;
  STR_���Ű��ʶ   VARCHAR2(50);
  str_֧������     varchar2(50);
  NUM_�������     NUMBER(10, 3);
  STR_��������     VARCHAR2(50);

BEGIN
  --���������������
  STR_ҽԺID       := FU_������ͨ_�ڵ�ֵ(STR_�������, 'HOS_ID');
  STR_ƽ̨������   := FU_������ͨ_�ڵ�ֵ(STR_�������, 'ORDER_ID');
  STR_ҽԺ������   := FU_������ͨ_�ڵ�ֵ(STR_�������, 'HOSP_ORDER_ID');
  STR_ƽ̨�˿�� := FU_������ͨ_�ڵ�ֵ(STR_�������, 'REFUND_ID');
  STR_�˿���ˮ��   := FU_������ͨ_�ڵ�ֵ(STR_�������, 'REFUND_SERIAL_NUM');
  STR_�ܽ��       := FU_������ͨ_�ڵ�ֵ(STR_�������, 'TOTAL_FEE');
  STR_�˿���     := FU_������ͨ_�ڵ�ֵ(STR_�������, 'REFUND_FEE');
  STR_�˿�����     := FU_������ͨ_�ڵ�ֵ(STR_�������, 'REFUND_DATE');
  STR_�˿�ʱ��     := FU_������ͨ_�ڵ�ֵ(STR_�������, 'REFUND_TIME');
  STR_������Ӧ���� := FU_������ͨ_�ڵ�ֵ(STR_�������, 'REFUND_RES_CODE');
  STR_������Ӧ���� := FU_������ͨ_�ڵ�ֵ(STR_�������, 'REFUND_RES_DESC');
  STR_�˿�ԭ��     := FU_������ͨ_�ڵ�ֵ(STR_�������, 'REFUND_REMARK');

  --����֤������
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
    STR_������Ϣ := '�봫��ҽԺ������';
    GOTO �˳�;
  END IF;
  IF STR_ƽ̨�˿�� IS NULL THEN
    INT_����ֵ   := 1;
    STR_������Ϣ := '�봫��ƽ̨�˿��';
    GOTO �˳�;
  END IF;
  IF STR_�ܽ�� IS NULL THEN
    INT_����ֵ   := 1;
    STR_������Ϣ := '�봫���ܽ��';
    GOTO �˳�;
  END IF;
  IF STR_�˿��� IS NULL THEN
    INT_����ֵ   := 1;
    STR_������Ϣ := '�봫���˿���';
    GOTO �˳�;
  END IF;
  STR_�������� := FU_������ͨ_ҽԺIDת��(STR_ƽ̨��ʶ, STR_ҽԺID, '1');
  IF STR_�������� IS NULL THEN
    INT_����ֵ   := 1;
    STR_������Ϣ := 'ҽԺID��Ч';
    GOTO �˳�;
  END IF;

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

  --����֤����״̬��
  BEGIN
    SELECT ��������, ����״̬, ʵ�����, ҽԺ������, ֧������
      INTO STR_ԤԼ����,
           STR_����״̬,
           NUM_ʵ�����,
           STR_ҽԺ������,
           str_֧������
      FROM ������ͨ_����
     WHERE ƽ̨��ʶ = STR_ƽ̨��ʶ
       AND ҽԺ���� = STR_��������
       AND ƽ̨������ = STR_ƽ̨������;
  EXCEPTION
    WHEN NO_DATA_FOUND THEN
      INT_����ֵ   := 201001;
      STR_������Ϣ := '�ҺŶ���������';
      GOTO �˳�;
    WHEN OTHERS THEN
      INT_����ֵ   := 99;
      STR_������Ϣ := 'ϵͳ�쳣��' || SQLERRM;
      GOTO �˳�;
  END;

  IF STR_����״̬ = '��ȡ��' THEN
    INT_����ֵ   := 200805;
    STR_������Ϣ := '�ҺŶ�����ȡ��';
    GOTO �˳�;
  ELSIF STR_����״̬ = '���˿�' THEN
    INT_����ֵ   := 200806;
    STR_������Ϣ := '�ҺŶ������˿�';
    GOTO �˳�;
  END IF;

  IF NUM_ʵ����� * NUM_������� <> TO_NUMBER(STR_�˿���) THEN
    INT_����ֵ   := 201003;
    STR_������Ϣ := '�˿����ȷ';
    GOTO �˳�;
  END IF;

  -- ��֤ԤԼ��
  BEGIN
    SELECT �հ�α�ʶ
      INTO STR_���Ű��ʶ
      FROM �������_ԤԼ�Һ�
     WHERE �������� = STR_��������
       AND ����ID = STR_ԤԼ����
       AND ȥ���־ = 'ԤԼ';
  EXCEPTION
    WHEN NO_DATA_FOUND THEN
      INT_����ֵ   := 201001;
      STR_������Ϣ := '�ҺŶ���������';
      GOTO �˳�;
    WHEN OTHERS THEN
      INT_����ֵ   := 99;
      STR_������Ϣ := 'ϵͳ�쳣��' || SQLERRM;
      GOTO �˳�;
    
  END;

  -- �����ܴ���
  BEGIN
  
    -- ������
    UPDATE �������_���Ű�ʱ�α�
       SET �ѹҺ��� = �ѹҺ��� - 1
     WHERE �������� = STR_��������
       AND �հ�α�ʶ = STR_���Ű��ʶ;
    INT_����ֵ := SQL%ROWCOUNT;
    IF INT_����ֵ = 0 THEN
      INT_����ֵ   := 99;
      STR_������Ϣ := '������Դʧ�ܣ�';
      GOTO �˳�;
    END IF;
  
    -- ����ԤԼ��
    UPDATE �������_ԤԼ�Һ�
       SET ȥ���־ = '����'
     WHERE �������� = STR_��������
       AND ����ID = STR_ԤԼ����
       AND ȥ���־ = 'ԤԼ';
  
    INT_����ֵ := SQL%ROWCOUNT;
    IF INT_����ֵ = 0 THEN
      INT_����ֵ   := 99;
      STR_������Ϣ := '����ԤԼ��ʧ�ܣ�';
      GOTO �˳�;
    END IF;
  
  
    -- ���¶���״̬
    UPDATE ������ͨ_���� T
       SET T.����״̬       = '���˿�',
           T.ƽ̨�˿��     = STR_ƽ̨�˿��,
           T.ƽ̨�˿���ˮ�� = STR_�˿���ˮ��,
           T.�˿�ʱ��       = DECODE(STR_�˿�����,
                                 NULL,
                                 SYSDATE,
                                 TO_DATE(STR_�˿����� || ' ' || STR_�˿�ʱ��,
                                         'yyyy-MM-dd hh24:mi:ss')),
           T.ҽԺ�˿��     = T.ҽԺ֧���� || '-1',
           T.�˿���       = NUM_ʵ�����,
           T.�˿��־       = '1', --�ɹ� ƽ̨�˿�
           T.������         = STR_ƽ̨��ʶ,
           T.����ʱ��       = SYSDATE
     WHERE T.ƽ̨��ʶ = STR_ƽ̨��ʶ
       AND T.ҽԺ���� = STR_��������
       AND T.ƽ̨������ = STR_ƽ̨������
       AND T.�������� = STR_ԤԼ����;
  
    INT_����ֵ := SQL%ROWCOUNT;
    IF INT_����ֵ = 0 THEN
      INT_����ֵ   := 99;
      STR_������Ϣ := '���¶���ʧ�ܣ�';
      GOTO �˳�;
    END IF;
  
    STR_SQL := 'SELECT ''' || STR_ҽԺ�˿�� ||
               ''' AS HOSP_REFUND_ID, 
                   ''1'' AS REFUND_FLAG FROM DUAL';
  
    LOB_��Ӧ���� := FU_������ͨ_�õ���Ӧ����(STR_SQL, 'RES', '');
  
    INT_����ֵ   := 0;
    STR_������Ϣ := '���׳ɹ�';
  
  EXCEPTION
    WHEN OTHERS THEN
      INT_����ֵ   := 99;
      STR_������Ϣ := '��Ӧ���󱨴�:' || SQLERRM;
      GOTO �˳�;
  END;

  COMMIT;

  --���쳣�˳���
  <<�˳�>>

  -- ��������־��
  PR_������ͨ_������־(STR_ƽ̨��ʶ => STR_ƽ̨��ʶ,
               STR_ҽԺ���� => STR_��������,
               STR_���ܱ��� => STR_���ܱ���,
               STR_������� => STR_�������,
               DAT_����ʱ�� => SYSDATE,
               INT_����ֵ   => INT_����ֵ,
               STR_������Ϣ => STR_������Ϣ,
               DAT_ִ��ʱ�� => SYSDATE);

  ROLLBACK;
  RETURN;

END PR_������ͨ_ԤԼ�Һ��˿�;
/
