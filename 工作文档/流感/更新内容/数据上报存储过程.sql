prompt PL/SQL Developer Export User Objects for user CLOUDHIS@47.104.4.221:9900/YKEY
prompt Created by syyyhl on 2020-09-18
set define off
spool �����ϱ��洢����.log

prompt
prompt Creating procedure PR_�����ϱ�_�������ﲡ����Ϣ
prompt ===================================
prompt
CREATE OR REPLACE PROCEDURE CLOUDHIS.PR_�����ϱ�_�������ﲡ����Ϣ(STR_����          IN VARCHAR2,
                                             CUR_����_�б���Ϣ OUT SYS_REFCURSOR) IS

  STR_�������� VARCHAR2(50) := FU_ͨ��_��ȡ�ַ���ֵ(STR_����, '|', 1);
  /*DAT_��Ժʱ����ʼ DATE := TO_DATE(FU_ͨ��_��ȡ�ַ���ֵ(STR_����, '|', 2),
                             'yyyy-MM-dd hh24:mi:ss');
  DAT_��Ժʱ���ֹ DATE := TO_DATE(FU_ͨ��_��ȡ�ַ���ֵ(STR_����, '|', 3),
                             'yyyy-MM-dd hh24:mi:ss');
  STR_��������     VARCHAR2(50) := FU_ͨ��_��ȡ�ַ���ֵ(STR_����, '|', 4);*/
  STR_��Ŀ���� VARCHAR2(50) := FU_ͨ��_��ȡ�ַ���ֵ(STR_����, '|', 5);
  STR_��ˮ��   VARCHAR2(50) := FU_ͨ��_��ȡ�ַ���ֵ(STR_����, '|', 6);
BEGIN
  BEGIN
    IF STR_��ˮ�� IS NULL THEN
      SELECT MAX(��ˮ��)
        INTO STR_��ˮ��
        FROM ��������_�����б�
       WHERE �������� = STR_��������
         AND ��Ŀ���� = STR_��Ŀ����;
    END IF;
  
    --������Ժ������Ϣ
    INSERT INTO ��ʱ��_�����ϱ�_��Ժ���з���
      (������, �����ܶ�, ��������, �����ܶ�, �Ը��ܶ�)
      SELECT B.���ﲡ����,
             C.�����ܶ�,
             (CASE
               WHEN D.������� = 2 AND D.С����� IN ('1', '2', '3', '12') THEN
                'ҩƷ��'
               WHEN D.������� = '1' AND D.С����� IN ('1', '2', '7') THEN
                '����'
               ELSE
                '������'
             END) AS ��������,
             D.�ܽ��,
             (C.�����ܶ� - C.�����ܶ�) AS �ο��Ը����
        FROM �������_�ҺŵǼ�     B,
             �������_���﷢Ʊ�Ǽ� C,
             �������_���ﴦ��     D
       WHERE B.�������� = C.��������
         AND C.�������� = D.��������
         AND B.���ﲡ���� = C.���ﲡ����
         AND C.���ﲡ���� = D.���ﲡ����
         AND C.��Ʊ��� = D.��Ʊ���
         AND C.�շ�ʱ�� >= B.�Һ�ʱ��
         AND D.�շ�ʱ�� >= B.�Һ�ʱ��
         AND B.�������� = STR_��������
         AND EXISTS (SELECT 1
                FROM ��������_�����б� A
               WHERE A.�������� = B.��������
                 AND A.������ = B.���ﲡ����
                 AND A.������� = '����'
                 AND A.��Ŀ���� = STR_��Ŀ����
                 AND A.��ˮ�� = STR_��ˮ��);
  
    OPEN CUR_����_�б���Ϣ FOR
    
      SELECT G.���ﲡ���� AS ������,
             G.�������� AS P900,
             'Ӫ�ھ��ü����������ڶ�����ҽԺ' AS P6891,
             NULL AS P686, --ҽ�Ʊ����ֲ��,
             XX.�������� AS P800,
             '01' AS P7501, -- ��������,
             G.���ﲡ���� AS P7502, -- ���￨��,
             G.���ﲡ���� AS P7000, -- ���������ˮ��,
             XX.���� AS P4,
             NVL(XX.�Ա�, '0') AS P5,
             XX.�������� AS P6,
             TO_NUMBER(REGEXP_REPLACE(XX.����, '[^-0-9.]', '')) P7,
             Q.�������� AS P12, --����,
             XX.����ID AS P11,
             DECODE(XX.����״��, '4', '9', XX.����״��) AS P8,
             XX.ְҵ AS P9, --B.ְҵ
             Q.֤�������� AS P7503, -- ע��֤�����ʹ���,
             XX.���֤�� AS P13,
             NVL(XX.��ס_��ַ, XX.��ͥ��ַ) AS P801, --B.��ס_��ַ
             XX.�ֻ����� AS P802,
             XX.��ס_�ʱ� AS P803, --B.��ס_�ʱ�
             NVL(XX.������λ����ַ, XX.������λ) AS P14, --B.������λ����ַ
             XX.��λ�绰 AS P15, --B.��λ�绰
             NULL AS P16, --������λ��������
             XX.��ϵ��_���� AS P18, --B.��ϵ��_����
             XX.��ϵ��_��ϵ AS P19, --B.��ϵ��_��ϵ
             XX.��ϵ��_��ַ AS P20, --B.��ϵ��_��ַ
             XX.��ϵ��_�绰 AS P21, --B.��ϵ��_�绰
             '1' AS P7505, --�������
             DECODE(G.�Ƿ���, '����', '��', '��') AS P7520, --�Ƿ����
             NULL AS P7521, --�Ƿ�ת��
             (SELECT A.�������
                FROM ������Ŀ_���Ҳ������� A
               WHERE A.�������� = G.��������
                 AND A.���ұ��� = G.������ұ���) P7504,
             (SELECT A.��������
                FROM ������Ŀ_�������� A
               WHERE A.�������� = G.��������
                 AND A.���ұ��� = G.������ұ���) AS P7522, --������Ҵ���
             G.�Һ�ʱ�� AS P7506, --��������
             NULL AS P7507, --����
             NULL AS P7523, --�ֲ�ʷ
             NULL AS P7524, --�����
             
             NULL AS P7525, --֢״����
             NULL AS P7526, --֢״����
             NULL AS P7527, --֢״����
             NULL AS P7528, --��������
             (CASE
               WHEN (SELECT COUNT(1)
                       FROM ���ﻤʿ_���۵Ǽ�
                      WHERE �������� = G.��������
                        AND ���ﲡ���� = G.���ﲡ����) > 0 THEN
                '1'
               ELSE
                '2'
             END) AS P7529, --�Ƿ�����
             G.�������� AS P28,
             G.�������� AS P281,
             NULL AS P7530,
             '9' AS P1, --ȷ����
             (SELECT SUM(B.�����ܶ�)
                FROM ��ʱ��_�����ϱ�_��Ժ���з��� B
               WHERE B.������ = G.���ﲡ����) AS P7508, --�ܷ���
             '0' AS P7509, --�Һŷ�
             (SELECT SUM(B.�����ܶ�)
                FROM ��ʱ��_�����ϱ�_��Ժ���з��� B
               WHERE B.������ = G.���ﲡ����
                 AND B.�������� = 'ҩƷ��') AS P7510, --ҩƷ��
             (SELECT SUM(B.�����ܶ�)
                FROM ��ʱ��_�����ϱ�_��Ժ���з��� B
               WHERE B.������ = G.���ﲡ����
                 AND B.�������� = '����') AS P7511, --����
             (SELECT SUM(B.�Ը��ܶ�)
                FROM ��ʱ��_�����ϱ�_��Ժ���з��� B
               WHERE B.������ = G.���ﲡ����) AS P7512 --�Ը��ܶ�
      
        FROM �������_�ҺŵǼ� G,
             (SELECT X.��������,
                     X.����ID,
                     X.����,
                     X.�Ա�,
                     X.��������,
                     X.����,
                     X.��ͥ��ַ,
                     X.������λ,
                     X.���֤��,
                     X.����ID,
                     X.����״��,
                     X.��������,
                     X.�ֻ�����,
                     X1.ְҵ,
                     X1.��ס_��ַ,
                     X1.��ס_�ʱ�,
                     X1.������λ����ַ,
                     X1.��λ�绰,
                     X1.��ϵ��_����,
                     X1.��ϵ��_��ϵ,
                     X1.��ϵ��_��ַ,
                     X1.��ϵ��_�绰
                FROM ������Ŀ_������Ϣ X
                LEFT JOIN ������Ŀ_���˲�����Ϣ X1
                  ON X.����ID = X1.����ID) XX,
             ������Ŀ_������Ϣ_���� Q
       WHERE G.�������� = XX.��������
         AND XX.�������� = Q.��������
         AND G.����ID = XX.����ID
         AND XX.����ID = Q.����ID
         AND G.�������� = STR_��������
         AND EXISTS (SELECT 1
                FROM ��������_�����б� A
               WHERE A.�������� = G.��������
                 AND A.������ = G.���ﲡ����
                 AND A.������� = '����'
                 AND A.��Ŀ���� = STR_��Ŀ����
                 AND A.��ˮ�� = STR_��ˮ��);
  
  END;

END PR_�����ϱ�_�������ﲡ����Ϣ;
/

prompt
prompt Creating procedure PR_�����ϱ�_�������ﲡ�˼���
prompt ===================================
prompt
CREATE OR REPLACE PROCEDURE CLOUDHIS.PR_�����ϱ�_�������ﲡ�˼���(STR_����          IN VARCHAR2,
                                           CUR_����_�б���Ϣ OUT SYS_REFCURSOR) IS

  STR_��������     VARCHAR2(50) := FU_ͨ��_��ȡ�ַ���ֵ(STR_����, '|', 1);
  DAT_��Ժʱ����ʼ DATE := TO_DATE(FU_ͨ��_��ȡ�ַ���ֵ(STR_����, '|', 2),
                             'yyyy-MM-dd hh24:mi:ss');
  DAT_��Ժʱ���ֹ DATE := TO_DATE(FU_ͨ��_��ȡ�ַ���ֵ(STR_����, '|', 3),
                             'yyyy-MM-dd hh24:mi:ss');
  STR_��������     VARCHAR2(50) := FU_ͨ��_��ȡ�ַ���ֵ(STR_����, '|', 4);
  STR_��Ŀ����     VARCHAR2(50) := FU_ͨ��_��ȡ�ַ���ֵ(STR_����, '|', 5);
  STR_��ˮ��       VARCHAR2(50) := FU_ͨ��_��ȡ�ַ���ֵ(STR_����, '|', 6);

  DAT_����ʱ�� DATE;
  NUM_��ˮ��   NUMBER;
  STR_�Ƿ�ش����� VARCHAR2(50);

BEGIN

  BEGIN
  
    SELECT SYSDATE INTO DAT_����ʱ�� FROM DUAL;
  BEGIN
      SELECT X.�Ƿ�ش�����
        INTO STR_�Ƿ�ش�����
        FROM ��������_��Ŀ��Ϣ X
       WHERE X.�������� = STR_��������
         AND X.��Ŀ���� = STR_��Ŀ����;
    EXCEPTION
      WHEN OTHERS THEN
        STR_�Ƿ�ش����� := '��';
    END;
  
    IF STR_�Ƿ�ش����� = '��' THEN
      DELETE FROM ��������_�����б�
       WHERE �������� = STR_��������
         AND ��Ŀ���� = STR_��Ŀ����;
    END IF;
    --DELETE FROM ��������_�����б�;
    IF STR_��ˮ�� IS NULL THEN
      SELECT NVL(MAX(��ˮ��), 0) + 1
        INTO NUM_��ˮ��
        FROM ��������_�����б�;
    
      INSERT INTO ��������_�����б�
        (��������,
         ������,
         ��ʼʱ��,
         ��ֹʱ��,
         ����ʱ��,
         ��ˮ��,
         �������,
         ��Ŀ����)
        SELECT STR_��������,
               Z.���ﲡ����,
               DAT_��Ժʱ����ʼ,
               DAT_��Ժʱ���ֹ,
               DAT_����ʱ��,
               NUM_��ˮ��,
               '����' AS �������,
               STR_��Ŀ����
          FROM �������_�ҺŵǼ� Z, ������Ŀ_������Ϣ T
         WHERE Z.�������� = T.��������
           AND Z.����ID = T.����ID
           AND Z.�������� = STR_��������
		   AND (Z.�Һſ��ұ��� ='000053' OR Z.������ұ���='000053')--���ȼ���
           AND NOT EXISTS (SELECT 1
                  FROM ��������_�����б� A
                 WHERE A.�������� = Z.��������
                   AND A.������ = Z.���ﲡ����
                   AND A.��Ŀ���� = STR_��Ŀ����)
           AND Z.�Һ�ʱ�� BETWEEN DAT_��Ժʱ����ʼ AND DAT_��Ժʱ���ֹ
           AND (Z.���ﲡ���� LIKE '%' || STR_�������� || '%' OR
               T.���� LIKE '%' || STR_�������� || '%');
    ELSE
      NUM_��ˮ�� := STR_��ˮ��;
    END IF;
  
    --�������ݼ�
    OPEN CUR_����_�б���Ϣ FOR
    
      SELECT Z.���ﲡ���� AS ������,
             T.����,
             DECODE(T.�Ա�, '1', '��', '2', 'Ů', 'δ֪') AS �Ա�,
             T.����,
             Z.�Һ�ʱ�� AS ����ʱ��,
             NULL AS ��Ժʱ��,
             '����' AS �������
        FROM �������_�ҺŵǼ� Z, ������Ŀ_������Ϣ T, ��������_�����б� D
       WHERE Z.�������� = T.��������
         AND T.�������� = D.��������
         AND Z.���ﲡ���� = D.������
         AND Z.����ID = T.����ID
         AND Z.�������� = STR_��������
         AND D.��ˮ�� = NUM_��ˮ��
         AND D.������� = '����';
  
  END;

END PR_�����ϱ�_�������ﲡ�˼���;
/

prompt
prompt Creating procedure PR_�����ϱ�_�������ﴦ����¼
prompt ===================================
prompt
CREATE OR REPLACE PROCEDURE CLOUDHIS.PR_�����ϱ�_�������ﴦ����¼(STR_����          IN VARCHAR2,
                                             CUR_����_�б���Ϣ OUT SYS_REFCURSOR) IS

  STR_�������� VARCHAR2(50) := FU_ͨ��_��ȡ�ַ���ֵ(STR_����, '|', 1);
  /*DAT_��Ժʱ����ʼ DATE := TO_DATE(FU_ͨ��_��ȡ�ַ���ֵ(STR_����, '|', 2),
                             'yyyy-MM-dd hh24:mi:ss');
  DAT_��Ժʱ���ֹ DATE := TO_DATE(FU_ͨ��_��ȡ�ַ���ֵ(STR_����, '|', 3),
                             'yyyy-MM-dd hh24:mi:ss');
  STR_��������     VARCHAR2(50) := FU_ͨ��_��ȡ�ַ���ֵ(STR_����, '|', 4);*/
  STR_��Ŀ���� VARCHAR2(50) := FU_ͨ��_��ȡ�ַ���ֵ(STR_����, '|', 5);
  STR_��ˮ��   VARCHAR2(50) := FU_ͨ��_��ȡ�ַ���ֵ(STR_����, '|', 6);
BEGIN
  BEGIN
   IF STR_��ˮ�� IS NULL THEN
      SELECT MAX(��ˮ��)
        INTO STR_��ˮ��
        FROM ��������_�����б�
       WHERE �������� = STR_��������
         AND ��Ŀ���� = STR_��Ŀ����;
    END IF;  
  
    OPEN CUR_����_�б���Ϣ FOR
      WITH W_���м����� AS
       (SELECT TT.�ӿڶ�����Ϣ����,
               TT.�ӿڶ�����Ϣ����,
               TTT.ϵͳ������Ϣ����
          FROM ��������_��Ŀ�ӿڶ��շ��� T,
               ��������_��Ŀ�ӿڶ�����ϸ TT,
               ��������_��Ŀϵͳ������ϸ TTT
         WHERE T.��ˮ�� = TT.���ID
           AND TT.��ˮ�� = TTT.���ID
           AND T.�������� = STR_��������
           AND T.��Ŀ���� = STR_��Ŀ����
           AND T.������� = 'CV06.00.102')
      
      SELECT G.���ﲡ���� AS ������,
             G.���ﲡ���� AS P7502, --���￨��
             G.�Һ�ʱ��   AS P7506, --��������
             G.���ﲡ���� P7000, --���������ˮ��
             X.����       AS P4,
             Y.�������   AS P7800,
             Y.¼��ʱ��   AS P7801, --��������ʱ��
             -----------------------------���������뿪ʼ--------------------
             (CASE
               WHEN (SELECT �������
                       FROM ������Ŀ_ҩƷ�ֵ�
                      WHERE ҩƷ���� = C.��Ŀ����) IN ('1', '2') THEN
                '7' --'��һ'
               WHEN (SELECT �������
                       FROM ������Ŀ_ҩƷ�ֵ�
                      WHERE ҩƷ���� = C.��Ŀ����) = 3 THEN
                '8' --'����'
               ELSE
                CASE
                  WHEN G.�Һſ��ұ��� IN (SELECT ���ұ���
                                      FROM ������Ŀ_���������б�
                                     WHERE �������� = G.��������
                                       AND ɾ����־ = '0'
                                       AND ���ͱ��� = '14') THEN
                   CASE
                     WHEN C.������� = '2' AND C.С����� = '1' THEN
                      '5' --'������ҩ'
                     WHEN C.������� = '2' AND C.С����� = '3' THEN
                      '6' --'�����в�ҩ'
                     WHEN C.������� = '2' AND C.С����� = '12' THEN
                      '9' --'��ҩ��Ƭ'
                     WHEN C.������� = '2' AND C.С����� = '2' THEN
                      '10' --'�г�ҩ'
                     ELSE
                      '99' --'����'
                   END
                  WHEN G.�Һſ��ұ��� IN (SELECT ���ұ���
                                      FROM ������Ŀ_���������б�
                                     WHERE �������� = G.��������
                                       AND ɾ����־ = '0'
                                       AND ���ͱ��� = '13') THEN
                   CASE
                     WHEN C.������� = '2' AND C.С����� = '1' THEN
                      '3' --'������ҩ'
                     WHEN C.������� = '2' AND C.С����� = '3' THEN
                      '4' --'�����в�ҩ'
                     WHEN C.������� = '2' AND C.С����� = '12' THEN
                      '9' --'��ҩ��Ƭ'
                     WHEN C.������� = '2' AND C.С����� = '2' THEN
                      '10' --'�г�ҩ'
                     ELSE
                      '99' --'����'
                   END
                  ELSE
                   CASE
                     WHEN C.������� = '2' AND C.С����� = '1' THEN
                      '1' --'������ҩ'
                     WHEN C.������� = '2' AND C.С����� = '3' THEN
                      '2' --'�����в�ҩ'
                     WHEN C.������� = '2' AND C.С����� = '12' THEN
                      '9' --'��ҩ��Ƭ'
                     WHEN C.������� = '2' AND C.С����� = '2' THEN
                      '10' --'�г�ҩ'
                     ELSE
                      '99' --'����'
                   END
                END
             END) AS P7802,
             -----------------------------�������������--------------------
             
             -----------------------------������Ŀ���࿪ʼ--------------------
             (CASE
               WHEN Y.������� = '2' AND Y.С����� = '1' THEN
                '11' --��ҩ
               WHEN Y.������� = '2' AND Y.С����� = '2' THEN
                '12' --�г�ҩ
               WHEN Y.������� = '2' AND Y.С����� = '3' THEN
                '13' --�г�ҩ
               WHEN Y.������� = '1' AND Y.С����� = '9' THEN
                '21' --����
               WHEN Y.������� = '1' AND Y.С����� = '1' THEN
                '22' --����
               WHEN Y.������� = '1' AND Y.С����� = '2' THEN
                '23' --���
               WHEN Y.������� = '1' AND Y.С����� = '3' THEN
                '24' --����
               WHEN Y.������� = '1' AND Y.С����� = '18' THEN
                '25' --����
               WHEN Y.������� = '1' AND Y.С����� = '8' THEN
                '26' --����
               WHEN Y.������� = '1' AND Y.С����� = '16' THEN
                '28' --��Ѫ
               ELSE
                '31' --����
             END) AS P7803, --������Ŀ�������
             (CASE
               WHEN Y.������� = '2' AND Y.С����� = '1' THEN
                '��ҩ' --��ҩ
               WHEN Y.������� = '2' AND Y.С����� = '2' THEN
                '�г�ҩ' --�г�ҩ
               WHEN Y.������� = '2' AND Y.С����� = '3' THEN
                '�г�ҩ' --�г�ҩ
               WHEN Y.������� = '1' AND Y.С����� = '9' THEN
                '����' --����
               WHEN Y.������� = '1' AND Y.С����� = '1' THEN
                '����' --����
               WHEN Y.������� = '1' AND Y.С����� = '2' THEN
                '���' --���
               WHEN Y.������� = '1' AND Y.С����� = '3' THEN
                '����' --����
               WHEN Y.������� = '1' AND Y.С����� = '18' THEN
                '����' --����
               WHEN Y.������� = '1' AND Y.С����� = '8' THEN
                '����' --����
               WHEN Y.������� = '1' AND Y.С����� = '16' THEN
                '��Ѫ' --��Ѫ
               ELSE
                '����' --����
             END) AS P7804, --������Ŀ��������
             -----------------------------������Ŀ�������--------------------
             
             C.��Ŀ���� AS P7805,
             C.��Ŀ���� AS P7806,
             (CASE
               WHEN C.������� = '2' AND C.С����� = '2' THEN
                '�г�ҩ'
               WHEN C.������� = '2' AND C.С����� = '3' THEN
                '�в�ҩ'
               WHEN C.������� = '2' AND C.С����� = '12' THEN
                '������ҩ'
               ELSE
                'δʹ��'
             END) AS P7807, --��ҩ�������
             (CASE
               WHEN C.������� = '2' AND C.С����� = '2' THEN
                '2'
               WHEN C.������� = '2' AND C.С����� = '3' THEN
                '3'
               WHEN C.������� = '2' AND C.С����� = '12' THEN
                '9'
               ELSE
                '1'
             END) AS P7808, --��ҩ������
             
             NULL AS P7809, --��ҩ��ע
             NULL AS P7810, --ҩ�����ʹ���
             NULL AS P7811, --ҩ����������
             C.���ͱ��� AS P7812, --ҩ����ʹ���
             C.�������� AS P7813, --ҩ���������
             C.��� AS P7814,
             C.Ƶ������ AS P7815,
             (SELECT A.����
                FROM ������Ŀ_Ƶ���ֵ� A
               WHERE A.�������� = C.��������
                 AND A.Ƶ�ʱ��� = C.Ƶ�ʱ���) * C.���� AS P7816,
             C.���� AS P7817,
             C.�������� AS P7818,
             (SELECT �ӿڶ�����Ϣ����
                FROM W_���м�����
               WHERE ϵͳ������Ϣ���� = C.�÷�����
                 AND ROWNUM = 1) AS P7819, --ҩ��ʹ��-;������
             (SELECT �ӿڶ�����Ϣ����
                FROM W_���м�����
               WHERE ϵͳ������Ϣ���� = C.�÷�����
                 AND ROWNUM = 1) AS P7820, --ҩ��ʹ��-;��
             DECODE(Y.Ƥ�Ա�־, '2', '2', '3', '1', '') AS P7821, --Ƥ���б��Ƿ����            
             Y.��ʼʱ�� AS P7822, --��ҩ��ʼʱ��
             NULL AS P7823, --��ҩ��ֹʱ��
             Y.���� AS P7824, --��ҩ����
             NULL AS P7825, --�Ƿ���ҩ
             decode(Y.����, '��', '1', '2') AS P7826, --�Ƿ�Ӽ�
             (SELECT A.�������
                FROM ������Ŀ_���Ҳ������� A
               WHERE A.�������� = y.��������
                 AND A.���ұ��� = y.�������ұ���) P7827,
             (SELECT A.��������
                FROM ������Ŀ_�������� A
               WHERE A.�������� = Y.��������
                 AND A.���ұ��� = Y.�������ұ���) AS P7828, --��������
             NULL AS P7829, --�Ƿ�ͳһ�ɹ�ҩƷ
             NULL AS P7830, --ҩƷ�ɹ���
             NULL AS P7831, --Ҫ��ƽ̨��
             NULL AS P7832 --�Ƿ������ҩ
        FROM �������_�ҺŵǼ�     G,
             ������Ŀ_������Ϣ     X,
             �������_����ҽ��     Y,
             �������_����ҽ����Ŀ Y1,
             �������_���ﴦ��     C
       WHERE G.�������� = C.��������
         AND X.�������� = C.��������
         AND X.�������� = Y.��������
         AND Y.�������� = Y1.��������
         AND G.���ﲡ���� = C.���ﲡ����
         AND C.���ﲡ���� = Y.���ﲡ����
         AND Y.��ĿID = Y1.��ĿID
         AND Y1.�Ƽ�ID = C.�Ƽ�ID
         AND G.����ID = X.����ID
         AND G.�������� = STR_��������
         AND EXISTS (SELECT 1
                FROM ��������_�����б� A
               WHERE A.�������� = G.��������
                 AND A.������ = G.���ﲡ����
                 AND A.������� = '����'
                 AND A.��Ŀ���� = STR_��Ŀ����
                 AND A.��ˮ�� = STR_��ˮ��);
  
  END;

END PR_�����ϱ�_�������ﴦ����¼;
/

prompt
prompt Creating procedure PR_�����ϱ�_�����������¼
prompt ===================================
prompt
CREATE OR REPLACE PROCEDURE CLOUDHIS.PR_�����ϱ�_�����������¼(STR_����          IN VARCHAR2,
                                             CUR_����_�б���Ϣ OUT SYS_REFCURSOR) IS

  STR_�������� VARCHAR2(50) := FU_ͨ��_��ȡ�ַ���ֵ(STR_����, '|', 1);
  /*DAT_��Ժʱ����ʼ DATE := TO_DATE(FU_ͨ��_��ȡ�ַ���ֵ(STR_����, '|', 2),
                             'yyyy-MM-dd hh24:mi:ss');
  DAT_��Ժʱ���ֹ DATE := TO_DATE(FU_ͨ��_��ȡ�ַ���ֵ(STR_����, '|', 3),
                             'yyyy-MM-dd hh24:mi:ss');
  STR_��������     VARCHAR2(50) := FU_ͨ��_��ȡ�ַ���ֵ(STR_����, '|', 4);*/
  STR_��Ŀ���� VARCHAR2(50) := FU_ͨ��_��ȡ�ַ���ֵ(STR_����, '|', 5);
  STR_��ˮ��   VARCHAR2(50) := FU_ͨ��_��ȡ�ַ���ֵ(STR_����, '|', 6);
BEGIN
  BEGIN
    IF STR_��ˮ�� IS NULL THEN
      SELECT MAX(��ˮ��)
        INTO STR_��ˮ��
        FROM ��������_�����б�
       WHERE �������� = STR_��������
         AND ��Ŀ���� = STR_��Ŀ����;
    END IF;
  
    OPEN CUR_����_�б���Ϣ FOR
      SELECT G.���ﲡ���� AS ������,
             G.���ﲡ���� AS P7502, --���￨��
             G.�Һ�ʱ�� AS P7506, --��������
             G.���ﲡ���� P7000, --���������ˮ��
             X.���� AS P4,
             G.�������� AS P7701, --�����������
             'Ӫ�ھ��ü����������ڶ�����ҽԺ' AS P7702, --�����������
             S.���뵥ID AS P7703,
             J.���浥�� AS P7704, --���浥��
             NULL AS P7705, --���浥����
             J.¼��ʱ�� AS P7706, --��������
             
             NULL AS P7707, --������
             S.��Ŀ���� AS P7708,
             NULL AS P7709, --�������
             S.��Ŀ���� AS P7710,
             M.ϸ����� AS P7711,
             M.ϸ������ AS P7712,
             NULL AS P7713, --��鲿λ
             DECODE(M.ϸ��ֵ, '����', '1', '2') AS P7714, --����Ƿ�����
             J.������� AS P7715,
             decode(M.����,'L','22','H','21','M','1','') AS P7716, --������쳣��ʶ
             J.������ AS P7717
      
        FROM �������_�ҺŵǼ�  G,
             ������Ŀ_������Ϣ  X,
             ������_����      S,
             ������_���      J,
             ������_���_��ϸ M
       WHERE G.�������� = X.��������
         AND X.�������� = S.��������
         AND S.�������� = J.��������
         AND G.����ID = X.����ID
         AND G.���ﲡ���� = S.������
         AND S.���뵥ID = J.���뵥ID
         AND J.ΨһID = M.���浥ID
         AND G.�������� = STR_��������
         AND S.ID���� = '����'
         AND S.���� = '���'
         AND EXISTS (SELECT 1
                FROM ��������_�����б� A
               WHERE A.�������� = G.��������
                 AND A.������ = G.���ﲡ����
                 AND A.��Ŀ���� = STR_��Ŀ����
                 AND A.��ˮ�� = STR_��ˮ��);
  
  END;

END PR_�����ϱ�_�����������¼;
/

prompt
prompt Creating procedure PR_�����ϱ�_������������¼
prompt ===================================
prompt
CREATE OR REPLACE PROCEDURE CLOUDHIS.PR_�����ϱ�_������������¼(STR_����          IN VARCHAR2,
                                             CUR_����_�б���Ϣ OUT SYS_REFCURSOR) IS

  STR_�������� VARCHAR2(50) := FU_ͨ��_��ȡ�ַ���ֵ(STR_����, '|', 1);
  /*DAT_��Ժʱ����ʼ DATE := TO_DATE(FU_ͨ��_��ȡ�ַ���ֵ(STR_����, '|', 2),
                             'yyyy-MM-dd hh24:mi:ss');
  DAT_��Ժʱ���ֹ DATE := TO_DATE(FU_ͨ��_��ȡ�ַ���ֵ(STR_����, '|', 3),
                             'yyyy-MM-dd hh24:mi:ss');
  STR_��������     VARCHAR2(50) := FU_ͨ��_��ȡ�ַ���ֵ(STR_����, '|', 4);*/
  STR_��Ŀ���� VARCHAR2(50) := FU_ͨ��_��ȡ�ַ���ֵ(STR_����, '|', 5);
  STR_��ˮ��   VARCHAR2(50) := FU_ͨ��_��ȡ�ַ���ֵ(STR_����, '|', 6);
BEGIN
  BEGIN
  
    IF STR_��ˮ�� IS NULL THEN
      SELECT MAX(��ˮ��)
        INTO STR_��ˮ��
        FROM ��������_�����б�
       WHERE �������� = STR_��������
         AND ��Ŀ���� = STR_��Ŀ����;
    END IF;
  
    OPEN CUR_����_�б���Ϣ FOR
      SELECT G.���ﲡ���� as ������,
             G.���ﲡ���� AS P7502, --���￨��
             G.�Һ�ʱ�� AS P7506, --��������
             G.���ﲡ���� P7000, --���������ˮ��
             X.���� AS P4,
             G.�������� AS P7601, --�����������
             'Ӫ�ھ��ü����������ڶ�����ҽԺ' AS P7602, --�����������
             NULL AS P7603, --���鱨�浥�������
             NULL AS P7604, --���鱨�浥������
             S.���뵥ID AS P7605,
             NULL AS P7606, --�������뵥������
             J.¼��ʱ�� AS P7607, --��������
             J.����ʱ�� AS P7608, --���鱨������
             NULL AS P7609, --�����ͼ�����
             NULL AS P7610, --�����������
             J.������ AS P7611,
             J.�걾���� AS P7612,
             S.��Ŀ���� AS P7613,
             S.��Ŀ���� AS P7614,
             
             S.��Ŀ���� AS P7515,
             S.��Ŀ���� AS P7516,
             NULL AS P7617, --���鷽��
             M.�ο�ֵ���� AS P7618,
             M.��λ AS P7619,
             M.ϸ��ֵ AS P7620,
             M.���� AS P7621,
             M.���浥ID AS P7622,
             M.ϸ����� AS P7623,
             M.ϸ������ AS P7624,
             DECODE(M.����, 'H', '21', 'L', '22', 'M', '1', '23') AS P7625
      
        FROM �������_�ҺŵǼ�  G,
             ������Ŀ_������Ϣ  X,
             ������_����      S,
             ������_���      J,
             ������_���_��ϸ M
       WHERE G.�������� = X.��������
         AND X.�������� = S.��������
         AND S.�������� = J.��������
         AND G.����ID = X.����ID
         AND G.���ﲡ���� = S.������
         AND S.���뵥ID = J.���뵥ID
         AND J.ΨһID = M.���浥ID
         AND G.�������� = STR_��������
         AND S.ID���� = '����'
         AND S.���� = '����'
         AND EXISTS (SELECT 1
                FROM ��������_�����б� A
               WHERE A.�������� = G.��������
                 AND A.������ = G.���ﲡ����
                 AND A.��Ŀ���� = STR_��Ŀ����
                 AND A.��ˮ�� = STR_��ˮ��);
  
  END;

END PR_�����ϱ�_������������¼;
/

prompt
prompt Creating procedure PR_�����ϱ�_���в��˼���
prompt =================================
prompt
CREATE OR REPLACE PROCEDURE PR_�����ϱ�_���в��˼���(STR_����          IN VARCHAR2,
                                           CUR_����_�б���Ϣ OUT SYS_REFCURSOR) IS

  STR_��������     VARCHAR2(50) := FU_ͨ��_��ȡ�ַ���ֵ(STR_����, '|', 1);
  DAT_��Ժʱ����ʼ DATE := TO_DATE(FU_ͨ��_��ȡ�ַ���ֵ(STR_����, '|', 2),
                             'yyyy-MM-dd hh24:mi:ss');
  DAT_��Ժʱ���ֹ DATE := TO_DATE(FU_ͨ��_��ȡ�ַ���ֵ(STR_����, '|', 3),
                             'yyyy-MM-dd hh24:mi:ss');
  STR_��������     VARCHAR2(50) := FU_ͨ��_��ȡ�ַ���ֵ(STR_����, '|', 4);
  STR_��Ŀ����     VARCHAR2(50) := FU_ͨ��_��ȡ�ַ���ֵ(STR_����, '|', 5);
  STR_��ˮ��       VARCHAR2(50) := FU_ͨ��_��ȡ�ַ���ֵ(STR_����, '|', 6);

  DAT_����ʱ�� DATE;
  NUM_��ˮ��   NUMBER;

  STR_�Ƿ�ش����� VARCHAR2(50);

BEGIN

  BEGIN
  
    SELECT SYSDATE INTO DAT_����ʱ�� FROM DUAL;
  
    BEGIN
      SELECT X.�Ƿ�ش�����
        INTO STR_�Ƿ�ش�����
        FROM ��������_��Ŀ��Ϣ X
       WHERE X.�������� = STR_��������
         AND X.��Ŀ���� = STR_��Ŀ����;
    EXCEPTION
      WHEN OTHERS THEN
        STR_�Ƿ�ش����� := '��';
    END;
  
    IF STR_�Ƿ�ش����� = '��' THEN
      DELETE FROM ��������_�����б�
       WHERE �������� = STR_��������
         AND ��Ŀ���� = STR_��Ŀ����;
    END IF;
  
    IF STR_��ˮ�� IS NULL THEN
      SELECT NVL(MAX(��ˮ��), 0) + 1
        INTO NUM_��ˮ��
        FROM ��������_�����б�;
    
      INSERT INTO ��������_�����б�
        (��������,
         ������,
         ��ʼʱ��,
         ��ֹʱ��,
         ����ʱ��,
         ��ˮ��,
         �������,
         ��Ŀ����)
        SELECT STR_��������,
               Z.���ﲡ����,
               DAT_��Ժʱ����ʼ,
               DAT_��Ժʱ���ֹ,
               DAT_����ʱ��,
               NUM_��ˮ��,
               '����' AS �������,
               STR_��Ŀ����
          FROM �������_�ҺŵǼ� Z, ������Ŀ_������Ϣ T
         WHERE Z.�������� = T.��������
           AND Z.����ID = T.����ID
           AND Z.�������� = STR_��������
           AND NOT EXISTS (SELECT 1
                  FROM ��������_�����б� A
                 WHERE A.�������� = Z.��������
                   AND A.������ = Z.���ﲡ����
                   AND A.��Ŀ���� = STR_��Ŀ����)
           AND Z.�Һ�ʱ�� BETWEEN DAT_��Ժʱ����ʼ AND DAT_��Ժʱ���ֹ
           AND (Z.���ﲡ���� LIKE '%' || STR_�������� || '%' OR
               T.���� LIKE '%' || STR_�������� || '%')
           AND (REGEXP_LIKE((SELECT B.ICD��
                              FROM ������Ŀ_�����ֵ� B
                             WHERE B.�������� = Z.��������),
                            '^(J[0-9]{2})') OR EXISTS
                (SELECT 1
                   FROM �������_����ҽ����Ŀ C
                  WHERE C.�������� = Z.��������
                    AND C.���ﲡ���� = Z.���ﲡ����
                    AND C.������� = '2'
                    AND REGEXP_LIKE(C.��Ŀ����,
                                    '(��˾��Τ|������Τ|������Τ|���ȶ��|���ȶ��|����鰷|����Ұ�|����Τ��)'))
               
               )
        
        UNION
        
        SELECT STR_��������,
               Z.סԺ������,
               DAT_��Ժʱ����ʼ,
               DAT_��Ժʱ���ֹ,
               DAT_����ʱ��,
               NUM_��ˮ��,
               '��Ժ' AS �������,
               STR_��Ŀ����
          FROM סԺ����_��Ժ������Ϣ Z, סԺ����_������ҳ T
         WHERE Z.�������� = T.��������
           AND Z.סԺ������ = T.סԺ������
           AND Z.�������� = STR_��������
           AND NOT EXISTS
         (SELECT 1
                  FROM ��������_�����б� A
                 WHERE A.�������� = Z.��������
                   AND A.������ = Z.סԺ������
                   AND A.��Ŀ���� = STR_��Ŀ����)
           AND T.��Ժ���� BETWEEN DAT_��Ժʱ����ʼ AND DAT_��Ժʱ���ֹ
           AND (T.סԺ������ LIKE '%' || STR_�������� || '%' OR
               T.������ LIKE '%' || STR_�������� || '%' OR
               T.�������� LIKE '%' || STR_�������� || '%')
           AND (EXISTS (SELECT 1
                          FROM סԺ����_��Ժ������� A
                         WHERE REGEXP_LIKE((CASE
                                             WHEN A.ICD�� = A.�������� THEN
                                              (SELECT B.ICD��
                                                 FROM ������Ŀ_�����ֵ� B
                                                WHERE B.�������� = A.��������)
                                             ELSE
                                              A.ICD��
                                           END),
                                           '^(J[0-9]{2})')
                           AND A.�������� = T.��������
                           AND A.סԺ������ = Z.סԺ������) OR EXISTS
                (SELECT 1
                   FROM �������_����ҽ����Ŀ C
                  WHERE C.�������� = Z.��������
                    AND C.���ﲡ���� = Z.סԺ������
                    AND C.������� = '2'
                    AND REGEXP_LIKE(C.��Ŀ����,
                                    '(��˾��Τ|������Τ|������Τ|���ȶ��|���ȶ��|����鰷|����Ұ�|����Τ��)')))
        
        UNION
        
        SELECT STR_��������,
               Z.סԺ������,
               DAT_��Ժʱ����ʼ,
               DAT_��Ժʱ���ֹ,
               DAT_����ʱ��,
               NUM_��ˮ��,
               '��Ժ' AS �������,
               STR_��Ŀ����
          FROM סԺ����_��Ժ������Ϣ Z, סԺ����_������ҳ T
         WHERE Z.�������� = T.��������
           AND Z.סԺ������ = T.סԺ������
           AND Z.�������� = STR_��������
           AND NOT EXISTS
         (SELECT 1
                  FROM ��������_�����б� A
                 WHERE A.�������� = Z.��������
                   AND A.������ = Z.סԺ������
                   AND A.��Ŀ���� = STR_��Ŀ����)
           AND T.��Ժ���� BETWEEN DAT_��Ժʱ����ʼ AND DAT_��Ժʱ���ֹ
           AND (T.סԺ������ LIKE '%' || STR_�������� || '%' OR
               T.������ LIKE '%' || STR_�������� || '%' OR
               T.�������� LIKE '%' || STR_�������� || '%')
           AND (EXISTS (SELECT 1
                          FROM סԺ����_��Ժ������� A
                         WHERE REGEXP_LIKE((CASE
                                             WHEN A.ICD�� = A.�������� THEN
                                              (SELECT B.ICD��
                                                 FROM ������Ŀ_�����ֵ� B
                                                WHERE B.�������� = A.��������)
                                             ELSE
                                              A.ICD��
                                           END),
                                           '^(J[0-9]{2})')
                           AND A.�������� = T.��������
                           AND A.סԺ������ = Z.סԺ������) OR EXISTS
                (SELECT 1
                   FROM �������_����ҽ����Ŀ C
                  WHERE C.�������� = Z.��������
                    AND C.���ﲡ���� = Z.סԺ������
                    AND C.������� = '2'
                    AND REGEXP_LIKE(C.��Ŀ����,
                                    '(��˾��Τ|������Τ|������Τ|���ȶ��|���ȶ��|����鰷|����Ұ�|����Τ��)')));
    ELSE
      NUM_��ˮ�� := STR_��ˮ��;
    END IF;
  
    --�������ݼ�
    OPEN CUR_����_�б���Ϣ FOR
    
      SELECT Z.���ﲡ���� AS ������,
             T.����,
             DECODE(T.�Ա�, '1', '��', '2', 'Ů', 'δ֪') AS �Ա�,
             T.����,
             Z.�Һ�ʱ�� AS ����ʱ��,
             NULL AS ��Ժʱ��,
             '����' AS �������
        FROM �������_�ҺŵǼ� Z, ������Ŀ_������Ϣ T, ��������_�����б� D
       WHERE Z.�������� = T.��������
         AND T.�������� = D.��������
         AND Z.���ﲡ���� = D.������
         AND Z.����ID = T.����ID
         AND Z.�������� = STR_��������
         AND D.��ˮ�� = NUM_��ˮ��
         AND D.������� = '����'
      
      UNION
      
      SELECT Z.סԺ������ AS ������,
             Z.�������� AS ����,
             DECODE(Z.�Ա�, '1', '��', '2', 'Ů', 'δ֪') AS �Ա�,
             Z.����,
             Z.��Ժʱ�� AS ����ʱ��,
             NULL AS ��Ժʱ��,
             '��Ժ' AS �������
        FROM סԺ����_��Ժ������Ϣ Z,
             סԺ����_������ҳ     T,
             ��������_�����б�     D
       WHERE Z.�������� = T.��������
         AND T.�������� = D.��������
         AND Z.סԺ������ = D.������
         AND Z.סԺ������ = T.סԺ������
         AND Z.�������� = STR_��������
         AND D.��ˮ�� = NUM_��ˮ��
         AND D.������� = '��Ժ'
      
      UNION
      
      SELECT Z.סԺ������ AS ������,
             Z.�������� AS ����,
             DECODE(Z.�Ա�, '1', '��', '2', 'Ů', 'δ֪') AS �Ա�,
             Z.����,
             Z.��Ժʱ�� AS ����ʱ��,
             Z.��Ժʱ�� AS ��Ժʱ��,
             '��Ժ' AS �������
        FROM סԺ����_��Ժ������Ϣ Z,
             סԺ����_������ҳ     T,
             ��������_�����б�     D
       WHERE Z.�������� = T.��������
         AND T.�������� = D.��������
         AND Z.סԺ������ = D.������
         AND Z.סԺ������ = T.סԺ������
         AND Z.�������� = STR_��������
         AND D.��ˮ�� = NUM_��ˮ��
         AND D.������� = '��Ժ';
  
  END;

END PR_�����ϱ�_���в��˼���;

/

prompt
prompt Creating procedure PR_�����ϱ�_���г�Ժ����
prompt =================================
prompt
CREATE OR REPLACE PROCEDURE CLOUDHIS.PR_�����ϱ�_���г�Ժ����(STR_����          IN VARCHAR2,
                                           CUR_����_�б���Ϣ OUT SYS_REFCURSOR) IS
  STR_SQL VARCHAR2(1000);

  STR_�������� VARCHAR2(50) := FU_ͨ��_��ȡ�ַ���ֵ(STR_����, '|', 1);
  /*DAT_��Ժʱ����ʼ DATE := TO_DATE(FU_ͨ��_��ȡ�ַ���ֵ(STR_����, '|', 2),
                             'yyyy-MM-dd hh24:mi:ss');
  DAT_��Ժʱ���ֹ DATE := TO_DATE(FU_ͨ��_��ȡ�ַ���ֵ(STR_����, '|', 3),
                             'yyyy-MM-dd hh24:mi:ss');
  STR_��������     VARCHAR2(50) := FU_ͨ��_��ȡ�ַ���ֵ(STR_����, '|', 4);*/
  STR_��Ŀ���� VARCHAR2(50) := FU_ͨ��_��ȡ�ַ���ֵ(STR_����, '|', 5);
  STR_��ˮ��   VARCHAR2(50) := FU_ͨ��_��ȡ�ַ���ֵ(STR_����, '|', 6);

  --��ȡ��ϼ�¼CURSOR
  CURSOR CUR_��ϼ�¼ IS
    SELECT *
      FROM (SELECT ROW_NUMBER() OVER(PARTITION BY T.סԺ������, T.������� ORDER BY T.�������, T.�Ƿ������ DESC) RN,
                   T.סԺ������,
                   (CASE
                     WHEN T.ICD�� = T.�������� THEN
                      (SELECT B.ICD��
                         FROM ������Ŀ_�����ֵ� B
                        WHERE B.�������� = T.��������)
                     ELSE
                      T.ICD��
                   END) ICD��,
                   T.��������,
                   T.��Ժ����,
                   T.��Ժ���,
                   T.�����,
                   T.��Ժʱ���,
                   T.��Ժ��ȷ������,
                   T.�������
              FROM סԺ����_��Ժ������� T,
                   סԺ����_��Ժ������Ϣ TT,
                   סԺ����_������ҳ     TTT
             WHERE T.סԺ������ = TT.סԺ������
               AND T.סԺ������ = TTT.סԺ������
               AND T.�������� = STR_��������
               AND EXISTS (SELECT 1
                      FROM ��������_�����б� A
                     WHERE A.�������� = T.��������
                       AND A.������ = T.סԺ������
                       AND A.������� = '��Ժ'
                       AND A.��Ŀ���� = STR_��Ŀ����
                       AND A.��ˮ�� = STR_��ˮ��)) G
     WHERE (G.������� = '�������' AND G.RN <= 10)
        OR (G.������� IN ('���˺��ж��ⲿԭ��', '�������') AND G.RN <= 3)
        OR (G.������� IN ('��Ժ���', '��Ժ���') AND G.RN = 1);
  ROW_��ϼ�¼ CUR_��ϼ�¼%ROWTYPE;

  --��ȡ������¼CURSOR
  CURSOR CUR_������¼ IS
    SELECT *
      FROM (SELECT ROW_NUMBER() OVER(PARTITION BY T.סԺ������ ORDER BY T.��ˮ�� DESC) RN,
                   T.סԺ������,
                   T.������,
                   T.������������,
                   T.�����������,
                   T.������������,
                   --������������
                   --��������ʱ��
                   (SELECT A.��Ա����
                      FROM ������Ŀ_��Ա���� A
                     WHERE A.�������� = T.��������
                       AND A.ɾ����־ = '0'
                       AND A.��Ա���� = T.����) AS ����,
                   (SELECT A.��Ա����
                      FROM ������Ŀ_��Ա���� A
                     WHERE A.�������� = T.��������
                       AND A.ɾ����־ = '0'
                       AND A.��Ա���� = T.I��) AS I��,
                   (SELECT A.��Ա����
                      FROM ������Ŀ_��Ա���� A
                     WHERE A.�������� = T.��������
                       AND A.ɾ����־ = '0'
                       AND A.��Ա���� = T.II��) AS II��,
                   T.����ʽ����,
                   T.����ּ�����,
                   T.�п����ϵȼ�����,
                   (SELECT A.��Ա����
                      FROM ������Ŀ_��Ա���� A
                     WHERE A.�������� = T.��������
                       AND A.ɾ����־ = '0'
                       AND A.��Ա���� = T.����ҽʦ) AS ����ҽʦ
              FROM סԺ����_������ҳ������ T,
                   סԺ����_��Ժ������Ϣ   TT,
                   סԺ����_������ҳ       TTT
             WHERE T.סԺ������ = TT.סԺ������
               AND T.סԺ������ = TTT.סԺ������
               AND T.�������� = STR_��������
               AND EXISTS (SELECT 1
                      FROM ��������_�����б� A
                     WHERE A.�������� = T.��������
                       AND A.������ = T.סԺ������
                       AND A.������� = '��Ժ'
                       AND A.��Ŀ���� = STR_��Ŀ����
                       AND A.��ˮ�� = STR_��ˮ��)) G
     WHERE G.RN <= 7;

  ROW_������¼ CUR_������¼%ROWTYPE;

  --��ȡ���ü�¼CURSOR
  CURSOR CUR_���ü�¼ IS
    SELECT T.��������,
           T.סԺ������,
           SUM(T.������) AS �ܽ��,
           (SELECT (A.ʵ�ս�� - A.�ܲ������)
              FROM סԺ����_��Ժ���˷�Ʊ�Ǽ� A
             WHERE A.�������� = STR_��������
               AND A.סԺ������ = T.סԺ������
               AND A.�ٻر�־ = '��') AS �Ը����,
           NVL(SUM(CASE T.�������
                     WHEN '10001' THEN
                      T.������
                   END),
               0) AS һ��ҽ�Ʒ������,
           NVL(SUM(CASE T.�������
                     WHEN '10002' THEN
                      T.������
                   END),
               0) AS һ�����Ʋ�����,
           NVL(SUM(CASE T.�������
                     WHEN '10003' THEN
                      T.������
                   END),
               0) AS �����,
           NVL(SUM(CASE T.�������
                     WHEN '10004' THEN
                      T.������
                   END),
               0) AS �ۺ�ҽ�Ʒ�������������,
           NVL(SUM(CASE T.�������
                     WHEN '10005' THEN
                      T.������
                   END),
               0) AS ������Ϸ�,
           NVL(SUM(CASE T.�������
                     WHEN '10006' THEN
                      T.������
                   END),
               0) AS ʵ������Ϸ�,
           NVL(SUM(CASE T.�������
                     WHEN '10007' THEN
                      T.������
                   END),
               0) AS Ӱ��ѧ��Ϸ�,
           NVL(SUM(CASE T.�������
                     WHEN '10008' THEN
                      T.������
                   END),
               0) AS �ٴ������Ŀ��,
           NVL(SUM(CASE T.�������
                     WHEN '10009' THEN
                      T.������
                     WHEN '10010' THEN
                      T.������
                   END),
               0) AS ������������Ŀ��,
           NVL(SUM(CASE T.�������
                     WHEN '10010' THEN
                      T.������
                   END),
               0) AS ����_�ٴ��������Ʒ�,
           NVL(SUM(CASE T.�������
                     WHEN '10011' THEN
                      T.������
                     WHEN '10012' THEN
                      T.������
                     WHEN '10013' THEN
                      T.������
                   END),
               0) AS �������Ʒ�,
           NVL(SUM(CASE T.�������
                     WHEN '10012' THEN
                      T.������
                   END),
               0) AS ����_�����,
           NVL(SUM(CASE T.�������
                     WHEN '10013' THEN
                      T.������
                   END),
               0) AS ����_������,
           NVL(SUM(CASE T.�������
                     WHEN '10014' THEN
                      T.������
                   END),
               0) AS ������,
           NVL(SUM(CASE T.�������
                     WHEN '10015' THEN
                      T.������
                   END),
               0) AS ��ҽ���Ʒ�,
           NVL(SUM(CASE T.�������
                     WHEN '10016' THEN
                      T.������
                     WHEN '10017' THEN
                      T.������
                   END),
               0) AS ��ҩ��,
           NVL(SUM(CASE T.�������
                     WHEN '10017' THEN
                      T.������
                   END),
               0) AS ����_����ҩ���,
           NVL(SUM(CASE T.�������
                     WHEN '10018' THEN
                      T.������
                   END),
               0) AS �г�ҩ��,
           NVL(SUM(CASE T.�������
                     WHEN '10019' THEN
                      T.������
                   END),
               0) AS �в�ҩ��,
           NVL(SUM(CASE T.�������
                     WHEN '10020' THEN
                      T.������
                   END),
               0) AS Ѫ��,
           NVL(SUM(CASE T.�������
                     WHEN '10021' THEN
                      T.������
                   END),
               0) AS �׵�������Ʒ��,
           NVL(SUM(CASE T.�������
                     WHEN '10022' THEN
                      T.������
                   END),
               0) AS �򵰰�����Ʒ��,
           NVL(SUM(CASE T.�������
                     WHEN '10023' THEN
                      T.������
                   END),
               0) AS ��Ѫ��������Ʒ��,
           NVL(SUM(CASE T.�������
                     WHEN '10024' THEN
                      T.������
                   END),
               0) AS ϸ����������Ʒ��,
           NVL(SUM(CASE T.�������
                     WHEN '10025' THEN
                      T.������
                   END),
               0) AS �����һ����ҽ�ò��Ϸ�,
           NVL(SUM(CASE T.�������
                     WHEN '10026' THEN
                      T.������
                   END),
               0) AS ������һ����ҽ�ò��Ϸ�,
           NVL(SUM(CASE T.�������
                     WHEN '10027' THEN
                      T.������
                   END),
               0) AS ������һ����ҽ�ò��Ϸ�,
           NVL(SUM(CASE T.�������
                     WHEN '10028' THEN
                      T.������
                   END),
               0) AS ������
      FROM (SELECT ZT.����       AS �������,
                   ZT.����       AS ��������,
                   ZZ.�ܽ��     AS ������,
                   ZZ.��������,
                   ZZ.סԺ������,
                   ZZ.����ʱ��
              FROM ������Ŀ_�ֵ���ϸ ZT
              LEFT JOIN (SELECT SUM(�ܽ��) AS �ܽ��,
                               ��ҽ��������,
                               ��������,
                               סԺ������,
                               ����ʱ��
                          FROM (SELECT A.�ܽ�� AS �ܽ��,
                                       NVL(Z.BAGLBM, '10028') AS ��ҽ��������,
                                       A.��������,
                                       A.סԺ������,
                                       A.����ʱ��
                                  FROM סԺ����_��Ժ���˴��� A
                                  LEFT JOIN JCXM_ZLZD_FJXM Z
                                    ON A.�������� = Z.JGBM
                                   AND A.��Ŀ���� = Z.XMBM
                                 WHERE A.�������� = STR_��������
                                   AND A.������� = '1'
                                
                                UNION ALL
                                
                                SELECT A.�ܽ�� AS �ܽ��,
                                       NVL(Y.BAGLBM, '10028') AS ��ҽ��������,
                                       A.��������,
                                       A.סԺ������,
                                       A.����ʱ��
                                  FROM סԺ����_��Ժ���˴��� A
                                  LEFT JOIN JCXM_YPZD_FJSX Y
                                    ON A.�������� = Y.JGBM
                                   AND A.��Ŀ���� = Y.YPBM
                                 WHERE A.�������� = STR_��������
                                   AND A.������� = '2')
                         GROUP BY ��������,
                                  סԺ������,
                                  ����ʱ��,
                                  ��ҽ��������) ZZ
                ON ZT.���� = ZZ.��ҽ��������
             WHERE ZT.������� = 'GB_009001'
               AND ZT.��Ч״̬ = '��Ч'
               AND ZT.ɾ����־ = '0'
               AND ZT.���� LIKE '��ҽ����_%') T,
           סԺ����_��Ժ������Ϣ TT,
           סԺ����_������ҳ TTT
     WHERE T.סԺ������ = TT.סԺ������
       AND T.סԺ������ = TTT.סԺ������
       AND EXISTS (SELECT 1
              FROM ��������_�����б� A
             WHERE A.�������� = T.��������
               AND A.������ = T.סԺ������
               AND A.������� = '��Ժ'
               AND A.��Ŀ���� = STR_��Ŀ����
               AND A.��ˮ�� = STR_��ˮ��)
     GROUP BY T.��������, T.סԺ������;

  ROW_���ü�¼ CUR_���ü�¼%ROWTYPE;

  --��ȡ��֢�໤��¼CURSOR
  CURSOR CUR_��֢�໤��¼ IS
    SELECT *
      FROM (SELECT ROW_NUMBER() OVER(PARTITION BY T.סԺ������ ORDER BY T.��ˮ�� DESC) RN,
                   T.��������,
                   T.����ʱ��,
                   T.�˳�ʱ��,
                   T.סԺ������
              FROM סԺ����_������ҳ_��֢�໤ T,
                   סԺ����_��Ժ������Ϣ      TT,
                   סԺ����_������ҳ          TTT
             WHERE T.סԺ������ = TT.סԺ������
               AND T.סԺ������ = TTT.סԺ������
               AND EXISTS (SELECT 1
                      FROM ��������_�����б� A
                     WHERE A.�������� = T.��������
                       AND A.������ = T.סԺ������
                       AND A.������� = '��Ժ'
                       AND A.��Ŀ���� = STR_��Ŀ����
                       AND A.��ˮ�� = STR_��ˮ��)) G
     WHERE G.RN <= 5;

  ROW_��֢�໤��¼ CUR_��֢�໤��¼%ROWTYPE;

BEGIN

  BEGIN
    
   IF STR_��ˮ�� IS NULL THEN
      SELECT MAX(��ˮ��)
        INTO STR_��ˮ��
        FROM ��������_�����б�
       WHERE �������� = STR_��������
         AND ��Ŀ���� = STR_��Ŀ����;
    END IF;
  
    DELETE FROM ��ʱ��_�����ϱ�_��Ժ���в���;
  
    --���»�����Ϣ
    INSERT INTO ��ʱ��_�����ϱ�_��Ժ���в���
      (������,
       P900, --ҽ�ƻ�������
       P6891, --��������
       P686, --ҽ�Ʊ����ֲᣨ������
       P800, --��������
       P1, --ҽ�Ƹ��ʽ
       P2, --סԺ����
       P3, --������
       P4, --����
       P5, --�Ա�
       P6, --��������
       P7, --����
       P8, --����״��
       P9, --ְҵ
       P101, --����ʡ��
       P102, --��������
       P103, --��������
       P11, --����
       P12, --����
       P13, --���֤��
       P801, --��סַ
       P802, --סլ�绰
       P803, --��סַ��������
       P14, --������λ����ַ
       P15, --�绰
       P16, --������λ��������
       P17, --���ڵ�ַ
       P171, --�������ڵ���������
       P18, --��ϵ������
       P19, --��ϵ
       P20, --��ϵ�˵�ַ
       P804, --��Ժ;��
       P21, --��ϵ�˵绰
       P22, --��Ժ����
       P23, --��Ժ�Ʊ�
       P231, --��Ժ����
       P24, --ת�ƿƱ�
       P25, --��Ժ����
       P26, --��Ժ�Ʊ�
       P261, --��Ժ����
       P27, --ʵ��סԺ����
       P28, --�ţ���������ϱ���
       P281, --�ţ��������������
       
       --������
       P372, --����ҩ������
       P38, --HBSAG
       P39, --HCV-AB
       P40, --HIV-AB
       P411, --�������Ժ��Ϸ������
       P412, --��Ժ���Ժ��Ϸ������
       P413, --��ǰ��������Ϸ������
       P414, --�ٴ��벡����Ϸ������
       P415, --�����벡����Ϸ������
       P421, --���ȴ���
       P422, --���ȳɹ�����
       P687, --����������
       P688, --�ֻ��̶�
       
        P431, --������
       P432, --��(����)��ҽʦ
       P433, --����ҽʦ
       P434, --סԺҽʦ
       P819, --���λ�ʿ
       P435, --����ҽʦ
       P436, --�о���ʵϰҽʦ
       P437, --ʵϰҽʦ
       P438, --����Ա
       P44, --��������
       P45, --�ʿ�ҽʦ
       P46, --�ʿػ�ʦ
       P47, --�ʿ�����
       
       --����
       
       P561, --�ؼ���������
       P562, --һ����������
       P563, --������������
       P564, --������������
       
       --��֢�໤����
       P57, --��������ʬ��
       P58, --���������ơ���顢���Ϊ��Ժ��һ��
       P581, --������������
       P60, --����
       P611, --��������
       P612, --��������
       P613, --��������
       P59, --ʾ�̲���
       P62, --ABOѪ��
       P63, --RHѪ��
       P64, --��Ѫ��Ӧ
       P651, --��ϸ��
       P652, --ѪС��
       P653, --Ѫ��
       P654, --ȫѪ
       P655, --�������
       P656, --����
       
       P66, --��Ӥ�׶�������
       P681, --��������������1
       P67, --��������Ժ����
       P731, --��Ժǰ����Сʱ(����ʱ��)
       P732, --��Ժǰ���ٷ���(����ʱ��)
       P733, --��Ժ�����Сʱ(����ʱ��)
       P734, --��Ժ����ٷ���(����ʱ��)
       P72, --������ʹ��ʱ��
       P830, --�Ƿ��г�Ժ31������סԺ�ƻ�
       P831, --��Ժ31����סԺ�ƻ�Ŀ��
       P741, --��Ժ��ʽ
       P742, --ת��ҽԺ����
       P743 --���������������
       )
      SELECT T.סԺ������,
             T.��������,
             'Ӫ�ھ��ü����������ڶ�����ҽԺ' AS ��������,
             NULL,
             T.��������,
             T.ҽ�Ƹ��ѷ�ʽ,
             T.סԺ����,
             T.������,
             T.��������,
             NVL(T.�Ա�, '0'),
             T.��������,
             TO_NUMBER(REGEXP_REPLACE(T.����, '[^-0-9.]', '')) ����,
             decode(T.����,'4','9',T.����),
             T.ְҵ,
             T.������ʡ,
             T.��������,
             T.��������,
             T.����,
             T.����,
             T.���֤��,
             T.��סַ,
             T.���ڵ绰,
             SUBSTR(T.��סַ�ʱ�, 0, 6),
             T.������λ��ַ,
             T.�����绰,
             SUBSTR(T.������������, 0, 6),
             T.���ڵ�ַ,
             SUBSTR(T.������������, 0, 6),
             T.��ϵ������,
             T.��ϵ,
             T.��ϵ�˵�ַ,
             DECODE(T.��Ժ;��,'9','4',T.��Ժ;��),
             T.��ϵ�˵绰,
             T.��Ժ����,
             (SELECT A.�������
                FROM ������Ŀ_���Ҳ������� A
               WHERE A.�������� = STR_��������
                 AND A.���ұ��� = T.��Ժ���ұ���) ��Ժ���ұ���,
             T.��Ժ����,
             (SELECT ת�����ұ���
                FROM סԺ����_��Ժ����ת�Ƽ�¼
               WHERE �������� = STR_��������
                 AND סԺ������ = T.סԺ������
                 AND ROWNUM = 1) ת�ƿƱ�,
             T.��Ժ����,
             (SELECT A.�������
                FROM ������Ŀ_���Ҳ������� A
               WHERE A.�������� = STR_��������
                 AND A.���ұ��� = T.��Ժ���ұ���) ��Ժ���ұ���,
             T.��Ժ����,
             T.סԺ����,
             (SELECT (CASE
                       WHEN A.ICD�� = A.�������� THEN
                        (SELECT B.ICD��
                           FROM ������Ŀ_�����ֵ� B
                          WHERE B.�������� = A.��������)
                       ELSE
                        A.ICD��
                     END) ICD��
                FROM סԺ����_��Ժ������� A
               WHERE A.�������� = STR_��������
                 AND A.סԺ������ = T.סԺ������
                 AND A.������� = '�������'
                 AND A.��Ϸ��� = '1'
                 AND ROWNUM = 1) AS ��������,
             (SELECT A.��������
                FROM סԺ����_��Ժ������� A
               WHERE A.�������� = STR_��������
                 AND A.סԺ������ = T.סԺ������
                 AND A.������� = '�������'
                 AND A.��Ϸ��� = '1'
                 AND ROWNUM = 1) AS �����������,
             
             --ʡ���������
           
             T.����ҩ��,
             T.HBSAG,
             T.HCV_AB,
             T.HIV_AB,
             DECODE(T.�������Ժ, '����', '1', '������', '2', '9'), --�������Ժ��Ϸ������
             DECODE(T.��Ժ���Ժ, '����', '1', '������', '2', '9'), --��Ժ���Ժ��Ϸ������
             DECODE(T.��ǰ������, '����', '1', '������', '2', '9'), --��ǰ��������Ϸ������
             DECODE(T.�ٴ��벡��, '����', '1', '������', '2', '9'), --�ٴ��벡����Ϸ������
             DECODE(T.�����벡��, '����', '1', '������', '2', '9'), --�����벡����Ϸ������
             T.���ȴ���,
             T.�ɹ�����,
             NULL, --����������
             NULL, --�ֻ��̶�
             
             (SELECT ��Ա����
                FROM ������Ŀ_��Ա����
               WHERE �������� = T.��������
                 AND ɾ����־ = '0'
                 AND ��Ա���� = T.������) AS ������,
             (SELECT ��Ա����
                FROM ������Ŀ_��Ա����
               WHERE �������� = T.��������
                 AND ɾ����־ = '0'
                 AND ��Ա���� = T.����ҽʦ) AS ����ҽʦ,
             (SELECT ��Ա����
                FROM ������Ŀ_��Ա����
               WHERE �������� = T.��������
                 AND ɾ����־ = '0'
                 AND ��Ա���� = T.����ҽʦ) AS ����ҽʦ,
             (SELECT ��Ա����
                FROM ������Ŀ_��Ա����
               WHERE �������� = T.��������
                 AND ɾ����־ = '0'
                 AND ��Ա���� = T.סԺҽʦ) AS סԺҽʦ,
             (SELECT ��Ա����
                FROM ������Ŀ_��Ա����
               WHERE �������� = T.��������
                 AND ɾ����־ = '0'
                 AND ��Ա���� = T.���λ�ʿ) AS ���λ�ʿ,
             (SELECT ��Ա����
                FROM ������Ŀ_��Ա����
               WHERE �������� = T.��������
                 AND ɾ����־ = '0'
                 AND ��Ա���� = T.����ҽʦ) AS ����ҽʦ,
             (SELECT ��Ա����
                FROM ������Ŀ_��Ա����
               WHERE �������� = T.��������
                 AND ɾ����־ = '0'
                 AND ��Ա���� = T.�о���ʵϰҽʦ) AS �о���ʵϰҽʦ,
             (SELECT ��Ա����
                FROM ������Ŀ_��Ա����
               WHERE �������� = T.��������
                 AND ɾ����־ = '0'
                 AND ��Ա���� = T.ʵϰҽʦ) AS ʵϰҽʦ,
             (SELECT ��Ա����
                FROM ������Ŀ_��Ա����
               WHERE �������� = T.��������
                 AND ɾ����־ = '0'
                 AND ��Ա���� = T.����Ա) AS ����Ա,
             T.��������,
             (SELECT ��Ա����
                FROM ������Ŀ_��Ա����
               WHERE �������� = T.��������
                 AND ɾ����־ = '0'
                 AND ��Ա���� = T.�ʿ�ҽ��) AS �ʿ�ҽ��,
             (SELECT ��Ա����
                FROM ������Ŀ_��Ա����
               WHERE �������� = T.��������
                 AND ɾ����־ = '0'
                 AND ��Ա���� = T.�ʿػ�ʿ) AS �ʿػ�ʿ,
             
             TRUNC(TO_DATE(T.�ʿ�����, 'yyyy-MM-dd hh24:mi:ss')) �ʿ�����,
             
             --ʡ����������
             
             T.�ؼ���������,
             T.һ����������,
             T.������������,
             T.������������,
             
             --��֢�໤����
             T.ʬ��,
             T.�Ƿ�Ժ��һ��,
             NULL, --������������
             
             T.����,
             T.����������,
             T.����������,
             T.����������,
             T.ʾ�̲���,
             T.Ѫ��,
             T.RH,
             T.��Ѫ��Ӧ,
             T.��ϸ��,
             T.ѪС��,
             T.Ѫ��,
             T.ȫѪ,
             T.����Ѫ����,
             T.����,
             
             TO_NUMBER(REGEXP_REPLACE(T.���䲻��1����, '[^-0-9.]', '')) ���䲻��1����,
             (CASE
               WHEN TO_NUMBER(T.��Ժ���� - T.��������) < 28 THEN
                REGEXP_REPLACE(T.��������������, '[^-0-9.]', '')
               ELSE
                ''
             END) AS ��������������,
             (CASE
               WHEN TO_NUMBER(T.��Ժ���� - T.��������) < 28 THEN
                REGEXP_REPLACE(T.��������Ժ����, '[^-0-9.]', '')
               ELSE
                ''
             END) AS ��������Ժ����,
             
             TO_NUMBER(REGEXP_REPLACE(­�����˻��߻���ʱ����ԺǰСʱ,
                                      '[^-0-9.]',
                                      '')) ­�����˻��߻���ʱ����ԺǰСʱ,
             TO_NUMBER(REGEXP_REPLACE(­�����˻��߻���ʱ����Ժǰ����,
                                      '[^-0-9.]',
                                      '')) ­�����˻��߻���ʱ����Ժǰ����,
             TO_NUMBER(REGEXP_REPLACE(­�����˻��߻���ʱ����Ժ��Сʱ,
                                      '[^-0-9.]',
                                      '')) ­�����˻��߻���ʱ����Ժ��Сʱ,
             TO_NUMBER(REGEXP_REPLACE(­�����˻��߻���ʱ����Ժ����,
                                      '[^-0-9.]',
                                      '')) ­�����˻��߻���ʱ����Ժ����,
             T.�д�������ʹ��ʱ��,
             T.�Ƿ��г�Ժ31����סԺ�ƻ�,
             T.Ŀ��,
             T.��Ժ��ʽ,
             T.ҽ��תԺ,
             T.ҽ��ת�����������     
        FROM סԺ����_������ҳ T, סԺ����_��Ժ������Ϣ TT
       WHERE T.סԺ������ = TT.סԺ������
         AND T.�������� = STR_��������
         AND EXISTS (SELECT 1
                FROM ��������_�����б� A
               WHERE A.�������� = T.��������
                 AND A.������ = T.סԺ������
                 AND A.������� = '��Ժ'
                 AND A.��Ŀ���� = STR_��Ŀ����
                 AND A.��ˮ�� = STR_��ˮ��);
  END;

  --������ϼ�¼��Ϣ
  FOR ROW_��ϼ�¼ IN CUR_��ϼ�¼ LOOP
    EXIT WHEN CUR_��ϼ�¼%NOTFOUND;
    IF ROW_��ϼ�¼.������� = '��Ժ���' THEN
      STR_SQL := 'UPDATE ��ʱ��_�����ϱ�_��Ժ���в��� SET P321=:1,P322=:2,P805=:3,P323=:4 WHERE ������=:5';
      EXECUTE IMMEDIATE STR_SQL
        USING ROW_��ϼ�¼.ICD��, ROW_��ϼ�¼.��������, ROW_��ϼ�¼.��Ժ����, ROW_��ϼ�¼.��Ժ���, ROW_��ϼ�¼.סԺ������;
    ELSIF ROW_��ϼ�¼.������� = '��Ժ���' THEN
      STR_SQL := 'UPDATE ��ʱ��_�����ϱ�_��Ժ���в��� SET P30=:1,P301=:2,P29=:3,P31=:4 WHERE ������=:5';
      EXECUTE IMMEDIATE STR_SQL
        USING ROW_��ϼ�¼.ICD��, ROW_��ϼ�¼.��������, ROW_��ϼ�¼.��Ժʱ���, ROW_��ϼ�¼.��Ժ��ȷ������, ROW_��ϼ�¼.סԺ������;
    ELSIF ROW_��ϼ�¼.������� = '�������' THEN
      STR_SQL := 'UPDATE ��ʱ��_�����ϱ�_��Ժ���в��� SET QTZDBM' || ROW_��ϼ�¼.RN ||
                 '=:1,QTZDMS' || ROW_��ϼ�¼.RN || '=:2,QTZDRYBQ' ||
                 ROW_��ϼ�¼.RN || '=:3,QTZDCYQK' || ROW_��ϼ�¼.RN ||
                 '=:4 WHERE ������=:5';
      EXECUTE IMMEDIATE STR_SQL
        USING ROW_��ϼ�¼.ICD��, ROW_��ϼ�¼.��������, ROW_��ϼ�¼.��Ժ����, ROW_��ϼ�¼.��Ժ���, ROW_��ϼ�¼.סԺ������;
    ELSIF ROW_��ϼ�¼.������� = '���˺��ж��ⲿԭ��' THEN
      STR_SQL := 'UPDATE ��ʱ��_�����ϱ�_��Ժ���в��� SET WBYSBM' || ROW_��ϼ�¼.RN ||
                 '=:1,WBYSMC' || ROW_��ϼ�¼.RN ||
                 '=:2 WHERE ������=:3';
      EXECUTE IMMEDIATE STR_SQL
        USING ROW_��ϼ�¼.ICD��, ROW_��ϼ�¼.��������, ROW_��ϼ�¼.סԺ������;
    ELSIF ROW_��ϼ�¼.������� = '�������' THEN
      STR_SQL := 'UPDATE ��ʱ��_�����ϱ�_��Ժ���в��� SET BLZDBM' || ROW_��ϼ�¼.RN ||
                 '=:1,BLZDMC' || ROW_��ϼ�¼.RN ||
                 '=:2,BLH' || ROW_��ϼ�¼.RN ||
                 '=:3 WHERE ������=:4';
      EXECUTE IMMEDIATE STR_SQL
        USING ROW_��ϼ�¼.ICD��, ROW_��ϼ�¼.��������, ROW_��ϼ�¼.�����, ROW_��ϼ�¼.סԺ������;
    END IF;
  END LOOP;

  --���·��ü�¼��Ϣ
  FOR ROW_���ü�¼ IN CUR_���ü�¼ LOOP
    EXIT WHEN CUR_���ü�¼%NOTFOUND;
    UPDATE ��ʱ��_�����ϱ�_��Ժ���в��� T
       SET T.P782 = ROW_���ü�¼.�ܽ��,
           T.P751 = ROW_���ü�¼.�Ը����,
           T.P752 = ROW_���ü�¼.һ��ҽ�Ʒ������,
           T.P754 = ROW_���ü�¼.һ�����Ʋ�����,
           T.P755 = ROW_���ü�¼.�����,
           T.P756 = ROW_���ü�¼.�ۺ�ҽ�Ʒ�������������,
           T.P757 = ROW_���ü�¼.������Ϸ�,
           T.P758 = ROW_���ü�¼.ʵ������Ϸ�,
           T.P759 = ROW_���ü�¼.Ӱ��ѧ��Ϸ�,
           T.P760 = ROW_���ü�¼.�ٴ������Ŀ��,
           T.P761 = ROW_���ü�¼.������������Ŀ��,
           T.P762 = ROW_���ü�¼.����_�ٴ��������Ʒ�,
           T.P763 = ROW_���ü�¼.�������Ʒ�,
           T.P764 = ROW_���ü�¼.����_�����,
           T.P765 = ROW_���ü�¼.����_������,
           T.P767 = ROW_���ü�¼.������,
           T.P768 = ROW_���ü�¼.��ҽ���Ʒ�,
           T.P769 = ROW_���ü�¼.��ҩ��,
           T.P770 = ROW_���ü�¼.����_����ҩ���,
           T.P771 = ROW_���ü�¼.�г�ҩ��,
           T.P772 = ROW_���ü�¼.�в�ҩ��,
           T.P773 = ROW_���ü�¼.Ѫ��,
           T.P774 = ROW_���ü�¼.�׵�������Ʒ��,
           T.P775 = ROW_���ü�¼.�򵰰�����Ʒ��,
           T.P776 = ROW_���ü�¼.��Ѫ��������Ʒ��,
           T.P777 = ROW_���ü�¼.ϸ����������Ʒ��,
           T.P778 = ROW_���ü�¼.�����һ����ҽ�ò��Ϸ�,
           T.P779 = ROW_���ü�¼.������һ����ҽ�ò��Ϸ�,
           T.P780 = ROW_���ü�¼.������һ����ҽ�ò��Ϸ�,
           T.P781 = ROW_���ü�¼.������
    
     WHERE T.������ = ROW_���ü�¼.סԺ������;
  END LOOP;

  --����������¼��Ϣ
  FOR ROW_������¼ IN CUR_������¼ LOOP
    EXIT WHEN CUR_������¼%NOTFOUND;
  
    STR_SQL := 'UPDATE ��ʱ��_�����ϱ�_��Ժ���в��� SET SSBM' || ROW_������¼.RN ||
               '=:1, SSRQ' || ROW_������¼.RN || '=:2, SSJB' || ROW_������¼.RN ||
               '= :3
               , SSMC' || ROW_������¼.RN || '=:4,  SZ' ||
               ROW_������¼.RN || '=:5, YZ' || ROW_������¼.RN || '=:6, EZ' ||
               ROW_������¼.RN || '=:7, MZFS' || ROW_������¼.RN || '=:8, MZFJ' ||
               ROW_������¼.RN || '=:9, QKYHDJ' || ROW_������¼.RN ||
               '=:10 WHERE ������=:11';
    EXECUTE IMMEDIATE STR_SQL
      USING ROW_������¼.������, ROW_������¼.������������, ROW_������¼.�����������, ROW_������¼.������������, ROW_������¼.����, ROW_������¼.I��, ROW_������¼.II��, ROW_������¼.����ʽ����, ROW_������¼.����ּ�����, ROW_������¼.�п����ϵȼ�����, ROW_������¼.סԺ������;
  
  END LOOP;

  --������֢�໤��¼��Ϣ
  FOR ROW_��֢�໤��¼ IN CUR_��֢�໤��¼ LOOP
    EXIT WHEN CUR_��֢�໤��¼%NOTFOUND;
    STR_SQL := 'UPDATE ��ʱ��_�����ϱ�_��Ժ���в��� SET JHSMC' || ROW_��֢�໤��¼.RN ||
               '=:1, JRSJ' || ROW_��֢�໤��¼.RN || '=:2, TCSJ' || ROW_��֢�໤��¼.RN ||
               '= :3 WHERE ������=:4';
  
    EXECUTE IMMEDIATE STR_SQL
      USING ROW_��֢�໤��¼.��������, ROW_��֢�໤��¼.����ʱ��, ROW_��֢�໤��¼.�˳�ʱ��, ROW_��֢�໤��¼.סԺ������;
  
  END LOOP;

  --�������ݼ�
  OPEN CUR_����_�б���Ϣ FOR
    SELECT T.* FROM ��ʱ��_�����ϱ�_��Ժ���в��� T;

END PR_�����ϱ�_���г�Ժ����;
/

prompt
prompt Creating procedure PR_�����ϱ�_���г�ԺС��
prompt =================================
prompt
CREATE OR REPLACE PROCEDURE CLOUDHIS.PR_�����ϱ�_���г�ԺС��(STR_����          IN VARCHAR2,
                                           CUR_����_�б���Ϣ OUT SYS_REFCURSOR) IS

  STR_�������� VARCHAR2(50) := FU_ͨ��_��ȡ�ַ���ֵ(STR_����, '|', 1);
  /*DAT_��Ժʱ����ʼ DATE := TO_DATE(FU_ͨ��_��ȡ�ַ���ֵ(STR_����, '|', 2),
                             'yyyy-MM-dd hh24:mi:ss');
  DAT_��Ժʱ���ֹ DATE := TO_DATE(FU_ͨ��_��ȡ�ַ���ֵ(STR_����, '|', 3),
                             'yyyy-MM-dd hh24:mi:ss');
  STR_��������     VARCHAR2(50) := FU_ͨ��_��ȡ�ַ���ֵ(STR_����, '|', 4);*/
  STR_��Ŀ���� VARCHAR2(50) := FU_ͨ��_��ȡ�ַ���ֵ(STR_����, '|', 5);
  STR_��ˮ��   VARCHAR2(50) := FU_ͨ��_��ȡ�ַ���ֵ(STR_����, '|', 6);

BEGIN

  BEGIN
   IF STR_��ˮ�� IS NULL THEN
      SELECT MAX(��ˮ��)
        INTO STR_��ˮ��
        FROM ��������_�����б�
       WHERE �������� = STR_��������
         AND ��Ŀ���� = STR_��Ŀ����;
    END IF;
    
    --�������ݼ�
    OPEN CUR_����_�б���Ϣ FOR
    
      SELECT T.סԺ������ AS ������,
             T.������,
             T.�������� AS ����,
             NVL(T.�Ա�, '0') AS �Ա�,
             TO_NUMBER(REGEXP_REPLACE(T.����, '[^-0-9.]', '')) ����,
             T.��Ժ����,
             (SELECT A.�������
                FROM ������Ŀ_���Ҳ������� A
               WHERE A.�������� = STR_��������
                 AND A.���ұ��� = T.��Ժ���ұ���) ��Ժ�Ʊ�,             
             (SELECT ת�����ұ���
                FROM סԺ����_��Ժ����ת�Ƽ�¼
               WHERE �������� = STR_��������
                 AND סԺ������ = T.סԺ������
                 AND ROWNUM = 1) ת�ƿƱ�,
             T.��Ժ����,
             (SELECT A.�������
                FROM ������Ŀ_���Ҳ������� A
               WHERE A.�������� = STR_��������
                 AND A.���ұ��� = T.��Ժ���ұ���) ��Ժ�Ʊ�,
             
             T.סԺ���� AS ʵ��סԺ����,
             (SELECT A.��������
                FROM סԺ����_��Ժ������� A
               WHERE A.�������� = STR_��������
                 AND A.סԺ������ = T.סԺ������
                 AND A.������� = '�������'
                 AND A.��Ϸ��� = '1'
                 AND ROWNUM = 1) AS ��Ժ���,
             (SELECT WM_CONCAT(A.��������)
                FROM סԺ����_��Ժ������� A
               WHERE A.�������� = STR_��������
                 AND A.סԺ������ = T.סԺ������
                 AND A.������� = '��Ժ���'
                 AND A.��Ϸ��� = '1') AS ��Ժ���,
             NULL AS ��Ժ��������ƾ���,
             NULL AS ��Ժ��������ƽ��,
             null AS ��Ժҽ��
             /*(SELECT A.ҽ������
                FROM סԺ����_��Ժ����ҽ�� A
               WHERE A.�������� = STR_��������
                 AND A.סԺ������ = T.סԺ������
                 AND A.��Ŀ���� = '��Ժҽ��') AS ��Ժҽ��*/
        FROM סԺ����_������ҳ T, סԺ����_��Ժ������Ϣ TT
       WHERE T.סԺ������ = TT.סԺ������
         AND T.�������� = STR_��������
         AND EXISTS (SELECT 1
                FROM ��������_�����б� A
               WHERE A.�������� = T.��������
                 AND A.������ = T.סԺ������
                 AND A.������� = '��Ժ'
                 AND A.��Ŀ���� = STR_��Ŀ����
                 AND A.��ˮ�� = STR_��ˮ��);
  END;

END PR_�����ϱ�_���г�ԺС��;
/

prompt
prompt Creating procedure PR_�����ϱ�_���м����¼
prompt =================================
prompt
CREATE OR REPLACE PROCEDURE CLOUDHIS.PR_�����ϱ�_���м����¼(STR_����          IN VARCHAR2,
                                           CUR_����_�б���Ϣ OUT SYS_REFCURSOR) IS

  STR_�������� VARCHAR2(50) := FU_ͨ��_��ȡ�ַ���ֵ(STR_����, '|', 1);
  /*DAT_��Ժʱ����ʼ DATE := TO_DATE(FU_ͨ��_��ȡ�ַ���ֵ(STR_����, '|', 2),
                             'yyyy-MM-dd hh24:mi:ss');
  DAT_��Ժʱ���ֹ DATE := TO_DATE(FU_ͨ��_��ȡ�ַ���ֵ(STR_����, '|', 3),
                             'yyyy-MM-dd hh24:mi:ss');
  STR_��������     VARCHAR2(50) := FU_ͨ��_��ȡ�ַ���ֵ(STR_����, '|', 4);*/
  STR_��Ŀ���� VARCHAR2(50) := FU_ͨ��_��ȡ�ַ���ֵ(STR_����, '|', 5);
  STR_��ˮ��   VARCHAR2(50) := FU_ͨ��_��ȡ�ַ���ֵ(STR_����, '|', 6);

BEGIN

  BEGIN
  
    IF STR_��ˮ�� IS NULL THEN
      SELECT MAX(��ˮ��)
        INTO STR_��ˮ��
        FROM ��������_�����б�
       WHERE �������� = STR_��������
         AND ��Ŀ���� = STR_��Ŀ����;
    END IF;
    --�������ݼ�
    OPEN CUR_����_�б���Ϣ FOR
      WITH W_���м����� AS
       (SELECT TT.�ӿڶ�����Ϣ����, TTT.ϵͳ������Ϣ����
          FROM ��������_��Ŀ�ӿڶ��շ��� T,
               ��������_��Ŀ�ӿڶ�����ϸ TT,
               ��������_��Ŀϵͳ������ϸ TTT
         WHERE T.��ˮ�� = TT.���ID
           AND TT.��ˮ�� = TTT.���ID
           AND T.�������� = STR_��������
           AND T.��Ŀ���� = STR_��Ŀ����
           and T.�������='RC040')
      
      SELECT '01' AS ��������,
             G.���ﲡ���� AS ������,
             G.�Һ�ʱ�� AS ��������,
             J.������ AS �걾��,
             (SELECT B.�ӿڶ�����Ϣ����
                FROM W_���м����� B
               WHERE B.ϵͳ������Ϣ���� = S.��Ŀ����
                 AND ROWNUM = 1) AS ���м�����,
             S.����ʱ�� AS �ͼ�ʱ��,
             NULL AS ����������,
             DECODE(M.ϸ��ֵ, '����', '1', '2') AS �������Ƿ�����,
             NULL AS �������������
        FROM �������_�ҺŵǼ�  G,
             ������_����      S,
             ������_���      J,
             ������_���_��ϸ M
       WHERE G.�������� = S.��������
         AND S.�������� = J.��������
         AND G.���ﲡ���� = S.������
         AND S.���뵥ID = J.���뵥ID
         AND J.ΨһID = M.ϸ������
         AND G.�������� = STR_��������
         AND S.���� = '����'
         AND S.ID���� = '����'
         AND EXISTS (SELECT 1
                FROM ��������_�����б� A
               WHERE A.�������� = G.��������
                 AND A.������ = G.���ﲡ����
                 AND A.������� = '����'
                 AND A.��Ŀ���� = STR_��Ŀ����
                 AND A.��ˮ�� = STR_��ˮ��)
         AND EXISTS (SELECT 1
                FROM W_���м����� B
               WHERE B.ϵͳ������Ϣ���� = S.��Ŀ����)
      
      UNION
      
      SELECT '03' AS ��������,
             G.סԺ������ AS ������,
             G.��Ժʱ�� AS ��������,
             J.������ AS �걾��,
             (SELECT B.�ӿڶ�����Ϣ����
                FROM W_���м����� B
               WHERE B.ϵͳ������Ϣ���� = S.��Ŀ����
                 AND ROWNUM = 1) AS ���м�����,
             S.����ʱ�� AS �ͼ�ʱ��,
             NULL AS ����������,
             DECODE(M.ϸ��ֵ, '����', '1', '2') AS �������Ƿ�����,
             NULL AS �������������
        FROM סԺ����_��Ժ������Ϣ G,
             ������_����         S,
             ������_���         J,
             ������_���_��ϸ    M
       WHERE G.�������� = S.�������� AND S.�������� = J.�������� AND G.סԺ������ = S.������ AND
       S.���뵥ID = J.���뵥ID AND J.ΨһID = M.ϸ������ AND G.�������� = STR_�������� AND
       S.���� = '����' AND S.ID���� = 'סԺ' AND EXISTS
       (SELECT 1
          FROM ��������_�����б� A
         WHERE A.�������� = G.��������
           AND A.������ = G.סԺ������
           AND A.������� = '��Ժ'
           AND A.��Ŀ���� = STR_��Ŀ����
           AND A.��ˮ�� = STR_��ˮ��) AND EXISTS
       (SELECT 1
          FROM W_���м����� B
         WHERE B.ϵͳ������Ϣ���� = S.��Ŀ����)
      
      UNION
      
      SELECT '03' AS ��������,
             G.סԺ������ AS ������,
             G.��Ժʱ�� AS ��������,
             J.������ AS �걾��,
             (SELECT B.�ӿڶ�����Ϣ����
                FROM W_���м����� B
               WHERE B.ϵͳ������Ϣ���� = S.��Ŀ����
                 AND ROWNUM = 1) AS ���м�����,
             S.����ʱ�� AS �ͼ�ʱ��,
             NULL AS ����������,
             DECODE(M.ϸ��ֵ, '����', '1', '2') AS �������Ƿ�����,
             NULL AS �������������
        FROM סԺ����_��Ժ������Ϣ G,
             ������_����         S,
             ������_���         J,
             ������_���_��ϸ    M
       WHERE G.�������� = S.�������� AND S.�������� = J.�������� AND G.סԺ������ = S.������ AND
       S.���뵥ID = J.���뵥ID AND J.ΨһID = M.ϸ������ AND G.�������� = STR_�������� AND
       S.���� = '����' AND S.ID���� = 'סԺ' AND EXISTS
       (SELECT 1
          FROM ��������_�����б� A
         WHERE A.�������� = G.��������
           AND A.������ = G.סԺ������
           AND A.������� = '��Ժ'
           AND A.��Ŀ���� = STR_��Ŀ����
           AND A.��ˮ�� = STR_��ˮ��) AND EXISTS
       (SELECT 1
          FROM W_���м����� B
         WHERE B.ϵͳ������Ϣ���� = S.��Ŀ����);
  
  END;

END PR_�����ϱ�_���м����¼;
/

prompt
prompt Creating procedure PR_�����ϱ�_�������Ｐ��Ժ����
prompt ====================================
prompt
CREATE OR REPLACE PROCEDURE CLOUDHIS.PR_�����ϱ�_�������Ｐ��Ժ����(STR_����          IN VARCHAR2,
                                              CUR_����_�б���Ϣ OUT SYS_REFCURSOR) IS
  STR_SQL VARCHAR2(1000);

  STR_�������� VARCHAR2(50) := FU_ͨ��_��ȡ�ַ���ֵ(STR_����, '|', 1);
  /*DAT_��Ժʱ����ʼ DATE := TO_DATE(FU_ͨ��_��ȡ�ַ���ֵ(STR_����, '|', 2),
                             'yyyy-MM-dd hh24:mi:ss');
  DAT_��Ժʱ���ֹ DATE := TO_DATE(FU_ͨ��_��ȡ�ַ���ֵ(STR_����, '|', 3),
                             'yyyy-MM-dd hh24:mi:ss');
  STR_��������     VARCHAR2(50) := FU_ͨ��_��ȡ�ַ���ֵ(STR_����, '|', 4);*/
  STR_��Ŀ���� VARCHAR2(50) := FU_ͨ��_��ȡ�ַ���ֵ(STR_����, '|', 5);
  STR_��ˮ��   VARCHAR2(50) := FU_ͨ��_��ȡ�ַ���ֵ(STR_����, '|', 6);

  --��ȡ��ϼ�¼CURSOR
  CURSOR CUR_��ϼ�¼ IS
    SELECT *
      FROM (SELECT ROW_NUMBER() OVER(PARTITION BY T.סԺ������, T.������� ORDER BY T.�������, T.�Ƿ������ DESC) RN,
                   T.סԺ������,
                   (CASE
                     WHEN T.ICD�� = T.�������� THEN
                      (SELECT B.ICD��
                         FROM ������Ŀ_�����ֵ� B
                        WHERE B.�������� = T.��������)
                     ELSE
                      T.ICD��
                   END) ICD��,
                   T.��������,
                   T.�������
              FROM סԺ����_��Ժ������� T
             WHERE T.�������� = STR_��������
               AND T.������� = '�������'
               AND EXISTS (SELECT 1
                      FROM ��������_�����б� A
                     WHERE A.�������� = T.��������
                       AND A.������ = T.סԺ������
                       AND A.������� = '��Ժ'
                       AND A.��Ŀ���� = STR_��Ŀ����
                       AND A.��ˮ�� = STR_��ˮ��)) G
     WHERE G.RN <= 10;
  ROW_��ϼ�¼ CUR_��ϼ�¼%ROWTYPE;

  --��ȡ��֢�໤��¼CURSOR
  CURSOR CUR_��֢�໤��¼ IS
    SELECT *
      FROM (SELECT ROW_NUMBER() OVER(PARTITION BY T.סԺ������ ORDER BY T.��ˮ�� DESC) RN,
                   T.��������,
                   T.����ʱ��,
                   T.�˳�ʱ��,
                   T.סԺ������
              FROM סԺ����_������ҳ_��֢�໤ T
             WHERE T.�������� = STR_��������
               AND EXISTS (SELECT 1
                      FROM ��������_�����б� A
                     WHERE A.�������� = T.��������
                       AND A.������ = T.סԺ������
                       AND A.������� = '��Ժ'
                       AND A.��Ŀ���� = STR_��Ŀ����
                       AND A.��ˮ�� = STR_��ˮ��)) G
     WHERE G.RN <= 5;

  ROW_��֢�໤��¼ CUR_��֢�໤��¼%ROWTYPE;

BEGIN

  BEGIN
    IF STR_��ˮ�� IS NULL THEN
      SELECT MAX(��ˮ��)
        INTO STR_��ˮ��
        FROM ��������_�����б�
       WHERE �������� = STR_��������
         AND ��Ŀ���� = STR_��Ŀ����;
    END IF;
  
    DELETE FROM ��ʱ��_�����ϱ�_��Ժ���в���;
  
    --������Ժ������Ϣ
    INSERT INTO ��ʱ��_�����ϱ�_��Ժ���в���
      (������,
       P900, --ҽ�ƻ�������
       P6891, --��������
       P686, --ҽ�Ʊ����ֲᣨ������
       P800, --��������
       P7501, --��������
       P7502, --���￨��
       
       P4, --����
       P5, --�Ա�
       P6, --��������
       P7, --����
       P7503, --ע��֤�����ʹ���
       P13, --ע��֤������
       P7504, --������Ҵ���
       P7505, --�������
       P7506, --��������
       P7507, --����
       P321, --��Ҫ��ϴ���
       P322, --��Ҫ�������
       P1,
       P8508 --�Ƿ�����
       
       )
      SELECT T.סԺ������,
             T.��������,
             'Ӫ�ھ��ü����������ڶ�����ҽԺ' ��������,
             NULL, --ҽ�Ʊ����ֲᣨ������
             T.��������,
             '03', --��������
             T.סԺ������, --���￨��
             NVL(T.��������, '_'),
             NVL(T.�Ա�, '0'),
             T.��������,
             TO_NUMBER(REGEXP_REPLACE(T.����, '[^-0-9.]', '')) ����,
             DECODE(T.֤�����, '1', '01', '99'), --ע��֤�����ʹ���
             NVL(T.���֤��, '-'), --ע��֤������
             (SELECT A.�������
                FROM ������Ŀ_���Ҳ������� A
               WHERE A.�������� = T.��������
                 AND A.���ұ��� = T.��Ժ���ұ���) ������Ҵ���, --������Ҵ���
             1, --�������
             T.��Ժ����, --��������
             NULL, --����
             (SELECT (CASE
                       WHEN A.ICD�� = A.�������� THEN
                        (SELECT B.ICD��
                           FROM ������Ŀ_�����ֵ� B
                          WHERE B.�������� = A.��������)
                       ELSE
                        A.ICD��
                     END) ICD��
                FROM סԺ����_��Ժ������� A
               WHERE A.�������� = STR_��������
                 AND A.סԺ������ = T.סԺ������
                 AND A.������� = '�������'
                 AND A.��Ϸ��� = '1'
                 AND ROWNUM = 1) AS ��������,
             (SELECT A.��������
                FROM סԺ����_��Ժ������� A
               WHERE A.�������� = STR_��������
                 AND A.סԺ������ = T.סԺ������
                 AND A.������� = '�������'
                 AND A.��Ϸ��� = '1'
                 AND ROWNUM = 1) AS �����������,
             T.ҽ�Ƹ��ѷ�ʽ,
             '2'
        FROM סԺ����_������ҳ T
       WHERE T.�������� = STR_��������
         AND EXISTS (SELECT 1
                FROM ��������_�����б� A
               WHERE A.�������� = T.��������
                 AND A.������ = T.סԺ������
                 AND A.������� = '��Ժ'
                 AND A.��Ŀ���� = STR_��Ŀ����
                 AND A.��ˮ�� = STR_��ˮ��);
  END;

  --������ϼ�¼��Ϣ
  FOR ROW_��ϼ�¼ IN CUR_��ϼ�¼ LOOP
    EXIT WHEN CUR_��ϼ�¼%NOTFOUND;
    IF ROW_��ϼ�¼.������� = '�������' THEN
      STR_SQL := 'UPDATE ��ʱ��_�����ϱ�_��Ժ���в��� SET QTZDBM' || ROW_��ϼ�¼.RN ||
                 '=:1,QTZDMS' || ROW_��ϼ�¼.RN || '=:2 WHERE ������=:3';
      EXECUTE IMMEDIATE STR_SQL
        USING ROW_��ϼ�¼.ICD��, ROW_��ϼ�¼.��������, ROW_��ϼ�¼.סԺ������;
    END IF;
  END LOOP;

  --������֢�໤��¼��Ϣ
  FOR ROW_��֢�໤��¼ IN CUR_��֢�໤��¼ LOOP
    EXIT WHEN CUR_��֢�໤��¼%NOTFOUND;
    STR_SQL := 'UPDATE ��ʱ��_�����ϱ�_��Ժ���в��� SET JHSMC' || ROW_��֢�໤��¼.RN ||
               '=:1, JRSJ' || ROW_��֢�໤��¼.RN || '=:2, TCSJ' || ROW_��֢�໤��¼.RN ||
               '= :3 WHERE ������=:4';
  
    EXECUTE IMMEDIATE STR_SQL
      USING ROW_��֢�໤��¼.��������, ROW_��֢�໤��¼.����ʱ��, ROW_��֢�໤��¼.�˳�ʱ��, ROW_��֢�໤��¼.סԺ������;
  
  END LOOP;

  DELETE FROM ��ʱ��_�����ϱ�_��Ժ���з���;

  --������Ժ������Ϣ
  INSERT INTO ��ʱ��_�����ϱ�_��Ժ���з���
    (������, �����ܶ�, ��������, �����ܶ�, �Ը��ܶ�)
    SELECT B.���ﲡ����,
           C.�����ܶ�,
           (case
             when d.������� = 2 and d.С����� in ('1', '2', '3', '12') then
              'ҩƷ��'
             when d.������� = '1' and d.С����� in ('1', '2', '7') then
              '����'
             else
              '������'
           end) as ��������,
           D.�ܽ��,
           (C.�����ܶ� - C.�����ܶ�) AS �ο��Ը����
      FROM �������_�ҺŵǼ�     B,
           �������_���﷢Ʊ�Ǽ� C,
           �������_���ﴦ��     D
     WHERE B.�������� = C.��������
       AND C.�������� = D.��������
       AND B.���ﲡ���� = C.���ﲡ����
       AND C.���ﲡ���� = D.���ﲡ����
       AND C.��Ʊ��� = D.��Ʊ���
       AND C.�շ�ʱ�� >= B.�Һ�ʱ��
       AND D.�շ�ʱ�� >= B.�Һ�ʱ��
       AND EXISTS (SELECT 1
              FROM ��������_�����б� A
             WHERE A.�������� = B.��������
               AND A.������ = B.���ﲡ����
               AND A.������� = '����'
               AND A.��Ŀ���� = STR_��Ŀ����
               AND A.��ˮ�� = STR_��ˮ��);

  --������»�����Ϣ
  INSERT INTO ��ʱ��_�����ϱ�_��Ժ���в���
    (������,
     P900, --ҽ�ƻ�������
     P6891, --��������
     P686, --ҽ�Ʊ����ֲᣨ������
     P800, --��������
     P7501, --��������
     P7502, --���￨��
     
     P4, --����
     P5, --�Ա�
     P6, --��������
     P7, --����
     P7503, --ע��֤�����ʹ���
     P13, --ע��֤������
     P7504, --������Ҵ���
     P7505, --�������
     P7506, --��������
     P7507, --����
     P321, --��Ҫ��ϴ���
     P322, --��Ҫ�������
     P1, --ҽ�Ƹ���֧����ʽ
     
     P7508, --�ܷ���
     P7509, --�Һŷ�
     P7510, --ҩƷ��
     P7511, --����
     P7512, --�Ը�����
     P8508 --�Ƿ�����
     
     )
    SELECT G.���ﲡ����,
           G.��������,
           'Ӫ�ھ��ü����������ڶ�����ҽԺ' ��������,
           NULL,
           X.��������,
           '01', --��������
           G.���ﲡ����, --���￨��
           NVL(X.����, '_'),
           NVL(X.�Ա�, '0'),
           X.��������,
           TO_NUMBER(REGEXP_REPLACE(X.����, '[^-0-9.]', '')) ����,
           '01', --ע��֤�����ʹ���
           NVL(X.���֤��, '_'), --ע��֤������
           (SELECT A.�������
              FROM ������Ŀ_���Ҳ������� A
             WHERE A.�������� = G.��������
               AND A.���ұ��� = G.�Һſ��ұ���) ������Ҵ���, --������Ҵ���
           1, --�������
           G.�Һ�ʱ��, --��������
           NULL, --����
           (SELECT A.ICD��
              FROM ������Ŀ_�����ֵ� A
             WHERE A.�������� = G.��������
               AND ROWNUM = 1) ��������,
           G.��������,
           '9', --ҽ�Ƹ���֧����ʽ
           (SELECT SUM(B.�����ܶ�)
              FROM ��ʱ��_�����ϱ�_��Ժ���з��� B
             WHERE B.������ = G.���ﲡ����), --�ܷ���
           '0', --�Һŷ�
           (SELECT SUM(B.�����ܶ�)
              FROM ��ʱ��_�����ϱ�_��Ժ���з��� B
             WHERE B.������ = G.���ﲡ����
               AND B.�������� = 'ҩƷ��'), --ҩƷ��
           (SELECT SUM(B.�����ܶ�)
              FROM ��ʱ��_�����ϱ�_��Ժ���з��� B
             WHERE B.������ = G.���ﲡ����
               AND B.�������� = '����'), --����
           (SELECT SUM(B.�Ը��ܶ�)
              FROM ��ʱ��_�����ϱ�_��Ժ���з��� B
             WHERE B.������ = G.���ﲡ����), --�Ը��ܶ�
           '2' --�Ƿ�����
    
      FROM �������_�ҺŵǼ� G, ������Ŀ_������Ϣ X
     WHERE G.�������� = X.��������
       AND G.����ID = X.����ID
       AND G.�������� = STR_��������
       AND G.�˺ű�־ = '��'
       AND EXISTS (SELECT 1
              FROM ��������_�����б� A
             WHERE A.�������� = G.��������
               AND A.������ = G.���ﲡ����
               AND A.������� = '����'
               AND A.��Ŀ���� = STR_��Ŀ����
               AND A.��ˮ�� = STR_��ˮ��);

  --�������ݼ�
  OPEN CUR_����_�б���Ϣ FOR
    SELECT T.* FROM ��ʱ��_�����ϱ�_��Ժ���в��� T;

END PR_�����ϱ�_�������Ｐ��Ժ����;
/

prompt
prompt Creating procedure PR_�����ϱ�_����������¼
prompt =================================
prompt
CREATE OR REPLACE PROCEDURE CLOUDHIS.PR_�����ϱ�_����������¼(STR_����          IN VARCHAR2,
                                           CUR_����_�б���Ϣ OUT SYS_REFCURSOR) IS

  STR_�������� VARCHAR2(50) := FU_ͨ��_��ȡ�ַ���ֵ(STR_����, '|', 1);
  /*DAT_��Ժʱ����ʼ DATE := TO_DATE(FU_ͨ��_��ȡ�ַ���ֵ(STR_����, '|', 2),
                             'yyyy-MM-dd hh24:mi:ss');
  DAT_��Ժʱ���ֹ DATE := TO_DATE(FU_ͨ��_��ȡ�ַ���ֵ(STR_����, '|', 3),
                             'yyyy-MM-dd hh24:mi:ss');
  STR_��������     VARCHAR2(50) := FU_ͨ��_��ȡ�ַ���ֵ(STR_����, '|', 4);*/
  STR_��Ŀ���� VARCHAR2(50) := FU_ͨ��_��ȡ�ַ���ֵ(STR_����, '|', 5);
  STR_��ˮ��   VARCHAR2(50) := FU_ͨ��_��ȡ�ַ���ֵ(STR_����, '|', 6);

BEGIN

  BEGIN
    IF STR_��ˮ�� IS NULL THEN
      SELECT MAX(��ˮ��)
        INTO STR_��ˮ��
        FROM ��������_�����б�
       WHERE �������� = STR_��������
         AND ��Ŀ���� = STR_��Ŀ����;
    END IF;
  
    --�������ݼ�
    OPEN CUR_����_�б���Ϣ FOR
    
      SELECT T.סԺ������ AS ������,
             T.������,
             T.�������� AS ����,
             NVL(T.�Ա�, '0') AS �Ա�,
             TO_NUMBER(REGEXP_REPLACE(T.����, '[^-0-9.]', '')) ����,
             
             T.��Ժ����,
             (SELECT A.�������
                FROM ������Ŀ_���Ҳ������� A
               WHERE A.�������� = STR_��������
                 AND A.���ұ��� = T.��Ժ���ұ���) ��Ժ�Ʊ�,
             
             (SELECT ת�����ұ���
                FROM סԺ����_��Ժ����ת�Ƽ�¼
               WHERE �������� = STR_��������
                 AND סԺ������ = T.סԺ������
                 AND ROWNUM = 1) ת�ƿƱ�,
             T.��Ժ����,
             (SELECT A.�������
                FROM ������Ŀ_���Ҳ������� A
               WHERE A.�������� = STR_��������
                 AND A.���ұ��� = T.��Ժ���ұ���) ��Ժ�Ʊ�,            
             T.סԺ���� AS ʵ��סԺ����,            
             (SELECT A.��������
                FROM סԺ����_��Ժ������� A
               WHERE A.�������� = STR_��������
                 AND A.סԺ������ = T.סԺ������
                 AND A.������� = '�������'
                 AND A.��Ϸ��� = '1'
                 AND ROWNUM = 1) AS ��Ժ���,
             NULL AS ��Ժ��������ƺ����Ⱦ���,
             TT.����������� AS �������,
             TT.����������� AS ����ԭ��,
             TT.����ʱ��
      
        FROM סԺ����_������ҳ T, סԺ����_����������¼ TT
       WHERE T.�������� = TT.��������
         AND T.סԺ������ = TT.סԺ������
         AND T.�������� = STR_��������
         AND EXISTS (SELECT 1
                FROM ��������_�����б� A
               WHERE A.�������� = T.��������
                 AND A.������ = T.סԺ������
                 AND A.������� = '��Ժ'
                 AND A.��Ŀ���� = STR_��Ŀ����
                 AND A.��ˮ�� = STR_��ˮ��);
  END;

END PR_�����ϱ�_����������¼;
/

prompt
prompt Creating procedure PR_�����ϱ�_������ҩ��¼
prompt =================================
prompt
CREATE OR REPLACE PROCEDURE CLOUDHIS.PR_�����ϱ�_������ҩ��¼(STR_����          IN VARCHAR2,
                                           CUR_����_�б���Ϣ OUT SYS_REFCURSOR) IS

  STR_�������� VARCHAR2(50) := FU_ͨ��_��ȡ�ַ���ֵ(STR_����, '|', 1);
  /*DAT_��Ժʱ����ʼ DATE := TO_DATE(FU_ͨ��_��ȡ�ַ���ֵ(STR_����, '|', 2),
                             'yyyy-MM-dd hh24:mi:ss');
  DAT_��Ժʱ���ֹ DATE := TO_DATE(FU_ͨ��_��ȡ�ַ���ֵ(STR_����, '|', 3),
                             'yyyy-MM-dd hh24:mi:ss');
  STR_��������     VARCHAR2(50) := FU_ͨ��_��ȡ�ַ���ֵ(STR_����, '|', 4);*/
  STR_��Ŀ���� VARCHAR2(50) := FU_ͨ��_��ȡ�ַ���ֵ(STR_����, '|', 5);
  STR_��ˮ��   VARCHAR2(50) := FU_ͨ��_��ȡ�ַ���ֵ(STR_����, '|', 6);

BEGIN

  BEGIN
    IF STR_��ˮ�� IS NULL THEN
      SELECT MAX(��ˮ��)
        INTO STR_��ˮ��
        FROM ��������_�����б�
       WHERE �������� = STR_��������
         AND ��Ŀ���� = STR_��Ŀ����;
    END IF;
  
    --�������ݼ�
    OPEN CUR_����_�б���Ϣ FOR
    
      SELECT '01' AS ��������,
             G.���ﲡ���� AS ������,
             G.�Һ�ʱ�� AS ��������,
             TO_CHAR(Y.���) AS ˳���,
             Y.��Ŀ���� AS ҩ������,
             (SELECT A.����
                FROM ������Ŀ_Ƶ���ֵ� A
               WHERE A.�������� = Y.��������
                 AND A.Ƶ�ʱ��� = Y.Ƶ�ʱ���) AS ҩ��ʹ��Ƶ��,
             (SELECT A.����
                FROM ������Ŀ_Ƶ���ֵ� A
               WHERE A.�������� = Y.��������
                 AND A.Ƶ�ʱ��� = Y.Ƶ�ʱ���) * Y.���� AS ҩ��ʹ���ܼ���,
             Y.���� AS ҩ��ʹ�ôμ���,
             Y.�������� AS ҩ��ʹ�ü�����λ,
             Y.��ʼʱ�� AS ҩ��ʹ�ÿ�ʼʱ��,
             NULL AS ҩ��ʹ�ý���ʱ��
        FROM �������_�ҺŵǼ�     G,
             �������_����ҽ��     Y,
             �������_����ҽ����Ŀ X
       WHERE G.�������� = Y.��������
         AND Y.�������� = X.��������
         AND G.���ﲡ���� = Y.���ﲡ����
         AND Y.���ﲡ���� = X.���ﲡ����
         AND Y.��ĿID = X.��ĿID
         AND G.�������� = STR_��Ŀ����
         AND Y.�շ�״̬ = '�ѷ�ҩ'
         AND Y.������� = '2'
         AND Y.С����� IN ('1', '2', '3', '12')
         AND EXISTS (SELECT 1
                FROM ��������_�����б� A
               WHERE A.�������� = G.��������
                 AND A.������ = G.���ﲡ����
                 AND A.������� = '����'
                 AND A.��Ŀ���� = STR_��Ŀ����
                 AND A.��ˮ�� = STR_��ˮ��)
      
      UNION
      
      SELECT '03' AS ��������,
             T.סԺ������ AS ������,
             T.��Ժ���� AS ��������,
             TO_CHAR(Y.���) AS ˳���,
             Y.��Ŀ���� AS ҩ������,
             (SELECT A.����
                FROM ������Ŀ_Ƶ���ֵ� A
               WHERE A.�������� = Y.��������
                 AND A.Ƶ�ʱ��� = Y.Ƶ�ʱ���) AS ҩ��ʹ��Ƶ��,
             (SELECT A.����
                FROM ������Ŀ_Ƶ���ֵ� A
               WHERE A.�������� = Y.��������
                 AND A.Ƶ�ʱ��� = Y.Ƶ�ʱ���) * Y.���� AS ҩ��ʹ���ܼ���,
             Y.���� AS ҩ��ʹ�ôμ���,
             Y.�������� AS ҩ��ʹ�ü�����λ,
             Y.��ʼʱ�� AS ҩ��ʹ�ÿ�ʼʱ��,
             NULL AS ҩ��ʹ�ý���ʱ��
        FROM סԺ����_������ҳ         T,
             סԺ����_��Ժ����ҽ��     Y,
             סԺ����_��Ժ����ҽ����Ŀ X
       WHERE T.�������� = Y.��������
         AND Y.�������� = X.��������
         AND T.סԺ������ = Y.סԺ������
         AND Y.��ĿID = X.��ĿID
         AND T.�������� = STR_��������
         AND Y.����״̬ = '�������շ�'
         AND Y.������� = '2'
         AND Y.С����� IN ('1', '2', '3', '12')
         AND EXISTS (SELECT 1
                FROM ��������_�����б� A
               WHERE A.�������� = T.��������
                 AND A.������ = T.סԺ������
                 AND A.������� = '��Ժ'
                 AND A.��Ŀ���� = STR_��Ŀ����
                 AND A.��ˮ�� = STR_��ˮ��)
      
      UNION
      
      SELECT '03' AS ��������,
             T.סԺ������ AS ������,
             T.��Ժ���� AS ��������,
             TO_CHAR(Y.���) AS ˳���,
             Y.��Ŀ���� AS ҩ������,
             (SELECT A.����
                FROM ������Ŀ_Ƶ���ֵ� A
               WHERE A.�������� = Y.��������
                 AND A.Ƶ�ʱ��� = Y.Ƶ�ʱ���) AS ҩ��ʹ��Ƶ��,
             (SELECT A.����
                FROM ������Ŀ_Ƶ���ֵ� A
               WHERE A.�������� = Y.��������
                 AND A.Ƶ�ʱ��� = Y.Ƶ�ʱ���) * Y.���� AS ҩ��ʹ���ܼ���,
             Y.���� AS ҩ��ʹ�ôμ���,
             Y.�������� AS ҩ��ʹ�ü�����λ,
             Y.��ʼʱ�� AS ҩ��ʹ�ÿ�ʼʱ��,
             NULL AS ҩ��ʹ�ý���ʱ��
        FROM סԺ����_������ҳ         T,
             סԺ����_��Ժ����ҽ��     Y,
             סԺ����_��Ժ����ҽ����Ŀ X
       WHERE T.�������� = Y.��������
         AND Y.�������� = X.��������
         AND T.סԺ������ = Y.סԺ������
         AND Y.��ĿID = X.��ĿID
         AND T.�������� = STR_��������
         AND Y.����״̬ = '�������շ�'
         AND Y.������� = '2'
         AND Y.С����� IN ('1', '2', '3', '12')
         AND EXISTS (SELECT 1
                FROM ��������_�����б� A
               WHERE A.�������� = T.��������
                 AND A.������ = T.סԺ������
                 AND A.������� = '��Ժ'
                 AND A.��Ŀ���� = STR_��Ŀ����
                 AND A.��ˮ�� = STR_��ˮ��);
  
  END;

END PR_�����ϱ�_������ҩ��¼;
/


prompt Done
spool off
set define on
