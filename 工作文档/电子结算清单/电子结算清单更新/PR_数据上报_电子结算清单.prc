CREATE OR REPLACE PROCEDURE PR_�����ϱ�_���ӽ����嵥(STR_����          IN VARCHAR2,
                                           CUR_����_�б���Ϣ OUT SYS_REFCURSOR) IS
  STR_SQL VARCHAR2(1000);

  STR_��������     VARCHAR2(50) := FU_ͨ��_��ȡ�ַ���ֵ(STR_����, '|', 1);
  DAT_��Ժʱ����ʼ DATE := TO_DATE(FU_ͨ��_��ȡ�ַ���ֵ(STR_����, '|', 2),
                             'yyyy-MM-dd hh24:mi:ss');
  DAT_��Ժʱ���ֹ DATE := TO_DATE(FU_ͨ��_��ȡ�ַ���ֵ(STR_����, '|', 3),
                             'yyyy-MM-dd hh24:mi:ss');
  STR_��������     VARCHAR2(50) := FU_ͨ��_��ȡ�ַ���ֵ(STR_����, '|', 4);

  --��ȡ��ϼ�¼CURSOR
  CURSOR CUR_��ϼ�¼_��ҽ IS
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
                   T.�������
              FROM סԺ����_��Ժ������� T,
                   סԺ����_��Ժ������Ϣ TT,
                   סԺ����_������ҳ     TTT
             WHERE T.�������� = TT.��������
               AND T.�������� = TTT.��������
               AND T.סԺ������ = TT.סԺ������
               AND T.סԺ������ = TTT.סԺ������
               AND T.�������� = STR_��������
               AND T.��Ϸ��� = '1'
               AND T.������� IN ('��Ժ���', '�������')
               AND TTT.�鵵�˱��� IS NOT NULL
               AND TT.�������ͱ��� = '2'
               AND TT.��Ժʱ�� BETWEEN DAT_��Ժʱ����ʼ AND DAT_��Ժʱ���ֹ
               AND T.����ʱ�� >= TT.��Ժʱ��
               AND (TT.סԺ������ LIKE '%' || STR_�������� || '%' OR
                   TT.������ LIKE '%' || STR_�������� || '%' OR
                   TT.�������� LIKE '%' || STR_�������� || '%')) G
     WHERE (G.������� = '�������' AND G.RN <= 9)
        OR (G.������� = '��Ժ���' AND G.RN = 1);
  ROW_��ϼ�¼_��ҽ CUR_��ϼ�¼_��ҽ%ROWTYPE;

  CURSOR CUR_��ϼ�¼_��ҽ IS
    SELECT *
      FROM (SELECT ROW_NUMBER() OVER(PARTITION BY T.סԺ������ ORDER BY T.�Ƿ������ DESC) RN,
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
                   
                   (CASE
                     WHEN T.ICD��1 = T.��������1 THEN
                      (SELECT B.ICD��
                         FROM ������Ŀ_�����ֵ� B
                        WHERE B.�������� = T.��������1)
                     ELSE
                      T.ICD��1
                   END) ICD��1,
                   T.��������1,
                   T.��Ժ����1,
                   
                   (CASE
                     WHEN T.ICD��2 = T.��������2 THEN
                      (SELECT B.ICD��
                         FROM ������Ŀ_�����ֵ� B
                        WHERE B.�������� = T.��������2)
                     ELSE
                      T.ICD��2
                   END) ICD��2,
                   T.��������2,
                   T.��Ժ����2,
                   
                   (CASE
                     WHEN T.ICD��3 = T.��������3 THEN
                      (SELECT B.ICD��
                         FROM ������Ŀ_�����ֵ� B
                        WHERE B.�������� = T.��������3)
                     ELSE
                      T.ICD��3
                   END) ICD��3,
                   T.��������3,
                   T.��Ժ����3,
                   
                   (CASE
                     WHEN T.ICD��4 = T.��������4 THEN
                      (SELECT B.ICD��
                         FROM ������Ŀ_�����ֵ� B
                        WHERE B.�������� = T.��������4)
                     ELSE
                      T.ICD��4
                   END) ICD��4,
                   T.��������4,
                   T.��Ժ����4,
                   
                   (CASE
                     WHEN T.ICD��5 = T.��������5 THEN
                      (SELECT B.ICD��
                         FROM ������Ŀ_�����ֵ� B
                        WHERE B.�������� = T.��������5)
                     ELSE
                      T.ICD��5
                   END) ICD��5,
                   T.��������5,
                   T.��Ժ����5,
                   
                   (CASE
                     WHEN T.ICD��6 = T.��������6 THEN
                      (SELECT B.ICD��
                         FROM ������Ŀ_�����ֵ� B
                        WHERE B.�������� = T.��������6)
                     ELSE
                      T.ICD��6
                   END) ICD��6,
                   T.��������6,
                   T.��Ժ����6,
                   
                   (CASE
                     WHEN T.ICD��7 = T.��������7 THEN
                      (SELECT B.ICD��
                         FROM ������Ŀ_�����ֵ� B
                        WHERE B.�������� = T.��������7)
                     ELSE
                      T.ICD��7
                   END) ICD��7,
                   T.��������7,
                   T.��Ժ����7,
                   
                   (CASE
                     WHEN T.ICD��8 = T.��������8 THEN
                      (SELECT B.ICD��
                         FROM ������Ŀ_�����ֵ� B
                        WHERE B.�������� = T.��������8)
                     ELSE
                      T.ICD��8
                   END) ICD��8,
                   T.��������8,
                   T.��Ժ����8,
                   
                   (CASE
                     WHEN T.ICD��9 = T.��������9 THEN
                      (SELECT B.ICD��
                         FROM ������Ŀ_�����ֵ� B
                        WHERE B.�������� = T.��������9)
                     ELSE
                      T.ICD��9
                   END) ICD��9,
                   T.��������9,
                   T.��Ժ����9
              FROM סԺ����_��Ժ������� T,
                   סԺ����_��Ժ������Ϣ TT,
                   סԺ����_������ҳ     TTT
             WHERE T.סԺ������ = TT.סԺ������
               AND T.סԺ������ = TTT.סԺ������
               AND T.�������� = STR_��������
               AND T.��Ϸ��� = '2'
               AND TT.�������ͱ��� = '2' --ҽ��
               AND TTT.�鵵�˱��� IS NOT NULL
               AND TT.��Ժʱ�� BETWEEN DAT_��Ժʱ����ʼ AND DAT_��Ժʱ���ֹ
               AND T.����ʱ�� >= TT.��Ժʱ��
               AND (TT.סԺ������ LIKE '%' || STR_�������� || '%' OR
                   TT.������ LIKE '%' || STR_�������� || '%' OR
                   TT.�������� LIKE '%' || STR_�������� || '%')) G
     WHERE G.RN = 1;
  ROW_��ϼ�¼_��ҽ CUR_��ϼ�¼_��ҽ%ROWTYPE;

  --��ȡ������¼CURSOR
  CURSOR CUR_������¼ IS
    SELECT *
      FROM (SELECT ROW_NUMBER() OVER(PARTITION BY T.סԺ������ ORDER BY T.��ˮ�� DESC) RN,
                   T.סԺ������,
                   T.������������,
                   T.������,
                   T.������������,
                   T.����ʽ����,
                   (SELECT A.��Ա����
                      FROM ������Ŀ_��Ա���� A
                     WHERE A.�������� = T.��������
                       AND A.ɾ����־ = '0'
                       AND A.��Ա���� = T.����) AS ��������,
                   T.����,
                   (SELECT A.��Ա����
                      FROM ������Ŀ_��Ա���� A
                     WHERE A.�������� = T.��������
                       AND A.ɾ����־ = '0'
                       AND A.��Ա���� = T.����ҽʦ) AS ����ҽʦ����,
                   T.����ҽʦ
              FROM סԺ����_������ҳ������ T,
                   סԺ����_��Ժ������Ϣ   TT,
                   סԺ����_������ҳ       TTT
             WHERE T.�������� = TT.��������
               AND T.�������� = TTT.��������
               AND T.סԺ������ = TT.סԺ������
               AND T.סԺ������ = TTT.סԺ������
               AND T.�������� = STR_��������
               AND TT.�������ͱ��� = '2'
               AND TTT.�鵵�˱��� IS NOT NULL
               AND TT.��Ժʱ�� BETWEEN DAT_��Ժʱ����ʼ AND DAT_��Ժʱ���ֹ
               AND T.����ʱ�� >= TT.��Ժʱ��
               AND (TT.סԺ������ LIKE '%' || STR_�������� || '%' OR
                   TT.������ LIKE '%' || STR_�������� || '%' OR
                   TT.�������� LIKE '%' || STR_�������� || '%')) G
     WHERE G.RN <= 10;

  ROW_������¼ CUR_������¼%ROWTYPE;

  /*--��ȡ���ü�¼CURSOR
  CURSOR CUR_���ü�¼ IS
    SELECT T.��������,
           T.סԺ������,
           SUM(T.�ܽ��) AS �ܽ��,
           NVL(SUM(CASE T.��������
                     WHEN '��λ��' THEN
                      T.�ܽ��
                   END),
               0) AS ��λ��,
           NVL(SUM(CASE T.��������
                     WHEN '����' THEN
                      T.�ܽ��
                   END),
               0) AS ����,
           NVL(SUM(CASE T.��������
                     WHEN '����' THEN
                      T.�ܽ��
                   END),
               0) AS ����,
           NVL(SUM(CASE T.��������
                     WHEN '�����' THEN
                      T.�ܽ��
                   END),
               0) AS �����,
           NVL(SUM(CASE T.��������
                     WHEN '���Ʒ�' THEN
                      T.�ܽ��
                   END),
               0) AS ���Ʒ�,
           NVL(SUM(CASE T.��������
                     WHEN '������' THEN
                      T.�ܽ��
                   END),
               0) AS ������,
           NVL(SUM(CASE T.��������
                     WHEN '�����' THEN
                      T.�ܽ��
                   END),
               0) AS �����,
           NVL(SUM(CASE T.��������
                     WHEN '�������Ϸ�' THEN
                      T.�ܽ��
                   END),
               0) AS �������Ϸ�,
           NVL(SUM(CASE T.��������
                     WHEN '��ҩ��' THEN
                      T.�ܽ��
                   END),
               0) AS ��ҩ��,
           NVL(SUM(CASE T.��������
                     WHEN '��ҩ��Ƭ��' THEN
                      T.�ܽ��
                   END),
               0) AS ��ҩ��Ƭ��,
           NVL(SUM(CASE T.��������
                     WHEN '�г�ҩ��' THEN
                      T.�ܽ��
                   END),
               0) AS �г�ҩ��,
           NVL(SUM(CASE T.��������
                     WHEN 'һ�����Ʒ�' THEN
                      T.�ܽ��
                   END),
               0) AS һ�����Ʒ�,
           NVL(SUM(CASE T.��������
                     WHEN '�Һŷ�' THEN
                      T.�ܽ��
                   END),
               0) AS �Һŷ�,
           NVL(SUM(CASE T.��������
                     WHEN '������' THEN
                      T.�ܽ��
                   END),
               0) AS ������
      FROM (SELECT C.��������,
                   C.סԺ������,
                   C.����ʱ��,
                   NVL((SELECT CASE
                                WHEN X.���� IN ('��λ��',
                                              '����',
                                              '����',
                                              '���Ʒ�',
                                              '������',
                                              '�����',
                                              '�������Ϸ�',
                                              '��ҩ��' �� '�г�ҩ��',
                                              '�Һŷ�') THEN
                                 X.����
                                WHEN X.���� = '�����' THEN
                                 '�����'
                                WHEN X.���� = '�в�ҩ��' THEN
                                 '��ҩ��Ƭ��'
                                ELSE
                                 '������'
                              END AS ����
                         FROM ������Ŀ_���ù��� G, ������Ŀ_�ֵ���ϸ X
                        WHERE G.�������� = C.��������
                          AND G.ɾ����־ = '0'
                          AND G.��� = 'סԺ��Ʊ��Ŀ����'
                          AND X.������� = 'GB_009001'
                          AND G.���ñ��� = C.�������
                          AND G.�������� = X.����),
                       '') AS ��������,
                   C.�ܽ��
              FROM סԺ����_��Ժ���˴��� C) T,
           סԺ����_��Ժ������Ϣ TT,
           סԺ����_������ҳ TTT
     WHERE T.�������� = TT.��������
       AND T.�������� = TTT.��������
       AND T.סԺ������ = TT.סԺ������
       AND T.סԺ������ = TTT.סԺ������
       AND T.�������� = STR_��������
       AND TTT.�鵵�˱��� IS NOT NULL
       AND TT.�������ͱ��� = '2'
       AND TT.��Ժʱ�� BETWEEN DAT_��Ժʱ����ʼ AND DAT_��Ժʱ���ֹ
       AND T.����ʱ�� >= TT.��Ժʱ��
       AND (TT.סԺ������ LIKE '%' || STR_�������� || '%' OR
           TT.������ LIKE '%' || STR_�������� || '%' OR
           TT.�������� LIKE '%' || STR_�������� || '%')
     GROUP BY T.��������, T.סԺ������;
  
  ROW_���ü�¼ CUR_���ü�¼%ROWTYPE;*/

  --��ȡ��֢�໤��¼CURSOR
  CURSOR CUR_��֢�໤��¼ IS
    SELECT *
      FROM (SELECT ROW_NUMBER() OVER(PARTITION BY T.סԺ������ ORDER BY T.��ˮ�� DESC) RN,
                   '9' AS ��������,
                   T.����ʱ��,
                   T.�˳�ʱ��,
                   (T.�˳�ʱ�� - T.����ʱ��) * 24 AS �ϼ�ʱ��,
                   T.סԺ������
              FROM סԺ����_������ҳ_��֢�໤ T,
                   סԺ����_��Ժ������Ϣ      TT,
                   סԺ����_������ҳ          TTT
             WHERE T.�������� = TT.��������
               AND T.�������� = TTT.��������
               AND T.סԺ������ = TT.סԺ������
               AND T.סԺ������ = TTT.סԺ������
               AND T.�������� = STR_��������
               AND TT.�������ͱ��� = '2'
               AND TTT.�鵵�˱��� IS NOT NULL
               AND TT.��Ժʱ�� BETWEEN DAT_��Ժʱ����ʼ AND DAT_��Ժʱ���ֹ
               AND T.����ʱ�� >= TT.��Ժʱ��
               AND (TT.סԺ������ LIKE '%' || STR_�������� || '%' OR
                   TT.������ LIKE '%' || STR_�������� || '%' OR
                   TT.�������� LIKE '%' || STR_�������� || '%')) G
     WHERE G.RN <= 6;

  ROW_��֢�໤��¼ CUR_��֢�໤��¼%ROWTYPE;

BEGIN

  BEGIN
  
    DELETE FROM ��ʱ��_�����ϱ�_���ӽ����嵥;
  
    --���»�����Ϣ
    INSERT INTO ��ʱ��_�����ϱ�_���ӽ����嵥
      (������,
       MS_ID, --�����嵥 ID
       BAH, --������
       DDYLJGMC, --����ҽ�ƻ�������
       DDYLJGDM, --����ҽ�ƻ�������
       YBJSDJ, --ҽ������ȼ�
       YBBH, --ҽ�����
       SBSJ, --�걨ʱ��
       XM, --����
       XB, --�Ա�
       CSRQ, --��������
       NL, --����
       BZZSNL, --����һ��������
       GJ, --����
       MZ, --����
       ZJLX, --����֤������
       ZJHM, --����֤������
       ZY, --ְҵ
       XZZ, --��סַ
       XZZ_S, --��סַ-ʡ
       XZZ_SHI, --��סַ-��
       XZZ_X, --��סַ-��
       XZZ_Z, --��סַ-���
       GZDWMC, --������λ����
       GZDWDZ, --������λ��ַ
       GZDWDH, --������λ�绰
       GZDWYB, --������λ�ʱ�
       LXRXM, --��ϵ������
       LXRGX, --��ϵ�˹�ϵ
       LXRDZ, --��ϵ�˵�ַ
       LXRDZ_S, --��ϵ�˵�ַ-ʡ
       LXRDZ_SHI, --��ϵ�˵�ַ-��
       LXRDZ_X, --��ϵ�˵�ַ-��
       LXRDZ_Z, --��ϵ�˵�ַ-���
       LXRDH, --��ϵ�˵绰
       YBLX, --ҽ������
       TSRYLX, --������Ա����
       CBD, --�α���
       XSEYYLX, --��������Ժ����
       XSECSTZ, --��������������
       XSERYTZ, --��������Ժ����       
       
       --�������Բ����
       
       ZYYLLX, --סԺҽ������
       RYTJ, --��Ժ;��
       ZLLB, --�������
       RYSJ, --��Ժʱ��
       RYKB, --��Ժ�Ʊ����
       RYKBMC, --��Ժ�Ʊ�����
       ZKKB, --ת�ƿƱ����
       ZKKBMC, --ת�ƿƱ�����
       CYSJ, --��Ժʱ��
       CYKB, --��Ժ�Ʊ����
       CYKBMC, --��Ժ�Ʊ�����
       SJZYTS, --ʵ��סԺ����
       mjzzd_xy, --�ż����������_��ҽ
       mjzzddm_xy, --�ż�����ϴ���_��ҽ
       mjzzd_zy, --�ż����������_��ҽ
       mjzzddm_zy, --�ż�����ϴ���_��ҽ
       
       --������
       
       --�������
       
       HXJSYSJ_T, --������ʹ��ʱ��-��
       HXJSYSJ_XS, --������ʹ��ʱ��-Сʱ
       HXJSYSJ_F, --������ʹ��ʱ��-����
       RYQ_T, --­�����˻��߻�����Ժǰʱ�䣺��
       RYQ_XS, --­�����˻��߻�����Ժǰʱ�䣺Сʱ
       RYQ_F, --­�����˻��߻�����Ժǰʱ�䣺��
       RYH_T, --­�����˻��߻�����Ժ��ʱ�䣺��
       RYH_XS, --­�����˻��߻�����Ժ��ʱ�䣺Сʱ
       RYH_F, --­�����˻��߻�����Ժ��ʱ�䣺��
       
       --��֢�໤
       
       SXPZ, --��ѪƷ��
       SXL, --��Ѫ��
       SXJLDW, --��Ѫ������λ
       TJHLTS, --�ؼ���������
       YJHLTS, --һ����������
       EJHLTS, --������������
       SJHLTS, --������������
       LYFS, -- ��Ժ��ʽ
       NJSJGMC, --ҽ��תԺ����ջ�������
       NJSJGDM, --ҽ��תԺ����ջ�������
       SFZZYJH, --�Ƿ��г�Ժ31����סԺ�ƻ�
       MD, --��סԺĿ��
       ZZYSXM, --����ҽʦ����
       ZZYSDM, --����ҽʦ����
       YWLSH, --ҽ���շ���Ϣҵ����ˮ��
       JSSJ_KS, --����ʱ��_��ʼ
       JSSJ_JS, --����ʱ��_����
       
       --�������
       YLJGTBBM, --ҽ�ƻ�������Ŵ���
       YLJGTBBMMC, --ҽ�ƻ������������
       YLJGTBR, --ҽ�ƻ�����˱��
       YLJGTBRXM, --ҽ�ƻ����������
       DMDATE_ID, --�ϱ���ʶ
       QDLSH, -- �嵥��ˮ��
       PJDM, --Ʊ�ݴ���
       PJHM, --Ʊ�ݺ���
       YBJGDM, --ҽ����������
       YBJGJBRDM, --ҽ�����������˴���
       ORG_CODE --��������
       
       )
      SELECT T.סԺ������,
             T.סԺ������,
             T.������,
             'Ӫ��������ҽ���ҽԺ��Ӫ�ھ��ü����������ڶ�����ҽԺ��',
             '001026',
             '2',
             T.���֤��,
             SYSDATE,
             T.��������,
             T.�Ա�,
             T.��������,
             TO_NUMBER(REGEXP_REPLACE(T.����, '[^-0-9.]', '')) ����,
             TO_NUMBER(nvl(REGEXP_REPLACE(T.���䲻��1����, '[^-0-9.]', ''),
                           '0')),
             '156', --�й�
             CASE
               WHEN TO_NUMBER(NVL(T.����, '1')) < 10 THEN
                '0' || NVL(T.����, '1')
               ELSE
                T.����
             END,
             NVL(T.֤�����, '01'),
             T.���֤��,
             T.ְҵ,
             T.��סַ,
             NULL,
             NULL,
             NULL,
             NULL,
             T.������λ,
             T.������λ��ַ,
             SUBSTR(T.�����绰, 0, 20),
             T.������������,
             T.��ϵ������,
             T.��ϵ,
             T.��ϵ�˵�ַ,
             NULL,
             NULL,
             NULL,
             NULL,
             T.��ϵ�˵绰,
             T.ҽ�Ƹ��ѷ�ʽ,
             '9',
             (SELECT J.C_3
                FROM �ӿڹ���_�ӿڲ�����Ϣ J
               WHERE J.�������� = T.��������
                 AND J.������ = T.סԺ������),
             NULL,
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
             
             --�������Բ����
             
             '1',
             T.��Ժ;��,
             T.�������,
             T.��Ժ����,
             (SELECT A.�������
                FROM ������Ŀ_���Ҳ������� A
               WHERE A.�������� = STR_��������
                 AND A.���ұ��� = T.��Ժ���ұ���) ��Ժ�Ʊ�,
             
             (SELECT A.�������
                FROM ������Ŀ_���Ҳ������� A
               WHERE A.�������� = STR_��������
                 AND A.���ұ��� = T.��Ժ���ұ���) ��Ժ�Ʊ�,
             NVL((SELECT ת�����ұ���
                   FROM סԺ����_��Ժ����ת�Ƽ�¼
                  WHERE �������� = STR_��������
                    AND סԺ������ = T.סԺ������
                    AND ROWNUM = 1),
                 (SELECT A.�������
                    FROM ������Ŀ_���Ҳ������� A
                   WHERE A.�������� = STR_��������
                     AND A.���ұ��� = T.��Ժ���ұ���)) ת�ƿƱ�,
             NVL((SELECT ת�����ұ���
                   FROM סԺ����_��Ժ����ת�Ƽ�¼
                  WHERE �������� = STR_��������
                    AND סԺ������ = T.סԺ������
                    AND ROWNUM = 1),
                 (SELECT A.�������
                    FROM ������Ŀ_���Ҳ������� A
                   WHERE A.�������� = STR_��������
                     AND A.���ұ��� = T.��Ժ���ұ���)) ת�ƿƱ�,
             T.��Ժ����,
             (SELECT A.�������
                FROM ������Ŀ_���Ҳ������� A
               WHERE A.�������� = STR_��������
                 AND A.���ұ��� = T.��Ժ���ұ���) ��Ժ�Ʊ�,
             (SELECT A.�������
                FROM ������Ŀ_���Ҳ������� A
               WHERE A.�������� = STR_��������
                 AND A.���ұ��� = T.��Ժ���ұ���) ��Ժ�Ʊ�,
             T.סԺ����,
             
             (SELECT A.��������
                FROM סԺ����_��Ժ������� A
               WHERE A.�������� = STR_��������
                 AND A.סԺ������ = T.סԺ������
                 AND A.������� = '�������'
                 AND A.��Ϸ��� = '1'
                 AND ROWNUM = 1) AS �������������ҽ,
             
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
                 AND ROWNUM = 1) AS ����������ҽ,
             
             (SELECT A.��������
                FROM סԺ����_��Ժ������� A
               WHERE A.�������� = STR_��������
                 AND A.סԺ������ = T.סԺ������
                 AND A.������� = '�������'
                 AND A.��Ϸ��� = '2'
                 AND ROWNUM = 1) AS �������������ҽ,
             
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
                 AND ROWNUM = 2) AS ����������ҽ,
             --������
             
             --�������
             
             trunc(TO_NUMBER(nvl(REGEXP_REPLACE(T.�д�������ʹ��ʱ��,
                                                '[^0-9.]',
                                                ''),
                                 '0')) / 24),
             
             trunc(mod(TO_NUMBER(nvl(REGEXP_REPLACE(T.�д�������ʹ��ʱ��,
                                                    '[^0-9.]',
                                                    ''),
                                     '0')),
                       24)),
             0,
             TO_NUMBER(REGEXP_REPLACE(T.­�����˻��߻���ʱ����Ժǰ��,
                                      '[^-0-9.]',
                                      '')) ­�����˻��߻���ʱ����Ժǰ��,
             TO_NUMBER(REGEXP_REPLACE(T.­�����˻��߻���ʱ����ԺǰСʱ,
                                      '[^-0-9.]',
                                      '')) ­�����˻��߻���ʱ����ԺǰСʱ,
             TO_NUMBER(REGEXP_REPLACE(T.­�����˻��߻���ʱ����Ժǰ����,
                                      '[^-0-9.]',
                                      '')) ­�����˻��߻���ʱ����Ժǰ����,
             TO_NUMBER(REGEXP_REPLACE(T.­�����˻��߻���ʱ����Ժ����,
                                      '[^-0-9.]',
                                      '')) ­�����˻��߻���ʱ����Ժ����,
             TO_NUMBER(REGEXP_REPLACE(T.­�����˻��߻���ʱ����Ժ��Сʱ,
                                      '[^-0-9.]',
                                      '')) ­�����˻��߻���ʱ����Ժ��Сʱ,
             TO_NUMBER(REGEXP_REPLACE(T.­�����˻��߻���ʱ����Ժ����,
                                      '[^-0-9.]',
                                      '')) ­�����˻��߻���ʱ����Ժ����,
             
             --��֢�໤
             
             NULL,
             NULL,
             NULL,
             T.�ؼ���������,
             T.һ����������,
             T.������������,
             T.������������,
             T.��Ժ��ʽ,
             T.ҽ��תԺ,
             T.ҽ��ת�����������,
             T.�Ƿ��г�Ժ31����סԺ�ƻ�,
             T.Ŀ��,
             (SELECT R.��Ա����
                FROM ������Ŀ_��Ա���� R
               WHERE R.�������� = T.��������
                 AND R.��Ա���� = T.����ҽʦ),
             '',
             NULL,
             (SELECT to_date(J.C_21, 'yyyy-MM-dd hh24:mi:ss')
                FROM �ӿڹ���_�ӿڲ�����Ϣ J
               WHERE J.�������� = T.��������
                 AND J.������ = T.סԺ������),
             (SELECT to_date(J.C_21, 'yyyy-MM-dd hh24:mi:ss')
                FROM �ӿڹ���_�ӿڲ�����Ϣ J
               WHERE J.�������� = T.��������
                 AND J.������ = T.סԺ������),
             
             --�������
             
             '001026',
             'Ӫ��������ҽ���ҽԺ��Ӫ�ھ��ü����������ڶ�����ҽԺ��',
             '1314',
             '�����',
             '001026' || TO_CHAR(SYSDATE, 'yyyyMMddhh24miss'),
             NULL,
             NULL,
             NULL,
             '001026',
             NULL,
             '122108044641640037'
      
        FROM סԺ����_������ҳ     T,
             סԺ����_��Ժ������Ϣ TT,
             �ӿڹ���_�ӿڲ�����Ϣ TTT
       WHERE T.�������� = TT.��������
         and TT.�������� = TTT.��������
         AND T.סԺ������ = TT.סԺ������
         AND TT.סԺ������ = TTT.������
         AND T.�������� = STR_��������
         AND T.�鵵�˱��� IS NOT NULL
         AND TT.�������ͱ��� = '2'
         AND T.��Ժ���� BETWEEN DAT_��Ժʱ����ʼ AND DAT_��Ժʱ���ֹ
         AND (T.סԺ������ LIKE '%' || STR_�������� || '%' OR
             T.������ LIKE '%' || STR_�������� || '%' OR
             T.�������� LIKE '%' || STR_�������� || '%');
  END;

  --������ҽ��ϼ�¼��Ϣ
  FOR ROW_��ϼ�¼_��ҽ IN CUR_��ϼ�¼_��ҽ LOOP
    EXIT WHEN CUR_��ϼ�¼_��ҽ%NOTFOUND;
    IF ROW_��ϼ�¼_��ҽ.������� = '��Ժ���' THEN
      STR_SQL := 'UPDATE ��ʱ��_�����ϱ�_���ӽ����嵥 SET ZYZD=:1,ZYZDDM=:2,RYBQ=:3 WHERE ������=:4';
      EXECUTE IMMEDIATE STR_SQL
        USING ROW_��ϼ�¼_��ҽ.��������, ROW_��ϼ�¼_��ҽ.ICD��, ROW_��ϼ�¼_��ҽ.��Ժ����, ROW_��ϼ�¼_��ҽ.סԺ������;
    ELSIF ROW_��ϼ�¼_��ҽ.������� = '�������' THEN
      STR_SQL := 'UPDATE ��ʱ��_�����ϱ�_���ӽ����嵥 SET QTZD' || ROW_��ϼ�¼_��ҽ.RN ||
                 '=:1,QTZDDM' || ROW_��ϼ�¼_��ҽ.RN || '=:2,QTZDRYBQ' ||
                 ROW_��ϼ�¼_��ҽ.RN || '=:3 WHERE ������=:4';
      EXECUTE IMMEDIATE STR_SQL
        USING ROW_��ϼ�¼_��ҽ.��������, ROW_��ϼ�¼_��ҽ.ICD��, ROW_��ϼ�¼_��ҽ.��Ժ����, ROW_��ϼ�¼_��ҽ.סԺ������;
    END IF;
  END LOOP;

  --������ҽ��ϼ�¼��Ϣ
  FOR ROW_��ϼ�¼_��ҽ IN CUR_��ϼ�¼_��ҽ LOOP
    EXIT WHEN CUR_��ϼ�¼_��ҽ%NOTFOUND;
    --��ҽ
    STR_SQL := 'UPDATE ��ʱ��_�����ϱ�_���ӽ����嵥 SET ZBZD=:1,ZBZDDM=:2, ZBZDRYBQ=:3,
                   ZZZD1=:4,ZZZDDM1=:5,ZZZDRYBQ1=:6 ,
                   ZZZD2=:7,ZZZDDM2=:8,ZZZDRYBQ2=:9,
                   ZZZD3=:10,ZZZDDM3=:11,ZZZDRYBQ3=:12,
                   ZZZD4=:13,ZZZDDM4=:14,ZZZDRYBQ4=:15, 
                   ZZZD5=:16,ZZZDDM5=:17,ZZZDRYBQ5=:18, 
                   ZZZD6=:19,ZZZDDM6=:20,ZZZDRYBQ6=:21, 
                   ZZZD7=:22,ZZZDDM7=:23,ZZZDRYBQ7=:24, 
                   ZZZD8=:25,ZZZDDM8=:26,ZZZDRYBQ8=:27, 
                   ZZZD9=:28,ZZZDDM9=:29,ZZZDRYBQ9=:30
                   WHERE ������=:31';
  
    EXECUTE IMMEDIATE STR_SQL
      USING ROW_��ϼ�¼_��ҽ.��������, SUBSTR(ROW_��ϼ�¼_��ҽ.ICD��, 0, 6), ROW_��ϼ�¼_��ҽ.��Ժ����, ROW_��ϼ�¼_��ҽ.��������1, SUBSTR(ROW_��ϼ�¼_��ҽ.ICD��1, 0, 6), ROW_��ϼ�¼_��ҽ.��Ժ����1, ROW_��ϼ�¼_��ҽ.��������2, SUBSTR(ROW_��ϼ�¼_��ҽ.ICD��2, 0, 6), ROW_��ϼ�¼_��ҽ.��Ժ����2, ROW_��ϼ�¼_��ҽ.��������3, SUBSTR(ROW_��ϼ�¼_��ҽ.ICD��3, 0, 6), ROW_��ϼ�¼_��ҽ.��Ժ����3, ROW_��ϼ�¼_��ҽ.��������4, SUBSTR(ROW_��ϼ�¼_��ҽ.ICD��4, 0, 6), ROW_��ϼ�¼_��ҽ.��Ժ����4, ROW_��ϼ�¼_��ҽ.��������5, SUBSTR(ROW_��ϼ�¼_��ҽ.ICD��5, 0, 6), ROW_��ϼ�¼_��ҽ.��Ժ����5, ROW_��ϼ�¼_��ҽ.��������6, SUBSTR(ROW_��ϼ�¼_��ҽ.ICD��6, 0, 6), ROW_��ϼ�¼_��ҽ.��Ժ����6, ROW_��ϼ�¼_��ҽ.��������7, SUBSTR(ROW_��ϼ�¼_��ҽ.ICD��7, 0, 6), ROW_��ϼ�¼_��ҽ.��Ժ����7, ROW_��ϼ�¼_��ҽ.��������8, SUBSTR(ROW_��ϼ�¼_��ҽ.ICD��8, 0, 6), ROW_��ϼ�¼_��ҽ.��Ժ����8, ROW_��ϼ�¼_��ҽ.��������9, SUBSTR(ROW_��ϼ�¼_��ҽ.ICD��9, 0, 6), ROW_��ϼ�¼_��ҽ.��Ժ����9, ROW_��ϼ�¼_��ҽ.סԺ������;
  END LOOP;

  --���·��ü�¼��Ϣ
  /*FOR ROW_���ü�¼ IN CUR_���ü�¼ LOOP
    EXIT WHEN CUR_���ü�¼%NOTFOUND;
    UPDATE ��ʱ��_�����ϱ�_���ӽ����嵥 T
       SET T.CWF_DM     = '01',
           T.CWF_MC     = '��λ��',
           T.CWF_XMJE   = ROW_���ü�¼.��λ��,
           T.ZCF_DM     = '02',
           T.ZCF_MC     = '����',
           T.ZCF_XMJE   = ROW_���ü�¼.����,
           T.JCF_DM     = '03',
           T.JCF_MC     = '����',
           T.JCF_XMJE   = ROW_���ü�¼.����,
           T.HYF_DM     = '04',
           T.HYF_MC     = '�����',
           T.HYF_XMJE   = ROW_���ü�¼.�����,
           T.ZLF_DM     = '05',
           T.ZLF_MC     = '���Ʒ�',
           T.ZLF_XMJE   = ROW_���ü�¼.���Ʒ�,
           T.SSF_DM     = '06',
           T.SSF_MC     = '������',
           T.SSF_XMJE   = ROW_���ü�¼.������,
           T.HLF_DM     = '07',
           T.HLF_MC     = '�����',
           T.HLF_XMJE   = ROW_���ü�¼.�����,
           T.WSCLF_DM   = '08',
           T.WSCLF_MC   = '�������Ϸ�',
           T.WSCLF_XMJE = ROW_���ü�¼.�������Ϸ�,
           T.XYF_DM     = '09',
           T.XYF_MC     = '��ҩ��',
           T.XYF_XMJE   = ROW_���ü�¼.��ҩ��,
           T.ZYYPF_DM   = '10',
           T.ZYYPF_MC   = '��ҩ��Ƭ��',
           T.ZYYPF_XMJE = ROW_���ü�¼.��ҩ��Ƭ��,
           T.ZCYF_DM    = '11',
           T.ZCYF_MC    = '�г�ҩ��',
           T.ZCYF_XMJE  = ROW_���ü�¼.�г�ҩ��,
           T.YBZLF_DM   = '12',
           T.YBZLF_MC   = 'һ�����Ʒ�',
           T.YBZLF_XMJE = ROW_���ü�¼.һ�����Ʒ�,
           T.GHF_DM     = '13',
           T.GHF_MC     = '�Һŷ�',
           T.GHF_XMJE   = ROW_���ü�¼.�Һŷ�,
           T.QTF_DM     = '14',
           T.QTF_MC     = '������',
           T.QTF_XMJE   = ROW_���ü�¼.������,
           T.YLZFY      = ROW_���ü�¼.�ܽ��
     WHERE T.������ = ROW_���ü�¼.סԺ������;
  END LOOP;*/

  --����������¼��Ϣ
  FOR ROW_������¼ IN CUR_������¼ LOOP
    EXIT WHEN CUR_������¼%NOTFOUND;
  
    IF ROW_������¼.RN = 1 THEN
      STR_SQL := 'UPDATE ��ʱ��_�����ϱ�_���ӽ����嵥 SET ZYSSCZMC=:1,ZYSSCZDM=:2,ZYSSCZRQ=:3
               ,ZYMZFS=:4,ZYSZYSXM=:5,ZYSZYSDM=:6,ZYMZYSXM=:7,ZYMZYSDM=:8,SSCZDMJS=:9 WHERE ������=:10';
    ELSE
      STR_SQL := 'UPDATE ��ʱ��_�����ϱ�_���ӽ����嵥 SET SSCZMC' || (ROW_������¼.RN - 1) ||
                 '=:1, SSCZDM' || (ROW_������¼.RN - 1) || '=:2, SSCZRQ' ||
                 (ROW_������¼.RN - 1) || '= :3
               , MZFS' || (ROW_������¼.RN - 1) ||
                 '=:4,   SZYSXM' || (ROW_������¼.RN - 1) || '=:5, SZYSDM' ||
                 (ROW_������¼.RN - 1) || '=:6, MZYSXM' || (ROW_������¼.RN - 1) ||
                 '=:7, MZYSDM' || (ROW_������¼.RN - 1) ||
                 '=:8,SSCZDMJS=:9 WHERE ������=:10';
    END IF;
  
    EXECUTE IMMEDIATE STR_SQL
      USING ROW_������¼.������������, ROW_������¼.������, ROW_������¼.������������, ROW_������¼.����ʽ����, ROW_������¼.��������, ROW_������¼.����, ROW_������¼.����ҽʦ����, ROW_������¼.����ҽʦ, ROW_������¼.RN, ROW_������¼.סԺ������;
  END LOOP;

  --������֢�໤��¼��Ϣ
  FOR ROW_��֢�໤��¼ IN CUR_��֢�໤��¼ LOOP
    EXIT WHEN CUR_��֢�໤��¼%NOTFOUND;
    STR_SQL := 'UPDATE ��ʱ��_�����ϱ�_���ӽ����嵥 SET ZZJHBFLX' || ROW_��֢�໤��¼.RN ||
               '=:1, JZZJHSSJ' || ROW_��֢�໤��¼.RN || '=:2, CZZJHSSJ' ||
               ROW_��֢�໤��¼.RN || '= :3, HJ' || ROW_��֢�໤��¼.RN ||
               '= :4 WHERE ������=:5';
  
    EXECUTE IMMEDIATE STR_SQL
      USING ROW_��֢�໤��¼.��������, ROW_��֢�໤��¼.����ʱ��, ROW_��֢�໤��¼.�˳�ʱ��, ROW_��֢�໤��¼.�ϼ�ʱ��, ROW_��֢�໤��¼.סԺ������;
  END LOOP;

  --�������ݼ�
  OPEN CUR_����_�б���Ϣ FOR
    SELECT ROW_NUMBER() OVER(ORDER BY T.������) ���, T.*
      FROM ��ʱ��_�����ϱ�_���ӽ����嵥 T;

END PR_�����ϱ�_���ӽ����嵥;
/
