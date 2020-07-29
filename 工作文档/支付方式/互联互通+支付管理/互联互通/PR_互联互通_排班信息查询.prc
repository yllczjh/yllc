CREATE OR REPLACE PROCEDURE PR_������ͨ_�Ű���Ϣ��ѯ(STR_������� IN VARCHAR2,
                                           STR_ƽ̨��ʶ IN VARCHAR2,
                                           STR_���ܱ��� IN VARCHAR2,
                                           LOB_��Ӧ���� OUT CLOB,
                                           INT_����ֵ   OUT INTEGER,
                                           STR_������Ϣ OUT VARCHAR2) IS

  --STR_SQL  VARCHAR2(2000);
  STR_SQL1 VARCHAR2(2000);

  --�����������
  STR_ҽԺID       VARCHAR2(50);
  STR_����ID       VARCHAR2(50);
  STR_ҽ��ID       VARCHAR2(50);
  STR_�Ű࿪ʼ���� VARCHAR2(50);
  STR_�Ű�������� VARCHAR2(50);

  NUM_������� NUMBER(10, 3);
  STR_�������� VARCHAR2(50);

  STR_��ʱҽ��ID   VARCHAR2(50) := '-999';
  STR_��ʱ�������� DATE := TO_DATE('1990-01-01', 'yyyy-MM-dd');

  CURSOR CUR_�Ű���Ϣ IS
    SELECT T.��������,
           T.��¼ID,
           T.���ұ���,
           T.ҽ������,
           (SELECT A.��Ա����
              FROM ������Ŀ_��Ա���� A
             WHERE A.�������� = T.��������
               AND A.��Ա���� = T.ҽ������) AS ҽ������,
           (SELECT A.ְ��
              FROM ������Ŀ_��Ա���� A
             WHERE A.�������� = T.��������
               AND A.��Ա���� = T.ҽ������) AS ҽ��ְ��,
           T.�Ű�����,
           T.����
      FROM �������_�����Ű��¼ T
     WHERE T.�������� = STR_��������
       AND T.���ұ��� = STR_����ID
       AND T.ҽ������ IS NOT NULL
       AND (T.ҽ������ = STR_ҽ��ID OR STR_ҽ��ID = '-1')
       AND T.�Ű����� BETWEEN TO_DATE(STR_�Ű࿪ʼ����, 'yyyy-MM-dd') AND
           TO_DATE(STR_�Ű��������, 'yyyy-MM-dd')
     ORDER BY T.���ұ���, T.ҽ������, T.�Ű�����;

  ROW_�Ű���Ϣ CUR_�Ű���Ϣ%ROWTYPE;

BEGIN

  BEGIN
    --�����������
    STR_ҽԺID       := FU_������ͨ_�ڵ�ֵ(STR_�������, 'HOS_ID');
    STR_����ID       := FU_������ͨ_�ڵ�ֵ(STR_�������, 'DEPT_ID');
    STR_ҽ��ID       := FU_������ͨ_�ڵ�ֵ(STR_�������, 'DOCTOR_ID');
    STR_�Ű࿪ʼ���� := FU_������ͨ_�ڵ�ֵ(STR_�������, 'START_DATE');
    STR_�Ű�������� := FU_������ͨ_�ڵ�ֵ(STR_�������, 'END_DATE');
  
    --��������֤��
    IF STR_ҽԺID IS NULL THEN
      INT_����ֵ   := 1;
      STR_������Ϣ := '�봫��ҽԺID';
      GOTO �˳�;
    END IF;
    IF STR_����ID IS NULL THEN
      INT_����ֵ   := 1;
      STR_������Ϣ := '�봫�����ID';
      GOTO �˳�;
    END IF;
    IF STR_ҽ��ID IS NULL THEN
      INT_����ֵ   := 1;
      STR_������Ϣ := '�봫��ҽ��ID';
      GOTO �˳�;
    END IF;
    IF STR_�Ű࿪ʼ���� IS NULL AND FU_����ת����(STR_�Ű࿪ʼ����) IS NULL THEN
      INT_����ֵ   := 1;
      STR_������Ϣ := '�봫���Ű࿪ʼ����';
      GOTO �˳�;
    END IF;
    IF STR_�Ű�������� IS NULL AND FU_����ת����(STR_�Ű��������) IS NULL THEN
      INT_����ֵ   := 1;
      STR_������Ϣ := '�봫���Ű��������';
      GOTO �˳�;
    END IF;
    STR_��������:=FU_������ͨ_ҽԺIDת��(STR_ƽ̨��ʶ,STR_ҽԺID,'1');
    IF STR_�������� IS NULL THEN
      INT_����ֵ   := 1;
      STR_������Ϣ := 'ҽԺID��Ч';
      GOTO �˳�;
    END IF;
  
    --����֤�����Ű���Ϣ��
    SELECT COUNT(1)
      INTO INT_����ֵ
      FROM �������_�����Ű��¼
     WHERE �������� = STR_��������
       AND ���ұ��� = STR_����ID
       AND �Ű����� BETWEEN TO_DATE(STR_�Ű࿪ʼ����, 'yyyy-MM-dd') AND
           TO_DATE(STR_�Ű��������, 'yyyy-MM-dd');
  
    IF INT_����ֵ = 0 THEN
      INT_����ֵ   := 200301;
      STR_������Ϣ := '���Ҳ�����';
      GOTO �˳�;
    END IF;
  
    --����֤ҽ���Ű���Ϣ��
    IF STR_ҽ��ID <> '-1' THEN
      SELECT COUNT(1)
        INTO INT_����ֵ
        FROM �������_�����Ű��¼
       WHERE �������� = STR_��������
         AND ҽ������ = STR_ҽ��ID
         AND �Ű����� BETWEEN TO_DATE(STR_�Ű࿪ʼ����, 'yyyy-MM-dd') AND
             TO_DATE(STR_�Ű��������, 'yyyy-MM-dd');
    
      IF INT_����ֵ = 0 THEN
        INT_����ֵ   := 200302;
        STR_������Ϣ := 'ҽ��������';
        GOTO �˳�;
      END IF;
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
  
    FOR ROW_�Ű���Ϣ IN CUR_�Ű���Ϣ LOOP
      EXIT WHEN CUR_�Ű���Ϣ%NOTFOUND;
      
      STR_SQL1:='SELECT DECODE((SELECT COUNT(1)
                                FROM �������_���Ű�ʱ�α�
                               WHERE �������� = T1.��������
                                 AND ��¼ID = T1.��¼ID),
                              1,
                              (SELECT �հ�α�ʶ
                                 FROM �������_���Ű�ʱ�α�
                                WHERE �������� = T1.��������
                                  AND ��¼ID = T1.��¼ID),
                              T1.��¼ID) AS REG_ID,
                       ''4'' AS TIME_FLAG,
                       T1.����״̬ AS REG_STATUS,
                       (SELECT SUM(�޺���)
                          FROM �������_���Ű�ʱ�α�
                         WHERE �������� = T1.��������
                           AND ��¼ID = T1.��¼ID
                           AND �޺��� >= 0) TOTAL,
                       (SELECT SUM(�޺���) - SUM(�ѹҺ���)
                          FROM �������_���Ű�ʱ�α�
                         WHERE �������� = T1.��������
                           AND ��¼ID = T1.��¼ID
                           AND �޺��� >= 0) OVER_COUNT,
                       1 AS REG_LEVEL,
                       T2.�Һŷ� * 100 AS REG_FEE,
                       T2.���� * 100 AS TREAT_FEE,
                       DECODE((SELECT COUNT(1)
                                FROM �������_���Ű�ʱ�α�
                               WHERE �������� = T1.��������
                                 AND ��¼ID = T1.��¼ID),
                              1,
                              0,
                              1) AS ISTIME
                  FROM �������_�����Ű��¼ T1, ������Ŀ_�Һ����� T2
                 WHERE T1.�������� = T2.��������
                   AND T1.�Һ����ͱ��� = T2.���ͱ���
                   AND T1.��������=' || STR_�������� ||
                      ' AND T1.��¼ID=''' || ROW_�Ű���Ϣ.��¼ID || '''';

    
      /*STR_SQL1 := 'SELECT T1.��¼ID AS REG_ID,
                         ''4'' AS TIME_FLAG,
                         T1.����״̬ AS REG_STATUS,      
                         (SELECT SUM(�޺���)
                            FROM �������_���Ű�ʱ�α�
                           WHERE �������� = T1.��������
                             AND ��¼ID = T1.��¼ID
                             AND �޺��� >= 0) TOTAL,
                         (SELECT SUM(�޺���) - SUM(�ѹҺ���)
                            FROM �������_���Ű�ʱ�α�
                           WHERE �������� = T1.��������
                             AND ��¼ID = T1.��¼ID
                             AND �޺��� >= 0) OVER_COUNT,
                         1 AS REG_LEVEL,
                         T2.�Һŷ� * ' || NUM_������� ||
                  ' AS REG_FEE,
                         T2.���� * ' || NUM_������� ||
                  ' AS TREAT_FEE,
                         1 AS ISTIME
                    FROM �������_�����Ű��¼ T1, ������Ŀ_�Һ����� T2
                   WHERE T1.�������� = T2.��������
                     AND T1.�Һ����ͱ��� = T2.���ͱ���
                   AND T1.��������=' || STR_�������� ||
                  ' AND T1.��¼ID=''' || ROW_�Ű���Ϣ.��¼ID || '''';*/
    
      IF STR_��ʱҽ��ID <> ROW_�Ű���Ϣ.ҽ������ THEN
        STR_��ʱҽ��ID   := ROW_�Ű���Ϣ.ҽ������;
        STR_��ʱ�������� := TO_DATE('1990-01-01', 'yyyy-MM-dd');
        IF DBMS_LOB.GETLENGTH(LOB_��Ӧ����) > 0 THEN
          LOB_��Ӧ���� := LOB_��Ӧ���� || '</REG_LIST>'; --�������ڽڵ����
          LOB_��Ӧ���� := LOB_��Ӧ���� || '</REG_DOCTOR_LIST>'; --�Ű�ҽ�����Ͻڵ����
        END IF;
        LOB_��Ӧ���� := LOB_��Ӧ���� || '<REG_DOCTOR_LIST>'; --�Ű�ҽ�����Ͻڵ㿪ʼ
        LOB_��Ӧ���� := LOB_��Ӧ���� || '<DOCTOR_ID>' || ROW_�Ű���Ϣ.ҽ������ ||
                    '</DOCTOR_ID>'; --ҽ��ID
        LOB_��Ӧ���� := LOB_��Ӧ���� || '<NAME>' || ROW_�Ű���Ϣ.ҽ������ || '</NAME>'; --ҽ������
        LOB_��Ӧ���� := LOB_��Ӧ���� || '<JOB_TITLE>' || ROW_�Ű���Ϣ.ҽ��ְ�� ||
                    '</JOB_TITLE>'; --ҽ��ְ��
      
        IF STR_��ʱ�������� <> ROW_�Ű���Ϣ.�Ű����� THEN
          STR_��ʱ�������� := ROW_�Ű���Ϣ.�Ű�����;
          LOB_��Ӧ����     := LOB_��Ӧ���� || '<REG_LIST>'; --�������ڽڵ㿪ʼ
          LOB_��Ӧ����     := LOB_��Ӧ���� || '<REG_DATE>' ||
                          TO_CHAR(ROW_�Ű���Ϣ.�Ű�����, 'yyyy-MM-dd') ||
                          '</REG_DATE>'; --��������
          LOB_��Ӧ����     := LOB_��Ӧ���� || '<REG_WEEKDAY>' || ROW_�Ű���Ϣ.���� ||
                          '</REG_WEEKDAY>'; --�������ڶ�Ӧ����
        
          LOB_��Ӧ���� := LOB_��Ӧ���� ||
                      FU_������ͨ_�õ���Ӧ����(STR_SQL1, 'REG_TIME_LIST', '');
        
        END IF;
      ELSE
        IF STR_��ʱ�������� <> ROW_�Ű���Ϣ.�Ű����� THEN
          IF DBMS_LOB.GETLENGTH(LOB_��Ӧ����) > 0 THEN
            LOB_��Ӧ���� := LOB_��Ӧ���� || '</REG_LIST>'; --�������ڽڵ����
          END IF;
          STR_��ʱ�������� := ROW_�Ű���Ϣ.�Ű�����;
          LOB_��Ӧ����     := LOB_��Ӧ���� || '<REG_LIST>'; --�������ڽڵ㿪ʼ
          LOB_��Ӧ����     := LOB_��Ӧ���� || '<REG_DATE>' ||
                          TO_CHAR(ROW_�Ű���Ϣ.�Ű�����, 'yyyy-MM-dd') ||
                          '</REG_DATE>'; --��������
          LOB_��Ӧ����     := LOB_��Ӧ���� || '<REG_WEEKDAY>' || ROW_�Ű���Ϣ.���� ||
                          '</REG_WEEKDAY>'; --�������ڶ�Ӧ����
        
          LOB_��Ӧ���� := LOB_��Ӧ���� ||
                      FU_������ͨ_�õ���Ӧ����(STR_SQL1, 'REG_TIME_LIST', '');
        
        ELSE
        
          LOB_��Ӧ���� := LOB_��Ӧ���� ||
                      FU_������ͨ_�õ���Ӧ����(STR_SQL1, 'REG_TIME_LIST', '');
        END IF;
      END IF;
    
    END LOOP;
    IF DBMS_LOB.GETLENGTH(LOB_��Ӧ����) > 0 THEN
      LOB_��Ӧ���� := LOB_��Ӧ���� || '</REG_LIST>'; --�������ڽڵ����
      LOB_��Ӧ���� := LOB_��Ӧ���� || '</REG_DOCTOR_LIST>'; --�Ű�ҽ�����Ͻڵ����
    
      LOB_��Ӧ���� := '<RES><HOS_ID>' || STR_ҽԺID || '</HOS_ID><DEPT_ID>' ||
                  STR_����ID || '</DEPT_ID>' || LOB_��Ӧ���� || '</RES>';
      INT_����ֵ   := 0;
      STR_������Ϣ := '���׳ɹ�';
    ELSE
      INT_����ֵ   := 200303;
      STR_������Ϣ := '�Ű಻���ڣ�δ��ѯ���Ű���Ϣ';
    END IF;
  EXCEPTION
    WHEN OTHERS THEN
      INT_����ֵ   := 99;
      STR_������Ϣ := '��Ӧ���󱨴�:' || SQLERRM;
      GOTO �˳�;
  END;
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

  RETURN;

END PR_������ͨ_�Ű���Ϣ��ѯ;
/
