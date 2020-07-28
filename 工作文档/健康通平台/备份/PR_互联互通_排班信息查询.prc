CREATE OR REPLACE PROCEDURE PR_������ͨ_�Ű���Ϣ��ѯ(STR_������� IN VARCHAR2,
                                           LOB_��Ӧ���� OUT CLOB,
                                           INT_����ֵ   OUT INTEGER,
                                           STR_������Ϣ OUT VARCHAR2) IS

  STR_SQL VARCHAR2(1000);
  --�����������
  STR_��������     VARCHAR2(50) := FU_������ͨ_�ڵ�ֵ(STR_�������, 'HOS_ID');
  STR_���ұ���     VARCHAR2(50) := FU_������ͨ_�ڵ�ֵ(STR_�������, 'DEPT_ID');
  STR_��Ա����     VARCHAR2(50) := FU_������ͨ_�ڵ�ֵ(STR_�������, 'DOCTOR_ID');
  STR_�Ű࿪ʼ���� VARCHAR2(50) := FU_������ͨ_�ڵ�ֵ(STR_�������, 'START_DATE');
  STR_�Ű�������� VARCHAR2(50) := FU_������ͨ_�ڵ�ֵ(STR_�������, 'END_DATE');

  str_��ʱҽ��ID   varchar2(50) := '';
  str_��ʱ�������� varchar2(50) := '';
  str_�Ű�ID       varchar2(50) := '';

  CURSOR CUR_�Ű���Ϣ IS
    SELECT T.��������,
           T.���ұ���,
           T.��Ա����,
           (SELECT A.��Ա����
              FROM ������Ŀ_��Ա���� A
             WHERE A.�������� = T.��������
               AND A.��Ա���� = T.��Ա����) AS ҽ������,
           (SELECT A.ְ��
              FROM ������Ŀ_��Ա���� A
             WHERE A.�������� = T.��������
               AND A.��Ա���� = T.��Ա����) AS ҽ��ְ��,
           T1.�Ű�����,
           T1.����,
           T2.�հ�α�ʶ,
           T2.ʱ�α���,
           T2.�޺���,
           T2.�ѹҺ���,
           T3.�Һŷ�,
           T3.����
      FROM ������Ŀ_��Ա�����б� T,
           �������_�����Ű��¼ T1,
           �������_���Ű�ʱ�α� T2,
           ������Ŀ_�Һ�����     T3
     WHERE T.�������� = T1.��������
       AND T1.�������� = T2.��������
       AND T2.�������� = T3.��������
       AND T.���ұ��� = T1.���ұ���
       and (t.��Ա���� = t1.ҽ������ or t1.ҽ������ is null)
       AND T1.��¼ID = T2.��¼ID
       AND T1.�Ű���� = T2.�Ű����
       AND T1.�Һ����ͱ��� = T3.���ͱ���
       AND T.�������� = STR_��������
       AND T.ɾ����־ = '0'
       AND T3.��Ч״̬ = '��Ч'
       AND T.���ұ��� = STR_���ұ���
       AND (T.��Ա���� = STR_��Ա���� OR T.��Ա���� = '-1')
       AND T1.�Ű����� BETWEEN TO_DATE(STR_�Ű࿪ʼ����, 'yyyy-MM-dd') AND
           TO_DATE(STR_�Ű��������, 'yyyy-MM-dd')
     order by T.���ұ���, T.��Ա����, T1.�Ű�����, T2.ʱ�α���;

  ROW_�Ű���Ϣ CUR_�Ű���Ϣ%ROWTYPE;

BEGIN

  BEGIN
  
    FOR ROW_�Ű���Ϣ IN CUR_�Ű���Ϣ LOOP
      EXIT WHEN CUR_�Ű���Ϣ%NOTFOUND;
    
      if ROW_�Ű���Ϣ.��Ա���� <> str_��ʱ�������� then
        LOB_��Ӧ���� := LOB_��Ӧ���� || '<REG_DOCTOR_LIST>';
        LOB_��Ӧ���� := LOB_��Ӧ���� || '<DOCTOR_ID>' || ROW_�Ű���Ϣ.��Ա���� ||
                    '</DOCTOR_ID>';
        LOB_��Ӧ���� := LOB_��Ӧ���� || '<NAME>' || ROW_�Ű���Ϣ.ҽ������ || '</NAME>';
        LOB_��Ӧ���� := LOB_��Ӧ���� || '<JOB_TITLE>' || ROW_�Ű���Ϣ.ҽ��ְ�� ||
                    '</JOB_TITLE>';
      else
        if ROW_�Ű���Ϣ.�Ű����� <> str_��ʱҽ��ID then
          LOB_��Ӧ���� := LOB_��Ӧ���� || '<REG_LIST>';
          LOB_��Ӧ���� := LOB_��Ӧ���� || '<REG_DATE>' || ROW_�Ű���Ϣ.�Ű����� ||
                      '</REG_DATE>';
          LOB_��Ӧ���� := LOB_��Ӧ���� || '<REG_WEEKDAY>' || ROW_�Ű���Ϣ.���� ||
                      '</REG_WEEKDAY>';
        
          if ROW_�Ű���Ϣ.�հ�α�ʶ <> str_�Ű�ID then
            LOB_��Ӧ���� := LOB_��Ӧ���� || '<REG_TIME_LIST>';
            LOB_��Ӧ���� := LOB_��Ӧ���� || '<REG_ID>' || ROW_�Ű���Ϣ.�հ�α�ʶ ||
                        '</REG_ID>';
            LOB_��Ӧ���� := LOB_��Ӧ���� || '<TIME_FLAG>4</TIME_FLAG>';
            LOB_��Ӧ���� := LOB_��Ӧ���� || '<REG_STATUS>1</REG_STATUS>';
            LOB_��Ӧ���� := LOB_��Ӧ���� || '<TOTAL>' || ROW_�Ű���Ϣ.�޺��� || '</TOTAL>';
            LOB_��Ӧ���� := LOB_��Ӧ���� || '<OVER_COUNT>' || ROW_�Ű���Ϣ.�޺��� -
                        ROW_�Ű���Ϣ.�ѹҺ��� || '</OVER_COUNT>';
            LOB_��Ӧ���� := LOB_��Ӧ���� || '<REG_LEVEL>1</REG_LEVEL>';
            LOB_��Ӧ���� := LOB_��Ӧ���� || '<REG_FEE>' || ROW_�Ű���Ϣ.�Һŷ� ||
                        '</REG_FEE>';
            LOB_��Ӧ���� := LOB_��Ӧ���� || '<TREAT_FEE>' || ROW_�Ű���Ϣ.���� ||
                        '</TREAT_FEE>';
            LOB_��Ӧ���� := LOB_��Ӧ���� || '<ISTIME>0</ISTIME>';
            LOB_��Ӧ���� := LOB_��Ӧ���� || '</REG_TIME_LIST>';
          end if;
          LOB_��Ӧ���� := LOB_��Ӧ���� || '</REG_LIST>';
        end if;
      end if;
    
    END LOOP;
  
  END;

END PR_������ͨ_�Ű���Ϣ��ѯ;
/
