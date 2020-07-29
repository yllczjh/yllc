CREATE OR REPLACE PROCEDURE PR_������ͨ_�ɷѵ����ڽɷ�(STR_��������         IN VARCHAR2,
                                            STR_����ID           IN VARCHAR2,
                                            STR_���ﲡ����       IN VARCHAR2,
                                            STR_ԭ��Ʊ���       IN VARCHAR2,
                                            STR_�����շ�Ԥ���� IN VARCHAR2,
                                            STR_�˷���ϸ��ˮ��   IN VARCHAR2) IS
  STR_ƽ̨��ʶ           VARCHAR2(10) := 12320;
  STR_�����շ�Ԥ������ VARCHAR2(500);
  DEC_ʵ���ܶ�           NUMERIC(18, 3) := 0;
  STR_��Ʊ���           VARCHAR2(50);
  STR_�˷�����           VARCHAR2(50);

  STR_������   VARCHAR2(50);
  INT_����ֵ   INTEGER;
  STR_������Ϣ VARCHAR2(50);

BEGIN

  --ȫ��ΪNULL   �����˼��ɷѲ�ΪNULL
  IF STR_�����շ�Ԥ���� IS NOT NULL THEN
    STR_�����շ�Ԥ������ := SUBSTR(STR_�����շ�Ԥ����,
                            INSTR(STR_�����շ�Ԥ����, '##') + 2);
    IF STR_�����շ�Ԥ������ IS NOT NULL THEN
      DEC_ʵ���ܶ� := FU_ͨ��_��ȡ�ַ���ֵ(STR_�����շ�Ԥ������, '~', 9);
      STR_��Ʊ��� := FU_ͨ��_��ȡ�ַ���ֵ(STR_�����շ�Ԥ������, '~', 10);
      STR_�˷����� := '������';
    ELSE
      STR_�˷����� := 'ȫ��';
    END IF;
  ELSE
    STR_�˷����� := 'ȫ��';
  END IF;

  BEGIN
    --�ɷ�
    IF STR_ԭ��Ʊ��� = 0 THEN
      UPDATE ������ͨ_���� T
         SET T.����״̬         = '��֧��',
             T.ʵ�����         = DEC_ʵ���ܶ�,
             T.ҽ��ͳ��֧����� = 0,
             T.ҽԺ֧����       = STR_��Ʊ���,
             T.֧��ʱ��         = SYSDATE,
             T.֧������         = '6', --����֧��
             T.������           = STR_ƽ̨��ʶ,
             T.����ʱ��         = SYSDATE
       WHERE T.ƽ̨��ʶ = STR_ƽ̨��ʶ
         AND T.ҽԺ���� = STR_��������
         AND T.����ID = STR_����ID
         AND T.���ﲡ���� = STR_���ﲡ����
         AND T.�������� = '����ɷ�'
         AND T.����״̬ = '��֧��';
    ELSE
      --�˷�
      IF STR_�˷����� = 'ȫ��' THEN
        --ȫ��
        UPDATE ������ͨ_���� T
           SET T.����״̬   = '���˿�',
               T.�˿�ʱ��   = SYSDATE,
               T.ҽԺ�˿�� = STR_ԭ��Ʊ��� || '-1',
               T.�˿���   = T.ʵ�����,
               T.�˿��־   = '2', --Ժ���˿�
               T.������     = STR_ƽ̨��ʶ,
               T.����ʱ��   = SYSDATE
         WHERE T.ƽ̨��ʶ = STR_ƽ̨��ʶ
           AND T.ҽԺ���� = STR_��������
           AND T.����ID = STR_����ID
           AND T.���ﲡ���� = STR_���ﲡ����
           AND T.ҽԺ֧���� = STR_ԭ��Ʊ���
           AND T.�������� = '����ɷ�'
           AND T.����״̬ = '��֧��';
      ELSE
        --������
      
        -- 1)���ɶ�����
        PR_��ȡ_ϵͳΨһ��(PRM_Ψһ�ű��� => '6002',
                    PRM_��������   => STR_��������,
                    PRM_��������   => '1',
                    PRM_����Ψһ�� => STR_������,
                    PRM_ִ�н��   => INT_����ֵ,
                    PRM_������Ϣ   => STR_������Ϣ);
        IF INT_����ֵ <> 0 THEN
          RETURN;
        END IF;
      
       
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
          SELECT SEQ_������ͨ_������ϸ_��ˮ��.NEXTVAL,
                 STR_������,
                 T.�Ƽ�ID,
                 T.�������,
                 T.С�����,
                 T.��Ŀ����,
                 T.��Ŀ����,
                 T.���,
                 T.���κ�,
                 T.����,
                 T.��λ����,
                 T.����,
                 T.�ܽ��,
                 T.�������
            FROM �������_���ﴦ�� T
           WHERE  T.�������� = STR_��������
             AND T.����ID = STR_����ID
             AND T.���ﲡ���� = STR_���ﲡ����
             AND T.��Ʊ��� = STR_��Ʊ���;
      
        INT_����ֵ := SQL%ROWCOUNT;
        IF INT_����ֵ = 0 THEN
          ROLLBACK;
          RETURN;
        END IF;
      
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
           ҽԺ֧����,
           ֧��ʱ��,
           ֧������,
           �ܽ��,
           Ӧ�����,
           ʵ�����,
           ҽ��ͳ��֧�����,
           ����״̬,
           ������,
           ����ʱ��,
           ������,
           ����ʱ��)
          SELECT SEQ_������ͨ_����_��ˮ��.NEXTVAL,
                 T.ƽ̨��ʶ,
                 T.ҽԺ����,
                 T.����ID,
                 T.���ﲡ����,
                 T.��������,
                 T.��������,
                 T.����ʱ��,
                 STR_������,
                 STR_��Ʊ���,
                 SYSDATE,
                 T.֧������,
                 DEC_ʵ���ܶ�,
                 DEC_ʵ���ܶ�,
                 DEC_ʵ���ܶ�,
                 0,
                 '��֧��',
                 STR_ƽ̨��ʶ,
                 SYSDATE,
                 STR_ƽ̨��ʶ,
                 SYSDATE
            FROM ������ͨ_���� T
           WHERE T.ƽ̨��ʶ = STR_ƽ̨��ʶ
             AND T.ҽԺ���� = STR_��������
             AND T.����ID = STR_����ID
             AND T.���ﲡ���� = STR_���ﲡ����
             AND T.ҽԺ֧���� = STR_ԭ��Ʊ���
             AND T.�������� = '����ɷ�'
             AND T.����״̬ = '��֧��';
      
        INT_����ֵ := SQL%ROWCOUNT;
        IF INT_����ֵ = 0 THEN
          ROLLBACK;
          RETURN;
        END IF;
      
        UPDATE ������ͨ_���� T
           SET T.����״̬   = '���˿�',
               T.�˿�ʱ��   = SYSDATE,
               T.ҽԺ�˿�� = STR_��Ʊ��� || '-1',
               T.�˿���   = T.ʵ�����,
               T.�˿��־   = '2', --Ժ���˿�
               T.������     = STR_ƽ̨��ʶ,
               T.����ʱ��   = SYSDATE
         WHERE T.ƽ̨��ʶ = STR_ƽ̨��ʶ
           AND T.ҽԺ���� = STR_��������
           AND T.����ID = STR_����ID
           AND T.���ﲡ���� = STR_���ﲡ����
           AND T.ҽԺ֧���� = STR_ԭ��Ʊ���
           AND T.�������� = '����ɷ�'
           AND T.����״̬ = '��֧��';
      
      END IF;
    END IF;
  
  EXCEPTION
    WHEN OTHERS THEN
      ROLLBACK;
      RETURN;
    
  END;
  COMMIT;

  RETURN;
END PR_������ͨ_�ɷѵ����ڽɷ�;
/
