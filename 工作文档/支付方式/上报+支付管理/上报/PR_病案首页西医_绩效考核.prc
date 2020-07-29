CREATE OR REPLACE PROCEDURE PR_������ҳ��ҽ_��Ч����(STR_����          IN VARCHAR2,
                                           CUR_����_�б���Ϣ OUT SYS_REFCURSOR) IS
  STR_SQL  VARCHAR2(1000);
  STR_��� VARCHAR2(20);

  STR_��������     VARCHAR2(50) := FU_ͨ��_��ȡ�ַ���ֵ(STR_����, '|', 1);
  DAT_��Ժʱ����ʼ DATE := TO_DATE(FU_ͨ��_��ȡ�ַ���ֵ(STR_����, '|', 2),
                             'yyyy-MM-dd hh24:mi:ss');
  DAT_��Ժʱ���ֹ DATE := TO_DATE(FU_ͨ��_��ȡ�ַ���ֵ(STR_����, '|', 3),
                             'yyyy-MM-dd hh24:mi:ss');
  STR_��������     VARCHAR2(50) := FU_ͨ��_��ȡ�ַ���ֵ(STR_����, '|', 4);

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
                   T.�������
              FROM סԺ����_��Ժ������� T,
                   סԺ����_��Ժ������Ϣ TT,
                   סԺ����_������ҳ     TTT
             WHERE T.סԺ������ = TT.סԺ������
               AND T.סԺ������ = TTT.סԺ������
               AND T.�������� = STR_��������
               AND TTT.������� = '3' --��ҽ
               AND TTT.�鵵�˱��� IS NOT NULL
               AND TT.��Ժʱ�� BETWEEN DAT_��Ժʱ����ʼ AND DAT_��Ժʱ���ֹ
               AND T.��Ϸ��� = '1'
               AND T.������� IN
                   ('��Ժ���', '�������', '���˺��ж��ⲿԭ��', '�������')
               AND T.����ʱ�� >= TT.��Ժʱ��
               AND (TT.סԺ������ LIKE '%' || STR_�������� || '%' OR
                   TT.������ LIKE '%' || STR_�������� || '%' OR
                   TT.�������� LIKE '%' || STR_�������� || '%')) G
     WHERE (G.������� = '�������' AND G.RN <= 15)
        OR (G.������� <> '�������' AND G.RN = 1);
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
                   T.�п����ϵȼ�����,
                   T.����ʽ����,
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
               AND TTT.������� = '3' --��ҽ
               AND TTT.�鵵�˱��� IS NOT NULL
               AND TT.��Ժʱ�� BETWEEN DAT_��Ժʱ����ʼ AND DAT_��Ժʱ���ֹ
               AND T.����ʱ�� >= TT.��Ժʱ��
               AND (TT.סԺ������ LIKE '%' || STR_�������� || '%' OR
                   TT.������ LIKE '%' || STR_�������� || '%' OR
                   TT.�������� LIKE '%' || STR_�������� || '%')) G
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
       AND TTT.������� = '3' --��ҽ
       AND TTT.�鵵�˱��� IS NOT NULL
       AND TT.��Ժʱ�� BETWEEN DAT_��Ժʱ����ʼ AND DAT_��Ժʱ���ֹ
       AND T.����ʱ�� >= TT.��Ժʱ��
       AND (TT.סԺ������ LIKE '%' || STR_�������� || '%' OR
           TT.������ LIKE '%' || STR_�������� || '%' OR
           TT.�������� LIKE '%' || STR_�������� || '%')
     GROUP BY T.��������, T.סԺ������;

  ROW_���ü�¼ CUR_���ü�¼%ROWTYPE;

  --��ȡ��֢�໤��¼CURSOR
  CURSOR CUR_��֢�໤��¼ IS
    SELECT *
      FROM (SELECT ROW_NUMBER() OVER(PARTITION BY T.סԺ������ ORDER BY T.��ˮ�� DESC) RN,
                   T.���ұ���,
                   T.����ʱ��,
                   T.�˳�ʱ��,
                   T.סԺ������
              FROM סԺ����_������ҳ_��֢�໤ T,
                   סԺ����_��Ժ������Ϣ      TT,
                   סԺ����_������ҳ          TTT
             WHERE T.סԺ������ = TT.סԺ������
               AND T.סԺ������ = TTT.סԺ������
               AND TTT.������� = '3' --��ҽ
               AND TTT.�鵵�˱��� IS NOT NULL
               AND TT.��Ժʱ�� BETWEEN DAT_��Ժʱ����ʼ AND DAT_��Ժʱ���ֹ
               AND T.����ʱ�� >= TT.��Ժʱ��
               AND (TT.סԺ������ LIKE '%' || STR_�������� || '%' OR
                   TT.������ LIKE '%' || STR_�������� || '%' OR
                   TT.�������� LIKE '%' || STR_�������� || '%')) G
     WHERE G.RN <= 5;

  ROW_��֢�໤��¼ CUR_��֢�໤��¼%ROWTYPE;

BEGIN

  BEGIN
  
    DELETE FROM ��ʱ��_������ҳ��ҽ_��Ч����;
  
    --���»�����Ϣ
    INSERT INTO ��ʱ��_������ҳ��ҽ_��Ч����
      (סԺ������,
       A01, --��֯��������
       A02, --ҽ�ƻ�������
       A48, --������
       A49, --סԺ����
       B12, --��Ժʱ��
       B15, --��Ժʱ��
       A47, --��������
       A46C, --ҽ�Ƹ��ѷ�ʽ
       A11, --����
       A12C, --�Ա�
       A13, --��������
       A14, --���䣨�꣩
       A15C, --����
       A21C, --����
       A38C, --ְҵ
       A19C, --����
       A20N, --֤�����
       A20, --֤������
       A22, --������ַ
       A23C, --����ʡ����������ֱϽ�У�
       A24, --���ڵ�ַ
       A25C, --���ڵ�ַ��������
       A26, --��סַ
       A27, --��סַ�绰,
       A28C, --��סַ��������
       A29, --������λ����ַ
       A30, --������λ�绰
       A31C, --������λ��������
       A32, --��ϵ������
       A33C, --��ϵ�˹�ϵ
       A34, --��ϵ�˵�ַ
       A35, --��ϵ�˵绰
       B38, --�Ƿ�Ϊ�ռ�����
       B11C, --��Ժ;��
       B13C, --��Ժ�Ʊ�
       B14, --��Ժ����
       B21C, --ת�ƿƱ�
       B16C, --��Ժ�Ʊ�
       B17, --��Ժ����
       B20, --ʵ��סԺ���죩
       C01C, --�ţ���������ϱ���
       C02N, --�ţ��������������
       
       --ʡ���������
       C11, --�����
       C24C, --����ҩ�����
       C25, --����ҩ������
       B22C, --������ִҵ֤�����
       B22, --������
       B23C, --���Σ������Σ�ҽʦִҵ֤�����
       B23, --���Σ������Σ�ҽʦ
       B24C, --����ҽʦִҵ֤�����
       B24, --����ҽʦ
       B25C, --סԺҽʦִҵ֤�����
       B25, --סԺҽʦ
       B26C, --���λ�ʿִҵ֤�����
       B26, --���λ�ʿ
       B27, --����ҽʦ
       B28, --ʵϰҽʦ
       B29, --����Ա
       B30C, --��������
       B31, --�ʿ�ҽʦ
       B32, --�ʿػ�ʿ
       B33, --�ʿ�����
       C34C, --��������ʬ��
       C26C, --ABOѪ��
       C27C, --RHѪ��
       
       --ʡ���������
       A16, --���䲻��1��������䣨�죩
       A18X01, --��������������(��)
       A17, --��������Ժ���أ��ˣ�
       C28, --­�����˻��߻�����Ժǰʱ����
       C29, --Сʱ
       C30, --����
       C31, --­�����˻��߻�����Ժ��ʱ����
       C32, --Сʱ
       C33, --����
       C47, --�д�������ʹ��ʱ��
       B36C, --�Ƿ��г�Ժ31������סԺ�ƻ�
       B37, --��Ժ31����סԺ�ƻ�Ŀ��
       B34C, --��Ժ��ʽ
       B35 --ҽ��תԺ��ת���������������/��������Ժ����
       
       --ʡ�Է�������
       )
      SELECT T.סԺ������,
             T.��������,
             'Ӫ�ھ��ü����������ڶ�����ҽԺ' ��������,
             T.������,
             TO_NUMBER(T.סԺ����),
             T.��Ժ����,
             T.��Ժ����,
             T.��������,
             T.ҽ�Ƹ��ѷ�ʽ,
             T.��������,
             TO_NUMBER(NVL(T.�Ա�, '0')),
             trunc(T.��������) ��������,
             TO_NUMBER(REGEXP_REPLACE(T.����, '[^-0-9.]', '')) ����,
             T.����,
             T.����,
             T.ְҵ,
             T.����,
             T.֤�����,
             SUBSTR(T.���֤��, 0, 18),
             T.������ַ,
             T.����ʡ,
             T.���ڵ�ַ,
             SUBSTR(T.������������, 0, 6),
             T.��סַ,
             SUBSTR(T.���ڵ绰, 0, 20),
             SUBSTR(T.��סַ�ʱ�, 0, 6),
             T.������λ || T.������λ��ַ,
             SUBSTR(T.�����绰, 0, 20),
             SUBSTR(T.������������, 0, 6),
             T.��ϵ������,
             T.��ϵ,
             T.��ϵ�˵�ַ,
             SUBSTR(T.��ϵ�˵绰, 0, 20),
             T.�Ƿ�Ϊ�ռ�����,
             T.��Ժ;��,
             (SELECT A.�������
                FROM ������Ŀ_���Ҳ������� A
               WHERE A.�������� = STR_��������
                 AND A.���ұ��� = T.��Ժ���ұ���) ��Ժ���ұ���,
             T.��Ժ����,
             (SELECT LISTAGG(ת�����ұ���, '��') WITHIN GROUP(ORDER BY ת��ʱ��) AS ת�ƿƱ�
                FROM סԺ����_��Ժ����ת�Ƽ�¼
               WHERE �������� = STR_��������
                 AND סԺ������ = T.סԺ������
                 AND ROWNUM <= 3) ת�ƿƱ�,
             (SELECT A.�������
                FROM ������Ŀ_���Ҳ������� A
               WHERE A.�������� = STR_��������
                 AND A.���ұ��� = T.��Ժ���ұ���) ��Ժ���ұ���,
             T.��Ժ����,
             TO_NUMBER(T.סԺ����),
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
             (SELECT SUBSTR(A.��������, 0, 50)
                FROM סԺ����_��Ժ������� A
               WHERE A.�������� = STR_��������
                 AND A.סԺ������ = T.סԺ������
                 AND A.������� = '�������'
                 AND A.��Ϸ��� = '1'
                 AND ROWNUM = 1) AS �����������,
             
             --ʡ���������
             T.�����,
             T.ҩ�����,
             T.����ҩ��,
             (SELECT ҽʦִҵ֤�����
                FROM ������Ŀ_��Ա����
               WHERE �������� = T.��������
                 AND ɾ����־ = '0'
                 AND ��Ա���� = T.������) AS ������ְҵ֤��,
             (SELECT ��Ա����
                FROM ������Ŀ_��Ա����
               WHERE �������� = T.��������
                 AND ɾ����־ = '0'
                 AND ��Ա���� = T.������) AS ������,
             (SELECT ҽʦִҵ֤�����
                FROM ������Ŀ_��Ա����
               WHERE �������� = T.��������
                 AND ɾ����־ = '0'
                 AND ��Ա���� = T.����ҽʦ) AS ����ҽʦְҵ֤��,
             (SELECT ��Ա����
                FROM ������Ŀ_��Ա����
               WHERE �������� = T.��������
                 AND ɾ����־ = '0'
                 AND ��Ա���� = T.����ҽʦ) AS ����ҽʦ,
             (SELECT ҽʦִҵ֤�����
                FROM ������Ŀ_��Ա����
               WHERE �������� = T.��������
                 AND ɾ����־ = '0'
                 AND ��Ա���� = T.����ҽʦ) AS ����ҽʦְҵ֤��,
             (SELECT ��Ա����
                FROM ������Ŀ_��Ա����
               WHERE �������� = T.��������
                 AND ɾ����־ = '0'
                 AND ��Ա���� = T.����ҽʦ) AS ����ҽʦ,
             (SELECT ҽʦִҵ֤�����
                FROM ������Ŀ_��Ա����
               WHERE �������� = T.��������
                 AND ɾ����־ = '0'
                 AND ��Ա���� = T.סԺҽʦ) AS סԺҽʦְҵ֤��,
             (SELECT ��Ա����
                FROM ������Ŀ_��Ա����
               WHERE �������� = T.��������
                 AND ɾ����־ = '0'
                 AND ��Ա���� = T.סԺҽʦ) AS סԺҽʦ,
             (SELECT ҽʦִҵ֤�����
                FROM ������Ŀ_��Ա����
               WHERE �������� = T.��������
                 AND ɾ����־ = '0'
                 AND ��Ա���� = T.���λ�ʿ) AS ���λ�ʿְҵ֤��,
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
             DECODE(T.ʬ��, '��', '1', '2'),
             T.Ѫ��,
             T.RH,
             --ʡ����������
             
             TO_NUMBER(REGEXP_REPLACE(T.���䲻��1����, '[^-0-9.]', '')) ���䲻��1����,
             (CASE
               WHEN TO_NUMBER(T.��Ժ���� - T.��������) < 28 THEN
                TO_NUMBER(REGEXP_REPLACE(T.��������������, '[^-0-9.]', ''))
               ELSE
                null
             END) AS ��������������,
             (CASE
               WHEN TO_NUMBER(T.��Ժ���� - T.��������) < 28 THEN
                TO_NUMBER(REGEXP_REPLACE(T.��������Ժ����, '[^-0-9.]', ''))
               ELSE
                null
             END) AS ��������Ժ����,
             
             TO_NUMBER(REGEXP_REPLACE(­�����˻��߻���ʱ����Ժǰ��,
                                      '[^-0-9.]',
                                      '')) ­�����˻��߻���ʱ����Ժǰ��,
             TO_NUMBER(REGEXP_REPLACE(­�����˻��߻���ʱ����ԺǰСʱ,
                                      '[^-0-9.]',
                                      '')) ­�����˻��߻���ʱ����ԺǰСʱ,
             TO_NUMBER(REGEXP_REPLACE(­�����˻��߻���ʱ����Ժǰ����,
                                      '[^-0-9.]',
                                      '')) ­�����˻��߻���ʱ����Ժǰ����,
             TO_NUMBER(REGEXP_REPLACE(­�����˻��߻���ʱ����Ժ����,
                                      '[^-0-9.]',
                                      '')) ­�����˻��߻���ʱ����Ժ����,
             TO_NUMBER(REGEXP_REPLACE(­�����˻��߻���ʱ����Ժ��Сʱ,
                                      '[^-0-9.]',
                                      '')) ­�����˻��߻���ʱ����Ժ��Сʱ,
             TO_NUMBER(REGEXP_REPLACE(­�����˻��߻���ʱ����Ժ����,
                                      '[^-0-9.]',
                                      '')) ­�����˻��߻���ʱ����Ժ����,
             
             T.�д�������ʹ��ʱ��,
             TO_NUMBER(T.�Ƿ��г�Ժ31����סԺ�ƻ�),
             T.Ŀ��,
             T.��Ժ��ʽ,
             T.ҽ��ת�����������
        FROM סԺ����_������ҳ T, סԺ����_��Ժ������Ϣ TT
       WHERE T.סԺ������ = TT.סԺ������
         AND T.�������� = STR_��������
         AND T.������� = '3' --��ҽ
         AND T.��Ժ���� BETWEEN DAT_��Ժʱ����ʼ AND DAT_��Ժʱ���ֹ
         AND T.�鵵�˱��� IS NOT NULL
         AND (T.סԺ������ LIKE '%' || STR_�������� || '%' OR
             T.������ LIKE '%' || STR_�������� || '%' OR
             T.�������� LIKE '%' || STR_�������� || '%');
  END;

  --������ϼ�¼��Ϣ
  FOR ROW_��ϼ�¼ IN CUR_��ϼ�¼ LOOP
    EXIT WHEN CUR_��ϼ�¼%NOTFOUND;
    IF ROW_��ϼ�¼.������� = '��Ժ���' THEN
      STR_SQL := 'UPDATE ��ʱ��_������ҳ��ҽ_��Ч���� SET C03C=:1,C04N=:2,C05C=:3 WHERE סԺ������=:4';
      EXECUTE IMMEDIATE STR_SQL
        USING ROW_��ϼ�¼.ICD��, SUBSTR(ROW_��ϼ�¼.��������, 0, 50), ROW_��ϼ�¼.��Ժ����, ROW_��ϼ�¼.סԺ������;
    ELSIF ROW_��ϼ�¼.������� = '�������' THEN
      IF ROW_��ϼ�¼.RN < 10 THEN
        STR_��� := '0' || ROW_��ϼ�¼.RN;
      ELSE
        STR_��� := ROW_��ϼ�¼.RN;
      END IF;
      STR_SQL := 'UPDATE ��ʱ��_������ҳ��ҽ_��Ч���� SET C06x' || STR_��� || 'C=:1,C07x' ||
                 STR_��� || 'N=:2,C08x' || STR_��� || 'C=:3 WHERE סԺ������=:4';
      EXECUTE IMMEDIATE STR_SQL
        USING ROW_��ϼ�¼.ICD��, SUBSTR(ROW_��ϼ�¼.��������, 0, 50), ROW_��ϼ�¼.��Ժ����, ROW_��ϼ�¼.סԺ������;
    ELSIF ROW_��ϼ�¼.������� = '���˺��ж��ⲿԭ��' THEN
      STR_SQL := 'UPDATE ��ʱ��_������ҳ��ҽ_��Ч���� SET C12C=:1,C13N=:2 WHERE סԺ������=:3';
      EXECUTE IMMEDIATE STR_SQL
        USING ROW_��ϼ�¼.ICD��, SUBSTR(ROW_��ϼ�¼.��������, 0, 50), ROW_��ϼ�¼.סԺ������;
    ELSIF ROW_��ϼ�¼.������� = '�������' THEN
      STR_SQL := 'UPDATE ��ʱ��_������ҳ��ҽ_��Ч���� SET C09C=:1,C10N=:2 WHERE סԺ������=:3';
      EXECUTE IMMEDIATE STR_SQL
        USING ROW_��ϼ�¼.ICD��, SUBSTR(ROW_��ϼ�¼.��������, 0, 50), ROW_��ϼ�¼.סԺ������;
    END IF;
  END LOOP;

  --���·��ü�¼��Ϣ
  FOR ROW_���ü�¼ IN CUR_���ü�¼ LOOP
    EXIT WHEN CUR_���ü�¼%NOTFOUND;
    UPDATE ��ʱ��_������ҳ��ҽ_��Ч���� T
       SET T.D01    = ROW_���ü�¼.�ܽ��,
           T.D09    = ROW_���ü�¼.�Ը����,
           T.D11    = ROW_���ü�¼.һ��ҽ�Ʒ������,
           T.D12    = ROW_���ü�¼.һ�����Ʋ�����,
           T.D13    = ROW_���ü�¼.�����,
           T.D14    = ROW_���ü�¼.�ۺ�ҽ�Ʒ�������������,
           T.D15    = ROW_���ü�¼.������Ϸ�,
           T.D16    = ROW_���ü�¼.ʵ������Ϸ�,
           T.D17    = ROW_���ü�¼.Ӱ��ѧ��Ϸ�,
           T.D18    = ROW_���ü�¼.�ٴ������Ŀ��,
           T.D19    = ROW_���ü�¼.������������Ŀ��,
           T.D19X01 = ROW_���ü�¼.����_�ٴ��������Ʒ�,
           T.D20    = ROW_���ü�¼.�������Ʒ�,
           T.D20X01 = ROW_���ü�¼.����_�����,
           T.D20X02 = ROW_���ü�¼.����_������,
           T.D21    = ROW_���ü�¼.������,
           T.D22    = ROW_���ü�¼.��ҽ���Ʒ�,
           T.D23    = ROW_���ü�¼.��ҩ��,
           T.D23X01 = ROW_���ü�¼.����_����ҩ���,
           T.D24    = ROW_���ü�¼.�г�ҩ��,
           T.D25    = ROW_���ü�¼.�в�ҩ��,
           T.D26    = ROW_���ü�¼.Ѫ��,
           T.D27    = ROW_���ü�¼.�׵�������Ʒ��,
           T.D28    = ROW_���ü�¼.�򵰰�����Ʒ��,
           T.D29    = ROW_���ü�¼.��Ѫ��������Ʒ��,
           T.D30    = ROW_���ü�¼.ϸ����������Ʒ��,
           T.D31    = ROW_���ü�¼.�����һ����ҽ�ò��Ϸ�,
           T.D32    = ROW_���ü�¼.������һ����ҽ�ò��Ϸ�,
           T.D33    = ROW_���ü�¼.������һ����ҽ�ò��Ϸ�,
           T.D34    = ROW_���ü�¼.������
    
     WHERE T.סԺ������ = ROW_���ü�¼.סԺ������;
  END LOOP;

  --����������¼��Ϣ
  FOR ROW_������¼ IN CUR_������¼ LOOP
    EXIT WHEN CUR_������¼%NOTFOUND;
  
    IF ROW_������¼.RN <= 10 THEN
      STR_��� := '0' || (ROW_������¼.RN - 1);
    ELSE
      STR_��� := (ROW_������¼.RN - 1);
    END IF;
    IF STR_��� = '00' THEN
      STR_SQL := 'UPDATE ��ʱ��_������ҳ��ҽ_��Ч���� SET C14x01C=:1, C16x01=:2, C17x01= :3
               , C15x01N=:4, C18x01=:5, C19x01=:6, C20x01=:7, C21x01C=:8, C22x01C=:9, C23x01=:10 WHERE סԺ������=:11';
    ELSE
      STR_SQL := 'UPDATE ��ʱ��_������ҳ��ҽ_��Ч���� SET C35x' || STR_��� ||
                 'C=:1, C37x' || STR_��� || '=:2, C38x' || STR_��� ||
                 '= :3, C36x' || STR_��� || 'N=:4, C39x' || STR_��� ||
                 '=:5, C40x' || STR_��� || '=:6, C41x' || STR_��� ||
                 '=:7, C42x' || STR_��� || 'C=:8, C43x' || STR_��� ||
                 'C=:9, C44x' || STR_��� || '=:10  WHERE סԺ������=:11';
    END IF;
  
    EXECUTE IMMEDIATE STR_SQL
      USING ROW_������¼.������, ROW_������¼.������������, ROW_������¼.�����������, ROW_������¼.������������, ROW_������¼.����, ROW_������¼.I��, ROW_������¼.II��, ROW_������¼.�п����ϵȼ�����, ROW_������¼.����ʽ����, ROW_������¼.����ҽʦ, ROW_������¼.סԺ������;
  
  END LOOP;

  --������֢�໤��¼��Ϣ
  FOR ROW_��֢�໤��¼ IN CUR_��֢�໤��¼ LOOP
    EXIT WHEN CUR_��֢�໤��¼%NOTFOUND;
  
    STR_��� := '0' || ROW_��֢�໤��¼.RN; 
    STR_SQL := 'UPDATE ��ʱ��_������ҳ��ҽ_��Ч���� SET C48x' || STR_��� || 'C=:1, C49x' ||
               STR_��� || '=:2, C50x' || STR_��� || '= :3 WHERE סԺ������=:4';
  
    EXECUTE IMMEDIATE STR_SQL
      USING ROW_��֢�໤��¼.���ұ���, ROW_��֢�໤��¼.����ʱ��, ROW_��֢�໤��¼.�˳�ʱ��, ROW_��֢�໤��¼.סԺ������;
  
  END LOOP;
  --�������ݼ�
  OPEN CUR_����_�б���Ϣ FOR
    SELECT T.*
      FROM ��ʱ��_������ҳ��ҽ_��Ч���� T
     WHERE T.C04N != '�޷���Ժ';

END PR_������ҳ��ҽ_��Ч����;
/
